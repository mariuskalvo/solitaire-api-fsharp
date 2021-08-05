using System;

namespace Solitaire.Api.Models
{
    public enum CardAreaTypeWeb
    {
        Tableau,
        Foundation,
        Active
    }

    public class CardAreaWeb
    {
        public CardAreaTypeWeb CardArea { get; set; }
        public int Index { get; set; }
    }

    public class MoveParams
    {
        public Guid GameId { get; set; }
        public CardAreaWeb Source { get; set; }
        public CardAreaWeb Destination { get; set; }
    }
}
