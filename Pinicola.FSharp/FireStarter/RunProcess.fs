namespace Pinicola.FSharp.FireStarter

open System.Diagnostics
open Pinicola.FSharp.IO

type RunProcessArgs = {
    Executable: string
    Arguments: string list
    CaptureStandardOutput: bool
    UseShellExecute: bool
    WorkingDirectory: string option
} with

    static member defaultCaptureStandardOutput = true
    static member defaultUseShellExecute = false

    static member fromExecutableAndArgs executable args = {
        Executable = executable
        Arguments = args
        CaptureStandardOutput = RunProcessArgs.defaultCaptureStandardOutput
        UseShellExecute = RunProcessArgs.defaultUseShellExecute
        WorkingDirectory = None
    }

    static member fromExecutableInDirectory executable dir args =
        let exeFullPath = dir </> executable

        {
            Executable = exeFullPath
            Arguments = args
            CaptureStandardOutput = RunProcessArgs.defaultCaptureStandardOutput
            UseShellExecute = RunProcessArgs.defaultUseShellExecute
            WorkingDirectory = Some dir
        }

    static member fromGlobalExeAndWorkingDirectory executable workingDir = {
        Executable = executable
        Arguments = []
        CaptureStandardOutput = RunProcessArgs.defaultCaptureStandardOutput
        UseShellExecute = RunProcessArgs.defaultUseShellExecute
        WorkingDirectory = Some workingDir
    }

    static member withCaptureStandardOutput value args = { args with CaptureStandardOutput = value }

    static member withArguments arguments args = { args with Arguments = arguments }

    member this.toProcessStartInfo() =
        ProcessStartInfo(
            this.Executable,
            this.Arguments,
            RedirectStandardOutput = this.CaptureStandardOutput,
            UseShellExecute = this.UseShellExecute,
            WorkingDirectory = (this.WorkingDirectory |> Option.toObj)
        )

[<RequireQualifiedAccess>]
module RunProcess =

    let private expect expected message actual =
        if expected <> actual then
            failwith message

    let startProcess (args: RunProcessArgs) =
        let startInfo = args.toProcessStartInfo ()
        let p = new Process(StartInfo = startInfo)
        p.Start() |> expect true "Failed to start process"
        p

    let startAndWait (args: RunProcessArgs) =
        let startInfo = args.toProcessStartInfo ()
        let p = new Process(StartInfo = startInfo)
        p.Start() |> expect true "Failed to start process"
        p.WaitForExit()
        p.ExitCode |> expect 0 $"Process exited with code {p.ExitCode}"

    let startAndForget = startProcess >> ignore

    let getOutput args =
        let fixedArgs = { args with CaptureStandardOutput = true }
        let p = startProcess fixedArgs
        let output = p.StandardOutput.ReadToEnd()
        p.WaitForExit()
        p.ExitCode |> expect 0 $"Process exited with code {p.ExitCode}"
        output
