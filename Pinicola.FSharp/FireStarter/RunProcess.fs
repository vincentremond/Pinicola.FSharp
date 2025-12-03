namespace Pinicola.FSharp.FireStarter

open System.Diagnostics
open Pinicola.FSharp.IO

type RunProcessArgs = {
    Executable: string
    Arguments: string list
    CaptureStandardOutput: bool
    CaptureStandardError: bool
    UseShellExecute: bool
    WorkingDirectory: string option
    CreateNoWindow: bool
} with

    static member defaultCaptureStandardOutput = true
    static member defaultCaptureStandardError = true
    static member defaultUseShellExecute = false
    static member defaultCreateNoWindow = true

    static member fromExecutableAndArgs executable args = {
        Executable = executable
        Arguments = args
        CaptureStandardOutput = RunProcessArgs.defaultCaptureStandardOutput
        CaptureStandardError = RunProcessArgs.defaultCaptureStandardError
        UseShellExecute = RunProcessArgs.defaultUseShellExecute
        WorkingDirectory = None
        CreateNoWindow = RunProcessArgs.defaultCreateNoWindow
    }

    static member fromExecutableInDirectory executable dir args =
        let exeFullPath = dir </> executable

        {
            Executable = exeFullPath
            Arguments = args
            CaptureStandardOutput = RunProcessArgs.defaultCaptureStandardOutput
            CaptureStandardError = RunProcessArgs.defaultCaptureStandardError
            UseShellExecute = RunProcessArgs.defaultUseShellExecute
            WorkingDirectory = Some dir
            CreateNoWindow = RunProcessArgs.defaultCreateNoWindow
        }

    static member fromGlobalExeAndWorkingDirectory executable workingDir = {
        Executable = executable
        Arguments = []
        CaptureStandardOutput = RunProcessArgs.defaultCaptureStandardOutput
        CaptureStandardError = RunProcessArgs.defaultCaptureStandardError
        UseShellExecute = RunProcessArgs.defaultUseShellExecute
        WorkingDirectory = Some workingDir
        CreateNoWindow = RunProcessArgs.defaultCreateNoWindow
    }

    static member withCaptureStandardOutput value args = { args with CaptureStandardOutput = value }

    static member withArguments arguments args = { args with Arguments = arguments }

    static member withCreateNoWindow value args = { args with CreateNoWindow = value }

    member this.toProcessStartInfo() =
        ProcessStartInfo(
            this.Executable,
            this.Arguments,
            RedirectStandardOutput = this.CaptureStandardOutput,
            RedirectStandardError = this.CaptureStandardError,
            UseShellExecute = this.UseShellExecute,
            WorkingDirectory = (this.WorkingDirectory |> Option.toObj),
            CreateNoWindow = this.CreateNoWindow
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

    let startAndWaitWithOutput (args: RunProcessArgs) out err =

        let handle f = Option.ofObj >> Option.iter f

        let startInfo = args.toProcessStartInfo ()
        let p = new Process(StartInfo = startInfo)
        p.OutputDataReceived.Add(_.Data >> (handle out))
        p.ErrorDataReceived.Add(_.Data >> (handle err))
        p.Start() |> expect true "Failed to start process"
        p.BeginErrorReadLine()
        p.BeginOutputReadLine()
        p.WaitForExit()

        match p.ExitCode with
        | 0 -> Ok()
        | code -> Error $"Process {args.Executable} exited with code {code}"

    let startAndForget = startProcess >> ignore

    let getOutput args =
        let fixedArgs = { args with CaptureStandardOutput = true }
        let p = startProcess fixedArgs
        let output = p.StandardOutput.ReadToEnd()
        p.WaitForExit()
        p.ExitCode |> expect 0 $"Process exited with code {p.ExitCode}"
        output
