module GameService

open MoveHandler
open Game
open Solitaire.Infrastructure.Repositories

type GameService(gameRepository: IGameRepository) =
    member this.Move(game: Game, source: CardArea, dest: CardArea) : Game =
        let value = gameRepository.getValue();
        handleMove(game, source, dest)