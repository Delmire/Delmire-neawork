using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACribbage
{
    class Probability
    {
        public Card NewCard = new Card(0, "S");
        public List<Card> BestDiscard = new List<Card>();
        public List<Card> TenDiscard = new List<Card>();
        private List<Card> Hand;
        private List<Card> Placed;
        private List<Card> DDeck = new List<Card>();
        List<Card> HHand;
        private bool crib;
        Deck TheDeck = new Deck();
        public Probability(List<Card> hand, bool Crib)
        {
            HHand = hand;
            crib = Crib;
        }

        public List<Card> GetBestDiscard()
        {
            BestDiscard = Best(DDeck, HHand, crib);
            return BestDiscard;
        }

        public List<Card> GetBestDiscardTen()
        {
            TenDiscard = BestTen(HHand);
            return BestDiscard;
        }

        public void SetHand(List<Card> NewHand)
        {
            Hand = NewHand;
            DDeck = ReadDeck(NewHand);
        }
        public void SetPlaced(List<Card> NewPlaced)
        {
            Placed = NewPlaced;
        }

        private static Card MakeCard(int cardNum)
        {
            int rank;
            string suit;
            Card NewCard = new Card(0,"s");
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
            else if (cardNum / 4 == 1)
            {
                suit = "D";
            }
            NewCard.SetRank(rank);

            return NewCard;
        }

        //The first section of this class will show the probability of the discard phase of the Card
        private List<Card> ReadDeck(List<Card> HHand)
        {
            List<Card> DDeck = new List<Card>();
            for (int i = 0; i < 48; i++)
            {
                if (MakeCard(i).GetRank() != HHand[0].GetRank() && MakeCard(i).GetSuit() != HHand[0].GetSuit())
                {
                    if (MakeCard(i).GetRank() != HHand[1].GetRank() && MakeCard(i).GetSuit() != HHand[1].GetSuit())
                    {
                        if (MakeCard(i).GetRank() != HHand[2].GetRank() && MakeCard(i).GetSuit() != HHand[2].GetSuit())
                        {
                            if (MakeCard(i).GetRank() != HHand[3].GetRank() && MakeCard(i).GetSuit() != HHand[3].GetSuit())
                            {
                                if (MakeCard(i).GetRank() != HHand[4].GetRank() && MakeCard(i).GetSuit() != HHand[4].GetSuit())
                                {
                                    if (MakeCard(i).GetRank() != HHand[5].GetRank() && MakeCard(i).GetSuit() != HHand[5].GetSuit())
                                    {
                                        DDeck.Add(MakeCard(i));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return DDeck;
        }

        //private List<Card> Best(List<Card> DDeck, List<Card> HHand, bool Crib)
        //{
        //    List<double> Scores = new List<double>();
        //    List<Card> best = new List<Card>();
        //    List<Card> Discard = new List<Card>();
        //    List<Card> Remaining = new List<Card>();
        //    best.Add(HHand[0]);
        //    best.Add(HHand[1]);
        //    double iterations;
        //    double ScoreFifteen;
        //    double ScoreSnap;
        //    double ScoreStraight;
        //    double Score = 0;
        //    int BestPosition = 1;
        //    int[] HandRanks = { 0, 0, 0, 0, 0 };
        //    //L[X] is short for layer x, such that it is how many of said layer there are initially
        //    int[] L = { 0, 0, 0, 0, 0 };
        //    int[] BrokenDeck = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //    int[] CheckerDeck = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //    //Used to keep track of how many of each number is in the calculated hand
        //    int[] CounterDeck = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //    //These will be used to establish how many variations of a hand can be made
        //    double[] ChoosesDeck = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        //    for (int i = 0; i < DDeck.Count; i++)
        //    {
        //        BrokenDeck[DDeck[i].GetRank() - 1] += 1;
        //    }
        //    for (int i = 0; i < 13; i++)
        //    {
        //        L[0] = BrokenDeck[i];
        //        CheckerDeck = BrokenDeck;
        //        if (CheckerDeck[i] > 0)
        //        {
        //            CheckerDeck[i] -= 1;
        //            for (int j = 0; j < 13; j++)
        //            {
        //                L[1] = BrokenDeck[j];
        //                if (CheckerDeck[j] > 0)
        //                {
        //                    CheckerDeck[j] -= 1;
        //                    for (int k = 0; k < 13; k++)
        //                    {
        //                        L[2] = BrokenDeck[k];
        //                        if (CheckerDeck[k] > 0)
        //                        {
        //                            CheckerDeck[k] -= 1;
        //                            for (int l = 0; l < 5; l++)
        //                            {
        //                                iterations = 0;
        //                                CheckerDeck[l] -= 1;
        //                                for (int m = 0; m < 6; m++)
        //                                {
        //                                    ScoreSnap = 0;
        //                                    ScoreStraight = 0;
        //                                    ScoreFifteen = 0;
        //                                    Discard.Add(HHand[l]);
        //                                    Discard.Add(HHand[m]);
        //                                    CounterDeck[i] += 1;
        //                                    CounterDeck[j] += 1;
        //                                    CounterDeck[k] += 1;
        //                                    HandRanks[0] = i + 1;
        //                                    HandRanks[1] = j + 1;
        //                                    HandRanks[2] = k + 1;
        //                                    HandRanks[3] = HHand[l].GetRank();
        //                                    HandRanks[4] = HHand[m].GetRank();
        //                                    for (int o = 0; o < 13; o++)
        //                                    {
        //                                        ChoosesDeck[o] = Choose(BrokenDeck[o], CounterDeck[o]);
        //                                    }
        //                                    if (i == j & i == k)
        //                                    {
        //                                        ScoreSnap += Snap(HandRanks) * ChoosesDeck[i];
        //                                        ScoreStraight += Straights(HandRanks) * ChoosesDeck[i];
        //                                        ScoreFifteen += Fifteens(HandRanks) * ChoosesDeck[i];
        //                                    }
        //                                    else if (i == j & j != k)
        //                                    {
        //                                        ScoreSnap += Snap(HandRanks) * ChoosesDeck[i] * ChoosesDeck[k];
        //                                        ScoreStraight += Straights(HandRanks) * ChoosesDeck[i] * ChoosesDeck[k];
        //                                        ScoreFifteen += Fifteens(HandRanks) * ChoosesDeck[i] * ChoosesDeck[k];
        //                                    }
        //                                    else if (i == k & j != i)
        //                                    {
        //                                        ScoreSnap += Snap(HandRanks) * ChoosesDeck[k] * ChoosesDeck[j];
        //                                        ScoreStraight += Straights(HandRanks) * ChoosesDeck[k] * ChoosesDeck[j];
        //                                        ScoreFifteen += Fifteens(HandRanks) * ChoosesDeck[k] * ChoosesDeck[j];
        //                                    }
        //                                    else if (j == k & k != i)
        //                                    {
        //                                        ScoreSnap += Snap(HandRanks) * ChoosesDeck[j] * ChoosesDeck[j];
        //                                        ScoreStraight += Straights(HandRanks) * ChoosesDeck[i] * ChoosesDeck[j];
        //                                        ScoreFifteen += Fifteens(HandRanks) * ChoosesDeck[i] * ChoosesDeck[j];
        //                                    }
        //                                    Score += ScoreSnap + ScoreStraight + ScoreFifteen;
        //                                    iterations += 1;
        //                                }
        //                                Scores.Add(Score / iterations);
        //                                if (Scores[BestPosition] <= Scores[Scores.Count - 1])
        //                                {
        //                                    BestPosition = Scores.Count - 1;
        //                                    best = Discard;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return best;
        //}

        private List<Card> Best(List<Card> DDeck, List<Card> HHand, bool Crib)
        {
            double ScoreFifteen = 0;
            double ScoreSnap = 0;
            double Score = 0;
            double ScoreStraight = 0;
            double ScoreCrib = 0;
            int[] RanksCrib = { 0, 0, 0, 0};
            int[] RanksHand = { 0, 0, 0, 0};
            List<Card> TheCards = new List<Card>();
            List<Card> RemainingCards = ReadDeck(HHand);
            List<Card> TestCrib = new List<Card>();
            List<Card> TestHand = new List<Card>();
            for (int i =0; i<5; i++)
            {
                for(int j = i; j<6; j++)
                {
                    Score = 0;
                    for (int iterator = 0; iterator < 6; iterator++)
                    {
                        if (HHand[i] != HHand[iterator])
                        {
                            if (HHand[j] != HHand[iterator])
                            {
                                TestHand[iterator] = HHand[iterator];
                            }
                        }
                    }
                    RanksHand[0] = TestHand[0].GetRank();
                    RanksHand[1] = TestHand[1].GetRank();
                    RanksHand[2] = TestHand[2].GetRank();
                    RanksHand[3] = TestHand[3].GetRank();
                    Score += Straights(RanksHand);
                    Score += Fifteens(RanksHand);
                    for (int k =0; k<RemainingCards.Count-2; k++)
                    {
                        for(int l =k; k<RemainingCards.Count-1; k++)
                        {
                            for (int m = l; m < RemainingCards.Count; m++)
                            {
                                Score = 0;
                                ScoreCrib = 0;
                                TestCrib.Add(HHand[i]);
                                TestCrib.Add(HHand[j]);
                                TestCrib.Add(RemainingCards[k]);
                                TestCrib.Add(RemainingCards[l]);
                                RanksCrib[0] = HHand[i].GetRank();
                                RanksCrib[1] = HHand[j].GetRank();
                                RanksCrib[2] = RemainingCards[k].GetRank();
                                RanksCrib[2] = RemainingCards[l].GetRank();
                                RanksCrib[2] = RemainingCards[m].GetRank();
                                ScoreSnap = Snap(RanksCrib);
                                ScoreStraight =  Straights(RanksCrib);
                                ScoreFifteen = Fifteens(RanksCrib);
                                ScoreCrib += NibsNobs(TestCrib, RemainingCards[m]);
                                ScoreCrib += ScoreSnap;
                                ScoreCrib += ScoreStraight;
                            }
                        }
                    }
                }
            }
            return HHand;
        }


        private int ScoreCribPotential(List<Card> CardNums)
        {
            int score = 0;
            List<Card> HHand = new List<Card>();
            if (CardNums[0] == CardNums[1])
            {
                if (CardNums[2] == CardNums[1])
                {
                    score += 6;
                }
                else
                {
                    score += 2;
                }
            }
            if (CardNums[0].GetRank() == CardNums[1].GetRank() - 1 && CardNums[1].GetRank() == CardNums[2].GetRank() - 1)
            {
                score += 3;
            }
            else if(CardNums[0].GetRank() == CardNums[2].GetRank() - 1 && CardNums[2].GetRank() == CardNums[1].GetRank() - 1)
            {
                score += 3;
            }
            else if (CardNums[1].GetRank() == CardNums[0].GetRank() - 1 && CardNums[0].GetRank() == CardNums[2].GetRank() - 1)
            {
                score += 3;
            }
            else if (CardNums[1].GetRank() == CardNums[2].GetRank() - 1 && CardNums[2].GetRank() == CardNums[0].GetRank() - 1)
            {
                score += 3;
            }
            else if (CardNums[2].GetRank() == CardNums[0].GetRank() - 1 && CardNums[0].GetRank() == CardNums[1].GetRank() - 1)
            {
                score += 3;
            }
            else if (CardNums[2].GetRank() == CardNums[1].GetRank() - 1 && CardNums[1].GetRank() == CardNums[0].GetRank() - 1)
            {
                score += 3;
            }
            return score;
        }

        private int NibsNobs(List<Card> Hand, Card TurnedCard)
        {
            for(int i =0; i<Hand.Count; i++)
            {
                if(Hand[i].GetRank() == 11)
                {
                    if(TurnedCard.GetSuit() == Hand[i].GetSuit())
                    {
                        return 1;
                    }
                }
            }
            if(TurnedCard.GetRank() == 11)
            {
                return 2;
            }
            return 0;
        }

        private List<Card> BestTen(List<Card> HHand)
        {
            double[] Scores = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[,] best = { { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 }, { 0, 5 }, { 1, 2 }, { 1, 3 }, { 1, 4 }, { 1, 5 }, { 2, 3 }, { 2, 4 }, { 2, 5 }, { 3, 4 }, { 3, 5 }, { 4, 5 } };
            int iterator = 0;
            int[] numbers = { 0, 0, 0, 0, 0 };
            List<Card> Discard = new List<Card>();
            int Best = 0
                ;
            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 6; j++)
                {
                    numbers[0] = HHand[i].GetRank();
                    numbers[1] = HHand[j].GetRank();
                    numbers[2] = 10;
                    numbers[3] = 10;
                    numbers[4] = 10;
                    Scores[iterator] = Snap(numbers);
                    Scores[iterator] += Fifteens(numbers);
                    Scores[iterator] = Straights(numbers);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                if (Scores[i] >= Scores[Best])
                {
                    Best = i;
                }
            }
            Discard.Add(HHand[best[Best, 0]]);
            Discard.Add(HHand[best[Best, 1]]);
            return Discard;
        }

        private double Choose(int N, int R)
        {
            int a = 1;
            int b = 1;
            int c = 1;
            for (int i = 1; i <= N; i++)
            {
                a = a * i;
            }
            for (int i = 1; i <= R; i++)
            {
                b = b * i;
            }
            for (int i = 1; i <= N - R; i++)
            {
                c = (N - R) * c;
            }
            return (Convert.ToDouble(a)) / (Convert.ToDouble(b) * Convert.ToDouble(c));
        }

        public double Fifteens(int[] Cards)
        {
            int score = 0;
            int[] Ranks = Cards;
            int total;
            string Numbers;
            for (int i = 0; i < 64; i++)
            {
                total = 0;
                Numbers = Convert.ToString(i, 2);
                for (int j = 0; j < 6; j++)
                {
                    if (Numbers[j] == '1')
                    {
                        total += Ranks[j];
                    }
                }
                if (total == 15)
                {
                    score += 2;
                }
            }
            return score;
        }
        public double Straights(int[] Cards)
        {
            int score = 0;
            int[] Totals = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Totals[Cards[0]] += 1;
            Totals[Cards[1]] += 1;
            Totals[Cards[2]] += 1;
            Totals[Cards[3]] += 1;
            Totals[Cards[4]] += 1;
            for (int i = 0; i <= 11; i++)
            {
                if (Totals[i] >= 1)
                {
                    if (Totals[i + 1] >= 1)
                    {
                        if (Totals[i + 2] >= 1)
                        {
                            if (Totals[i + 3] >= 1)
                            {
                                if (Totals[i + 4] >= 1)
                                {
                                    return 5;
                                }
                                else
                                {
                                    score = Totals[i] * Totals[i + 1] * Totals[i + 2] * Totals[i + 3];
                                    return score;
                                }
                            }
                            else
                            {
                                score = Totals[i] * Totals[i + 1] * Totals[i + 2];
                                return score;
                            }
                        }
                    }
                }
            }
            return 0;
        }


        public double Snap(int[] Cards)
        {
            int score = 0;
            int[] Totals = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Totals[Cards[0]] += 1;
            Totals[Cards[1]] += 1;
            Totals[Cards[2]] += 1;
            Totals[Cards[3]] += 1;
            Totals[Cards[4]] += 1;
            for (int i = 0; i < 13; i++)
            {
                if (Totals[i] == 2)
                {
                    score += 2;
                }
                else if (Totals[i] == 3)
                {
                    score += 6;
                }
                if (Totals[i] == 4)
                {
                    score += 12;
                }
            }
            return score;
        }
        //this part of probability is dedicated to the playing phase of the Card and which cards to place


        //shows the probability of any given card being placed next
        public int[] PlaceChances(List<Card> Placed, List<int> Hand)
        {
            int[] chances = { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            List<int> PlaceToInt = new List<int>();
            List<int> numbers = new List<int>();
            numbers = Hand;

            for (int i = 0; i < Placed.Count; i++)
            {
                PlaceToInt.Add(Placed[i].GetRank());
            }
            for (int i = 0; i < Placed.Count; i++)
            {
                numbers.Add(PlaceToInt[i]);
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                chances[numbers[i]] -= 1;
            }
            return chances;
        }

        //sees which card is best to place
        public int PlaceBest(List<int> hand, List<Card> Placed)
        {
            int total = 0;
            for (int i = 0; i < Placed.Count; i++)
            {
                total += Placed[i].GetRank();
            }
            int[] Average = { 0, 0, 0, 0 };
            List<Card> testing = Placed;
            List<int> scores = new List<int>();
            int[] Probabilities = PlaceChances(Placed, hand);
            int tempScore;
            int score;
            int best = 0;
            for(int i = 0; i<hand.Count-2;i++)
            {
                for(int j = 0; j<13; j++)
                {
                    if(Probabilities[j] >0)
                    {
                        for(int k =i + 1; k<hand.Count -1; k++)
                        {
                            testing = Placed;
                            testing.Add(MakeCard(hand[i]*13));
                            tempScore = ScorePlacedCard(Placed);
                            testing.Add(MakeCard(j * 13));
                            tempScore = tempScore - ScorePlacedCard(Placed);
                            testing.Add(MakeCard(k * 13));
                            tempScore += ScorePlacedCard(Placed);
                            Average[i] += tempScore *  Probabilities[j] * Probabilities[k];
                            Average[k] += tempScore *  Probabilities[j] * Probabilities[k];
                        }
                    }
                }
            }
            score = 0;
            for(int i = 0; i<4;i++)
            {
                if(Average[i] >score)
                {
                    score = Average[i];
                    best = i;
                }
            }
            return best;
        }


        public int PlaceBestTen(List<int> hand, List<Card> Placed)
        {
            int total = 0;
            for (int i = 0; i < Placed.Count; i++)
            {
                total += Placed[i].GetRank();
            }
            List<int> average = new List<int>();
            for(int i = 0; i< hand.Count; i++)
            {
                average.Add(0);
            }
            int[] Average;
            Average = average.ToArray();
            List<Card> testing = Placed;
            List<int> scores = new List<int>();
            int[] Probabilities = PlaceChances(Placed, hand);
            int tempScore;
            int score = 0;
            int best = 0;
            for (int i = 0; i < hand.Count - 2; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (Probabilities[j] > 0)
                    {
                        for (int k = i + 1; k < hand.Count - 1; k++)
                        {
                            testing = Placed;
                            testing.Add(MakeCard(hand[i] * 13));
                            tempScore = ScorePlacedCardTen(Placed);
                            testing.Add(MakeCard(10 * 13));
                            tempScore = tempScore - ScorePlacedCard(Placed);
                            testing.Add(MakeCard(10 * 13));
                            tempScore += ScorePlacedCardTen(Placed);
                            Average[i] += tempScore * Probabilities[j] * Probabilities[k];
                        }
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (Average[i] > score)
                {
                    score = Average[i];
                    best = i;
                }
            }
            return best;
        }


        //shows the score for the last placed card
        public int ScorePlacedCard(List<Card> Placed)
        {
            int total = 0;
            total += ScoreRoundSnap(Placed);
            ScoreRoudStraight(Placed);
            total += ScoreRoundTotals(Placed);
            return total;
        }
        public int ScorePlacedCardTen(List<Card> Placed)
        {
            int total = 0;
            total += ScoreRoundTotals(Placed);
            return total;
        }

        //shows if the total is 15 or 31
        private int ScoreRoundTotals(List<Card> Placed)
        {
            int total = 0;
            for(int i = 0; i< Placed.Count; i++)
            {
                total += Placed[i].GetRank();
            }
            if (total == 15)
            {
                return 2;
            }
            else if( total == 31)
            {
                return 2;
            }
            return 0;
        }


        //shows if there is a pair, tripple, or quadrouple
        private int ScoreRoundSnap(List<Card> Placed)
        {
            if (Placed[Placed.Count - 1].GetRank() == Placed[Placed.Count - 2].GetRank() && Placed[Placed.Count - 3].GetRank() == Placed[Placed.Count - 4].GetRank() && Placed[Placed.Count - 1].GetRank() == Placed[Placed.Count - 3].GetRank())
            {
                return 12;
            }
            else if (Placed[Placed.Count - 1].GetRank() == Placed[Placed.Count - 2].GetRank() && Placed[Placed.Count - 2].GetRank() == Placed[Placed.Count - 3].GetRank())
            {
                return 6;
            }
            else if (Placed[Placed.Count - 1].GetRank() == Placed[Placed.Count - 2].GetRank())
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        //checks if there is a straight
        private int ScoreRoudStraight(List<Card> Placed)
        {
            int[] Totals = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] chances = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if (Placed[Placed.Count - 1].GetRank() != Placed[Placed.Count - 2].GetRank() && Placed[Placed.Count - 1].GetRank() != Placed[Placed.Count - 3].GetRank() && Placed[Placed.Count - 3].GetRank() != Placed[Placed.Count - 2].GetRank())
            {
                if (Placed.Count >= 7)
                {
                    for (int i = Placed.Count - 8; i < Placed.Count; i++)
                    {
                        Totals[Placed[i].GetRank()] += 1;
                    }

                    for (int i = 0; i < 7; i++)
                    {
                        if (Totals[i] == 1)
                        {
                            if (Totals[i + 1] == 1)
                            {
                                if (Totals[i + 2] == 1)
                                {
                                    if (Totals[i + 3] == 1)
                                    {
                                        if (Totals[i + 4] == 1)
                                        {
                                            if (Totals[i + 5] == 1)
                                            {
                                                if (Totals[i + 6] == 1)
                                                {
                                                    return 7;
                                                }
                                                else
                                                {
                                                    return 6;
                                                }
                                            }
                                            else
                                            {
                                                return 5;
                                            }
                                        }
                                        else
                                        {
                                            return 4;
                                        }
                                    }
                                    else
                                    {
                                        return 3;
                                    }
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else if (Placed.Count == 6)
                {
                    for (int i = Placed.Count - 7; i < Placed.Count; i++)
                    {
                        Totals[Placed[i].GetRank()] += 1;
                    }
                    for (int i = 0; i <= 11; i++)
                    {
                        if (Totals[i] == 1)
                        {
                            if (Totals[i + 1] == 1)
                            {
                                if (Totals[i + 2] == 1)
                                {
                                    if (Totals[i + 3] == 1)
                                    {
                                        if (Totals[i + 4] == 1)
                                        {
                                            if (Totals[i + 5] == 1)
                                            {
                                                return 6;
                                            }
                                            else
                                            {
                                                return 5;
                                            }
                                        }
                                        else
                                        {
                                            return 4;
                                        }
                                    }
                                    else
                                    {
                                        return 3;
                                    }
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else if (Placed.Count == 5)
                {
                    for (int i = Placed.Count - 6; i < Placed.Count; i++)
                    {
                        Totals[Placed[i].GetRank()] += 1;
                    }
                    for (int i = 0; i <= 11; i++)
                    {
                        if (Totals[i] == 1)
                        {
                            if (Totals[i + 1] == 1)
                            {
                                if (Totals[i + 2] == 1)
                                {
                                    if (Totals[i + 3] == 1)
                                    {
                                        if (Totals[i + 4] == 1)
                                        {
                                            return 5;
                                        }
                                        else
                                        {
                                            return 4;
                                        }
                                    }
                                    else
                                    {
                                        return 3;
                                    }
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else if (Placed.Count == 4)
                {
                    for (int i = Placed.Count - 5; i < Placed.Count; i++)
                    {
                        Totals[Placed[i].GetRank()] += 1;
                    }
                    for (int i = 0; i <= 11; i++)
                    {
                        if (Totals[i] == 1)
                        {
                            if (Totals[i + 1] == 1)
                            {
                                if (Totals[i + 2] == 1)
                                {
                                    if (Totals[i + 3] == 1)
                                    {
                                        return 4;
                                    }
                                    else
                                    {
                                        return 3;
                                    }
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else if (Placed.Count == 3)
                {
                    for (int i = Placed.Count - 5; i < Placed.Count; i++)
                    {
                        Totals[Placed[i].GetRank()] += 1;
                    }

                    for (int i = 0; i <= 11; i++)
                    {
                        if (Totals[i] == 1)
                        {
                            if (Totals[i + 1] == 1)
                            {
                                if (Totals[i + 2] == 1)
                                {
                                    return 3;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}