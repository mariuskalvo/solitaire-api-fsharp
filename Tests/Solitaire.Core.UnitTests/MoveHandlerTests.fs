module Solitaire.Core.MoveHandlerTests

open NUnit.Framework
open MoveHandler
open Game

[<Test>]
let WhenMoveFromStockToTableauAppendsToCorrectTableauColumn () =
    let game = GameDealer.dealGame ()

    let destIdx = 0
    let moveCommand = StockToTableau({ DestIndex = destIdx })
    let updatedGame = handleMove (game, moveCommand)

    let oldTableauColumn = game.Tableau.[destIdx]
    let newTableauColumn = updatedGame.Tableau.[destIdx]

    Assert.AreEqual(oldTableauColumn.Length + 1, newTableauColumn.Length)
    Assert.AreEqual(game.Stock.Length - 1, updatedGame.Stock.Length)
    Assert.AreEqual(game.Stock.Head, newTableauColumn.Head)

[<Test>]
let WhenMoveFromStockToTableauStockIsEmptyThenIsNoOp () =
    let game =
        { Tableau = [ [ { Rank = 1; Suit = Diamonds } ] ]
          Stock = []
          Foundations = []
          Wastepile = [] }

    let destIdx = 0
    let moveCommand = StockToTableau({ DestIndex = destIdx })
    let updatedGame = handleMove (game, moveCommand)

    Assert.AreEqual(game, updatedGame)

[<Test>]
let WhenMoveFromStockToTableauStockHasOneCardThenStockIsLeftEmpty () =
    let game =
        { Tableau = [ [] ]
          Stock = [ { Rank = 1; Suit = Diamonds } ]
          Foundations = []
          Wastepile = [] }

    let destIdx = 0
    let moveCommand = StockToTableau({ DestIndex = destIdx })
    let updatedGame = handleMove (game, moveCommand)

    let oldTableauColumn = game.Tableau.[destIdx]
    let newTableauColumn = updatedGame.Tableau.[destIdx]

    Assert.AreEqual(oldTableauColumn.Length + 1, newTableauColumn.Length)
    Assert.AreEqual(game.Stock.Length - 1, updatedGame.Stock.Length)
    Assert.AreEqual(game.Stock.Head, newTableauColumn.Head)
    Assert.IsEmpty(updatedGame.Stock)
