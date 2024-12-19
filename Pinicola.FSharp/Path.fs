namespace Pinicola.FSharp

open System.IO

[<AutoOpen>]
module Path =

    let (</>) (path1: string) (path2: string) = Path.Join(path1, path2)
