module GameService

open MoveHandler
open Game
open Solitaire.Infrastructure.Repositories
open System

type GameService(gameRepository: IGameRepository) =

    member this.Create() : Async<Game> =
        async {
            let newGame = GameDealer.dealGame ()
            let newGameDbo = GameDboMapper.mapGameToGameDbo (newGame)

            let! persistedGameDbo =
                gameRepository.CreateGame(newGameDbo)
                |> Async.AwaitTask

            return GameDboMapper.mapGameDboToGame (persistedGameDbo)
        }

    member this.GetAllGames() : Async<GameOverview list> =
        async {
            let! games = gameRepository.GetGames() |> Async.AwaitTask

            return
                games
                |> Seq.toList
                |> List.map (GameDboMapper.mapGameOverviewDboToGame)
        }

    member this.GetGameById(id: Guid) : Async<Game> =
        async {
            let! game = gameRepository.GetGameById(id) |> Async.AwaitTask
            return GameDboMapper.mapGameDboToGame (game)
        }

    member _.Move(id: Guid, source: CardArea, destination: CardArea) =
        async {
            let game =
                gameRepository.GetGameById(id)
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> GameDboMapper.mapGameDboToGame

            let updatedGame =
                handleMove (game, source, destination)
                |> GameDboMapper.mapGameToGameDbo

            return
                gameRepository.UpdateGame(updatedGame)
                |> Async.AwaitTask
                |> Async.RunSynchronously
        }
