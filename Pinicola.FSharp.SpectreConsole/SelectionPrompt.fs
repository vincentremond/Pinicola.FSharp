namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module SelectionPrompt =

    let init<'a> () = SelectionPrompt<'a>()

    let withTitle (title: SpectreConsoleString) (prompt: SelectionPrompt<'a>) =
        prompt.Title <- title |> SpectreConsoleString.asString
        prompt

    let withRawTitle (title: string) =
        title |> SpectreConsoleString.fromString |> withTitle

    let addChoices choices (prompt: SelectionPrompt<'a>) =
        choices |> Array.ofSeq |> prompt.AddChoices

    let withWrapAround wrapAround (prompt: SelectionPrompt<'a>) =
        prompt.WrapAround <- wrapAround
        prompt

    let useConverter (converter: 'a -> SpectreConsoleString) (prompt: SelectionPrompt<'a>) =
        prompt.UseConverter(converter >> SpectreConsoleString.asString)
