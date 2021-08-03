namespace Solitaire.Infrastructure.Models
{
    public class CardDbo
    {

        public SuitDbo Suit { get; set; }
        public int Rank { get; set; }

        public CardDbo() { }
        public CardDbo(SuitDbo suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}
