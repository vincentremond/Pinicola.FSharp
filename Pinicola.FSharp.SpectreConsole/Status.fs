namespace Pinicola.FSharp.SpectreConsole

open System.Threading.Tasks
open Spectre.Console

[<RequireQualifiedAccess>]
module Status =

    let run (title: string) (action: StatusContext -> unit) (status: Status) = status.Start(title, action)
    let start (title: string) (func: StatusContext -> 'a) (status: Status) : 'a = status.Start(title, func)
    let runAsync (title: string) (func: StatusContext -> Task) (status: Status) = status.StartAsync(title, func)

    let startAsync (title: string) (func: StatusContext -> Task<'a>) (status: Status) : Task<'a> =
        status.StartAsync(title, func)

[<RequireQualifiedAccess>]
module SimpleStatus =

    let run (title: string) (func: StatusContext -> 'a) : 'a = AnsiConsole.Status().Start(title, func)
