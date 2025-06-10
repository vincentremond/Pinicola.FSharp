namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module ProgressContext =

    let addTask (name: string) (context: ProgressContext) = context.AddTask(name)
