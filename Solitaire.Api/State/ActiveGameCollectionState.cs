using Solitaire.Api.Models;
using System.Collections.Generic;

namespace Solitaire.Api.State
{
    public class ActiveGameCollectionState : IActiveGameCollectionState
    {
        private readonly Dictionary<string, GameWeb> _gameState;

        public ActiveGameCollectionState()
        {
            _gameState = new Dictionary<string, GameWeb>();
        }

        public void CreateGame(GameWeb game)
        {
            var key = game.Id.ToString();
            _gameState.Add(key, game);
        }

        public IEnumerable<GameWeb> GetGames()
        {
            return _gameState.Values;
        }
    }
}
