﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlayingCards;
using PokerDeck;

namespace PokerSim
{
    public static class EnumHelper
    {
        #region Public Method
 
        // This extension method is broken out so you can use a similar pattern with 
        // other MetaData elements in the future. This is your base method for each.
        //In short this is generic method to get any type of attribute.
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes.FirstOrDefault();//attributes.Length > 0 ? (T)attributes[0] : null;
        }
 
        // This method creates a specific call to the above method, requesting the
        // Display MetaData attribute.
        //e.g. [Display(Name = "Sunday")]
        public static string ToDisplayName(this Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();
            return attribute == null ? value.ToString() : attribute.Name;
        }
 
        // This method creates a specific call to the above method, requesting the
        // Description MetaData attribute.
        //e.g. [Description("Day of week. Sunday")]
        public static string ToDescription(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
 
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();

            Console.WriteLine("Poker Simulator");
            deck.Shuffle();

            var pokerHand = new PokerHand();
            
            for (int i = 1; i <= 5; i++)
            {
                pokerHand.cards.Add(deck.Deal());
            }
            
            var handResult = pokerHand.Evaluate();

            foreach(var card in pokerHand.cards)
            {
                Console.WriteLine(card.ShowCard());
            }
            Console.WriteLine("================");
            Console.WriteLine(handResult.ToDisplayName());
        }
    }
}
