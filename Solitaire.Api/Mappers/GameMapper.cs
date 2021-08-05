using Microsoft.FSharp.Collections;
using Solitaire.Api.Models;
using System;
using System.Linq;
using static MoveHandler;

namespace Solitaire.Api.Mappers
{
    public class GameMapper : IGameMapper
    {
        private CardSuitWeb GetCardSuitWebFromCard(Game.Suit suit)
        {
            if (suit == Game.Suit.Hearts)
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
                Stock = game.Stock.ToList().Select(MapCardToCardWeb).ToList(),
                Wastepile = game.Wastepile.ToList().Select(MapCardToCardWeb).ToList(),
                Active = game.ActiveStock.ToList().Select(MapCardToCardWeb).ToList(),
                Foundations = game.Foundations.Select((column) => column.Select(MapCardToCardWeb).ToList()).ToList(),
                Tableau = game.Tableau.Select((column) => column.Select(MapCardToCardWeb).ToList()).ToList(),
            };
        }

        public Game.Game MapGameWebToGame(GameWeb gameWeb)
        {
            var stock = ListModule.OfSeq(gameWeb.Stock.Select(MapCardWebToCard));
            var active = ListModule.OfSeq(gameWeb.Active.Select(MapCardWebToCard));
            var wastepile = ListModule.OfSeq(gameWeb.Wastepile.Select(MapCardWebToCard));

            var tableau = ListModule.OfSeq(gameWeb.Tableau.Select(
                (column) => ListModule.OfSeq(column.Select(MapCardWebToCard))
            ));

            var foundations = ListModule.OfSeq(gameWeb.Tableau.Select(
                (column) => ListModule.OfSeq(column.Select(MapCardWebToCard))
             ));

            return new Game.Game(tableau, stock, active, wastepile, foundations);
        }

        public CardArea MapCardAreaWebToCardGame(CardAreaWeb cardAreaWeb)
        {
            return cardAreaWeb.CardArea switch
            {
                CardAreaTypeWeb.Tableau => CardArea.NewTableau(cardAreaWeb.Index),
                CardAreaTypeWeb.Foundation => CardArea.NewTableau(cardAreaWeb.Index),
                CardAreaTypeWeb.Active => CardArea.ActiveStock,
                _ => throw new ArgumentException("Invalid card area specified")
            };
        }
    }
}
