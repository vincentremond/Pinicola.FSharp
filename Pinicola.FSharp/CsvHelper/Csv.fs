namespace Pinicola.FSharp.CsvHelper

open System.Globalization
open System.IO
open System.Text
open CsvHelper

[<RequireQualifiedAccess>]
module Csv =

    let writeRecordsToFileWithCulture<'T> (records: 'T seq) (culture: CultureInfo) (filePath: string) =
        use writer =
            new StreamWriter(filePath, (*append*) false, UTF8Encoding( (*encoderShouldEmitUTF8Identifier*) true))

        use csv = new CsvWriter(writer, culture)
        csv.WriteRecords(records)

    let rec writeRecordsToFile<'T> (records: 'T seq) (filePath: string) =
        writeRecordsToFileWithCulture records CultureInfo.InvariantCulture filePath
