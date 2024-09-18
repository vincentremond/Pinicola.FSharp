namespace Pinicola.FSharp

open System.Text.RegularExpressions

[<AutoOpen>]
module Regex =

    let (|MatchRegex|_|) (regex: Regex) (input: string) =
        let m = regex.Match(input)

        if m.Success then Some(m) else None
