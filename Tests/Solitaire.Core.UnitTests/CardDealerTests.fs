module Solitaire.Core.CardDealerTests

open NUnit.Framework

[<Test>]
let WhenDealCardsThen52CardsAreCreated () =
    let cards = CardDealer.dealCards ()
    Assert.AreEqual(cards.Length, 52)

[<Test>]
let WhenDealCardsThenAllCardsAreUnique () =
    let cards = CardDealer.dealCards ()
    let distinctCards = List.distinct (cards)
    Assert.AreEqual(cards.Length, distinctCards.Length)
