namespace Pinicola.FSharp.Http

open System
open System.Web
open Pinicola.FSharp

type HttpUriBuilder = {
    Scheme: string
    Credentials: (string * string option) option
    Host: string
    Port: int option
    Path: string option
    Query: (string * string) list
    Fragment: string option
} with

    static member fromAbsoluteUri uriString =

        let parsedUri = Uri(uriString, UriKind.Absolute)

        let credentials =
            parsedUri.UserInfo
            |> Option.ofObj
            |> Option.map (fun userInfo ->
                let parts = userInfo.Split(':')

                match parts with
                | [| user |] -> user, None
                | [| user; password |] -> user, Some password
                | _ -> failwith "Unexpected"
            )

        let query =
            match parsedUri.Query with
            | IsNullOrEmpty -> []
            | q ->
                let q = q.TrimStart('?')
                let split = q.Split('&')

                split
                |> Seq.map (fun p ->
                    let kv = p.Split('=')

                    match kv with
                    | [| key; value |] -> (key, value) |> Tuple2.map HttpUtility.UrlDecode
                    | _ -> failwith "Unexpected"
                )
                |> Seq.toList

        {
            Scheme = parsedUri.Scheme
            Credentials = credentials
            Host = parsedUri.Host
            Port =
                if parsedUri.IsDefaultPort then
                    None
                else
                    Some parsedUri.Port
            Path =
                if String.IsNullOrEmpty(parsedUri.AbsolutePath) then
                    None
                else
                    Some parsedUri.AbsolutePath
            Query = query
            Fragment =
                if String.IsNullOrEmpty(parsedUri.Fragment) then
                    None
                else
                    Some(parsedUri.Fragment.TrimStart('#'))

        }

    static member addQuery queries builder = { builder with Query = builder.Query @ queries }

    member this.build() =

        let query =
            match this.Query with
            | [] -> None
            | q ->
                q
                |> List.map ((Tuple2.map HttpUtility.UrlEncode) >> Tuple2.curry (sprintf "%s=%s"))
                |> List.fold (fun acc p -> $"%s{acc}&%s{p}") "?"
                |> Some

        let builder = UriBuilder()
        builder.Scheme <- this.Scheme

        match this.Credentials with
        | Some(user, None) -> builder.UserName <- user
        | Some(user, Some password) ->
            builder.UserName <- user
            builder.Password <- password
        | None -> ()

        builder.Host <- this.Host

        this.Port |> Option.iter (fun port -> builder.Port <- port)
        this.Path |> Option.iter (fun path -> builder.Path <- path)
        query |> Option.iter (fun query -> builder.Query <- query)

        this.Fragment
        |> Option.iter (fun fragment -> builder.Fragment <- HttpUtility.UrlEncode fragment)

        builder.Uri
