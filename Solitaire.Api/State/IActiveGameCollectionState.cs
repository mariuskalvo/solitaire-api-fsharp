using Solitaire.Api.Models;
using System.Collections.Generic;

namespace Solitaire.Api.State
{
    public interface IActiveGameCollectionState
    {
        IEnumerable<GameWeb> GetGames();
        void CreateGame(GameWeb game);
    }
}
