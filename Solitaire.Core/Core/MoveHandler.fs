module MoveHandler

open Game

type CardArea =
    | Foundations of int
    | Tableau of int
    | ActiveStock

let private moveFromActiveToTableau (game: Game, tableauIndex: int) =
    match game.ActiveStock with
    | [] -> game
    | aHead :: aTail ->
        let newTableauColumn = aHead :: game.Tableau.[tableauIndex]

        let newTableau =
            ListUtils.replaceAt (game.Tableau, tableauIndex, newTableauColumn)

        { game with
              Tableau = newTableau
              ActiveStock = aTail }


let private moveFromActiveToFoundations (game, destIndex) =
    match game.ActiveStock with
    | [] -> game
    | stockHead :: stockTail ->
        let newDestFoundation =
            stockHead :: game.Foundations.[destIndex]

        let newFoundations =
            ListUtils.replaceAt (game.Foundations, destIndex, newDestFoundation)

        { game with
              Foundations = newFoundations
              ActiveStock = stockTail }

let private moveFromTableauToFoundations (game, srcIndex, destIndex) =
    let tableauColumn = game.Tableau.[srcIndex]
    let foundation = game.Foundations.[destIndex]

    match tableauColumn with
    | [] -> game
    | tabHead :: tabTail ->
        let newFoundation = tabHead :: foundation

        let newFoundations =
            ListUtils.replaceAt (game.Foundations, destIndex, newFoundation)

        let newTableau =
            ListUtils.replaceAt (game.Tableau, srcIndex, tabTail)

        { game with
              Foundations = newFoundations
              Tableau = newTableau }


let private moveFromFoundationsToTableau (game, srcIndex, destIndex) =
    let tableauColumn = game.Tableau.[srcIndex]
    let foundation = game.Foundations.[destIndex]

    match foundation with
    | [] -> game
    | fHead :: fTail ->
        let newTableau = fHead :: tableauColumn

        let newFoundations =
            ListUtils.replaceAt (game.Foundations, destIndex, fTail)

        let newTableau =
            ListUtils.replaceAt (game.Tableau, srcIndex, newTableau)

        { game with
              Foundations = newFoundations
              Tableau = newTableau }




let handleMove (game: Game, source: CardArea, dest: CardArea) : Game =
    match (source, dest) with
    | (ActiveStock, Tableau i) -> moveFromActiveToTableau (game, i)
    | (ActiveStock, Foundations i) -> moveFromActiveToFoundations (game, i)
    | (Tableau i, Foundations j) -> moveFromTableauToFoundations (game, i, j)
    | (Tableau i, Tableau j) -> game
    | (Foundations i, Tableau j) -> moveFromFoundationsToTableau (game, i, j)
    | (Foundations i, Foundations j) -> game
    | _, _ -> game
