using System.Windows;
using System.Windows.Controls;

namespace Zadanie3Lista2
{
    public partial class Gracz2 : Window
    {
        public Gracz2()
        {
            InitializeComponent();
            GeneratorPrzyciskow.Dodaj(plnPersonForm, "PersonIdTwo", "YesNoToBooleanConverter", 0, Button_Click);
            GeneratorPrzyciskow.Dodaj(plnPersonForm, "PersonIdOne", "YesNoToBooleanConverter2", 11, Button_Click_shot);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Game gra = (Game)plnPersonForm.DataContext;
            int nr = Convert.ToInt32(btn.Tag.ToString());
            if (gra.PersonIdTwo[nr] == 0)
                gra.PersonIdTwo[nr]++;
            else if (gra.PersonIdTwo[nr] == 1)
                gra.PersonIdTwo[nr]--;
        }

        private void Button_Click_shot(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Game gra = (Game)plnPersonForm.DataContext;
            int nr = Convert.ToInt32(btn.Tag.ToString());
            if (gra.PersonIdOne[nr] == 0 || gra.PersonIdOne[nr] == 1)
                gra.PersonIdOne[nr] += 2;
        }
    }
}