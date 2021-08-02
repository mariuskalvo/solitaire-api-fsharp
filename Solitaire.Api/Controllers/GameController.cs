using Microsoft.AspNetCore.Mvc;
using Solitaire.Api.Models;
using Solitaire.Api.State;
using Solitaire.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solitaire.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IActiveGameCollectionState _gameState;
        private GameService.GameService _gameService;

        public GameController(IActiveGameCollectionState gameState, GameService.GameService gameService)
        {
            _gameState = gameState;
            _gameService = gameService;
        }

        [HttpPost]
        public async Task CreateGame()
        {
            var game = GameDealer.dealGame();
            _gameState.CreateGame(game);
            await Task.Yield();
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            var game = GameDealer.dealGame();
            var updatedGame = _gameService.Move(game, MoveHandler.CardArea.NewTableau(1), MoveHandler.CardArea.NewTableau(0));
            return  Ok();
        }
    }
}
