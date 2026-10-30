using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Zadanie2Lista2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ModelWidoku();
        }
    }

    public class ZmianaWlasciwosci : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Powiadom(string nazwa)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazwa));
        }
    }

    public class PoleGry : ZmianaWlasciwosci
    {
        private string tekst = "";

        public int Indeks { get; }

        public string Tekst
        {
            get => tekst;
            set
            {
                tekst = value;
                Powiadom(nameof(Tekst));
            }
        }

        public PoleGry(int indeks)
        {
            Indeks = indeks;
        }
    }

    public class PrzekaznikKomendy : ICommand
    {
        private readonly Action<object?> akcja;

        public PrzekaznikKomendy(Action<object?> akcja)
        {
            this.akcja = akcja;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parametr) => true;

        public void Execute(object? parametr) => akcja(parametr);
    }

    public class ModelWidoku : ZmianaWlasciwosci
    {
        private readonly LogikaGry logika = new();
        private string status;

        public ObservableCollection<PoleGry> Pola { get; } = new();

        public string Status
        {
            get => status;
            set
            {
                status = value;
                Powiadom(nameof(Status));
            }
        }

        public ICommand RuchKomenda { get; }
        public ICommand NowaGraKomenda { get; }

        public ModelWidoku()
        {
            for (int i = 0; i < 9; i++)
                Pola.Add(new PoleGry(i));

            status = logika.Status;

            RuchKomenda = new PrzekaznikKomendy(Ruch);
            NowaGraKomenda = new PrzekaznikKomendy(_ => NowaGra());
        }

        private void Ruch(object? parametr)
        {
            if (parametr is PoleGry pole && logika.Ruch(pole.Indeks))
                Odswiez();
        }

        private void NowaGra()
        {
            logika.Resetuj();
            Odswiez();
        }

        private void Odswiez()
        {
            foreach (PoleGry pole in Pola)
                pole.Tekst = logika.PobierzPole(pole.Indeks);

            Status = logika.Status;
        }
    }
}