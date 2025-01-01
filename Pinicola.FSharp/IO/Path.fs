namespace Pinicola.FSharp

open System.IO

[<AutoOpen>]
module PathOperators =

    let (</>) (path1: string) (path2: string) = Path.Join(path1, path2)

[<RequireQualifiedAccess>]
module Path =
    let getFullPath path = Path.GetFullPath path
