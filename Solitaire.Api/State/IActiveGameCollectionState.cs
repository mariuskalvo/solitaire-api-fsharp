using Solitaire.Api.Models;
using System.Collections.Generic;

namespace Solitaire.Api.State
{
    public interface IActiveGameCollectionState
    {
        IEnumerable<Game.Game> GetGames();
        void CreateGame(Game.Game game);
    }
}
