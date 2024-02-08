namespace Pinicola.FSharp.SpectreConsole

[<RequireQualifiedAccess>]
module Rule =
    open Spectre.Console

    let init () = Rule()

    let setStyle style (rule: Rule) =
        rule.Style <- style
        rule
