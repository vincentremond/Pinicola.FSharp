namespace Pinicola.FSharp.FireStarter

open System.Diagnostics

type RunProcessArgs = {
    Executable: string
    Arguments: string list
    RedirectStandardOutput: bool
    UseShellExecute: bool
} with

    static member defaultRedirectStandardOutput = true
    static member defaultUseShellExecute = false

    static member fromExecutableAndArgs executable args = {
        Executable = executable
        Arguments = args
        RedirectStandardOutput = RunProcessArgs.defaultRedirectStandardOutput
        UseShellExecute = RunProcessArgs.defaultUseShellExecute
    }

    member this.toProcessStartInfo() =
        ProcessStartInfo(this.Executable, this.Arguments, RedirectStandardOutput = this.RedirectStandardOutput, UseShellExecute = this.UseShellExecute)

[<RequireQualifiedAccess>]
module RunProcess =

    let private expect expected message actual =
        if expected <> actual then
            failwith message

    let private startProcess (args: RunProcessArgs) =
        let startInfo = args.toProcessStartInfo ()
        let p = new Process(StartInfo = startInfo)
        p.Start() |> expect true "Failed to start process"
        p

    let getOutput args =
        let fixedArgs = { args with RedirectStandardOutput = true }
        let p = startProcess fixedArgs
        let output = p.StandardOutput.ReadToEnd()
        p.WaitForExit()
        p.ExitCode |> expect 0 $"Process exited with code {p.ExitCode}"
        output
