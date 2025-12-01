namespace Pinicola.FSharp

open System
open System.IO
open Pinicola.FSharp.IO

[<RequireQualifiedAccess>]
module Where =

    let private win32ExecutableExtensions = [
        ".exe"
        ".bat"
        ".cmd"
    ]

    let private executableExtensions =
        if Environment.OSVersion.Platform = PlatformID.Win32NT then
            win32ExecutableExtensions
        else
            [ "" ]

    let private hasWin32ExecutableExtension (executable: string) =

        Path.HasExtension(executable)
        && win32ExecutableExtensions |> List.contains (Path.GetExtension(executable))

    let private toPossibleExecutableNames executable =
        if Environment.OSVersion.Platform = PlatformID.Win32NT then
            match hasWin32ExecutableExtension executable with
            | true -> [ executable ]
            | false -> executableExtensions |> List.map (fun ext -> executable + ext)
        else
            [ executable ]

    let tryFindInPath executable =
        let paths =
            Environment.GetEnvironmentVariable("PATH")
            |> _.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
            |> Seq.map DirectoryInfo
            |> Seq.toList

        let executableNames = toPossibleExecutableNames executable

        let rec findInPaths paths =
            match paths with
            | [] -> None
            | path :: rest ->
                let exe =
                    executableNames
                    |> List.tryPick (fun name ->
                        let file = path <?/> name
                        if file.Exists then Some file else None
                    )

                match exe with
                | Some file -> Some file
                | None -> findInPaths rest

        findInPaths paths

    let findInPath executable =
        match tryFindInPath executable with
        | Some file -> file
        | None -> failwithf $"Executable '%s{executable}' not found in PATH"
