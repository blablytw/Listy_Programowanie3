using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zadanie2Lista2
{
    public partial class MainWindow : Window
    {
        string gracz = "X";
        bool koniecGry = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Pole_Click(object sender, RoutedEventArgs e)
        {
            if (koniecGry)
                return;

            Button pole = (Button)sender;

            if (pole.Content != null &&
                pole.Content.ToString() != "")
                return;

            pole.Content = gracz;

            if (CzyWygrana())
            {
                Status.Text = "Wygrywa gracz " + gracz + "!";
                koniecGry = true;
                return;
            }

            if (CzyRemis())
            {
                Status.Text = "Remis!";
                koniecGry = true;
                return;
            }

            if (gracz == "X")
                gracz = "O";
            else
                gracz = "X";

            Status.Text = "Ruch gracza: " + gracz;
        }

        private bool CzyWygrana()
        {
            Button[] pola = new Button[9];

            int i = 0;

            foreach (UIElement element in
                     ((UniformGrid)((Grid)Content).Children[1]).Children)
            {
                pola[i] = (Button)element;
                i++;
            }

            int[,] linie =
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 0, 4, 8 },
                { 2, 4, 6 }
            };

            for (int j = 0; j < 8; j++)
            {
                int a = linie[j, 0];
                int b = linie[j, 1];
                int c = linie[j, 2];

                if (pola[a].Content != null &&
                    pola[a].Content.ToString() == gracz &&
                    pola[b].Content != null &&
                    pola[b].Content.ToString() == gracz &&
                    pola[c].Content != null &&
                    pola[c].Content.ToString() == gracz)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CzyRemis()
        {
            UniformGrid plansza =
                (UniformGrid)((Grid)Content).Children[1];

            foreach (UIElement element in plansza.Children)
            {
                Button pole = (Button)element;

                if (pole.Content == null ||
                    pole.Content.ToString() == "")
                    return false;
            }

            return true;
        }

        private void NowaGra_Click(object sender, RoutedEventArgs e)
        {
            UniformGrid plansza =
                (UniformGrid)((Grid)Content).Children[1];

            foreach (UIElement element in plansza.Children)
            {
                Button pole = (Button)element;
                pole.Content = "";
            }

            gracz = "X";
            koniecGry = false;
            Status.Text = "Ruch gracza: X";
        }
    }
}