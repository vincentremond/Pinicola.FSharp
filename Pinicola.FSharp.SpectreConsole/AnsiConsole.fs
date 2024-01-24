namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module AnsiConsole =
    let prompt = AnsiConsole.Prompt
    let ask = AnsiConsole.Ask
    let status = AnsiConsole.Status
    let markupLine = AnsiConsole.MarkupLine
    let markupInterpolated = AnsiConsole.MarkupInterpolated
    let markupLineInterpolated = AnsiConsole.MarkupLineInterpolated
