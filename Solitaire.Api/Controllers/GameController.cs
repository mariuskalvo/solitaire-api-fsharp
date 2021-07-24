using Microsoft.AspNetCore.Mvc;
using Solitaire.Api.Models;
using Solitaire.Api.State;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solitaire.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IActiveGameCollectionState _gameState;

        public GameController(IActiveGameCollectionState gameState)
        {
            _gameState = gameState;
        }

        [HttpPost]
        public async Task CreateGame()
        {
            var game = GameService.dealGame();
            await Task.Yield();
        }

        [HttpGet]
        public async Task<IEnumerable<GameWeb>> GetGames()
        {
            return await Task.FromResult(_gameState.GetGames());
        }
    }
}
