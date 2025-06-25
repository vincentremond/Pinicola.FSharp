namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module Result =

    let ofOption e =
        function
        | Some x -> Ok x
        | None -> Error e
