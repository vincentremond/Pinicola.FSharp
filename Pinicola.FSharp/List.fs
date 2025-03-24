namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module List =

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
