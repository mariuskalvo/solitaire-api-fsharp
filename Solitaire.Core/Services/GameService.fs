module GameService

open MoveHandler
open Game
open Solitaire.Infrastructure.Repositories

type GameService(gameRepository: IGameRepository) =
    member this.Move(game: Game, source: CardArea, dest: CardArea) : Game =
        let updatedGame = handleMove(game, source, dest)
        
        let updatesGameDbo = GameDboMapper.mapGameToGameDbo(updatedGame)
        let persistedGameDbo = gameRepository.UpdateGame(updatesGameDbo);
        GameDboMapper.mapGameDboToGame(persistedGameDbo);

    member this.Create() : Game =
        let newGame = GameDealer.dealGame()
        let newGameDbo = GameDboMapper.mapGameToGameDbo(newGame)
        let persistedGameDbo = gameRepository.UpdateGame(newGameDbo);
        GameDboMapper.mapGameDboToGame(persistedGameDbo);