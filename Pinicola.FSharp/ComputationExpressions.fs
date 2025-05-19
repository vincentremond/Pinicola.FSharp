namespace Pinicola.FSharp

[<AutoOpen>]
module ComputationExpressions =

    type ListBuilder() =
        member this.Bind(m, f) = m |> List.collect f

        member this.Zero() = []

        member this.Yield(x) = [ x ]

        member this.YieldFrom(m: 'a list) = m
        member this.YieldFrom(m: 'a seq) = m |> Seq.toList

        member this.For(m, f) = this.Bind(m, f)

        member this.Combine(a, b) =
            List.concat [
                a
                b
            ]

        member this.Delay(f) = f ()

    let list = ListBuilder()
