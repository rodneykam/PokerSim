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

            var pokerHand = new PokerHand.PokerHand();
            pokerHand.cards.Add(deck.cards[0]);
            pokerHand.cards.Add(deck.cards[1]);
            pokerHand.cards.Add(deck.cards[2]);
            pokerHand.cards.Add(deck.cards[3]);
            pokerHand.cards.Add(deck.cards[4]);

            var handResults = pokerHand.Evaluate();

            foreach(var card in pokerHand.cards)
            {
                Console.WriteLine(card.ShowCard());
            }
            Console.WriteLine(handResults);
        }
    }
}
