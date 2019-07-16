using System.Collections.Generic;
using System.Linq;
using PlayingCards;

namespace PokerHand
{
    public class PokerHand
    {
        public List<Card> cards;

        public PokerHand()
        {
            cards = new List<Card>();
        }

        public string Evaluate()
        {
            var handValue = "Rubbish";

            var groupedCards = cards
                                .GroupBy(u => u.Rank)
                                .ToDictionary(group => group.Key, group => group.Count());
            
            var quads = groupedCards.ContainsValue(4);
            var trips = groupedCards.ContainsValue(3);
            var pair = false;
            var twoPair = false;
            foreach (var item in groupedCards)
            {
                if (item.Value == 2)
                {
                    if (pair)
                    {
                        twoPair = true;
                    } 
                    else
                    {
                        pair = true;
                    }    
                }           
            }
            if (quads)
                handValue = "Four of a Kind";
            else if (trips && pair)
                handValue = "Full House";    
            else if (trips)
                handValue = "Three of a Kind";
            else if (twoPair)
                handValue = "Two Pair";
            else if (pair)
                handValue = "Pair";    
    
            return handValue;
        }
    }
}