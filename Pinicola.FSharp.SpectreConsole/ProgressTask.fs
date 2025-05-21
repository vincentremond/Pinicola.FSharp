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
        let t = ctx.AddTask(taskName, autoStart = true)

        task {
            t.IsIndeterminate <- true
            let result = fn t
            t.IsIndeterminate <- false
            t.StopTask()
            return result
        }
