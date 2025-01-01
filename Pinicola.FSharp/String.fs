namespace Pinicola.FSharp

open System

[<AutoOpen>]
module StringActivePatterns =

    let (|Contains|_|) comparisonType (sub: string) (s: string) =
        if s.Contains(sub, comparisonType) then Some() else None

[<RequireQualifiedAccess>]
module String =

    let private equalsWithComparison (comparison: StringComparison) (s1: string) (s2: string) =
        System.String.Equals(s1, s2, comparison)

    let equalsOrdinalIgnoreCase =
        equalsWithComparison StringComparison.OrdinalIgnoreCase

    let equalsOrdinal = equalsWithComparison StringComparison.Ordinal

    let equalsCurrentCultureIgnoreCase =
        equalsWithComparison StringComparison.CurrentCultureIgnoreCase

    let equalsCurrentCulture = equalsWithComparison StringComparison.CurrentCulture

    let equalsInvariantCultureIgnoreCase =
        equalsWithComparison StringComparison.InvariantCultureIgnoreCase

    let equalsInvariantCulture = equalsWithComparison StringComparison.InvariantCulture

    let split (separator: char) (s: string) = s.Split(separator) |> List.ofArray

    let trim (s: string) = s.Trim()
    let trimChar (c: char) (s: string) = s.Trim(c)

    let trimEnd (s: string) = s.TrimEnd()

    let trimEndChar (c: char) (s: string) = s.TrimEnd(c)

    let trimStart (s: string) = s.TrimStart()

    let trimStartChar (c: char) (s: string) = s.TrimStart(c)

    let toLower (s: string) = s.ToLower()
