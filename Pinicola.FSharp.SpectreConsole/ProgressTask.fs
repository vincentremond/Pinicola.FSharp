namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module ProgressTask =

    let run<'t> (taskName: string) fn (ctx: ProgressContext) : 't =
        let task = ctx.AddTask(taskName, autoStart = true)
        task.IsIndeterminate <- true
        let result = fn task
        task.IsIndeterminate <- false
        task.StopTask()
        result
