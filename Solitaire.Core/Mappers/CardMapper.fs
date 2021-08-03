module CardMapper

open Solitaire.Infrastructure.Models
open Game

let mapCardDboToCard(card: CardDbo): Card =
    
    let suit = match card.Suit with
    | SuitDbo.Diamonds -> Diamonds
    | SuitDbo.Spades -> Spades
    | SuitDbo.Hearts -> Hearts
    | SuitDbo.Clubs -> Clubs

    {
        Rank = card.Rank
        Suit = suit
    }

let mapCardToCardDbo(card: Card) = 

    let suit = match card.Suit with
    | Diamonds -> SuitDbo.Diamonds
    | Spades -> SuitDbo.Spades
    | Hearts -> SuitDbo.Hearts
    | Clubs -> SuitDbo.Clubs

    CardDbo(suit, card.Rank);

