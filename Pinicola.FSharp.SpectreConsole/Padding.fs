namespace Pinicola.FSharp.SpectreConsole

open Spectre.Console

[<RequireQualifiedAccess>]
module Padding =

    let fromSize (size: int) = Padding(size)

    let fromHorizontalAndVertical (horizontal: int, vertical: int) = Padding(horizontal, vertical)

    let fromLeftTopRightBottom (left: int) (top: int) (right: int) (bottom: int) = Padding(left, top, right, bottom)
