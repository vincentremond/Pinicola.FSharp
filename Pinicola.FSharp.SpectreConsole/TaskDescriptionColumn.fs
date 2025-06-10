namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module TaskDescriptionColumn =

    let withAlignment a (c: TaskDescriptionColumn) =
        c.Alignment <- a
        c
