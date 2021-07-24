module MoveHandler

open Game

type DestinationMove = { DestIndex: int }
type SourceDestinationMove = { SrcIndex: int; DestIndex: int }

type MoveCommand =
    | StockToTableau of DestinationMove
    | StockToFoundations of DestinationMove
    | TableauToFoundation of SourceDestinationMove
    | FoundationsToTableau of SourceDestinationMove

let private moveFromStockToTableau (game: Game, tableauIndex: int) =
    match game.Stock with
    | [] -> game
    | _ ->
        let cardToMove = game.Stock.Head
        let newStock = game.Stock.Tail

        let newTableauColumn =
            cardToMove :: game.Tableau.[tableauIndex]

        let newTableau =
            ListUtils.replaceAt (game.Tableau, tableauIndex, newTableauColumn)

        { game with
              Tableau = newTableau
              Stock = newStock }


let private moveFromStockToFoundations (game, destIndex) = game

let private moveFromTableauToFoundations (game, srcIndex, destIndex) = game

let private moveFromFoundationsToTableau (game, srcIndex, destIndex) = game

let handleMove (game: Game, moveCommand: MoveCommand) : Game =
    match moveCommand with
    | StockToTableau s -> moveFromStockToTableau (game, s.DestIndex)
    | StockToFoundations s -> moveFromStockToFoundations (game, s.DestIndex)
    | TableauToFoundation s -> moveFromTableauToFoundations (game, s.SrcIndex, s.DestIndex)
    | FoundationsToTableau s -> moveFromFoundationsToTableau (game, s.SrcIndex, s.DestIndex)
