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
