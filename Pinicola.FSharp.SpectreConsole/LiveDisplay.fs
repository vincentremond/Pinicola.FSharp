namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console
open Spectre.Console.Rendering

[<RequireQualifiedAccess>]
module LiveDisplay =

    let withAutoClear v (l: LiveDisplay) =
        l.AutoClear <- v
        l

    let withOverflow v (l: LiveDisplay) =
        l.Overflow <- v
        l

    let withCropping v (l: LiveDisplay) =
        l.Cropping <- v
        l

    let start (f: LiveDisplayContext -> unit) (l: LiveDisplay) = l.Start(f)

[<RequireQualifiedAccess>]
module LiveDisplayContext =
    let refresh (ctx: LiveDisplayContext) = ctx.Refresh()
    let updateTarget (ctx: LiveDisplayContext) (target: IRenderable) = ctx.UpdateTarget(target)
