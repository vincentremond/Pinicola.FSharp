namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module Option =

    let getOrFail message =
        function
        | Some x -> x
        | None -> failwith message
