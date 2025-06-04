namespace Pinicola.FSharp.IO

open System
open System.IO

[<AutoOpen>]
module PathOperators =

    let (</>) (path1: string) (path2: string) = Path.Join(path1, path2)

[<RequireQualifiedAccess>]
module Path =
    let getFullPath path = Path.GetFullPath path

    let tryFindFileInLocations fileName locations =

        locations
        |> List.tryPick (fun location ->
            let fullPath = location </> fileName
            if File.Exists(fullPath) then Some fullPath else None
        )

    let tryFindInPathEnvVar fileName =
        if File.Exists(fileName) then
            Some(Path.GetFullPath(fileName))
        else
            let pathEnvVar = Environment.GetEnvironmentVariable("PATH")

            let split =
                pathEnvVar.Split(
                    Path.PathSeparator,
                    StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries
                )

            split
            |> Array.tryPick (fun path ->
                let fullPath = Path.Join(path, fileName)
                if File.Exists(fullPath) then Some fullPath else None
            )

    let findInPathEnvVar fileName =
        tryFindInPathEnvVar fileName
        |> (function
        | None -> failwith $"Failed to find {fileName} in PATH"
        | Some p -> p)
