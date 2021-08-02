using Solitaire.Api.Models;
using System.Linq;

namespace Solitaire.Api.Mappers
{
    public class GameMapper : IGameMapper
    {
        private CardSuitWeb GetCardSuitWebFromCard(Game.Suit suit)
        {

            if (suit.IsClubs)
            {
                return CardSuitWeb.CLUBS;
            }
            if (suit.IsDiamonds)
            {
                return CardSuitWeb.DIAMONDS;
            }
            if (suit.IsSpades)
            {
                return CardSuitWeb.SPADES;
            }
            if (suit.IsHearts)
            {
                return CardSuitWeb.HEARTS;
            }
            return CardSuitWeb.HEARTS;
        }

        private Game.Suit MapCardSuitWebToCardSuit(CardSuitWeb cardSuiteWeb)
        {
            switch (cardSuiteWeb)
            {
                case CardSuitWeb.DIAMONDS:
                    return Game.Suit.Diamonds;
                case CardSuitWeb.CLUBS:
                    return Game.Suit.Clubs;
                case CardSuitWeb.SPADES:
                    return Game.Suit.Spades;
                case CardSuitWeb.HEARTS:
                    return Game.Suit.Hearts;
                default:
                    return Game.Suit.Hearts;
            }
        }

        private CardWeb MapCardToCardWeb(Game.Card card)
        {
            return new CardWeb
            {
                Rank = card.Rank,
                Suit = GetCardSuitWebFromCard(card.Suit)
            };
        }

        private Game.Card MapCardWebToCard(CardWeb cardWeb)
        {
            return new Game.Card(cardWeb.Rank, MapCardSuitWebToCardSuit(cardWeb.Suit));
        }

        public GameWeb MapGameToGameWeb(Game.Game game)
        {
            return new GameWeb
            {
                Stock = game.Stock.ToList().Select(MapCardToCardWeb),
                Wastepile = game.Wastepile.ToList().Select(MapCardToCardWeb)
            };
        }

        public Game.Game MapGameWebToGame(GameWeb gameWeb)
        {
            throw new System.NotImplementedException();
        }
    }
}
