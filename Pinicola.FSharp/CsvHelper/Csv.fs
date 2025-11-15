namespace Pinicola.FSharp.CsvHelper

open System.Globalization
open System.IO
open System.Text
open CsvHelper

[<RequireQualifiedAccess>]
module Csv =

    let writeRecordsToFileWithCulture<'T> (culture: CultureInfo) (filePath: string) (records: 'T seq) =
        using
            (new StreamWriter(filePath, append = false, encoding = UTF8Encoding(encoderShouldEmitUTF8Identifier = true)))
            (fun writer -> using (new CsvWriter(writer, culture)) _.WriteRecords(records))

    let writeRecordsToFile<'T> =
        writeRecordsToFileWithCulture<'T> CultureInfo.InvariantCulture

    let readRecordsFromFileWithCulture<'T> (culture: CultureInfo) (filePath: string) : 'T list =
        using
            (new StreamReader(filePath, UTF8Encoding(encoderShouldEmitUTF8Identifier = true)))
            (fun streamReader ->
                using
                    (new CsvReader(streamReader, culture))
                    (fun csvReader -> csvReader.GetRecords<'T>() |> Seq.toList)
            )

    let readRecordsFromFile<'T> (filePath: string) : 'T list =
        readRecordsFromFileWithCulture<'T> CultureInfo.InvariantCulture filePath
