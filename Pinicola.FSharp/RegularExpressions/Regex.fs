namespace Pinicola.FSharp.RegularExpressions

open System.Text.RegularExpressions

[<AutoOpen>]
module RegexPatterns =

    let (|MatchRegex|_|) (regex: Regex) (input: string) =
        let m = regex.Match(input)

        if m.Success then Some(m) else None

[<RequireQualifiedAccess>]
module Regex =

    let isMatch (i: string) (r: Regex) = r.IsMatch(i)
    let isMatch' (pattern: string) (i: string) = Regex.IsMatch(i, pattern)
