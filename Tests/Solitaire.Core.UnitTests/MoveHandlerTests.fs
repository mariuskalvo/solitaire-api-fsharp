module Solitaire.Core.MoveHandlerTests

open NUnit.Framework
open MoveHandler
open Game


[<Test>]
let handleMove_moveFromStockToTableau_appendsToCorrectTableauColumn () =
    let game = GameDealer.dealGame ()

    let tableauIndex = 0

    let moveCommand =
        StockToTableau({ destIndex = tableauIndex })

    let updatedGame =
        MoveHandler.handleMove (game, moveCommand)

    let oldTableauColumn = game.Tableau.[tableauIndex]
    let newTableauColumn = updatedGame.Tableau.[tableauIndex]

    Assert.AreEqual(oldTableauColumn.Length + 1, newTableauColumn.Length)
    Assert.AreEqual(game.Stock.Length - 1, updatedGame.Stock.Length)
    Assert.AreEqual(game.Stock.Head, newTableauColumn.Head)

[<Test>]
let handleMoveMoveFromStockToTableauStockIsEmptyIsNoOp () =
    let game =
        { Tableau = [ [ { Rank = 1; Suit = Diamonds } ] ]
          Stock = []
          Foundations = []
          Wastepile = [] }

    let tableauIndex = 0

    let moveCommand =
        StockToTableau({ destIndex = tableauIndex })

    let updatedGame =
        MoveHandler.handleMove (game, moveCommand)

    Assert.AreEqual(game, updatedGame)

[<Test>]
let handleMove_moveFromStockToTableau_stockHasOneCard_stockIsLeftEmpty () =
    let game =
        { Tableau = [ [] ]
          Stock = [ { Rank = 1; Suit = Diamonds } ]
          Foundations = []
          Wastepile = [] }

    let tableauIndex = 0

    let moveCommand =
        StockToTableau({ destIndex = tableauIndex })

    let updatedGame =
        MoveHandler.handleMove (game, moveCommand)

    let oldTableauColumn = game.Tableau.[tableauIndex]
    let newTableauColumn = updatedGame.Tableau.[tableauIndex]

    Assert.AreEqual(oldTableauColumn.Length + 1, newTableauColumn.Length)
    Assert.AreEqual(game.Stock.Length - 1, updatedGame.Stock.Length)
    Assert.AreEqual(game.Stock.Head, newTableauColumn.Head)
    Assert.IsEmpty(updatedGame.Stock)
