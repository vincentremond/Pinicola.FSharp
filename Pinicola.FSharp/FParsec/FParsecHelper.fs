namespace Pinicola.FSharp.FParsec

[<AutoOpen>]
module FParsecHelper =

    open FParsec

    let parseOrFail parser input =
        match run parser input with
        | Success(result, _userState, _position) -> result
        | Failure(errorMsg, _parserError, _userState) -> failwithf $"Failed to parse input: %s{errorMsg}"

