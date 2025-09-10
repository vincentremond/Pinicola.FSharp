namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module TableColumn =

    let withAlignment (justify: Justify) (tableColumn: TableColumn) =
        tableColumn.Alignment <- justify
        tableColumn
