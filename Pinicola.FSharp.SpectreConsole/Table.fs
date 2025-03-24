namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module Table =

    let init () = Table()

    let addColumns (columns: string list) (table: Table) =
        table.AddColumns(columns |> List.toArray)

    let addRow (row: string list) (table: Table) = table.AddRow(row |> List.toArray)
