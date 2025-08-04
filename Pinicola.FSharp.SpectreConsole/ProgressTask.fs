namespace Pinicola.FSharp.SpectreConsole

open System.Threading.Tasks
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

    let runAsync<'t> (taskName: string) (fn: ProgressTask -> 't) (ctx: ProgressContext) : Task<'t> =

        Task.Run(fun () ->
            let t = ctx.AddTask(taskName, autoStart = true)

            t.IsIndeterminate <- true
            let result = fn t
            t.IsIndeterminate <- false
            t.StopTask()
            result
        )

    let execAsync (taskName: string) (fn: ProgressTask -> unit) (ctx: ProgressContext) : Task =

        Task.Run(fun () ->
            let t = ctx.AddTask(taskName, autoStart = true)
            t.IsIndeterminate <- true
            fn t
            t.IsIndeterminate <- false
            t.StopTask()
        )

    let stop (task: ProgressTask) =
        task.IsIndeterminate <- false
        task.StopTask()
