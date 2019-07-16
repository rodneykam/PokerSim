using System.Collections.Generic;
using System.Linq;
using PlayingCards;

namespace PokerHand
{
    public class PokerHand
    {
        public List<Card> cards;

        public Card.enumRank Ace { get; private set; }

        public PokerHand()
        {
            cards = new List<Card>();
        }
        class RankCardCompare : IComparer<Card> 
        { 
            public int Compare(Card c1, Card c2) 
            { 
                
                if (c1 == null || c2 == null) 
                { 
                    return 0; 
                } 
                
                // "CompareTo()" method 
                return c1.CompareTo(c2); 
                
            } 
        }

        public string Evaluate()
        {
            var handValue = "Rubbish";

            cards.Sort();

            var groupedRank = cards
                                .GroupBy(u => u.Rank)
                                .OrderBy(k => k.Key)
                                .ToDictionary(group => group.Key, group => group.Count());
            
            var quads = groupedRank.ContainsValue(4);
            var trips = groupedRank.ContainsValue(3);
            var pair = groupedRank.ContainsValue(2);
            var pairs = groupedRank.Where( g => g.Value == 2)
                  .Select( g => g.Key);

            var groupedSuit = cards
                                .GroupBy(u => u.Suit)
                                .ToDictionary(group => group.Key, group => group.Count());

            var flush = groupedSuit.ContainsValue(5);
            var sum = ((int)cards[4].Rank - (int)cards[0].Rank);
            var straight = (((int)cards[4].Rank - (int)cards[0].Rank) == 4) ? true : false;
            // Check for a wheel Straight
            if (cards[4].Rank == Card.enumRank.Ace &&
                (((int)cards[3].Rank - (int)cards[0].Rank) == 3))
            {
                straight = true;
            }

            if (straight && flush && cards[4].Rank == Card.enumRank.Ace)
                handValue = "Royal Flush";
            else if (straight && flush)
                handValue = "Straight Flush";
            else if (quads)
                handValue = "Four of a Kind";
            else if (trips && pair)
                handValue = "Full House";    
            else if (flush)
                handValue = "Flush";
            else if (straight)
                handValue = "Straight";
            else if (trips)
                handValue = "Three of a Kind";
            else if (pairs.Count() > 1)
                handValue = "Two Pair";
            else if (pair)
                handValue = "Pair";    
    
            return handValue;
        }
    }
}