namespace Pinicola.FSharp.IO

open System.IO
open Pinicola.FSharp

[<RequireQualifiedAccess>]
module Directory =

    let exists path = Directory.Exists(path)

    let findRelativeParent directoryName =
        let rec findRelativeParent' (current: DirectoryInfo) =
            if current.Parent = null then None
            elif current.Name = directoryName then Some current.FullName
            else findRelativeParent' current.Parent

        let currentDirectory = Directory.GetCurrentDirectory() |> DirectoryInfo
        findRelativeParent' currentDirectory

    let ensureExists path =
        if not (Directory.Exists(path)) then
            Directory.CreateDirectory(path) |> ignore

    let getAllDirectories path =
        Directory.GetDirectories(path) |> Array.mapToList DirectoryInfo

    let getDirectories pattern path =
        Directory.GetDirectories(path, pattern) |> Array.mapToList DirectoryInfo

    let isSymlink (directory: DirectoryInfo) =
        directory.Attributes.HasFlag(FileAttributes.ReparsePoint)
