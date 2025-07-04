﻿namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module List =

    [<RequireQualifiedAccess>]
    type TryPickExactlyOneResult<'a> =
        | ExactlyOne of 'a
        | MoreThanOne of 'a list
        | None

    let tryMapAll mapper list =
        let rec loop acc =
            function
            | [] -> Some(List.rev acc)
            | x :: xs ->
                match mapper x with
                | Some y -> loop (y :: acc) xs
                | None -> None

        loop [] list

    let mapSnd f = List.map (fun (a, b) -> (a, f b))
    let mapFst f = List.map (fun (a, b) -> (f a, b))

    let chooseSnd f =
        List.choose (fun (a, b) ->
            match f b with
            | Some b' -> Some(a, b')
            | None -> None
        )

    let groupByFst s =
        List.groupBy fst s |> List.map (fun (a, b) -> a, List.map snd b)

    let groupBySnd s =
        List.groupBy snd s |> List.map (fun (a, b) -> a, List.map fst b)

    let mapTupleFst f = List.map (fun a -> (f a, a))
    let mapTupleSnd f = List.map (fun a -> (a, f a))

    let tryPickExactlyOne f list =
        let result = List.choose f list

        match result with
        | [ x ] -> TryPickExactlyOneResult.ExactlyOne x
        | [] -> TryPickExactlyOneResult.None
        | xs -> TryPickExactlyOneResult.MoreThanOne xs

    let trySingle f list =
        match list |> List.filter f with
        | [] -> None
        | [ x ] -> Some x
        | xs -> failwithf $"Expected exactly one element, but found %d{List.length xs}"

    let any list =
        match list with
        | [] -> false
        | _ -> true

    let iterTaskResult f list =
        let rec loop acc =
            task {
                match acc with
                | [] -> return Ok()
                | x :: xs ->
                    match! f x with
                    | Ok() -> return! loop xs
                    | Error e -> return Error e
            }

        loop list

    let mergeBy fLeft fRight left right =
        let leftMap = left |> List.map (fun x -> fLeft x, x) |> Map.ofList
        let rightMap = right |> List.map (fun x -> fRight x, x) |> Map.ofList

        let allKeys = Set.union (Map.keySet leftMap) (Map.keySet rightMap)

        allKeys
        |> Set.toList
        |> List.map (fun key ->
            match Map.tryFind key leftMap, Map.tryFind key rightMap with
            | Some l, Some r -> Both(l, r)
            | Some l, None -> LeftOnly l
            | None, Some r -> RightOnly r
            | None, None -> failwith "This should never happen"
        )

    let indexed list = list |> List.mapi (fun i x -> i, x)

    let iteriAsync f list =
        let indexedList = indexed list

        task {
            for i, x in indexedList do
                do! f i x
        }
