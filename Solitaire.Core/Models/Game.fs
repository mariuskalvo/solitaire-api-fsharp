module Game

type Suit =
    | Clubs
    | Diamonds
    | Hearts
    | Spades

type Card = { Rank: int; Suit: Suit }

type Game =
    { Tableau: Card list list
      Stock: Card list
      ActiveStock: Card list
      Wastepile: Card list
      Foundations: Card list list }
