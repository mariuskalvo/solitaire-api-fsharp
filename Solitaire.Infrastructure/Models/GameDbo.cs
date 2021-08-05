using System;
using System.Collections.Generic;

namespace Solitaire.Infrastructure.Models
{
    public class GameDbo
    {
        public Guid id { get; set; }
        public List<CardDbo> Stock { get; set; }
        public List<CardDbo> Active { get; set; }
        public List<CardDbo> Wastepile { get; set; }
        public List<List<CardDbo>> Tableau { get; set; }
        public List<List<CardDbo>> Foundations { get; set; }
        public GameDbo() { }
        public GameDbo(List<CardDbo> stock, List<CardDbo> active, List<CardDbo> wastepile, List<List<CardDbo>> tableau, List<List<CardDbo>> foundations)
        {
            Stock = stock;
            Active = active;
            Wastepile = wastepile;
            Tableau = tableau;
            Foundations = foundations;
        }
    }
}
