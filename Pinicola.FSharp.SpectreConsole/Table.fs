namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console
open Spectre.Console.Rendering

[<RequireQualifiedAccess>]
module Table =

    let init () = Table()

    let addColumns (columns: string list) (table: Table) =
        table.AddColumns(columns |> List.toArray)

    let addColumnsCallback (columns: (string * (TableColumn -> unit)) list) (table: Table) : Table =
        (table, columns)
        ||> List.fold (fun table (column, configure) -> table.AddColumn(column, configure))

    let addRow (row: string list) (table: Table) = table.AddRow(row |> List.toArray)

    let addRows (rows: string list list) (table: Table) =
        (table, rows) ||> List.fold (fun t row -> t.AddRow(row |> List.toArray))

    let removeRow index (table: Table) = table.RemoveRow(index)

    let withBorder b (table: Table) =
        table.Border <- b
        table

    let withShowHeaders (show: bool) (table: Table) =
        table.ShowHeaders <- show
        table

    let withWidth (width: int) (table: Table) =
        table.Width <- width
        table

    let updateCell (row: int) (column: int) (renderable: IRenderable) (table: Table) =
        table.UpdateCell(row, column, renderable) |> ignore

    let withExpand (expand: bool) (table: Table) =
        table.Expand <- expand
        table
