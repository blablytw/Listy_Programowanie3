using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Zadanie3Lista2
{
    public static class GeneratorPrzyciskow
    {
        public static void Dodaj(Grid siatka, string wlasciwosc, string konwerter, int pierwszyWiersz, RoutedEventHandler klik)
        {
            for (int wiersz = 0; wiersz < 10; wiersz++)
            {
                for (int kolumna = 0; kolumna < 10; kolumna++)
                {
                    int indeks = wiersz * 10 + kolumna;

                    Button przycisk = new Button();
                    przycisk.Content = (wiersz + 1) + "," + (kolumna + 1);
                    przycisk.Tag = indeks;
                    Grid.SetRow(przycisk, pierwszyWiersz + wiersz);
                    Grid.SetColumn(przycisk, kolumna);

                    Binding binding = new Binding(wlasciwosc + "[" + indeks + "]");
                    binding.Mode = BindingMode.TwoWay;
                    binding.Converter = (IValueConverter)siatka.FindResource(konwerter);
                    przycisk.SetBinding(Button.BackgroundProperty, binding);

                    przycisk.Click += klik;
                    siatka.Children.Add(przycisk);
                }
            }
        }
    }
}