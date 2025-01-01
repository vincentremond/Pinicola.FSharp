namespace Pinicola.FSharp.IO

open System.IO

[<RequireQualifiedAccess>]
module Directory =

    let exists path = Directory.Exists(path)
