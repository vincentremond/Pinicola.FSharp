namespace Pinicola.FSharp

open System

[<RequireQualifiedAccess>]
module Enum =

    let getValues<'t when 't :> Enum> () : 't list =
        Enum.GetValues(typeof<'t>) |> Seq.cast<'t> |> Seq.toList

    let getFlags<'t when 't :> Enum> (value: 't) =
        () |> getValues<'t> |> List.filter value.HasFlag
