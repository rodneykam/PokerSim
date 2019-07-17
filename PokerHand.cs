using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using PlayingCards;

namespace PokerDeck
{
    
    public class PokerHand
    {
        public List<Card> cards;

        public enum PokerRank {
            [Display(Name = "Rubbish")]
            Nothing,
            [Display(Name = "Pair")]
            Pair,
            [Display(Name = "Two Pair")]
            TwoPair,
            [Display(Name = "Three of a Kind")]
            Set,
            [Display(Name = "Straight")]
            Straight,
            [Display(Name = "Flush")]
            Flush,
            [Display(Name = "Full House")]
            Boat,
            [Display(Name = "Four of a Kind")]
            Quads,
            [Display(Name = "Straight Flush")]
            StraightFlush,
            [Display(Name = "Royal Flush")]
            Royal
        }

        public PokerRank handRank { get; set; }

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

        public PokerRank Evaluate()
        {
            handRank = PokerRank.Nothing;
            
            if (cards.Count() < 5)
                return handRank;

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
            if (cards[4].Rank == Card.enumRank.Ace 
                    && cards[0].Rank == Card.enumRank.Two
                    && cards[0].Rank == Card.enumRank.Three
                    && cards[0].Rank == Card.enumRank.Four
                    && cards[0].Rank == Card.enumRank.Five)
            {
                straight = true;
            }

            if (straight && flush && cards[4].Rank == Card.enumRank.Ace)
                handRank = PokerRank.Royal;
            else if (straight && flush)
                handRank = PokerRank.StraightFlush;
            else if (quads)
               handRank = PokerRank.Quads;
            else if (trips && pair)
                handRank = PokerRank.Boat;    
            else if (flush)
                handRank = PokerRank.Flush;
            else if (straight)
                handRank = PokerRank.Straight;
            else if (trips)
                handRank = PokerRank.Set;
            else if (pairs.Count() > 1)
                handRank = PokerRank.TwoPair;
            else if (pair)
                handRank = PokerRank.Pair;    

            return handRank;
        }

        public void Show()
        {
            foreach (var card in cards)
            {
                Console.WriteLine("{0} of {1}", card.Rank, card.Suit);
            }
        }
    }
}