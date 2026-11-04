using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Zadanie3Lista2
{
    public partial class OknoGry : Window, INotifyPropertyChanged
    {
        StanGry gra;
        int gracz;

        public ObservableCollection<string> MojePola { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> PolaWroga { get; } = new ObservableCollection<string>();

        private string status = "";
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public OknoGry(StanGry stanGry, int numerGracza)
        {
            InitializeComponent();

            gra = stanGry;
            gracz = numerGracza;

            Title = "Gracz " + gracz;
            Left = 40 + gracz * 40;
            Top = 20 + gracz * 40;

            for (int i = 0; i < 100; i++)
            {
                MojePola.Add("");
                PolaWroga.Add("");

                Button moj = new Button();
                moj.Tag = i;
                moj.Margin = new Thickness(1);
                moj.SetBinding(Button.ContentProperty, new Binding("MojePola[" + i + "]"));
                moj.Click += MojePole_Click;
                mojaSiatka.Children.Add(moj);

                Button wrog = new Button();
                wrog.Tag = i;
                wrog.Margin = new Thickness(1);
                wrog.SetBinding(Button.ContentProperty, new Binding("PolaWroga[" + i + "]"));
                wrog.Click += WrogPole_Click;
                wrogaSiatka.Children.Add(wrog);
            }

            DataContext = this;
        }

        private void MojePole_Click(object sender, RoutedEventArgs e)
        {
            int numer = Convert.ToInt32(((Button)sender).Tag);

            gra.Postaw(gracz, numer);
        }

        private void WrogPole_Click(object sender, RoutedEventArgs e)
        {
            int numer = Convert.ToInt32(((Button)sender).Tag);

            gra.Strzel(gracz, numer);
        }

        public void Odswiez()
        {
            bool[] mojeStatki = gra.Statki[gracz];
            bool[] mojeStrzaly = gra.Strzaly[gracz];
            bool[] statkiWroga = gra.Statki[3 - gracz];
            bool[] strzalyWroga = gra.Strzaly[3 - gracz];

            for (int i = 0; i < 100; i++)
            {
                if (strzalyWroga[i])
                    MojePola[i] = mojeStatki[i] ? "X" : "-";
                else
                    MojePola[i] = mojeStatki[i] ? "S" : "";

                if (mojeStrzaly[i])
                    PolaWroga[i] = statkiWroga[i] ? "X" : "-";
                else
                    PolaWroga[i] = "";
            }

            if (gra.Zwyciezca != 0)
                Status = gra.Zwyciezca == gracz ? "Wygrałeś!" : "Przegrałeś!";
            else if (gra.Tura == 0)
                Status = "Gracz " + gracz + ": ustaw statki (" + gra.Policz(mojeStatki) + "/3)";
            else if (gra.Tura == gracz)
                Status = "Twoja kolej - oddaj strzał!";
            else
                Status = "Czekaj na ruch przeciwnika.";
        }
    }
}