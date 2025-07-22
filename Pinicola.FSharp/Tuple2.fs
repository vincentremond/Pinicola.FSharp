namespace Pinicola.FSharp

[<RequireQualifiedAccess>]
module Tuple2 =

    let mapSnd f (a, b) = (a, f b)
    let mapFst f (a, b) = (f a, b)

    let map f (a1, a2) = (f a1, f a2)
    let map1 (f1, f2) (a1, a2) = (f1 a1, f2 a2)
    let map2 (f1, f2) (a1, a2) (b1, b2) = (f1 a1 b1, f2 a2 b2)

    let init a b = (a, b)

    let toList (a, b) = [
        a
        b
    ]
    
    let curry f (a, b) = f a b
