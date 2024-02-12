using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACribbage
{
    class Deck
    {
        static Random rng = new Random();
        public List<Card> deck = new List<Card>();

        public Deck()
        {
            MakeDeck();
        }


        public void MakeDeck()
        {
            for(int i = 0; i<53; i++)
            {
                deck.Add(MakeCard(i));
            }
        }

        private static Card MakeCard(int cardNum)
        {
            int rank;
            string suit;
            Card NewCard = new Card(0, "s");
            {
                rank = cardNum % 13;
            }
            if (cardNum / 4 == 0)
            {
                suit = "S";
            }
            else if (cardNum / 4 == 1)
            {
                suit = "C";
            }
            else if (cardNum / 4 == 2)
            {
                suit = "H";
            }
            else
            {
                suit = "D";
            }
            NewCard.SetRank(rank);
            NewCard.SetSuit(suit);
            return NewCard;
        }

        public void Shuffle()
        {
            for (int i = 0; i < 1000; i++)
            {
                int a;
                Card A;
                int b;
                Card B;
                a = rng.Next(1, 52);
                A = deck[a];
                b = rng.Next(1, 52);
                B = deck[b];
                deck[a] = B;
                deck[b] = A;
            }
        }

        public Card ShowTopCard()
        {
            return deck[deck.Count - 1];
        }
    }
}
