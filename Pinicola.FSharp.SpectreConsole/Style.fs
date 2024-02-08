namespace Pinicola.FSharp.SpectreConsole

[<RequireQualifiedAccess>]
module Style =

    open Spectre.Console

    let init foreground background decoration link =
        Style(foreground |> Option.toNullable, background |> Option.toNullable, decoration |> Option.toNullable, link |> Option.toObj)

type StyleBuilder = {
    Foreground: Spectre.Console.Color option
    Background: Spectre.Console.Color option
    Decoration: Spectre.Console.Decoration option
    Link: string option
} with

    static member default_ = {
        Foreground = None
        Background = None
        Decoration = None
        Link = None
    }
    
    static member withForeground color this = { this with Foreground = Some color }
    static member withBackground color this = { this with Background = Some color }
    static member withDecoration decoration this = { this with Decoration = Some decoration }
    static member withLink link this = { this with Link = Some link }

    static member build this =
        Style.init this.Foreground this.Background this.Decoration this.Link
