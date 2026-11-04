using System.Windows;

namespace Zadanie3Lista2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            StanGry gra = new StanGry();

            gra.Okna[1] = new OknoGry(gra, 1);
            gra.Okna[2] = new OknoGry(gra, 2);

            gra.Okna[1]!.Show();
            gra.Okna[2]!.Show();
            gra.Odswiez();

            this.Close();
        }
    }
}