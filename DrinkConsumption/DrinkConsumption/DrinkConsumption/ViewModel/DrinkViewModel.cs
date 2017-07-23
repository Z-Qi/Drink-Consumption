using DrinkConsumption.Database;
using DrinkConsumption.Model;
using DrinkConsumption.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DrinkConsumption.ViewModel
{
    public class DrinkViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Drink> _drinks;
        private string _searchEntry;
        private Drink _selectedDrink;
        private DrinkHistory _history;

        private bool _isRefreshing = false;

        public ICommand AddDrinkCommand { get; private set; }
        public ICommand PullToRefreshCommand { get; private set; }
        public ICommand ClearDrinksCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DrinkViewModel()
        {
            _drinks = new ObservableCollection<Drink>();
            _history = new DrinkHistory(DateTime.Today);
            //TestSample();
            AddDrinkCommand = new Command(async () => await AddDrink());
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());
            ClearDrinksCommand = new Command(async () => await ClearDrinks());
        }

        public DrinkViewModel(DrinkHistory history)
        {
            _drinks = new ObservableCollection<Drink>();
            _history = history;
            //TestSample();
            AddDrinkCommand = new Command(async () => await AddDrink());
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());
            ClearDrinksCommand = new Command(async () => await ClearDrinks());
        }
        /*
        private void TestSample()
        {
            DatabaseManager.DatabaseManagerInstance.PostDrink(new Drink("TEST 1", 100, 1.33333, 15.5, new Guid("00000000-0000-0000-0000-000000000001")));
            DatabaseManager.DatabaseManagerInstance.PostDrink(new Drink("TEST 2", 600, 3, 45.99, new Guid("00000000-0000-0000-0000-000000000001")));
            DatabaseManager.DatabaseManagerInstance.PostDrink(new Drink("TEST 3", 010, 0.2, 5, new Guid("00000000-0000-0000-0000-000000000002")));
            DatabaseManager.DatabaseManagerInstance.PostDrink(new Drink(null, 100, 1.33333, 15.5, new Guid("00000000-0000-0000-0000-000000000003")));
            DatabaseManager.DatabaseManagerInstance.PostDrink(new Drink("", 600, 3, 45.99, new Guid("00000000-0000-0000-0000-000000000003")));
            DatabaseManager.DatabaseManagerInstance.PostDrink(new Drink ("TEST 3", 010, 0.2, 5, new Guid("00000000-0000-0000-0000-000000000099")));
        }
        */
        public void Add(Drink drink)
        {
            Drinks.Add(drink);
        }

        public ObservableCollection<Drink> Drinks
        {
            get => _drinks;
            set
            {
                _drinks = value;
                this.OnPropertyChanged();
            }
        }

        public string SearchEntry
        {
            get => _searchEntry;
            set
            {
                _searchEntry = value;
                this.OnPropertyChanged();
            }
        }

        public Drink SelectedDrink
        {
            get => _selectedDrink;
            set
            {
                _selectedDrink = value;
                if (_selectedDrink != null)
                {
                    EditDrink();
                }
                _selectedDrink = null;
            }
        }

        public DrinkHistory History
        {
            get => _history;
            set
            {
                _history = value;
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

        public String DateString
        {
            get => History.DateString;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task AddDrink()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddDrinkPage(new AddDrinkViewModel(SearchEntry, History)));
            SearchEntry = null;
        }

        private async void EditDrink()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditDrinkPage(new EditDrinkViewModel(SelectedDrink, History)));
        }

        private async Task OnPullToRefresh()
        {
            Refreshing = true;
            Drinks = new ObservableCollection<Drink>(await DatabaseManager.DatabaseManagerInstance.GetDrinks(History));
            Refreshing = false;
        }

        private async Task ClearDrinks()
        {
            bool result = true;

            if (History.Date == DateTime.Today)
                result = await Application.Current.MainPage.DisplayAlert("Remove all drinks from today", "Are you sure?", "Remove", "Cancel");
            else
                result = await Application.Current.MainPage.DisplayAlert("Remove all drinks from this day", "Are you sure?", "Remove", "Cancel");

            if (!result) { return; }

            await DatabaseManager.DatabaseManagerInstance.ClearDrinks(History);

            if (History.Date != DateTime.Today)
            {
                await DatabaseManager.DatabaseManagerInstance.RemoveHistory(History);
            }
        }
    }
}
