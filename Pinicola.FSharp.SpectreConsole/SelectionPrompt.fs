﻿namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module SelectionPrompt =
    let withTitle (title: string) (prompt: SelectionPrompt<'a>) =
        prompt.Title <- title
        prompt

    let addChoices choices (prompt: SelectionPrompt<'a>) =
        choices |> Array.ofSeq |> prompt.AddChoices

    let init () = SelectionPrompt<'a>()

    let useConverter (converter: 'a -> string) (prompt: SelectionPrompt<'a>) = prompt.UseConverter(converter)
