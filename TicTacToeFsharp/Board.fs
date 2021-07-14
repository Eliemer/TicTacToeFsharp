module Board

open Models

/// <summary>
/// Initialize a 3x3 tic-tac-toe board
/// </summary>
/// <returns>
/// A 3x3, 2D array of None's
/// </returns>
let initBoard() : Board = Array2D.create 3 3 None

/// <summary>
/// Returns a printable string representation of the board state
/// </summary>
/// <param name="board">The Board object representing the game state</param>
let drawBoard (board : Board) : string =
    let mutable result = "\n\t  A | B | C\n"
    for i in 0..2 do
        result <- result + (sprintf "\t+%s\n" (String.replicate 3 "---+"))

        result <- result + sprintf " %d\t|" (i+1)
        for j in 0..2  do
            match board.[i,j] with
            | Some mark ->
                result <- result + sprintf " %s |" (mark.ToString())
            | None ->
                result <- result + sprintf "   |"
        result <- result + sprintf "\n"

    result + "\t+---+---+---+\n"

/// <summary>
/// Mark a Position on the Board with a Mark
/// </summary>
/// <param name="mark">Either an X or an O</param>
/// <param name="pos">The desired position to place a mark on</param>
/// <param name="board">The game state</param>
let placeMark (mark : Mark) (pos: Position) (board : Board) : unit =
    board.[pos.X, pos.Y] <- Some mark

/// <summary>
/// Checks if a certain position is empty
/// </summary>
let canPlaceMark (pos : Position) (board: Board) : bool =
    Option.isNone board.[pos.X, pos.Y]

/// <summary>
/// Checks if there is a winner given a game state.
/// </summary>
/// <remarks>
/// A board contains a winner if 3 marks of the same kind line up horizontally, vertically, or diagonally.
/// </remarks>
let checkWinner (board : Board) : Mark option =
    let mutable result = None

    let helper (mark : Mark) : Mark option =
        let equalMark = (fun e -> e = Some mark)
        if Seq.forall equalMark board.[0,*] ||
           Seq.forall equalMark board.[1,*] ||
           Seq.forall equalMark board.[2,*] ||
           Seq.forall equalMark board.[*,0] ||
           Seq.forall equalMark board.[*,1] ||
           Seq.forall equalMark board.[*,2] ||
           Seq.forall equalMark [board.[0,0]; board.[1,1]; board.[2,2]] ||
           Seq.forall equalMark [board.[0,2]; board.[1,1]; board.[2,0]]
        then Some mark
        else None

    match helper X with
        | Some _ -> result <- Some X
        | None -> result <- helper O

    result

/// <summary>
/// Checks if there are no more None's in the board.
/// </summary>
let isBoardFull (board : Board) : bool =
    seq { for i in 0..2 do
            for j in 0..2 do
                Option.isSome board.[i,j] }
    |> Seq.forall ((=) true)
