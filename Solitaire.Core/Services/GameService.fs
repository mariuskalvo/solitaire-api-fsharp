module GameService

open MoveHandler
open Game
open Solitaire.Infrastructure.Repositories

type GameService(gameRepository: IGameRepository) =
    member this.Move(game: Game, source: CardArea, dest: CardArea) : Async<Game> =
        async {
            let updatedGame = handleMove(game, source, dest)
            let updatesGameDbo = GameDboMapper.mapGameToGameDbo(updatedGame)
        
            let updatedGameState = 
                gameRepository.UpdateGame(updatesGameDbo)
                |> Async.AwaitTask 
                |> Async.RunSynchronously
        
            return GameDboMapper.mapGameDboToGame(updatedGameState);
        }

    member this.Create() : Async<Game> =
        async {
            let newGame = GameDealer.dealGame()
            let newGameDbo = GameDboMapper.mapGameToGameDbo(newGame)
            let persistedGameDbo =
                gameRepository.CreateGame(newGameDbo)
                |> Async.AwaitTask
                |> Async.RunSynchronously
            return GameDboMapper.mapGameDboToGame(persistedGameDbo);
        }

    member this.GetAllGames() :Async<Game list> =
        async {
            let games = 
                gameRepository.GetGames()
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> Seq.toList

            return games |> List.map(fun g -> GameDboMapper.mapGameDboToGame(g))
        }