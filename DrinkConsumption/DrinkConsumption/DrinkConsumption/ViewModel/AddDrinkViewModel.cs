using DrinkConsumption.Database;
using DrinkConsumption.Model;
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

        public ICommand AddDrinkCommand { get; private set; }

        public AddDrinkViewModel(string name)
        {
            _name = name;
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

        private async Task AddDrink()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Whoops!", "You have not entered a drink", "OK");
                return;
            }
            await DatabaseManager.DatabaseManagerInstance.PostDrink(new Drink(Name, Volume, StandardDrinks, Price));
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
