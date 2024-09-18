namespace Pinicola.FSharp

open System

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
