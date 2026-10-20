using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zadanie3Lista2
{
    public partial class GameWindow : Window
    {
        GameState gra;
        int gracz;

        Button[,] mojePola = new Button[5, 5];
        Button[,] polaWroga = new Button[5, 5];

        public GameWindow(GameState stanGry, int numerGracza)
        {
            InitializeComponent();

            gra = stanGry;
            gracz = numerGracza;

            Title = "Gracz " + gracz;

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Button moje = new Button();
                    moje.FontSize = 20;
                    moje.Margin = new Thickness(2);
                    moje.Width = 55;
                    moje.Height = 50;
                    moje.Tag = r + "," + c;
                    moje.Click += MojePole_Click;

                    mojePola[r, c] = moje;
                    OwnBoard.Children.Add(moje);

                    Button wrog = new Button();
                    wrog.FontSize = 20;
                    wrog.Margin = new Thickness(2);
                    wrog.Width = 55;
                    wrog.Height = 50;
                    wrog.Tag = r + "," + c;
                    wrog.Click += WrogPole_Click;

                    polaWroga[r, c] = wrog;
                    EnemyBoard.Children.Add(wrog);
                }
            }

            gra.Updated += OdrysujPlansze;

            OdrysujPlansze();
        }

        private void MojePole_Click(object sender, RoutedEventArgs e)
        {
            if (gra.Turn != 0)
                return;

            Button pole = (Button)sender;
            string[] pozycja = pole.Tag.ToString().Split(',');

            int r = int.Parse(pozycja[0]);
            int c = int.Parse(pozycja[1]);

            gra.PlaceShip(gracz, r, c);
        }

        private void WrogPole_Click(object sender, RoutedEventArgs e)
        {
            Button pole = (Button)sender;
            string[] pozycja = pole.Tag.ToString().Split(',');

            int r = int.Parse(pozycja[0]);
            int c = int.Parse(pozycja[1]);

            gra.Shoot(gracz, r, c);
        }

        private void OdrysujPlansze()
        {
            int[,] mojeStatki = gracz == 1
                ? gra.Ships1 : gra.Ships2;

            bool[,] mojeStrzaly = gracz == 1
                ? gra.Shots2 : gra.Shots1;

            int[,] statkiWroga = gracz == 1
                ? gra.Ships2 : gra.Ships1;

            bool[,] mojeAtaki = gracz == 1
                ? gra.Shots1 : gra.Shots2;

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    // Wlasna plansza
                    if (mojeStrzaly[r, c])
                    {
                        mojePola[r, c].Content =
                            mojeStatki[r, c] == 1 ? "X" : "-";
                    }
                    else if (mojeStatki[r, c] == 1)
                    {
                        mojePola[r, c].Content = "S";
                    }
                    else
                    {
                        mojePola[r, c].Content = "";
                    }

                    mojePola[r, c].IsEnabled =
                        gra.Turn == 0 &&
                        gra.CountShips(gracz) < 3 &&
                        mojeStatki[r, c] == 0;

                    // Plansza przeciwnika
                    if (mojeAtaki[r, c])
                    {
                        polaWroga[r, c].Content =
                            statkiWroga[r, c] == 1 ? "X" : "-";
                    }
                    else
                    {
                        polaWroga[r, c].Content = "";
                    }

                    polaWroga[r, c].IsEnabled =
                        gra.Turn == gracz &&
                        gra.Winner == 0 &&
                        !mojeAtaki[r, c];
                }
            }

            if (gra.Winner != 0)
            {
                Status.Text = gra.Winner == gracz
                    ? "Wygrales!"
                    : "Przegrales!";
            }
            else if (gra.Turn == 0)
            {
                Status.Text = "Gracz " + gracz +
                    ": ustaw statki (" +
                    gra.CountShips(gracz) + "/3)";
            }
            else if (gra.Turn == gracz)
            {
                Status.Text = "Twoja kolej — oddaj strzal!";
            }
            else
            {
                Status.Text = "Czekaj na ruch przeciwnika.";
            }
        }
    }
}
