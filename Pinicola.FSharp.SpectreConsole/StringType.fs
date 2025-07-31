namespace Pinicola.FSharp.SpectreConsole

type RawString = string
type MarkupString = string

type StringType =
    | Raw of RawString
    | Markup of MarkupString

    static member inline AsString(s: StringType) : string =
        match s with
        | Raw r -> r |> Markup.escape
        | Markup m -> m
