module Solitaire.Core.GameServiceTests

open NUnit.Framework

[<Test>]
let WhenDealTableauThenSevenColumnsAreCreated () =
    let cards = CardDealer.dealCards ()
    let tableauState = GameDealer.dealTableau (cards)
    Assert.AreEqual(tableauState.Tableau.Length, 7)

[<Test>]
let WhenDealTableauThenFirstColumnHasOneCard () =
    let cards = CardDealer.dealCards ()
    let tableauState = GameDealer.dealTableau (cards)
    Assert.AreEqual(tableauState.Tableau.Head.Length, 1)

[<Test>]
let WhenDealTableauThenLastColumnHasSevenCards () =
    let cards = CardDealer.dealCards ()
    let tableauState = GameDealer.dealTableau (cards)

    let lastColumn =
        tableauState.Tableau.[tableauState.Tableau.Length - 1]

    Assert.AreEqual(lastColumn.Length, 7)
