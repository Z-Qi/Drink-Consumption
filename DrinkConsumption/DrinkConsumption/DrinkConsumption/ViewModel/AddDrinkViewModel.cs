using DrinkConsumption.Database;
using DrinkConsumption.Model;
using System;
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

        public ICommand AddDrinkCommand { get; private set; }

        public AddDrinkViewModel(string name, DrinkHistory history)
        {
            _name = name;
            _history = history;
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

        private async Task AddDrink()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Whoops!", "You have not entered a drink", "OK");
                return;
            }
            await Application.Current.MainPage.Navigation.PopModalAsync();
            Drink newDrink = new Drink(Name, Volume, StandardDrinks, Price, History.Guid);
            History.Add(newDrink);
            await DatabaseManager.DatabaseManagerInstance.PostDrink(newDrink);
        }
    }
}
