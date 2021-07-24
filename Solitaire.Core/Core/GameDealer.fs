module GameDealer
open Game


let shuffleCards(cards: Card list): Card list =
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


type TableauDealState = {
    remainingCards: Card list;
    tableau: Card list list;
}

let dealTableau(deck: Card list) =
    let initialValue = { 
        remainingCards = deck; 
        tableau = List.empty 
    }

    seq { 1..7 }
    |> Seq.rev
    |> Seq.fold(fun acc tabIndex ->
        let (new_tableau, remainder) = acc.remainingCards |> List.splitAt(tabIndex)
        {
            remainingCards = remainder;
            tableau = new_tableau :: acc.tableau
        }
    ) initialValue


let dealGame(): Game =
    let deck = dealCards()
    let { remainingCards = stock; tableau = tableau } = dealTableau(deck)
    {
        Id = System.Guid.NewGuid();
        Wastepile = List.Empty;
        Stock = stock;
        Tableau = tableau;
    }
    