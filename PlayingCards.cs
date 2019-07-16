using System.Collections.Generic;

namespace PlayingCards
{
    public class Card
    {
        public enum enumRank {Two,Three,Four,Five,Six,Seven,Eight,Nine,Ten,Jack,Queen,King,Ace};
        public enum enumSuit {Spades,Hearts,Clubs,Diamonds};

        public enumRank Rank { get; set; }
        public enumSuit Suit { get; set; }

        public Card(int iRank, int iSuit)
        {
            Rank = (enumRank)iRank;
            Suit = (enumSuit)iSuit;
        }

        public string ShowCard()
        {
            var cardText = "";

            cardText = string.Format("{0} of {1}", Rank.ToString(), Suit.ToString());
            return cardText;
        }
    }
 
    public class Deck
    {
        public List<Card> cards;
        
        public void Shuffle()
        {
            var rand = new System.Random();

            for (int n = cards.Count - 1; n > 0; --n)
            {
                int k = rand.Next(n+1);
                var temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }

        }
        public Deck()
        {
            cards = new List<Card>();

            for (int i = 0; i < 52; i++)
            {
                cards.Add(new Card(i%13,i%4));
            }
        }        
    } 
}