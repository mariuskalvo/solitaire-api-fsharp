module GameWeb

type CardWeb = { Rank: int; Suit: string }

type GameWeb =
    { Tableau: CardWeb list list
      Stock: CardWeb list
      ActiveStock: CardWeb list
      Wastepile: CardWeb list
      Foundations: CardWeb list list }
