module GameDealer

open Game

type TableauDealState =
    { RemainingCards: Card list
      Tableau: Card list list }

let dealTableau (deck: Card list) =
    let initialValue =
        { RemainingCards = deck
          Tableau = List.empty }

    seq { 1 .. 7 }
    |> Seq.rev
    |> Seq.fold
        (fun acc tabIndex ->
            let (newTableau, remainder) =
                acc.RemainingCards |> List.splitAt (tabIndex)

            { RemainingCards = remainder
              Tableau = newTableau :: acc.Tableau })
        initialValue

let dealGame () : Game =
    let deck = CardDealer.dealCards ()
    let dealTableauState = dealTableau (deck)

    { Wastepile = List.Empty
      Stock = dealTableauState.RemainingCards
      Tableau = dealTableauState.Tableau
      Foundations = List.Empty }
