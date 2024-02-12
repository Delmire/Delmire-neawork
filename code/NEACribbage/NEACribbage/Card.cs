using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACribbage
{
    class Card
    {
        private int Rank;
        private string Suit;

        public Card(int rank, string suit)
        {
            Rank = rank;
            Suit = suit;
        }
        public void SetRank(int NewRank)
        {
            Rank = NewRank;
        }
        public void SetSuit(string NewSuit)
        {
            Suit = NewSuit;
        }
        public int GetRank()
        {
            return Rank;
        }
        public string GetSuit()
        {
            return Suit;
        }
    }
}
