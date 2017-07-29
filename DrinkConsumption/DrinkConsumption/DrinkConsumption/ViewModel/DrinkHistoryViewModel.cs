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
        private ObservableCollection<DrinkHistory> _drinkHistories;
        private DrinkHistory _selectedHistory;

        private bool _isRefreshing;

        public ICommand ClearHistoryCommand { get; private set; }
        public ICommand PullToRefreshCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private DrinkHistoryViewModel()
        {
            _drinkHistories = new ObservableCollection<DrinkHistory>();
            //TestSample();
            ClearHistoryCommand = new Command(async () => await ClearHistory());
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());

            PullToRefreshCommand.Execute(null);
            _isRefreshing = false;
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

        public ObservableCollection<DrinkHistory> Histories
        {
            get => _drinkHistories;
            set
            {
                _drinkHistories = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
            Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(new DrinkViewModel(SelectedHistory)));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ClearHistory()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("REMOVE ALL HISTORY", "This cannot be undone\nAre you sure?", "Remove", "Cancel");

            if (!result) { return; }
            await DatabaseManager.DatabaseManagerInstance.ClearHistory();
        }

        private async Task OnPullToRefresh()
        {
            Refreshing = true;
            Histories = new ObservableCollection<DrinkHistory>(await DatabaseManager.DatabaseManagerInstance.GetHistory());
            Refreshing = false;
        }
    }
}
