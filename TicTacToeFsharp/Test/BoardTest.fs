module BoardTest

open NUnit.Framework
open Board

[<Test>]
let``Print some boards``() =
    let boardToTest : Board = Array2D.create 3 3 None
    let correctBoard = "+---+---+---+\n\
                        |   |   |   |\n\
                        +---+---+---+\n\
                        |   |   |   |\n\
                        +---+---+---+\n\
                        |   |   |   |\n\
                        +---+---+---+"
    Assert.AreEqual(correctBoard, drawBoard boardToTest)

