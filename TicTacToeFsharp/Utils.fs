module Utils

open Models

let parsePlayerInput (input : string) : Position option =

    let y = if input.Length <> 2 then None
            else match input.[0] with
                 | 'A' -> Some 0
                 | 'B' -> Some 1
                 | 'C' -> Some 2
                 | _ -> None

    let x = if input.Length <> 2 then None
            else match input.[1] with
                 | '1' -> Some 0
                 | '2' -> Some 1
                 | '3' -> Some 2
                 | _ -> None

    if Option.isSome x && Option.isSome y then Some {X=x.Value; Y=y.Value} else None