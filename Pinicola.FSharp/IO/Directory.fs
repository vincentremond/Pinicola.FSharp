namespace Pinicola.FSharp.IO

open System
open System.IO
open Pinicola.FSharp

[<RequireQualifiedAccess>]
type SpecialDirectory =
    | Desktop
    | Documents
    | UserProfile
    | Downloads
    | ApplicationData
    | LocalApplicationData
    | ProgramFiles
    | ProgramFilesX86
    | CommonApplicationData

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

    let getSpecialDirectory (specialDirectory: SpecialDirectory) =
        let specialFolder =
            match specialDirectory with
            | SpecialDirectory.Desktop -> Environment.SpecialFolder.Desktop
            | SpecialDirectory.Documents -> Environment.SpecialFolder.MyDocuments
            | SpecialDirectory.UserProfile -> Environment.SpecialFolder.UserProfile
            | SpecialDirectory.Downloads -> Environment.SpecialFolder.UserProfile
            | SpecialDirectory.ApplicationData -> Environment.SpecialFolder.ApplicationData
            | SpecialDirectory.LocalApplicationData -> Environment.SpecialFolder.LocalApplicationData
            | SpecialDirectory.ProgramFiles -> Environment.SpecialFolder.ProgramFiles
            | SpecialDirectory.ProgramFilesX86 -> Environment.SpecialFolder.ProgramFilesX86
            | SpecialDirectory.CommonApplicationData -> Environment.SpecialFolder.CommonApplicationData
            
        Environment.GetFolderPath(specialFolder) |> DirectoryInfo

    let getUserProfile () =
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) |> DirectoryInfo
