module CardDealer

open Game

let private shuffleCards(cards: Card list): Card list =
    let rnd = System.Random()
    cards |> List.sortBy(fun _ -> rnd.Next(1, cards.Length))

let dealCards() =
    let suits = [Diamonds; Clubs; Spades; Hearts]
    let ranks = seq { 1..13 } |> Seq.toList
    
    suits
    |> List.map(fun suit -> 
        ranks
        |> List.map(fun rank -> {
            Suit = suit;
            Rank = rank;
        })
    )
    |> List.concat
    |> shuffleCards