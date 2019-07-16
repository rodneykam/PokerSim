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
            pokerHand.cards.Add(new Card(5,2));
            pokerHand.cards.Add(new Card(2,2));
            pokerHand.cards.Add(new Card(4,2));
            pokerHand.cards.Add(new Card(3,2));
            pokerHand.cards.Add(new Card(6,2));

            var handResults = pokerHand.Evaluate();

            foreach(var card in pokerHand.cards)
            {
                Console.WriteLine(card.ShowCard());
            }
            Console.WriteLine("================");
            Console.WriteLine(handResults);
        }
    }
}
