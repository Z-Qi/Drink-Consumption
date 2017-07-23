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
        private DrinkHistory _todaysHistory;
        private DrinkHistory _selectedHistory;

        private bool _isRefreshing = false;

        public ICommand PullToRefreshCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private DrinkHistoryViewModel()
        {
            _drinkHistory = new ObservableCollection<DrinkHistory>();
            _todaysHistory = new DrinkHistory(DateTime.Today);
            _drinkHistory.Add(_todaysHistory);
            //TestSample();
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());
        }
        /*
        private void TestSample()
        {
            DrinkHistory h2 = new DrinkHistory(new DateTime(1990, 11, 2)); h2.Add(new Drink("history test 1", 1, 1, 1, h2.Guid)); h2.Add(new Drink("history test 2", 1, 1, 1, h2.Guid));
            DrinkHistory h3 = new DrinkHistory(new DateTime(2017, 11, 2)); h3.Add(new Drink("history test 3", 1, 1, 1, h3.Guid)); h3.Add(new Drink("history test 4", 1, 1, 1, h3.Guid));
            DrinkHistory h4 = new DrinkHistory(new DateTime(1992, 11, 2)); h4.Add(new Drink("history test 5", 1, 1, 1, h4.Guid)); h4.Add(new Drink("history test 6", 1, 1, 1, h4.Guid));

            DatabaseManager.DatabaseManagerInstance.PostHistory(h2);
            DatabaseManager.DatabaseManagerInstance.PostHistory(h3);
            DatabaseManager.DatabaseManagerInstance.PostHistory(h4);
        }
        */
        public static DrinkHistoryViewModel HistoryInstance()
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

        public DrinkHistory TodaysHistory
        {
            get => _todaysHistory;
            set
            {
                _todaysHistory = value;
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
            Application.Current.MainPage.Navigation.PushModalAsync(new HistoryDetailsPage(new DrinkViewModel(SelectedHistory)));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task OnPullToRefresh()
        {
            Refreshing = true;
            if (!History.Any(h => h.Date == DateTime.Today))
            {
                TodaysHistory = new DrinkHistory(DateTime.Today);
                History.Add(TodaysHistory);
                await DatabaseManager.DatabaseManagerInstance.PostHistory(TodaysHistory);
            }
            History = new ObservableCollection<DrinkHistory>(await DatabaseManager.DatabaseManagerInstance.GetHistory());
            Refreshing = false;
        }
    }
}
