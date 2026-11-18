using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Zadanie2Lista2
{
    public class Game
    {
        private ObservableCollection<string> _tab = new ObservableCollection<string>();

        public ObservableCollection<string> tab
        {
            get { return _tab; }
            set
            {
                _tab = value;

                OnPropertyChanged("tab");
                this.ValuateWinCndition();
            }
        }

        // Event handler for INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public Game()
        {
            tab = new ObservableCollection<string>();
            for (int i = 0; i < 9; i++)
                tab.Add("");
        }

        public void ValuateWinCndition()
        {
            if (tab.Count == 9)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (tab[i * 3] == tab[i * 3 + 1] && tab[i * 3 + 1] == tab[i * 3 + 2])
                    {
                        if (tab[i * 3] != "")
                            this.EndGame(tab[i * 3]);
                    }
                    if (tab[i] == tab[i + 3] && tab[i + 3] == tab[i + 6])
                    {
                        if (tab[i] != "")
                            this.EndGame(tab[i]);
                    }
                }
                if ((tab[0] == tab[4] && tab[4] == tab[8]) || (tab[2] == tab[4] && tab[4] == tab[6]))
                {
                    if (tab[4] != "")
                        this.EndGame(tab[4]);
                }
            }
        }

        private void EndGame(string a)
        {
            MessageBox.Show("Game won by " + a);
            for (int i = 0; i < 9; i++)
            {
                tab[i] = "";
            }
        }
    }
}