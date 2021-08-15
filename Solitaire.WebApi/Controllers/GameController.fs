namespace Solitaire.WebApi.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open GameService
open System
open GameMoveWeb
open MoveHandler

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

    [<HttpPut("{id}")>]
    member _.Move(id: Guid, moveInfo: MoveWeb) =

        let srcCardArea =
            match (moveInfo.Source, moveInfo.SourceIndex) with
            | TableauWeb, Some (index) -> Tableau(index)
            | FoundationWeb, Some (index) -> Foundations(index)
            | ActiveStockWeb, _ -> ActiveStock
            | _ -> raise (ArgumentException("Invalid source card area specified"))

        let destCardArea =
            match (moveInfo.Destination, moveInfo.DestinationIndex) with
            | TableauWeb, Some (index) -> Tableau(index)
            | FoundationWeb, Some (index) -> Foundations(index)
            | ActiveStockWeb, _ -> ActiveStock
            | _ -> raise (ArgumentException("Invalid dest card area specified"))

        gameService.Move(id, srcCardArea, destCardArea)
