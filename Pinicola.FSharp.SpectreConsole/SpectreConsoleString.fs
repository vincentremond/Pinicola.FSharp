namespace Pinicola.FSharp.SpectreConsole

open System

type RawString = string
type MarkupString = string

type SpectreConsoleString =
    | Raw of RawString
    | Markup of MarkupString

    static member inline asString(s: SpectreConsoleString) : string =
        match s with
        | Raw r -> r |> Markup.escape
        | Markup m -> m

    static member inline fromInterpolated(f: FormattableString) : SpectreConsoleString =
        let str = Markup.escapeInterpolated f
        Markup str
