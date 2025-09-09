namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module Map =

    let tryFindOrDefault key defaultValue map =
        match Map.tryFind key map with
        | Some value -> value
        | None -> defaultValue

    let safeOfSeq (seq: ('a * 'b) seq) : Map<'a, 'b> =
        let folder acc (k, v) =
            match Map.containsKey k acc with
            | true -> failwithf $"Duplicate key: %A{k}"
            | false -> Map.add k v acc

        seq |> Seq.fold folder Map.empty

    let keySet (map: Map<'k, 'v>) : Set<'k> = map |> Map.keys |> Set.ofSeq

    let merge (map1: Map<'k, 'v1>) (map2: Map<'k, 'v2>) : Map<'k, 'v1 option * 'v2 option> =

        let keys = Set.union (keySet map1) (keySet map2)

        keys
        |> Set.fold
            (fun (acc: Map<'k, 'v1 option * 'v2 option>) (k: 'k) ->
                let v1 = Map.tryFind k map1
                let v2 = Map.tryFind k map2
                Map.add k (v1, v2) acc
            )
            Map.empty

    let toMutableDictionary (map: Map<'k, 'v>) =
        System.Collections.Generic.Dictionary(map)

    let updateOrAdd key value update map =
        let newValue =
            match map |> Map.tryFind key with
            | Some existingValue -> update existingValue value
            | None -> value

        Map.add key newValue map

    let mergeAll merger maps =
        let allKeys = maps |> Seq.collect Map.keys |> Seq.distinct |> Seq.toList

        allKeys
        |> List.fold
            (fun acc key ->
                let mergedValues = maps |> List.choose (Map.tryFind key) |> List.reduce merger
                acc |> Map.add key mergedValues
            )
            Map.empty

    let ofSeqWith f seq =
        seq |> Seq.map (fun x -> (f x, x)) |> Map.ofSeq

    let safeOfSeqWith f seq =
        seq |> Seq.map (fun x -> (f x, x)) |> safeOfSeq

    let iterTryFind f map =
        map |> Map.toSeq |> Seq.tryFind (fun (k, v) -> f k v)

    let iterTryFindValue f map = map |> iterTryFind f |> Option.map snd
