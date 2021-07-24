module MoveHandler
open Game

type DestinationMove = { destIndex: int }
type SourceDestinationMove = { srcIndex: int; destIndex: int }

type MoveCommand = 
    | StockToTableau of DestinationMove
    | StockToFoundations of DestinationMove
    | TableauToFoundation of SourceDestinationMove
    | FoundationsToTableau of SourceDestinationMove

let private moveFromStockToTableau(game: Game, tableauIndex: int) =
    if game.Stock.IsEmpty then
        game
    else
        let cardToMove = game.Stock.Head
        let newStock = game.Stock.Tail

        let newTableauColumn = cardToMove :: game.Tableau.[tableauIndex]
        let newTableau = ListUtils.replaceAt(game.Tableau, tableauIndex, newTableauColumn)

        {
            game with
                Tableau = newTableau;
                Stock = newStock;
        }

let private moveFromStockToFoundations(game, destIndex) =
    game

let private moveFromTableauToFoundations(game, srcIndex, destIndex) =
    game

let private moveFromFoundationsToTableau(game, srcIndex, destIndex) =
    game

let handleMove(game: Game, moveCommand: MoveCommand): Game =
    match moveCommand with
    | StockToTableau s -> moveFromStockToTableau(game, s.destIndex)
    | StockToFoundations s -> moveFromStockToFoundations(game, s.destIndex)
    | TableauToFoundation s -> moveFromTableauToFoundations(game, s.srcIndex, s.destIndex)
    | FoundationsToTableau s -> moveFromFoundationsToTableau(game, s.srcIndex, s.destIndex)