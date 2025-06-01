namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module Try =

    let toOption =
        function
        | true, x -> Some x
        | false, _ -> None
