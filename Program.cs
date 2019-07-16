using System;
using System.Collections.Generic;
using System.Linq;
using PlayingCards;

namespace PokerSim
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();

            Console.WriteLine("Poker Simulator");
            deck.Shuffle();

            var hand = new List<Card>();
            hand.Add(deck.cards[0]);
            hand.Add(deck.cards[1]);
            hand.Add(deck.cards[2]);
            hand.Add(deck.cards[3]);
            hand.Add(deck.cards[4]);

            var groupedCards = hand
                    .GroupBy(u => u.Rank)
                    .Select(grp => grp.ToList())
                    .ToList();

            foreach (var group in groupedCards)
            {
                Console.WriteLine("Group: {0}",group.Count);
            }

            foreach(var card in hand)
            {
                Console.WriteLine(card.ShowCard());
            }
        }
    }
}
