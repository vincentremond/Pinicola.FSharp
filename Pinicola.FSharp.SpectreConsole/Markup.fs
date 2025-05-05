namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module Markup =

    let escape = Markup.Escape
    let remove = Markup.Remove
    let fromInterpolated = Markup.FromInterpolated
