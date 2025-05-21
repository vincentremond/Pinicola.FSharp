namespace Pinicola.FSharp.SpectreConsole

open System.Threading.Tasks
open Spectre.Console

[<RequireQualifiedAccess>]
module Progress =

    module Columns =
        let downloaded = DownloadedColumn()
        let elapsedTime = ElapsedTimeColumn()
        let percentage = PercentageColumn()
        let progressBar = ProgressBarColumn()
        let spinner = SpinnerColumn()
        let remainingTime = RemainingTimeColumn()
        let taskDescription = TaskDescriptionColumn()
        let transferSpeed = TransferSpeedColumn()

    let init () =
        let ansiConsole = AnsiConsole.Console
        Progress(ansiConsole)

    let start (fn: ProgressContext -> 'a) (p: Progress) = p.Start(fn)
    let startAsync (fn: ProgressContext -> Task<'a>) (p: Progress) = p.StartAsync(fn)

    let startAsync' fn p =
        startAsync fn p |> Async.AwaitTask |> Async.RunSynchronously

    let runTasks list getDescription run (p: Progress) =
        p.StartAsync(fun progressContext ->
            task {
                let progressTasks =
                    list
                    |> List.map (fun item ->
                        let description = getDescription item
                        progressContext.AddTask(description), item
                    )

                let runningTasks =
                    progressTasks
                    |> List.map (fun (t, item) ->
                        t.StartTask()

                        task {
                            let result = run item
                            t.StopTask()
                            return result
                        }
                    )

                let! results = Task.WhenAll(runningTasks)
                return results |> List.ofArray
            }
        )
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let withHideCompleted v (p: Progress) =
        p.HideCompleted <- v
        p

    let withAutoClear v (p: Progress) =
        p.AutoClear <- v
        p

    let withAutoRefresh v (p: Progress) =
        p.AutoRefresh <- v
        p

    let withColumns v (p: Progress) = p.Columns(v |> List.toArray)
