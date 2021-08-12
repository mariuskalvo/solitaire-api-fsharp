using Solitaire.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solitaire.Infrastructure.Repositories
{
    public interface IGameRepository
    {
        Task<GameDbo> UpdateGame(GameDbo game);
        Task<GameDbo> CreateGame(GameDbo game);
        Task<List<GameOverviewDbo>> GetGames();
        Task<GameDbo> GetGameById(Guid id);
    }
}
