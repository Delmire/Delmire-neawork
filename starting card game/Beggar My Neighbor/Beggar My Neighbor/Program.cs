using System;

namespace Beggar_My_Neighbor
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                string[] deck = { };
                deck = GenDeck();
                string[] handPlayer;
                string[] handComp;
                string[] d;
                d = Shuffle(deck);
                handPlayer = DealP(d);
                handComp = DealC(d);
                handComp = DealC(d);
                play(handPlayer, handComp);
                Console.ReadLine();
            }
        }
        static string[] GenDeck()
        {
            string[] number = { "ace", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "jack", "queen", "king" };
            string[] suit = { "S", "H", "C", "D" };
            string[] deck;
            deck = new string[52];
            for (int i = 0; i < 52; i++)
            {
                deck[i] = number[i % 13] + " " + suit[i / 13];
            }
            return deck;
        }
        static void showDeck(string[] d)
        {
            for (int i = 0; i < 52; i++)
            {
                Console.WriteLine(d[i]);
            }
        }
        static string[] Shuffle(string[] deck)
        {
            Random rng = new Random();
            int cardNum = 0;
            int cardNumT;
            string tmp;
            for(int i = 0; i<= deck.Length*deck.Length* deck.Length * deck.Length; i++)
            {
                cardNum = rng.Next(0, 52);
                cardNumT = rng.Next(0, 52);
                tmp = deck[cardNum];
                deck[cardNum] = deck[cardNumT];
                deck[cardNumT] = tmp;
            }
            return deck;
        }
        static string[] DealC(string[] deck)
        {
            string[] CHand;
            CHand = new string[26];
            for (int i = 1; i<52; i+=2)
            {
                CHand[(i-1)/2] = deck[i];
            }
            return CHand;
        }
        static string[] DealP(string[] deck)
        {
            string[] PHand;
            PHand = new string[26];
            for (int i = 0; i < 51; i += 2)
            {
                PHand[i/2] = deck[i];
            }
            return PHand;
        }
        static bool play(string[] Phand, string[] Chand)
        {
            bool win = false;
            int turn = 0;
            int Countdown = 10000;
            string[] FullPhand;
            string[] FullChand;
            string[] prize;
            FullPhand = new string[52];
            for(int i =0; i<52;i++)
            {
                FullPhand[i] = "";
            }
            FullChand = new string[52];
            for (int i = 0; i < 52; i++)
            {
                FullChand[i] = "";
            }
            prize = new string[52];
            for(int i = 0; i<52;i++)
            {
                prize[i] = "";
            }
            for (int i =0; i<Phand.Length;i++)
            {
                FullPhand[i] = Phand[i];
            }
            for (int i = 0; i < Chand.Length; i++)
            {
                FullChand[i] = Chand[i];
            }
            while (win == false)
            {
                while (Countdown > 0)
                {
                    if (turn % 2 == 0)
                    {
                        prize[turn] = FullPhand[0];
                        Countdown = assess(FullPhand[0], Countdown);
                        Console.WriteLine(FullPhand[0]);
                        turn += 1;
                        for (int i = 1; i < FullPhand.Length; i++)
                        {
                            FullPhand[i - 1] = FullPhand[i];
                        }
                    }
                    else if (turn % 2 == 1)
                    {
                        prize[turn] = FullChand[0];
                        Countdown = assess(FullChand[0], Countdown);
                        Console.WriteLine(FullChand[0]);
                        turn += 1;
                        for (int i = 1; i < FullChand.Length; i++)
                        {
                            FullChand[i - 1] = FullChand[i];
                        }
                    }
                }
                if(turn%2==0)
                {
                    FullPhand = winHand(prize, FullPhand);
                    if (FullPhand[51]!= "")
                    {
                        win = true;
                        for (int i = 0; i < 52; i++)
                        {
                            Console.Write("   {0}", FullPhand);
                        }
                        Console.WriteLine("Player Wins");
                    }
                    
                }
                else
                {
                    FullChand = winHand(prize, FullChand);
                    if (FullChand[51] != "")
                    {
                        win = true;
                        for(int i =0;i<52;i++)
                        {
                            Console.Write("   {0}", FullChand);
                        }
                        Console.WriteLine("Computer Wins");
                    }
                }

            }
            return true;
        }
        static string[] winHand(string[] Prize, string[] Hand)
        {
            for (int i = 0; i < 52; i++)
            {
                if (Hand[i] == "")
                {
                    Hand[i] = Prize[i];
                }
            }
            return Hand;
        }
        static int assess(string card, int currentCount)
        {
            if(card.Contains("ace"))
            {
                return 4;
            }
            else if (card.Contains("jack"))
            {
                return 1;
            }
            else if (card.Contains("queen"))
            {
                return 2;
            }
            else if (card.Contains("king"))
            {
                return 3;
            }
            else
            {
                return currentCount-1;
            }
        }
    }
}
