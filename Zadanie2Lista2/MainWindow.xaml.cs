using System.Windows;
using System.Windows.Controls;

namespace Zadanie2Lista2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game gra = new Game();

        string User = "X";

        public MainWindow()
        {
            InitializeComponent();
            ctlGrid.DataContext = gra;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            gra.tab[Convert.ToInt16(btn.Tag)] = User;
            if (User == "X")
                User = "O";
            else
                User = "X";
            gra.ValuateWinCndition();
        }
    }
}