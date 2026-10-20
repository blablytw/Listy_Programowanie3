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
            GameState gra = new GameState();

            GameWindow gracz1 = new GameWindow(gra, 1);
            GameWindow gracz2 = new GameWindow(gra, 2);

            gracz1.Show();
            gracz2.Show();

            this.Hide();
        }
    }
}