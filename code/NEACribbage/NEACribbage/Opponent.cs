using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACribbage
{
    class Opponent
    {
        private int difficulty;
        Random rng = new Random();
        List<Card> Cards = new List<Card>();
        List<Card> Hand = new List<Card>();
        List<Card> Placed = new List<Card>();
        bool Crib;
        Probability move;
        public Opponent(int difficulty, bool crib)
        {
            Crib = crib;
        }

        public void triggerStats()
        {
            move = new Probability(Hand, Crib);
        }

        public void SetPlaced(List<Card> placed)
        {
            placed = Placed;
        }

        public void sethand(List<Card> hand)
        {
            Hand = hand;
            move.SetHand(Hand);
        }

        public void SetFirstHand(List<Card> hand)
        {
            Hand = hand;
        }

        public void setDifficulty(int Difficulty)
        {
            difficulty = Difficulty;
        }

        public void WasPlaced(Card CardPlaced)
        {
            Placed.Add(CardPlaced);
        }

        public List<Card> Discard(List<Card> Hand, bool Crib)
        {
            int x = 0;
            int y = 0;
            List<Card> discard = new List<Card>();
            
            if (difficulty == 1)
            {
                discard = move.BestDiscard;
            }
            else if(difficulty == 2)
            {
                discard = move.TenDiscard;
            }
            else
            {
                x = rng.Next(0, 5);
                y = rng.Next(0, 5);
                while (x==y)
                {
                    y = rng.Next(0, 5);
                }
                discard.Add(Hand[x]);
                discard.Add(Hand[y]);
            }
            return Hand;
        }

        public List<Card> Showhand()
        {
            return Cards;
        }

        public void Take(List<Card> Dealt)
        {
            Cards = Dealt;
        }

        private Card PlaceBest()
        {
            List<int> HandRanks = new List<int>();
            for(int i = 0; i<Hand.Count; i++)
            {
                HandRanks.Add(Hand[i].GetRank());
            }
            return Hand[move.PlaceBest(HandRanks, Placed)];
        }

        public Card Place()
        {
            List<int> HandRanks = new List<int>();
            List<Card> TempHand = new List<Card>();

            Card C1;
            if(difficulty == 1)
            {
                C1 = Hand[rng.Next(0, Hand.Count)];
            }
            else if(difficulty == 2)
            { 
                for (int i = 0; i < Hand.Count; i++)
                {
                    HandRanks.Add(Hand[i].GetRank());
                }
                C1 = Hand[move.PlaceBestTen(HandRanks, Placed)];
            }
            else
            {
                C1 = Hand[move.PlaceBest(HandRanks, Placed)];
            }
            for(int i = 0; i<Hand.Count; i++)
            {
                if(C1 != Hand[i])
                {
                    TempHand.Add(Hand[i]);
                }
            }
            Hand = TempHand;
            return C1;
        }
    }
}