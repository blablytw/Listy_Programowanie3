
using System;
using System.Windows;
using System.Windows.Controls;

namespace lista2
{
    public partial class MainWindow : Window
    {
        double liczba1 = 0;
        string znak = "";
        bool nowaLiczba = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Liczba_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string cyfra = b.Content.ToString();

            if (nowaLiczba || ekran.Text == "Blad")
            {
                ekran.Text = cyfra;
                nowaLiczba = false;
            }
            else
            {
                if (ekran.Text == "0")
                    ekran.Text = cyfra;
                else
                    ekran.Text += cyfra;
            }
        }

        private void Znak_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(ekran.Text, out liczba1))
                return;

            Button b = (Button)sender;
            znak = b.Content.ToString();
            nowaLiczba = true;
        }

        private void Wynik_Click(object sender, RoutedEventArgs e)
        {
            double liczba2;

            if (!double.TryParse(ekran.Text, out liczba2))
                return;

            double wynik = 0;

            if (znak == "+")
                wynik = liczba1 + liczba2;
            else if (znak == "-")
                wynik = liczba1 - liczba2;
            else if (znak == "*")
                wynik = liczba1 * liczba2;
            else if (znak == "/")
            {
                if (liczba2 == 0)
                {
                    ekran.Text = "Blad";
                    znak = "";
                    nowaLiczba = true;
                    return;
                }

                wynik = liczba1 / liczba2;
            }
            else
                return;

            ekran.Text = wynik.ToString();
            znak = "";
            nowaLiczba = true;
        }

        private void Czysc_Click(object sender, RoutedEventArgs e)
        {
            ekran.Text = "0";
            liczba1 = 0;
            znak = "";
            nowaLiczba = true;
        }
    }
}
