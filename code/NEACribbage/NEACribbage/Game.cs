using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEACribbage
{
    class Game
    {
        static Random rng = new Random();
        private double ScorePlayer;
        private double ScoreBot;
        public int Difficulty;
        Probability Stats;
        Opponent bot;
        public Player human;
        Deck TheDeck = new Deck();
        List<Card> PlacedPlayer = new List<Card>();
        List<Card> PlacedBot = new List<Card>();
        List<Card> PlacedTotal = new List<Card>();
        List<int> BHand = new List<int>();
        List<int> Phand = new List<int>();
        List<Card> BotHand = new List<Card>();
        List<Card> PlayerHand = new List<Card>();
        List<Card> PHHand = new List<Card>();
        List<Card> BHHand = new List<Card>();
        List<Card> Crib = new List<Card>();
        bool cribWho = false;
        //False means the bot controlls the crib and true means the player

        public Game(int difficulty, List<Card> playerHand, List<Card> botHand, bool Cribb)
        {
            bot = new Opponent(difficulty, cribWho);
            human = new Player(playerHand);
            Difficulty = difficulty;
            SetDifficulty(Difficulty);
            bot.triggerStats();
            bot.SetFirstHand(DealFirstBot());
            SetPLaced();
            bot.SetPlaced(PlacedTotal);
            ScorePlayer = 0;
            ScoreBot = 0;
        }

        public void SetDifficulty(int difficulty)
        {
            Difficulty = difficulty;
        }

        private void SetPLaced()
        {
            List<Card> PlacedAll = new List<Card>();
            if (cribWho)//if the player does have the crib
            {
                if (PlacedPlayer.Count == PlacedBot.Count)
                {
                    for (int i = 0; i < PlacedBot.Count; i++)
                    {
                        PlacedTotal.Add(PlacedBot[i]);
                        PlacedTotal.Add(PlacedPlayer[i]);
                    }
                }
                else if (PlacedBot.Count > PlacedPlayer.Count)
                {
                    for (int i = 0; i < PlacedBot.Count; i++)
                    {
                        PlacedTotal.Add(PlacedBot[i]);
                        if (i < PlacedPlayer.Count)
                        {
                            PlacedTotal.Add(PlacedPlayer[i]);
                        }
                    }
                }
                else
                {
                    if (PlacedPlayer.Count > PlacedBot.Count)
                    {
                        for (int i = 0; i < PlacedPlayer.Count; i++)
                        {
                            if (PlacedBot.Count > i)
                            {
                                PlacedTotal.Add(PlacedBot[i]);
                            }
                            PlacedTotal.Add(PlacedPlayer[i]);
                        }
                    }
                }
            }
            else
            {//if the player does NOT have the crib
                if (PlacedPlayer.Count == PlacedBot.Count)
                {
                    for (int i = 0; i < PlacedBot.Count; i++)
                    {
                        PlacedTotal.Add(PlacedPlayer[i]);
                        PlacedTotal.Add(PlacedBot[i]);
                    }
                }
                else if (PlacedBot.Count > PlacedPlayer.Count)
                {
                    for (int i = 0; i < PlacedBot.Count; i++)
                    {
                        if (i < PlacedPlayer.Count)
                        {
                            PlacedTotal.Add(PlacedPlayer[i]);
                        }
                        PlacedTotal.Add(PlacedBot[i]);
                    }
                }

                else if (PlacedPlayer.Count > PlacedBot.Count)
                {
                    for (int i = 0; i < PlacedPlayer.Count; i++)
                    {

                        PlacedTotal.Add(PlacedPlayer[i]);
                        if (PlacedBot.Count > i)
                        {
                            PlacedTotal.Add(PlacedBot[i]);
                        }
                    }
                }
            }
        }

        public void setDeck(Deck d)
        {
            TheDeck = d;
        }

        

        public void PlaceBot()
        {
            bot.WasPlaced(bot.Place());
            PlacedBot.Add(bot.Place());
            ScorePlacedCalculateAdd(false);
        }

        public Card PlacePlayer(int CardNum)
        {
            PlacedPlayer.Add(human.Showhand()[CardNum]);
            PlacedTotal.Add(human.Place(CardNum));
            ScorePlacedCalculateAdd(true);
            return human.Place(CardNum);
        }

        public void Deal()
        {
            TheDeck.Shuffle();
            for (int i = TheDeck.deck.Count; i < 6; i ++)
            {
                PHHand.Add(TheDeck.deck[48 - (i * 2 - 1)]);
                BHHand.Add(TheDeck.deck[48 - (i * 2)]);
            }
            bot.Take(BHHand);
        }

        public List<Card> DealFirstBot()
        {
            List<Card> PHHHand = new List<Card>();
            List<Card> BHHHand = new List<Card>();
            TheDeck.Shuffle();
            for (int i = TheDeck.deck.Count; i < 6; i ++)
            {
                PHHHand.Add(TheDeck.deck[48 - (i * 2 - 1)]);
                BHHHand.Add(TheDeck.deck[48 - (i * 2)]);
            }
            human.Take(PHHHand);
            return BHHHand;
        }

        public List<Card> DealFirstPlayer()
        {
            List<Card> PHand = new List<Card>();
            List<Card> BHand = new List<Card>();
            TheDeck.Shuffle();
            for (int i = TheDeck.deck.Count; i >= 48; i -= 2)
            {
                PHand.Add(TheDeck.deck[i * 2 - 1]);
                BHand.Add(TheDeck.deck[i * 2]);
            }
            return PHand;
        }

        private void ScorePlacedCalculateAdd(bool PlacedBy)
        {
            if (PlacedBy)
            {
                ScorePlayer += Stats.ScorePlacedCard(PlacedTotal);
            }
            else
            {
                ScoreBot += Stats.ScorePlacedCard(PlacedTotal);
            }
        }

        public List<Card> BotDiscard()
        {
            Crib.Add(bot.Discard(bot.Showhand(), cribWho)[0]);
            Crib.Add(bot.Discard(bot.Showhand(), cribWho)[1]);
            return bot.Discard(bot.Showhand(), cribWho);
        }

        public Card DiscardPlayer(int card)
        {
            Crib.Add(human.Place(card));
            return human.Place(card);
        }

        private void ScoreRoundEnd()
        {
            int[] PHandValues = { human.Showhand()[0].GetRank(), human.Showhand()[1].GetRank(), human.Showhand()[2].GetRank(), human.Showhand()[3].GetRank() };
            double score = 0;
            score += Stats.Fifteens(PHandValues);
            score += Stats.Snap(PHandValues);
            score += Stats.Straights(PHandValues);
            ScorePlayer += score;
            score = 0;
            int[] BHandValues = { bot.Showhand()[0].GetRank(), bot.Showhand()[1].GetRank(), bot.Showhand()[2].GetRank(), bot.Showhand()[3].GetRank() };
            score += Stats.Fifteens(BHandValues);
            score += Stats.Snap(BHandValues);
            score += Stats.Straights(BHandValues);
            ScoreBot += score;
        }

        public List<Card> GetPlayerHand()
        {
            return human.Showhand();
        }
    }
}
