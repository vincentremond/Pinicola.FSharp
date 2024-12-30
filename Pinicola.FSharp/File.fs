namespace Pinicola.FSharp

open System.IO

[<RequireQualifiedAccess>]
module File =

    let writeAllText path (text: string) = File.WriteAllText(path, text)
