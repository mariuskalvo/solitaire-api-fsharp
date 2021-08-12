namespace Solitaire.WebApi.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open GameService
open System

[<ApiController>]
[<Route("[controller]")>]
type GameController(logger: ILogger<GameController>, gameService: GameService) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() = gameService.GetAllGames()

    [<HttpGet("{id}")>]
    member _.GetById(id: Guid) =
        gameService.GetGameById(id)
        |> Async.RunSynchronously
        |> GameMapper.mapGameToGameWeb

    [<HttpPost>]
    member _.CreateGame() =
        gameService.Create()
        |> Async.RunSynchronously
        |> GameMapper.mapGameToGameWeb


