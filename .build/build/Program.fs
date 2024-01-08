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

let scriptFile, args =
    match System.Environment.GetCommandLineArgs() |> Array.toList with
    | [] -> "???", []
    | script :: others -> script, others

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

FakeExecutionContext.Create false scriptFile args
|> RuntimeContext.Fake
|> setExecutionContext

Target.create
    "Clean"
    (fun _ ->
        !! "**/*.fsproj" -- ".build/**"
        |> Seq.iter (fun fsproj ->
            let processResult = DotNet.exec id "clean" fsproj

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
            DotNet.build (fun option -> { option with Configuration = DotNet.BuildConfiguration.Release }) projectFile
        )
    )

Target.create
    "Publish nuget"
    (fun _ ->

        let outputPath =
            match Environment.GetEnvironmentVariable("LOCAL_NUGET_FEED") with
            | s when String.isNotNullOrEmpty s && Directory.Exists s -> s
            | _ -> "./nuget"

        // 2024-01-08 08:05:46 +0100  (HEAD -> main, tag: v0.0.1)
        let tagRegex = Regex(@"^(?<Date>.+)  \(.*tag: v(?<Tag>\d+\.\d+\.\d+)\)$")

        let latestTagVersion =
            CommandHelper.runGitCommand "." "log --tags --simplify-by-decoration --pretty=\"format:%ci %d\""
            |> Result.get
            |> List.map (fun line ->
                let m = tagRegex.Match(line)

                if not m.Success then
                    failwith $"Unexpected git log output %A{line}"
                else
                    let date = DateTimeOffset.Parse(m.Groups.["Date"].Value)
                    let versionTag = m.Groups.["Tag"].Value |> SemVer.parse

                    {|
                        Version = versionTag
                        Date = date
                    |}
            )
            |> List.sortByDescending (_.Version)
            |> List.head

        let ageSinceLastTag =
            (DateTimeOffset.Now - latestTagVersion.Date).TotalSeconds
            |> int
            |> (_.ToString("X16"))

        let tempVersion =
            SemVer.parse $"%s{string latestTagVersion.Version.AsString}-%s{ageSinceLastTag}"
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
