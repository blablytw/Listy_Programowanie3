using System.ComponentModel;

namespace Zadanie3Lista2
{
    public class Pole : INotifyPropertyChanged
    {
        private string tekst = "";
        private bool czyAktywne;

        public int Wiersz { get; set; }
        public int Kolumna { get; set; }

        public string Tekst
        {
            get { return tekst; }
            set
            {
                if (tekst == value)
                    return;
                tekst = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tekst)));
            }
        }

        public bool CzyAktywne
        {
            get { return czyAktywne; }
            set
            {
                if (czyAktywne == value)
                    return;
                czyAktywne = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CzyAktywne)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}