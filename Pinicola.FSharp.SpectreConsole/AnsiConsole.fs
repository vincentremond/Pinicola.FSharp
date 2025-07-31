namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console
open Spectre.Console.Rendering

[<RequireQualifiedAccess>]
module AnsiConsole =
    let prompt = AnsiConsole.Prompt
    let ask = AnsiConsole.Ask
    let status = AnsiConsole.Status
    let markup = AnsiConsole.Markup
    let markupLine = AnsiConsole.MarkupLine
    let markupInterpolated = AnsiConsole.MarkupInterpolated
    let markupLineInterpolated = AnsiConsole.MarkupLineInterpolated
    let write: IRenderable -> unit = AnsiConsole.Write
    let writeLine = StringType.AsString >> AnsiConsole.WriteLine
    let clear = AnsiConsole.Clear
    let live = AnsiConsole.Live
    let confirm = StringType.AsString >> AnsiConsole.Confirm
