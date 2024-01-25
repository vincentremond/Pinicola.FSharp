namespace Pinicola.Build

open System
open System.IO
open System.Text.RegularExpressions
open Fake.Core
open Fake.Core.Context
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.Tools.Git

module DefaultConfig =

    let parseTag line =
        let tagRegex = Regex(@"^v(?<Tag>\d+\.\d+\.\d+)$")

        let m = tagRegex.Match(line)

        if not m.Success then
            failwith $"Unexpected git log output %A{line}"
        else
            m.Groups["Tag"].Value |> SemVer.parse

    [<RequireQualifiedAccess>]
    module Result =
        let get r =
            match r with
            | Ok r -> r
            | Error _ -> failwith $"Failed to get Result value %A{r}"

    [<RequireQualifiedAccess>]
    module CommandHelper =
        let runGitCommand repository command =
            CommandHelper.runGitCommand repository command
            |> (function
            | false, [], errorOutput -> Error errorOutput
            | true, output, _ -> Ok output
            | output -> failwith $"Unexpected git command result %A{output}")

    let build () =

        let scriptFile, args =
            match Environment.GetCommandLineArgs() |> Array.toList with
            | [] -> "???", []
            | script :: others -> script, others

        FakeExecutionContext.Create false scriptFile args
        |> RuntimeContext.Fake
        |> setExecutionContext

        Target.create
            "Clean"
            (fun _ ->
                !! "**/*.fsproj" -- ".build/**"
                |> Seq.iter (fun fsproj ->
                    let command = "clean"

                    let args =
                        [
                            fsproj
                            "--configuration"
                            "Release"
                        ]
                        |> Args.toWindowsCommandLine

                    let processResult = DotNet.exec id command args

                    if not processResult.OK then
                        failwith $"Failed to clean {fsproj}"
                )
            )

        Target.create
            "Build"
            (fun _ ->
                !! "**/paket.template"
                |> Seq.iter (fun template ->
                    let projectDirectory = template |> FileInfo |> (_.DirectoryName)
                    let projectFile = projectDirectory |> Directory.findFirstMatchingFile "*.fsproj"

                    DotNet.build
                        (fun option -> { option with Configuration = DotNet.BuildConfiguration.Release })
                        projectFile
                )
            )

        Target.create
            "Publish nuget"
            (fun _ ->

                let outputPath =
                    match Environment.GetEnvironmentVariable("LOCAL_NUGET_FEED") with
                    | s when String.isNotNullOrEmpty s && Directory.Exists s -> s
                    | _ -> "./nuget"

                let tags =
                    CommandHelper.runGitCommand "." "tag --list"
                    |> Result.get
                    |> List.map parseTag
                    |> List.sortDescending

                let latestTagVersion =
                    match tags with
                    | [] -> failwith "No tags found"
                    | tag :: _ -> tag

                let timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmss")

                let tempVersion =
                    SemVer.parse $"%s{string latestTagVersion.AsString}-%s{timestamp}"
                    |> (_.AsString)

                printfn $"Publishing version %s{tempVersion}"

                Paket.pack (fun options ->
                    let localTool = ToolType.CreateLocalTool id

                    {
                        options with
                            ToolType = localTool
                            Version = tempVersion
                            OutputPath = outputPath
                    }
                )
            )

        let defaultTarget = "Clean" ==> "Build" ==> "Publish nuget"

        Target.runOrDefault defaultTarget
