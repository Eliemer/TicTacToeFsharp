module Main

open System
open Models
open Board
open Utils

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