namespace Pinicola.FSharp.SpectreConsole

open System
open Spectre.Console

[<RequireQualifiedAccess>]
module TableColumn =

    let withAlignment (justify: Justify) (tableColumn: TableColumn) =
        tableColumn.Alignment <- justify
        tableColumn

    let withWidth (width: int) (tableColumn: TableColumn) =
        tableColumn.Width <- width
        tableColumn

    let unsetWidth (tableColumn: TableColumn) =
        tableColumn.Width <- Nullable<int>()
        tableColumn

    let withNoWrap (tableColumn: TableColumn) =
        tableColumn.NoWrap <- true
        tableColumn

    let withWrap (tableColumn: TableColumn) =
        tableColumn.NoWrap <- false
        tableColumn
