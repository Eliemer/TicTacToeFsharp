module Board

type Mark = X | O
type Position = { X : int; Y : int }
type Board = Mark option [,]

let initBoard() : Board = Array2D.create 3 3 None
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

let placeMark (mark : Mark) (pos: Position) (board : Board) : unit =
    board.[pos.X, pos.Y] <- Some mark

let canPlaceMark (pos : Position) (board: Board) : bool =
    Option.isNone board.[pos.X, pos.Y]

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

type MaybeHelper() =
    member this.Bind(x, f) = 
        match x with
        | Some _ -> f x
        | None -> None

    member this.Return(x) =
        Some x

// TEST code
let mutable (board : Board) = Array2D.create 3 3 None
printf "%s\n" (drawBoard board)

board.[0, 0] <- Some X
printf "%s\n" (drawBoard board)

placeMark O {X=2;Y=1} board
printf "%s\n" (drawBoard board)

placeMark O {X=2; Y=0} board
placeMark O {X=2; Y=2} board
printfn "%s" (drawBoard board)
printf "%A\n" (checkWinner board)