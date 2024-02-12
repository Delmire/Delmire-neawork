using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEACribbage
{

    public partial class Form1 : Form
    { 

        bool PlayerCanPlace = false;
        bool PlayerCanDiscard = false;
        int ticks = 0;
        Deck d = new Deck();
        List<Card> tmp = new List<Card>();
        Game TheGame;
        decimal difficulty;
        Random rng = new Random();
        List<Card> PlayerStack = new List<Card>();
        List<Card> BotStack = new List<Card>();
        List<Card> Crib = new List<Card>();
        List<Card> PlayerHand;
        List<Card> BotHand;
        Player Human;




        public Form1()
        {
            InitializeComponent();

        }


        public void displayCards()
        {
            label1.Text = TheGame.GetPlayerHand().Count.ToString();
            //label2.Text = TheGame.GetPlayerHand()[0].GetSuit();
            //label1.Text = TheGame.GetPlayerHand()[0].GetRank().ToString() + TheGame.GetPlayerHand()[0].GetSuit();
            //label2.Text = TheGame.GetPlayerHand()[1].GetRank().ToString() + TheGame.GetPlayerHand()[1].GetSuit();
            //label3.Text = TheGame.GetPlayerHand()[2].GetRank().ToString() + TheGame.GetPlayerHand()[2].GetSuit();
            //label4.Text = TheGame.GetPlayerHand()[3].GetRank().ToString() + TheGame.GetPlayerHand()[3].GetSuit();
            //label5.Text = TheGame.GetPlayerHand()[4].GetRank().ToString() + TheGame.GetPlayerHand()[4].GetSuit();
            //label6.Text = TheGame.GetPlayerHand()[5].GetRank().ToString() + TheGame.GetPlayerHand()[5].GetSuit();
        }

        private void ShuffleTime_Tick(object sender, EventArgs e)
        {
            ticks += 1;
            d.Shuffle();
            if (ticks == 3000)
            {
                string NewOrder = "";
                for (int i = 0; i < 52; i++)
                {
                    NewOrder = NewOrder + d.deck[i].GetRank() + d.deck[i].GetRank();
                }
                MessageBox.Show(NewOrder);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string order = "";
            for (int i = 0; i < 3001; i++)
            {
                d.Shuffle();
            }
            //for (int i = 0; i < 52; i++)
            //{
            //    order = order + d.deck[i].rank + d.deck[i].suit + " ";
            //}
            //MessageBox.Show(order);

        }

        private void UpDownDifficulty_ValueChanged(object sender, EventArgs e)
        {
            difficulty = UpDownDifficulty.Value;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            int TheGameDif;
            d.Shuffle();
            TheGameDif = Convert.ToInt32(UpDownDifficulty.Value);
            TheGame = new Game(TheGameDif, tmp, tmp, false);
            TheGame.SetDifficulty(TheGameDif);
            displayCards();
        }



        private void Card1_Click(object sender, EventArgs e)
        {
            if (PlayerCanDiscard)
            {
                TheGame.DiscardPlayer(0);
            }
            if (PlayerCanPlace)
            {
                Human.Place(0);
            }
        }

        private void Card2_Click(object sender, EventArgs e)
        {
            if (PlayerCanDiscard)
            {
                TheGame.DiscardPlayer(1);
            }
            if (PlayerCanPlace)
            {
                Human.Place(1);
            }
        }

        private void Card3_Click(object sender, EventArgs e)
        {
            if (PlayerCanDiscard)
            {
                TheGame.DiscardPlayer(2);
            }
            if (PlayerCanPlace)
            {
                Human.Place(2);
            }
        }

        private void Card4_Click(object sender, EventArgs e)
        {
            if (PlayerCanDiscard)
            {
                TheGame.DiscardPlayer(3);
            }
            if (PlayerCanPlace)
            {
                Human.Place(3);
            }
        }

        private void Card5_Click(object sender, EventArgs e)
        {
            if (PlayerCanDiscard)
            {
                TheGame.DiscardPlayer(4);
            }
            if (PlayerCanPlace)
            {
                Human.Place(4);
            }
        }

        private void Card6_Click(object sender, EventArgs e)
        {
            if (PlayerCanDiscard)
            {
                TheGame.DiscardPlayer(5);
            }
            if (PlayerCanPlace)
            {
                Human.Place(5);
            }
        }
    }
}