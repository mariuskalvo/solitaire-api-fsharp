namespace Solitaire.Api.Models
{
    public enum CardSuitWeb
    {
        DIAMONDS,
        CLUBS,
        SPADES,
        HEARTS
    }

    public class CardWeb
    {
        public int Rank { get; set; }
        public CardSuitWeb Suit { get; set; }
    }
}
