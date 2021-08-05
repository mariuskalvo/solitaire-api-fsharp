using Microsoft.AspNetCore.Mvc;
using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;
using Solitaire.Api.Mappers;
using Solitaire.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static MoveHandler;

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
        public GameWeb CreateGame()
        {
            var createdGame = FSharpAsync.RunSynchronously(
              _gameService.Create(),
              timeout: FSharpOption<int>.None,
              cancellationToken: FSharpOption<CancellationToken>.None
          );
            var newGameWeb = _gameMapper.MapGameToGameWeb(createdGame);
            return newGameWeb;
        }

        [HttpGet]
        public List<GameWeb> GetGames()
        {
            var createdGame = FSharpAsync.RunSynchronously(
                _gameService.GetAllGames(),
                timeout: FSharpOption<int>.None,
                cancellationToken: FSharpOption<CancellationToken>.None);

            return createdGame.Select(_gameMapper.MapGameToGameWeb).ToList();
        }


        [HttpPost]
        public GameWeb Move([FromBody] MoveParams moveParams)
        {
            var source = _gameMapper.MapCardAreaWebToCardGame(moveParams.Source);
            var destination = _gameMapper.MapCardAreaWebToCardGame(moveParams.Destination);

            var game = FSharpAsync.RunSynchronously(
                _gameService.GetGameById(moveParams.GameId),
                timeout: FSharpOption<int>.None,
                cancellationToken: FSharpOption<CancellationToken>.None);

            var updatedGame = FSharpAsync.RunSynchronously(
                _gameService.Move(game, source, destination),
                timeout: FSharpOption<int>.None,
                cancellationToken: FSharpOption<CancellationToken>.None);

            return _gameMapper.MapGameToGameWeb(updatedGame);
        }
    }
}
