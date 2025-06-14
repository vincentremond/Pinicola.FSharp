namespace Pinicola.FSharp

type MergeResult<'left, 'right> =
    | LeftOnly of 'left
    | RightOnly of 'right
    | Both of 'left * 'right
