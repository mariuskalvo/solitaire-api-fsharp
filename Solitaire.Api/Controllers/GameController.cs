using Microsoft.AspNetCore.Mvc;
using Solitaire.Api.Mappers;
using Solitaire.Api.Models;
using System.Threading.Tasks;

namespace Solitaire.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private GameService.GameService _gameService;
        private IGameMapper _gameMapper;

        public GameController(GameService.GameService gameService, IGameMapper gameMapper)
        {
            _gameService = gameService;
            _gameMapper = gameMapper;
        }

        [HttpPost]
        public async Task<GameWeb> CreateGame()
        {
            var newGame = _gameService.Create();
            var newGameWeb = _gameMapper.MapGameToGameWeb(newGame);
            return await Task.FromResult(newGameWeb);
        }

        [HttpGet]
        public async Task<GameWeb> GetGame()
        {
            var newGame = _gameService.Create();
            var newGameWeb = _gameMapper.MapGameToGameWeb(newGame);
            return await Task.FromResult(newGameWeb);
        }
    }
}
