namespace Pinicola.FSharp

open System
open System.Threading.Tasks
open Fargo

[<RequireQualifiedAccess>]
module FargoCmdLine =
    let run appName parser func =
        let args = Environment.GetCommandLineArgs().[1..]

        let exitCode =
            run
                appName
                parser
                args
                (fun _ parseResult ->
                    func parseResult
                    Task.FromResult(0)
                )

        Environment.ExitCode <- exitCode
