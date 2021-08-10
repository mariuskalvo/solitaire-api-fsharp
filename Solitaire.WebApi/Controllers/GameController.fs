namespace Solitaire.WebApi.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open GameService

[<ApiController>]
[<Route("[controller]")>]
type GameController (logger : ILogger<GameController>, gameService : GameService) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() =
        gameService.GetAllGames()
