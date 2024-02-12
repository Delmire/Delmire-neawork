using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACribbage
{
    class Player
    {
        readonly Random rng = new Random();
        List<Card> Cards = new List<Card>();

        public Player(List<Card> cards)
        {
            Cards = cards;
        }

        public void Take(List<Card> Dealt)
        {
            Cards = Dealt;
        }
        
        public Card Place(int CardNum)
        {
            return Cards[CardNum];
        }

        public List<Card> Showhand()
        {
            return Cards;
        }
        
    }
}