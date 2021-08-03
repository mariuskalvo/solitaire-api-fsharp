using Solitaire.Infrastructure.Models;

namespace Solitaire.Infrastructure.Repositories
{
    public interface IGameRepository
    {
        GameDbo UpdateGame(GameDbo game);
        GameDbo CreateGame(GameDbo game);
    }
}
