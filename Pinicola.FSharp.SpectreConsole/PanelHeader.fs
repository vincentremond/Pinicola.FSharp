﻿namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module PanelHeader =

    let fromString (s: string) = PanelHeader(s)

    let withJustification (j: Justify) (header: PanelHeader) =
        header.Justification <- j
        header
