using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Zadanie3Lista2
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<int> _PersonIdOne = new ObservableCollection<int>();
        ObservableCollection<int> _PersonIdTwo = new ObservableCollection<int>();

        public ObservableCollection<int> PersonIdOne
        {
            get { return _PersonIdOne; }
            set
            {
                _PersonIdOne = value;
                OnPropertyChanged("PersonIdOne");
            }
        }

        public ObservableCollection<int> PersonIdTwo
        {
            get { return _PersonIdTwo; }
            set
            {
                _PersonIdTwo = value;
                OnPropertyChanged("PersonIdTwo");
            }
        }

           

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}