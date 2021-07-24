module Game

open System

type Suit = Clubs | Diamonds | Hearts | Spades

type Card = {
    Rank:int;
    Suit:Suit;
}

type Game = {
    Id:Guid;
    Tableau: Card list list;
    Stock: Card list;
    Wastepile: Card list;
}