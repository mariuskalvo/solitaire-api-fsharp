using Solitaire.Api.Models;
using System;
using System.Collections.Generic;

namespace Solitaire.Api.State
{
    public class ActiveGameCollectionState : IActiveGameCollectionState
    {
        private readonly Dictionary<string, Game.Game> _gameState;

        public ActiveGameCollectionState()
        {
            _gameState = new Dictionary<string, Game.Game>();
        }

        public void CreateGame(Game.Game game)
        {
            var key = Guid.NewGuid().ToString();
            _gameState.Add(key, game);
        }

        public IEnumerable<Game.Game> GetGames()
        {
            return _gameState.Values;
        }
    }
}
