using DrinkConsumption.Database;
using DrinkConsumption.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DrinkConsumption.ViewModel
{
    public class AddDrinkViewModel
    {
        private string _name;
        private double _volume;
        private double _stdDrinks;
        private double _price;
        private DrinkHistory _history;
        private ObservableCollection<Drink> _drinks;

        public ICommand AddDrinkCommand { get; private set; }

        public AddDrinkViewModel(string name, DrinkHistory history, ObservableCollection<Drink> drinks)
        {
            _name = name;
            _history = history;
            _drinks = drinks;

            AddDrinkCommand = new Command(async () => await AddDrink());
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public double Volume
        {
            get => _volume;
            set
            {
                _volume = value;
            }
        }

        public double StandardDrinks
        {
            get => _stdDrinks;
            set
            {
                _stdDrinks = value;
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
            }
        }

        public DrinkHistory History
        {
            get => _history;
        }

        public ObservableCollection<Drink> Drinks
        {
            get => _drinks;
        }

        private async Task AddDrink()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Whoops!", "You have not entered a drink", "OK");
                return;
            }

            Drink newDrink = new Drink(Name, Volume, StandardDrinks, Price, History.Guid);
            Drinks.Insert(0,newDrink);
            await DatabaseManager.DatabaseManagerInstance.PostDrink(newDrink);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
