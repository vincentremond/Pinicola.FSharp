namespace Pinicola.FSharp.IO

open System.IO

[<RequireQualifiedAccess>]
module File =

    let writeAllText path (text: string) = File.WriteAllText(path, text)
    let readAllText path = File.ReadAllText(path)
