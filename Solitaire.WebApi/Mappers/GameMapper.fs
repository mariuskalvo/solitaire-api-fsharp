module GameMapper

open Game
open GameWeb
open System

[<Literal>]
let ClubsSuit = "CLUBS"

[<Literal>]
let DiamondsSuit = "DIAMONDS"

[<Literal>]
let HeartsSuit = "HEARTS"

[<Literal>]
let SpadesSuit = "SPADES"

let private mapCardToCardWeb (card: Card) : CardWeb =
    let suit =
        match card.Suit with
        | Clubs -> ClubsSuit
        | Hearts -> HeartsSuit
        | Spades -> SpadesSuit
        | Diamonds -> DiamondsSuit

    { Suit = suit; Rank = card.Rank }

let private mapCardWebToCard (card: CardWeb) : Card =
    let suit =
        match card.Suit with
        | ClubsSuit -> Clubs
        | DiamondsSuit -> Diamonds
        | HeartsSuit -> Hearts
        | SpadesSuit -> Spades
        | _ -> raise (ArgumentException("Attempting to map invalid suit value"))

    { Suit = suit; Rank = card.Rank }

let private mapCardListToCardListWeb (cards: Card list) : CardWeb list = cards |> List.map (mapCardToCardWeb)

let private map2dCardListTo2dCardListWeb (cards: Card list list) : CardWeb list list =
    cards |> List.map (mapCardListToCardListWeb)

let private mapCardListWebToCardList (cards: CardWeb list) : Card list = cards |> List.map (mapCardWebToCard)

let private map2dCardListWebTo2dCardList (cards: CardWeb list list) : Card list list =
    cards |> List.map (mapCardListWebToCardList)

let mapGameToGameWeb (game: Game) : GameWeb =
    { Tableau = map2dCardListTo2dCardListWeb (game.Tableau)
      Foundations = map2dCardListTo2dCardListWeb (game.Foundations)
      ActiveStock = mapCardListToCardListWeb (game.ActiveStock)
      Stock = mapCardListToCardListWeb (game.Stock)
      Wastepile = mapCardListToCardListWeb (game.Wastepile) }

let mapGameWebToGame (game: GameWeb) : Game =
    { Tableau = map2dCardListWebTo2dCardList (game.Tableau)
      Foundations = map2dCardListWebTo2dCardList (game.Foundations)
      ActiveStock = mapCardListWebToCardList (game.ActiveStock)
      Stock = mapCardListWebToCardList (game.Stock)
      Wastepile = mapCardListWebToCardList (game.Wastepile) }
