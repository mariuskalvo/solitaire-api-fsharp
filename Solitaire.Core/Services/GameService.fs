module GameService

open MoveHandler
open Game
open Solitaire.Infrastructure.Repositories
open System

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
            let! persistedGameDbo =
                gameRepository.CreateGame(newGameDbo)
                |> Async.AwaitTask
            return GameDboMapper.mapGameDboToGame(persistedGameDbo);
        }

    member this.GetAllGames() :Async<GameOverview list> =
        async {
            let! games = 
                gameRepository.GetGames()
                |> Async.AwaitTask

            return 
                games 
                |> Seq.toList 
                |> List.map(GameDboMapper.mapGameOverviewDboToGame)
        }

    member this.GetGameById(id: Guid) :Async<Game> =
        async {
            let! game = 
                gameRepository.GetGameById(id)
                |> Async.AwaitTask
            return GameDboMapper.mapGameDboToGame(game)
        }