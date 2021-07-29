module Solitaire.Core.MoveHandlerTests

open NUnit.Framework
open MoveHandler
open Game

[<Test>]
let WhenMoveFromStockToTableauAppendsToCorrectTableauColumn () =
    let game =
        { Tableau = [ [] ]
          Stock = []
          ActiveStock = [ { Rank = 1; Suit = Diamonds } ]
          Foundations = []
          Wastepile = [] }

    let destIdx = 0
    let updatedGame = handleMove (game, ActiveStock, Tableau 0)
    let newTableauColumn = updatedGame.Tableau.[destIdx]

    Assert.AreEqual(1, newTableauColumn.Length)
    Assert.AreEqual(0, updatedGame.ActiveStock.Length)
    Assert.AreEqual(game.ActiveStock.Head, newTableauColumn.Head)

[<Test>]
let WhenMoveFromStockToTableauStockIsEmptyThenIsNoOp () =
    let game =
        { Tableau = [ [ { Rank = 1; Suit = Diamonds } ] ]
          Stock = []
          ActiveStock = []
          Foundations = []
          Wastepile = [] }

    let updatedGame = handleMove (game, ActiveStock, Tableau 0)

    Assert.AreEqual(game, updatedGame)

[<Test>]
let WhenMoveFromActiveToTableauActiveHasOneCardThenActiveIsLeftEmpty () =
    let game =
        { Tableau = [ [] ]
          Stock = []
          Foundations = []
          ActiveStock = [ { Rank = 1; Suit = Diamonds } ]
          Wastepile = [] }

    let destIdx = 0
    let updatedGame = handleMove (game, ActiveStock, Tableau 0 )

    let oldTableauColumn = game.Tableau.[destIdx]
    let newTableauColumn = updatedGame.Tableau.[destIdx]

    Assert.AreEqual(oldTableauColumn.Length + 1, newTableauColumn.Length)
    Assert.AreEqual(game.ActiveStock.Head, newTableauColumn.Head)
    Assert.IsEmpty(updatedGame.ActiveStock)

[<Test>]
let WhenMoveFromStockToFoundationAndStockIsEmptyThenIsNoOp () =
    let game =
        { Tableau = []
          Stock = []
          ActiveStock = []
          Foundations = [ [] ]
          Wastepile = [] }

    let updatedGame = handleMove (game, ActiveStock, Foundations 0)
    Assert.AreEqual(game, updatedGame)

[<Test>]
let WhenMoveFromSTockToFoundationAndStockIsNotEmptyThenCardIsMoved () =
    let game =
        { Tableau = []
          Stock = []
          ActiveStock = [ { Rank = 1; Suit = Diamonds } ]
          Foundations = [ [] ]
          Wastepile = [] }

    let updatedGame = handleMove (game, ActiveStock, Foundations 0)

    let updatedFoundation = updatedGame.Foundations.Head
    Assert.AreEqual(1, updatedFoundation.Length)
    Assert.IsEmpty(updatedGame.ActiveStock)
