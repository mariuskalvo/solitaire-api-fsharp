﻿using Dapper;
using Newtonsoft.Json;
using Npgsql;
using Solitaire.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Solitaire.Infrastructure.Repositories
{

    class InsertGameDto
    {
        [Column(TypeName = "json")]
        public string State { get; set; }

    }

    class GameDboDto
    {
        public Guid Id { get; set; }
        [Column(TypeName = "json")]
        public string State { get; set; }
    }


    public class GameRepository : IGameRepository
    {
        const string connectionString = "Server=localhost;Port=5432;Database=solitaire;User ID=postgres;Password=12345;";
        public async Task<GameDbo> CreateGame(GameDbo game)
        {
            using (var dbConn = new NpgsqlConnection(connectionString)) {

                var id = await dbConn.ExecuteScalarAsync<Guid>("INSERT INTO game (state) VALUES (CAST(@State AS json)) RETURNING id", new 
                {
                    State = JsonConvert.SerializeObject(game)
                });
            }
            return game;
        }

        public async Task<GameDbo> GetGameById(Guid id)
        {
            using (var dbConnection = new NpgsqlConnection(connectionString))
            {
                var sql = "SELECT p.id as Id, p.state as State, p.created_at as CreatedAt FROM game p WHERE p.id = @Id";
                var gameDbo = await dbConnection.QuerySingleAsync<GameDboDto>(sql, new {
                    Id = id
                });

                var game = JsonConvert.DeserializeObject<GameDbo>(gameDbo.State);
                game.id = id;

                return game;
            }
        }

        public async Task<List<GameOverviewDbo>> GetGames()
        {
            using (var dbConnection = new NpgsqlConnection(connectionString))
            {
                var sql = "SELECT p.id as Id, p.created_at as CreatedAt FROM game p";
                var polls = await dbConnection.QueryAsync<GameOverviewDbo>(sql);
                return polls.ToList();
            }
        }

        public async Task<GameDbo> UpdateGame(GameDbo game)
        {
            return game;
        }
    }
}
