using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace Zadanie3Lista2
{
    public partial class GameWindow : Window, INotifyPropertyChanged
    {
        GameState gra;
        int gracz;

        Pole[,] mojePola = new Pole[5, 5];
        Pole[,] polaWroga = new Pole[5, 5];

        public List<Pole> MojePola { get; } = new List<Pole>();
        public List<Pole> PolaWroga { get; } = new List<Pole>();

        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
                    Pole moje = new Pole { Wiersz = r, Kolumna = c };
                    mojePola[r, c] = moje;
                    MojePola.Add(moje);

                    Pole wrog = new Pole { Wiersz = r, Kolumna = c };
                    polaWroga[r, c] = wrog;
                    PolaWroga.Add(wrog);
                }
            }

            DataContext = this;

            gra.Updated += OdrysujPlansze;

            OdrysujPlansze();
        }

        private void MojePole_Click(object sender, RoutedEventArgs e)
        {
            if (gra.Turn != 0)
                return;

            Pole pole = (Pole)((Button)sender).DataContext;

            gra.PlaceShip(gracz, pole.Wiersz, pole.Kolumna);
        }

        private void WrogPole_Click(object sender, RoutedEventArgs e)
        {
            Pole pole = (Pole)((Button)sender).DataContext;

            gra.Shoot(gracz, pole.Wiersz, pole.Kolumna);
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
                        mojePola[r, c].Tekst =
                            mojeStatki[r, c] == 1 ? "X" : "-";
                    }
                    else if (mojeStatki[r, c] == 1)
                    {
                        mojePola[r, c].Tekst = "S";
                    }
                    else
                    {
                        mojePola[r, c].Tekst = "";
                    }

                    mojePola[r, c].CzyAktywne =
                        gra.Turn == 0 &&
                        gra.CountShips(gracz) < 3 &&
                        mojeStatki[r, c] == 0;

                    // Plansza przeciwnika
                    if (mojeAtaki[r, c])
                    {
                        polaWroga[r, c].Tekst =
                            statkiWroga[r, c] == 1 ? "X" : "-";
                    }
                    else
                    {
                        polaWroga[r, c].Tekst = "";
                    }

                    polaWroga[r, c].CzyAktywne =
                        gra.Turn == gracz &&
                        gra.Winner == 0 &&
                        !mojeAtaki[r, c];
                }
            }

            if (gra.Winner != 0)
            {
                Status = gra.Winner == gracz
                    ? "Wygrales!"
                    : "Przegrales!";
            }
            else if (gra.Turn == 0)
            {
                Status = "Gracz " + gracz +
                    ": ustaw statki (" +
                    gra.CountShips(gracz) + "/3)";
            }
            else if (gra.Turn == gracz)
            {
                Status = "Twoja kolej — oddaj strzal!";
            }
            else
            {
                Status = "Czekaj na ruch przeciwnika.";
            }
        }
    }
}
