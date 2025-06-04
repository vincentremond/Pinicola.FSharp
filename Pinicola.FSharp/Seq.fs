namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module Seq =

    let groupByFst s = Seq.groupBy fst s
    let groupBySnd s = Seq.groupBy snd s
    let mapTupleFst f = Seq.map (fun a -> (f a, a))
    let mapTupleSnd f = Seq.map (fun a -> (a, f a))
    let any s = s |> Seq.isEmpty |> not
