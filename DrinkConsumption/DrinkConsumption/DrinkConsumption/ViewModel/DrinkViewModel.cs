using DrinkConsumption.Database;
using DrinkConsumption.Model;
using DrinkConsumption.View;
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

        private bool _isRefreshing = false;

        public ICommand AddDrinkCommand { get; private set; }
        public ICommand PullToRefreshCommand { get; private set; }
        public ICommand ClearDrinksCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DrinkViewModel()
        {
            _drinks = new ObservableCollection<Drink>();
            //TestSample();
            AddDrinkCommand = new Command(async () => await AddDrink());
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());
            ClearDrinksCommand = new Command(async () => await ClearDrinks());
        }

        private void TestSample()
        {
            Drinks.Add(new Drink("TEST 1", 100, 1.33333, 15.5));
            Drinks.Add(new Drink("TEST 2", 600, 3, 45.99));
            Drinks.Add(new Drink("TEST 3", 010, 0.2, 5));
            Drinks.Add(new Drink(null, 100, 1.33333, 15.5));
            Drinks.Add(new Drink("", 600, 3, 45.99));
            Drinks.Add(new Drink("TEST 3", 010, 0.2, 5));
            Drinks.Add(new Drink("TEST 1", 100, 1.33333, 15.5));
            Drinks.Add(new Drink("TEST 2", 600, 3, 45.99));
            Drinks.Add(new Drink("TEST 3", 010, 0.2, 5));
            Drinks.Add(new Drink("TEST 1", 100, 1.33333, 15.5));
            Drinks.Add(new Drink("TEST 2", 600, 3, 45.99));
            Drinks.Add(new Drink("TEST 3", 010, 0.2, 5));
            Drinks.Add(new Drink("TEST 1", 100, 1.33333, 15.5));
            Drinks.Add(new Drink("TEST 2", 600, 3, 45.99));
            Drinks.Add(new Drink("TEST 3", 010, 0.2, 5));
            Drinks.Add(new Drink("TEST 1", 100, 1.33333, 15.5));
            Drinks.Add(new Drink("TEST 2", 600, 3, 45.99));
            Drinks.Add(new Drink("TEST 3", 010, 0.2, 5));
        }

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

        public string SearchEntry
        {
            get => _searchEntry;
            set
            {
                _searchEntry = value;
                this.OnPropertyChanged();
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

        private async Task AddDrink()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddDrinkPage(new AddDrinkViewModel(SearchEntry)));
            SearchEntry = null;
        }

        private async void EditDrink()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditDrinkPage(new EditDrinkViewModel(SelectedDrink)));
        }

        private async Task OnPullToRefresh()
        {
            Refreshing = true;
            Drinks = new ObservableCollection<Drink>(await DatabaseManager.DatabaseManagerInstance.GetDrinks());
            Refreshing = false;
        }

        private async Task ClearDrinks()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("Remove all Drinks", "Are you sure?", "Remove", "Cancel");
            if (!result)
            {
                return;
            }
            await DatabaseManager.DatabaseManagerInstance.ClearDrinks();
        }
    }
}
