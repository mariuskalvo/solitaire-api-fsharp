module MoveHandler

open Game
open ListUtils

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
            replaceAt tableauIndex newTableauColumn game.Tableau

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
            replaceAt destIndex newDestFoundation game.Foundations

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
            replaceAt destIndex newFoundation game.Foundations

        let newTableau = replaceAt srcIndex tabTail game.Tableau

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
            replaceAt destIndex fTail game.Foundations

        let newTableaus =
            replaceAt srcIndex newTableau game.Tableau

        { game with
              Foundations = newFoundations
              Tableau = newTableaus }


let private moveFromTableauToTableau (game: Game, srcIndex: int, destIndex: int) : Game =
    let srcColumn = game.Tableau.[srcIndex]
    let destColumn = game.Tableau.[destIndex]

    match srcColumn with
    | [] -> game
    | tHead :: tTail ->
        let newDestColumn = tHead :: destColumn

        let newTableau =
            game.Tableau
            |> replaceAt srcIndex tTail
            |> replaceAt destIndex newDestColumn

        { game with Tableau = newTableau }

let private moveFromFoundationToFoundation (game: Game, srcIndex: int, destIndex: int) : Game =
    let srcColumn = game.Foundations.[srcIndex]
    let destColumn = game.Foundations.[destIndex]

    match srcColumn with
    | [] -> game
    | fHead :: tTail ->
        let newDestColumn = fHead :: destColumn

        let newTableau =
            game.Foundations
            |> replaceAt srcIndex tTail
            |> replaceAt destIndex newDestColumn

        { game with Foundations = newTableau }


let handleMove (game: Game, source: CardArea, dest: CardArea) : Game =
    match (source, dest) with
    | (ActiveStock, Tableau i) -> moveFromActiveToTableau (game, i)
    | (ActiveStock, Foundations i) -> moveFromActiveToFoundations (game, i)
    | (Tableau i, Foundations j) -> moveFromTableauToFoundations (game, i, j)
    | (Tableau i, Tableau j) -> moveFromTableauToTableau (game, i, j)
    | (Foundations i, Tableau j) -> moveFromFoundationsToTableau (game, i, j)
    | (Foundations i, Foundations j) -> moveFromFoundationToFoundation (game, i, j)
    | _, _ -> game
