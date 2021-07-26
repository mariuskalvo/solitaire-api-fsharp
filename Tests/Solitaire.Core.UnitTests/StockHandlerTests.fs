module Solitaire.Core.StockHandlerTests

open NUnit.Framework
open Game


[<Test>]
let WhenStockIsEmptyAndIsCycledThenActiveCardsAndWastepileIsNewStock () =
    let game =
        { Tableau = []
          Stock = []
          ActiveStock = [ { Rank = 1; Suit = Diamonds } ]
          Foundations = []
          Wastepile = [ { Rank = 2; Suit = Diamonds } ] }

    let cycledStock = StockHandler.cycleStock (game)
    Assert.IsEmpty(cycledStock.ActiveStock)
    Assert.IsEmpty(cycledStock.Wastepile)

[<Test>]
let WhenStockHasOneItemAndActiveCardsIsEmptyThenCardIsMovedToActiveCards () =
    let game =
        { Tableau = []
          Stock = [ { Rank = 1; Suit = Diamonds } ]
          ActiveStock = []
          Foundations = []
          Wastepile = [] }

    let cycledStock = StockHandler.cycleStock (game)
    Assert.IsEmpty(cycledStock.Stock)
    Assert.AreEqual(1, cycledStock.ActiveStock.Length)

[<Test>]
let WhenStockHasTwoItemsAndActiveCardsIsEmptyThenCardIsMovedToActiveCards () =
    let game =
        { Tableau = []
          Stock =
              [ { Rank = 1; Suit = Diamonds }
                { Rank = 2; Suit = Diamonds } ]
          ActiveStock = []
          Foundations = []
          Wastepile = [] }

    let cycledStock = StockHandler.cycleStock (game)
    Assert.AreEqual(1, cycledStock.Stock.Length)
    Assert.AreEqual(1, cycledStock.ActiveStock.Length)

[<Test>]
let WhenStockHasItemsAndActiveCardsHasOneThenCardIsMovedToActiveCards () =
    let game =
        { Tableau = []
          Stock =
              [ { Rank = 1; Suit = Diamonds }
                { Rank = 2; Suit = Diamonds }
                { Rank = 3; Suit = Diamonds }
                { Rank = 4; Suit = Diamonds } ]
          ActiveStock = [ { Rank = 5; Suit = Diamonds } ]
          Foundations = []
          Wastepile = [] }

    let cycledStock = StockHandler.cycleStock (game)
    Assert.AreEqual(game.Stock.Length - 1, cycledStock.Stock.Length)
    Assert.AreEqual(game.ActiveStock.Length + 1, cycledStock.ActiveStock.Length)

[<Test>]
let WhenStockHasItemsAndActiveCardsHasThreeItemsThenCardIsMovedToActiveCardsAndActiveCardIsCycledToWaste () =
    let game =
        { Tableau = []
          Stock =
              [ { Rank = 1; Suit = Diamonds }
                { Rank = 2; Suit = Diamonds } ]
          ActiveStock =
              [ { Rank = 3; Suit = Diamonds }
                { Rank = 4; Suit = Diamonds }
                { Rank = 3; Suit = Diamonds } ]
          Foundations = []
          Wastepile = [] }

    let cycledStock = StockHandler.cycleStock (game)
    Assert.AreEqual(game.Stock.Length - 1, cycledStock.Stock.Length)
    Assert.AreEqual(3, cycledStock.ActiveStock.Length)
    Assert.AreEqual(1, cycledStock.Wastepile.Length)
