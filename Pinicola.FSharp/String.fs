﻿namespace Pinicola.FSharp

open System

[<AutoOpen>]
module StringActivePatterns =

    let (|Contains|_|) comparisonType (sub: string) (s: string) =
        if s.Contains(sub, comparisonType) then Some() else None

[<RequireQualifiedAccess>]
module String =

    let private equals (comparison: StringComparison) (s1: string) (s2: string) = String.Equals(s1, s2, comparison)

    let equalsOrdinalIgnoreCase = equals StringComparison.OrdinalIgnoreCase

    let equalsOrdinal = equals StringComparison.Ordinal

    let equalsCurrentCultureIgnoreCase =
        equals StringComparison.CurrentCultureIgnoreCase

    let equalsCurrentCulture = equals StringComparison.CurrentCulture

    let equalsInvariantCultureIgnoreCase =
        equals StringComparison.InvariantCultureIgnoreCase

    let equalsInvariantCulture = equals StringComparison.InvariantCulture

    let split (separator: char) (s: string) = s.Split(separator) |> List.ofArray

    let trim (s: string) = s.Trim()
    let trimChar (c: char) (s: string) = s.Trim(c)

    let trimEnd (s: string) = s.TrimEnd()

    let trimEndChar (c: char) (s: string) = s.TrimEnd(c)

    let trimStart (s: string) = s.TrimStart()

    let trimStartChar (c: char) (s: string) = s.TrimStart(c)

    let toLower (s: string) = s.ToLower()

    let replace (oldValue: string) (newValue: string) (s: string) = s.Replace(oldValue, newValue)

    let splitWithOptions (separator: string) options (s: string) = s.Split(separator, options)

    let private startsWith stringComparison (str: string) (value: string) = str.StartsWith(value, stringComparison)

    let startsWithCurrentCultureIgnoreCase =
        startsWith StringComparison.CurrentCultureIgnoreCase

    let private endsWith stringComparison (str: string) (value: string) = str.EndsWith(value, stringComparison)

    let concatC (separator: char) (strings: seq<string>) = String.Join(separator, strings)
