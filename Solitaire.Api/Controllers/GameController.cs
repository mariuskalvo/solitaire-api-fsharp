using Microsoft.AspNetCore.Mvc;
using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;
using Solitaire.Api.Mappers;
using Solitaire.Api.Models;
using System.Threading;
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
            var createdGame = FSharpAsync.RunSynchronously(
              _gameService.Create(),
              timeout: FSharpOption<int>.None,
              cancellationToken: FSharpOption<CancellationToken>.None
          );
            var newGameWeb = _gameMapper.MapGameToGameWeb(createdGame);
            return await Task.FromResult(newGameWeb);
        }

        [HttpGet]
        public async Task<GameWeb> GetGame()
        {
            var createdGame = FSharpAsync.RunSynchronously(
                _gameService.Create(),
                timeout: FSharpOption<int>.None,
                cancellationToken: FSharpOption<CancellationToken>.None
            );

            var newGameWeb = _gameMapper.MapGameToGameWeb(createdGame);
            return await Task.FromResult(newGameWeb);
        }
    }
}
