namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module SelectionPrompt =
    let withTitle (title: StringType) (prompt: SelectionPrompt<'a>) =
        prompt.Title <- title |> StringType.AsString
        prompt

    let addChoices choices (prompt: SelectionPrompt<'a>) =
        choices |> Array.ofSeq |> prompt.AddChoices

    let init () = SelectionPrompt<'a>()

    let mk title choices =
        init () |> withTitle title |> addChoices choices

    let prompt title choices = mk title choices |> AnsiConsole.prompt

    let useConverter (converter: 'a -> string) (prompt: SelectionPrompt<'a>) = prompt.UseConverter(converter)
