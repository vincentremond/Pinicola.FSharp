namespace Pinicola.FSharp.IO

open System.IO

[<RequireQualifiedAccess>]
module File =

    let writeAllText path (text: string) = File.WriteAllText(path, text)
    let writeAllText' (path: FileInfo) (text: string) = File.WriteAllText(path.FullName, text)
    let readAllText path = File.ReadAllText(path)
    let readAllText' (path: FileInfo) = File.ReadAllText(path.FullName)
    let exists path = File.Exists(path)
    let delete = File.Delete
