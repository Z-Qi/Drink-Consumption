using DrinkConsumption.Database;
using DrinkConsumption.Model;
using DrinkConsumption.View;
using System;
using System.Collections.Generic;
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

            ClearHistoryCommand = new Command(async () => await ClearHistory());
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());

            PullToRefreshCommand.Execute(null);
            _isRefreshing = false;
        }

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void HistoryDetails()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(new DrinkViewModel(SelectedHistory)));
        }

        private async Task ClearHistory()
        {
            bool remove = await Application.Current.MainPage.DisplayAlert("REMOVE ALL HISTORY", "This cannot be undone\nAre you sure?", "Remove", "Cancel");
            if (!remove)
            {
                return;
            }
            await DatabaseManager.DatabaseManagerInstance.ClearHistory();
        }

        private async Task OnPullToRefresh()
        {
            Refreshing = true;
            Histories = new ObservableCollection<DrinkHistory>((await DatabaseManager.DatabaseManagerInstance.GetHistory()).OrderByDescending(h => h.Date).ToList());
            Refreshing = false;
        }
    }
}
