namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console
open Spectre.Console.Rendering

[<RequireQualifiedAccess>]
module Table =

    let init () = Table()

    let addColumns (columns: string list) (table: Table) =
        table.AddColumns(columns |> List.toArray)

    let addRow (row: string list) (table: Table) = table.AddRow(row |> List.toArray)

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
