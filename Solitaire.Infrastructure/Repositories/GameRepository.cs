using Solitaire.Infrastructure.Models;

namespace Solitaire.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        public GameDbo CreateGame(GameDbo game)
        {
            return game;
        }

        public GameDbo UpdateGame(GameDbo game)
        {
            return game;
        }
    }
}
