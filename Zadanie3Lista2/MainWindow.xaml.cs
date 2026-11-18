using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Zadanie3Lista2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Game gra = new Game();
            gra.PersonIdOne = new ObservableCollection<int>(new int[100]);
            gra.PersonIdTwo = new ObservableCollection<int>(new int[100]);
            plnPersonForm.DataContext = gra;
            GeneratorPrzyciskow.Dodaj(plnPersonForm, "PersonIdOne", "YesNoToBooleanConverter", 0, Button_Click);
            GeneratorPrzyciskow.Dodaj(plnPersonForm, "PersonIdTwo", "YesNoToBooleanConverter2", 11, Button_Click_shot);
            Gracz2 okno = new Gracz2();
            okno.DataContext = gra;
            okno.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Game gra = (Game)plnPersonForm.DataContext;
            int nr = Convert.ToInt32(btn.Tag.ToString());
            if (gra.PersonIdOne[nr] == 0)
                gra.PersonIdOne[nr]++;
            else if (gra.PersonIdOne[nr] == 1)
                gra.PersonIdOne[nr]--;
        }

        private void Button_Click_shot(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Game gra = (Game)plnPersonForm.DataContext;
            int nr = Convert.ToInt32(btn.Tag.ToString());
            if (gra.PersonIdTwo[nr] == 0 || gra.PersonIdTwo[nr] == 1)
                gra.PersonIdTwo[nr] += 2;
        }
    }
}