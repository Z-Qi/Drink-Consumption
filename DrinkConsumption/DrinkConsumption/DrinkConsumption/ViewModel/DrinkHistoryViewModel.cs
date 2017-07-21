using DrinkConsumption.Database;
using DrinkConsumption.Model;
using DrinkConsumption.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DrinkConsumption.ViewModel
{
    public class DrinkHistoryViewModel : INotifyPropertyChanged
    {
        private static DrinkHistoryViewModel _history = null;
        private ObservableCollection<DrinkHistory> _drinkHistory;
        private DrinkHistory _selectedHistory;

        private bool _isRefreshing = false;

        public ICommand PullToRefreshCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private DrinkHistoryViewModel()
        {
            _drinkHistory = new ObservableCollection<DrinkHistory>();
            //TestSample();
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());
        }

        private void TestSample()
        {
            History.Add(new DrinkHistory(DateTime.Today, new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
            History.Add(new DrinkHistory(new DateTime(1990, 11, 2), new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
            History.Add(new DrinkHistory(new DateTime(2017, 11, 2), new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
            History.Add(new DrinkHistory(new DateTime(1992, 11, 2), new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
            History.Add(new DrinkHistory(new DateTime(1524, 11, 2), new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
        }

        public static DrinkHistoryViewModel NewHistory()
        {
            if (_history == null)
            {
                _history = new DrinkHistoryViewModel();
            }
            return _history;
        }

        public void Add(DrinkHistory drinkList)
        {
            if (!History.Any(h => h.Date == drinkList.Date))
            {
                History.Add(drinkList);
            }
        }

        public ObservableCollection<DrinkHistory> History
        {
            get => _drinkHistory;
            set
            {
                _drinkHistory = value;
            }
        }

        public DrinkHistory SelectedHistory
        {
            get => _selectedHistory;
            set
            {
                _selectedHistory = value;
                if (_selectedHistory != null)
                {
                    HistoryDetails();
                }
                _selectedHistory = null;
            }
        }

        public bool Refreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private void HistoryDetails()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new HistoryDetailsPage(SelectedHistory));
            _selectedHistory = null;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task OnPullToRefresh()
        {
            Refreshing = true;
            History = new ObservableCollection<DrinkHistory>(await DatabaseManager.DatabaseManagerInstance.GetHistory());
            Refreshing = false;
        }

    }
}
