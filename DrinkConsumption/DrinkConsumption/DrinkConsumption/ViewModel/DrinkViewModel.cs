using DrinkConsumption.Database;
using DrinkConsumption.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DrinkConsumption.ViewModel
{
    public class DrinkViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Drink> _drinks;
        private string _searchEntry;

        public ICommand AddDrinkCommand { get; private set; }
        public ICommand PullToRefreshCommand { get; private set; }

        private bool _isRefreshing = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public DrinkViewModel()
        {
            _drinks = new ObservableCollection<Drink>();
            this.TestSample();
            AddDrinkCommand = new Command(async () => await AddDrinkAsync());
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());
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

        private async Task AddDrinkAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddDrinkPage(new AddDrinkViewModel(SearchEntry)));
            SearchEntry = null;
        }

        private async Task OnPullToRefresh()
        {
            Refreshing = true;
            List<Drink> drinks = await DatabaseManager.DatabaseManagerInstance.GetDrinks();

            Drinks = new ObservableCollection<Drink>(drinks);
            Refreshing = false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public bool Refreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                this.OnPropertyChanged();
            }
        }


    }
}
