module Solitaire.Core.GameServiceTests

open NUnit.Framework
open GameDealer

[<SetUp>]
let Setup () =
    ()

[<Test>]
let DealCards__52CardsAreCreated () =
    let cards = GameDealer.dealCards()
    Assert.AreEqual(cards.Length, 52)

[<Test>]
let DealCards_AllCardsAreUnique () =
    let cards = GameDealer.dealCards()
    let distinctCards = List.distinct(cards)
    Assert.AreEqual(cards.Length, distinctCards.Length)

[<Test>]
let DealTableau_SevenColumnsAreCreated () =
    let cards = GameDealer.dealCards()
    let tableauState = GameDealer.dealTableau(cards)
    Assert.AreEqual(tableauState.tableau.Length, 7);

[<Test>]
let DealTableau_FirstColumnHasOneCard () =
    let cards = GameDealer.dealCards()
    let tableauState = GameDealer.dealTableau(cards)
    Assert.AreEqual(tableauState.tableau.Head.Length, 1);

[<Test>]
let DealTableau_LastColumnHasSevenCards () =
    let cards = GameDealer.dealCards()
    let tableauState = GameDealer.dealTableau(cards)
    let lastColumn = tableauState.tableau.[tableauState.tableau.Length - 1]
    Assert.AreEqual(lastColumn.Length, 7);