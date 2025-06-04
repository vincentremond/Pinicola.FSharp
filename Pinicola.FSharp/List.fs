namespace Pinicola.FSharp

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

    let ofType<'t> (list: 'a list) =
        list
        |> List.choose (fun x ->
            match box x with
            | :? 't as t -> Some t
            | _ -> None
        )

    let trySingle f list =
        match list |> List.filter f with
        | [] -> None
        | [ x ] -> Some x
        | xs -> failwithf $"Expected exactly one element, but found %d{List.length xs}"
