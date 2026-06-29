namespace Pinicola.FSharp.SpectreConsole

[<RequireQualifiedAccess>]
module Style =

    open Spectre.Console

    let init foreground background decoration =
        Style(foreground |> Option.toNullable, background |> Option.toNullable, decoration |> Option.toNullable)

type StyleBuilder = {
    Foreground: Spectre.Console.Color option
    Background: Spectre.Console.Color option
    Decoration: Spectre.Console.Decoration option
} with

    static member default_ = {
        Foreground = None
        Background = None
        Decoration = None
    }

    static member withForeground color this = { this with Foreground = Some color }
    static member withBackground color this = { this with Background = Some color }
    static member withDecoration decoration this = { this with Decoration = Some decoration }

    static member build this =
        Style.init this.Foreground this.Background this.Decoration
