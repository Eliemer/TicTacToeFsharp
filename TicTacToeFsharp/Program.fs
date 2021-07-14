module Main

open System
open Board

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

[<EntryPoint>]
let main argv =
    let mutable gameBoard = initBoard()
    let mutable playerMark = X

    while Option.isNone (checkWinner gameBoard) do
        Console.Clear()
        printfn "%A" (drawBoard gameBoard)
        let mutable playerMove : Position option = None

        while Option.isNone playerMove do
            printfn "\n%A's turn\nInput move: ex. A2\n" playerMark
            let userInput = Console.ReadLine()
            playerMove <- parsePlayerInput userInput
            match playerMove with
            | None -> ()
            | Some move -> do
                if canPlaceMark move gameBoard then do
                    placeMark playerMark move gameBoard
                    playerMark <- if playerMark = X then O else X

    Console.WriteLine($"The winner is... {(checkWinner gameBoard).Value}")
    Console.WriteLine("\nPress enter to exit...")
    Console.ReadLine() |> ignore
    0