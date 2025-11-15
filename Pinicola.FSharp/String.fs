namespace Pinicola.FSharp

open System

[<AutoOpen>]
module StringActivePatterns =

    let (|Contains|_|) comparisonType (sub: string) (s: string) =
        if s.Contains(sub, comparisonType) then Some() else None

    let (|ContainsICIC|_|) (sub: string) (s: string) =
        if s.Contains(sub, StringComparison.InvariantCultureIgnoreCase) then
            Some()
        else
            None

    let (|Eq|_|) comparisonType (s1: string) (s2: string) =
        if String.Equals(s1, s2, comparisonType) then
            Some()
        else
            None

    let (|EqOCI|_|) (s1: string) (s2: string) =
        if String.Equals(s1, s2, StringComparison.OrdinalIgnoreCase) then
            Some()
        else
            None

    let (|EqICIC|_|) (s1: string) (s2: string) =
        if String.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase) then
            Some()
        else
            None

    let (|IsNullOrEmpty|_|) (s: string) =
        if String.IsNullOrEmpty(s) then Some() else None

    let (|StartsWith|_|) comparisonType (value: string) (s: string) =
        if s.StartsWith(value, comparisonType) then Some() else None

    let (|StartsWithICIC|_|) (value: string) (s: string) =
        if s.StartsWith(value, StringComparison.InvariantCultureIgnoreCase) then
            Some()
        else
            None

    let (|EndsWith|_|) comparisonType (value: string) (s: string) =
        if s.EndsWith(value, comparisonType) then Some() else None

[<RequireQualifiedAccess>]
module String =

    let equals (comparison: StringComparison) (s1: string) (s2: string) = String.Equals(s1, s2, comparison)

    let equalsOIC = equals StringComparison.OrdinalIgnoreCase

    let equalsO = equals StringComparison.Ordinal

    let equalsCCIC = equals StringComparison.CurrentCultureIgnoreCase

    let equalsCC = equals StringComparison.CurrentCulture

    let equalsICIC = equals StringComparison.InvariantCultureIgnoreCase

    let equalsInvariantCulture = equals StringComparison.InvariantCulture

    let split (separator: char) (s: string) = s.Split(separator) |> List.ofArray
    let splitMax (separator: char) (max: int) (s: string) = s.Split(separator, max) |> List.ofArray

    let trim (s: string) = s.Trim()
    let trimChar (c: char) (s: string) = s.Trim(c)

    let trimEnd (s: string) = s.TrimEnd()

    let trimEndChar (c: char) (s: string) = s.TrimEnd(c)

    let trimStart (s: string) = s.TrimStart()

    let trimStartChar (c: char) (s: string) = s.TrimStart(c)

    let toLower (s: string) = s.ToLower()

    let replace (oldValue: string) (newValue: string) (s: string) = s.Replace(oldValue, newValue)

    let splitWithOptions (separator: string) options (s: string) = s.Split(separator, options)

    let startsWith stringComparison (value: string) (str: string) = str.StartsWith(value, stringComparison)

    let startsWithCurrentCultureIgnoreCase =
        startsWith StringComparison.CurrentCultureIgnoreCase

    let startsWithICIC = startsWith StringComparison.InvariantCultureIgnoreCase

    let endsWith stringComparison (value: string) (str: string) = str.EndsWith(value, stringComparison)
    let endsWithICIC = endsWith StringComparison.InvariantCultureIgnoreCase

    let concatC (separator: char) (strings: seq<string>) = String.Join(separator, strings)

    let ofSeq (chars: char seq) : string = chars |> Seq.toArray |> String

    let implode: string seq -> string = String.concat String.Empty

    let contains stringComparison (sub: string) (s: string) = s.Contains(sub, stringComparison)

    let containsICIC = contains StringComparison.InvariantCultureIgnoreCase
