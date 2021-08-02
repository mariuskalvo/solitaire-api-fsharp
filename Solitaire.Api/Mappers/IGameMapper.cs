using Solitaire.Api.Models;

namespace Solitaire.Api.Mappers
{
    public interface IGameMapper
    {
        Game.Game MapGameWebToGame(GameWeb gameWeb);
        GameWeb MapGameToGameWeb(Game.Game game);

    }
}
