module Main

open System
open Models
open Board
open Utils

/// <summary>
/// A basic game of tic-tac-toe.
/// </summary>
[<EntryPoint>]
let main args =
    let mutable gameBoard = initBoard()
    let mutable playerMark = X // starting player

    // Game Loop
    while Option.isNone (checkWinner gameBoard) && not (isBoardFull gameBoard) do
        Console.Clear()
        printfn "%s" (drawBoard gameBoard)
        let mutable playerMove : Position option = None

        while Option.isNone playerMove do
            // obtain player's input
            printfn "\n%A's turn\nInput move: ex. A2\n" playerMark
            let userInput = Console.ReadLine()
            playerMove <- parsePlayerInput userInput

            // play input
            match playerMove with
            | None -> ()
            | Some move -> do
                if canPlaceMark move gameBoard then do
                    placeMark playerMark move gameBoard
                    playerMark <- if playerMark = X then O else X

    Console.Clear()
    printfn "%s" (drawBoard gameBoard)

    // endstate messages
    if isBoardFull gameBoard then do
        printfn "...There were no winners ..."
    else
        Console.WriteLine($"The winner is... {(checkWinner gameBoard).Value}")

    Console.WriteLine("\nPress enter to exit...")
    Console.ReadLine() |> ignore

    // exit code
    0