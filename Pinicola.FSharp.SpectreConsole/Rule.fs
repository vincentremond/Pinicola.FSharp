namespace Pinicola.FSharp.SpectreConsole

[<RequireQualifiedAccess>]
module Rule =
    open Spectre.Console

    let initBlank () = Rule()

    let initWithTitle title = Rule(title)

    let withTitle title (rule: Rule) =
        rule.Title <- title
        rule

    let withStyle style (rule: Rule) =
        rule.Style <- style
        rule

    let withJustify (justification: Justify) (rule: Rule) =
        rule.Justification <- justification
        rule
