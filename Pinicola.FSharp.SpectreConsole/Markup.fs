namespace Pinicola.FSharp.SpectreConsole

open System
open Spectre.Console

[<RequireQualifiedAccess>]
module Markup =

    let fromString = Markup
    let escape = Markup.Escape

    let escapeInterpolated (value: FormattableString) =
        let args =
            value.GetArguments()
            |> Array.map (fun arg ->
                match arg with
                | :? string as s -> s.EscapeMarkup() |> box<obj>
                | arg -> arg
            )

        String.Format(value.Format, args)

    let remove = Markup.Remove
    let fromInterpolated = Markup.FromInterpolated
