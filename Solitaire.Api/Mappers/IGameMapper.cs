using Solitaire.Api.Models;
using static MoveHandler;

namespace Solitaire.Api.Mappers
{
    public interface IGameMapper
    {
        Game.Game MapGameWebToGame(GameWeb gameWeb);
        GameWeb MapGameToGameWeb(Game.Game game);
        CardArea MapCardAreaWebToCardGame(CardAreaWeb cardArea);

    }
}
