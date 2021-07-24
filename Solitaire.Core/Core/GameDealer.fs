module GameDealer
open Game

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
    let deck = CardDealer.dealCards()
    let { remainingCards = stock; tableau = tableau } = dealTableau(deck)
    {
        Id = System.Guid.NewGuid();
        Wastepile = List.Empty;
        Stock = stock;
        Tableau = tableau;
        Foundations = List.Empty;
    }
    