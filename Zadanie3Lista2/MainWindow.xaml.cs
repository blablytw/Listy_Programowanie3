using System.Windows;
using System.Windows.Controls;

namespace Zadanie3Lista2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int[] tab = new int[100];
            int[] tab2 = new int[100];
            Game gra = new Game(tab, tab2);
            plnPersonForm.DataContext = gra;
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