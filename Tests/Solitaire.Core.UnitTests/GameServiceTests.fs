module Solitaire.Core.GameServiceTests

open NUnit.Framework

[<Test>]
let DealTableau_SevenColumnsAreCreated () =
    let cards = CardDealer.dealCards()
    let tableauState = GameDealer.dealTableau(cards)
    Assert.AreEqual(tableauState.tableau.Length, 7);

[<Test>]
let DealTableau_FirstColumnHasOneCard () =
    let cards = CardDealer.dealCards()
    let tableauState = GameDealer.dealTableau(cards)
    Assert.AreEqual(tableauState.tableau.Head.Length, 1);

[<Test>]
let DealTableau_LastColumnHasSevenCards () =
    let cards = CardDealer.dealCards()
    let tableauState = GameDealer.dealTableau(cards)
    let lastColumn = tableauState.tableau.[tableauState.tableau.Length - 1]
    Assert.AreEqual(lastColumn.Length, 7);