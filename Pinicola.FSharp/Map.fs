namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module Map =

    let tryFindOrDefault key defaultValue map =
        match Map.tryFind key map with
        | Some value -> value
        | None -> defaultValue

