namespace Pinicola.FSharp

type MergeResult<'left, 'right> =
    | LeftOnly of 'left
    | RightOnly of 'right
    | Both of 'left * 'right

[<RequireQualifiedAccess>]
module Array =

    let mergeBy fLeft fRight left right =
        let leftMap = left |> Array.map (fun x -> fLeft x, x) |> Map.ofArray
        let rightMap = right |> Array.map (fun x -> fRight x, x) |> Map.ofArray

        let allKeys = Set.union (Map.keySet leftMap) (Map.keySet rightMap)

        allKeys
        |> Set.toArray
        |> Array.map (fun key ->
            match Map.tryFind key leftMap, Map.tryFind key rightMap with
            | Some l, Some r -> Both(l, r)
            | Some l, None -> LeftOnly l
            | None, Some r -> RightOnly r
            | None, None -> failwith "This should never happen"
        )
