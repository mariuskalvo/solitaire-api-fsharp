module MoveHandler

open Game

type DestinationMove = { DestIndex: int }
type SourceDestinationMove = { SrcIndex: int; DestIndex: int }

type MoveCommand =
    | ActiveToTableau of DestinationMove
    | ActiveToFoundations of DestinationMove
    | TableauToFoundation of SourceDestinationMove
    | FoundationsToTableau of SourceDestinationMove

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

let private moveFromTableauToFoundations (game, srcIndex, destIndex) = game

let private moveFromFoundationsToTableau (game, srcIndex, destIndex) = game

let handleMove (game: Game, moveCommand: MoveCommand) : Game =
    match moveCommand with
    | ActiveToTableau s -> moveFromActiveToTableau (game, s.DestIndex)
    | ActiveToFoundations s -> moveFromActiveToFoundations (game, s.DestIndex)
    | TableauToFoundation s -> moveFromTableauToFoundations (game, s.SrcIndex, s.DestIndex)
    | FoundationsToTableau s -> moveFromFoundationsToTableau (game, s.SrcIndex, s.DestIndex)
