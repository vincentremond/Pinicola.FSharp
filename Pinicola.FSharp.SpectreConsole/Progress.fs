namespace Pinicola.FSharp.SpectreConsole

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

    let start (fn: ProgressContext -> unit) (p: Progress) = p.Start(fn)

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
