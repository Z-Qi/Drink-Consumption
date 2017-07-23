using DrinkConsumption.Database;
using DrinkConsumption.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DrinkConsumption.ViewModel
{
    public class EditDrinkViewModel
    {
        private string _name;
        private double _volume;
        private double _stdDrinks;
        private double _price;
        private Drink _selectedDrink;
        private DrinkHistory _history;

        public ICommand EditDrinkCommand { get; private set; }
        public ICommand RemoveDrinkCommand { get; private set; }

        public EditDrinkViewModel(Drink drink, DrinkHistory history)
        {
            _name = drink.Type;
            _volume = drink.Volume;
            _stdDrinks = drink.StandardDrinks;
            _price = drink.Price;
            _selectedDrink = drink;
            _history = history;

            EditDrinkCommand = new Command(async () => await EditDrink());
            RemoveDrinkCommand = new Command(async () => await RemoveDrink());
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

        public Drink SelectedDrink
        {
            get => _selectedDrink;
            set
            {
                _selectedDrink = value;
            }
        }

        public DrinkHistory History
        {
            get => _history;
        }

        private async Task EditDrink()
        {
            SelectedDrink.Type = Name;
            SelectedDrink.Volume = Volume;
            SelectedDrink.StandardDrinks = StandardDrinks;
            SelectedDrink.Price = Price;
            await DatabaseManager.DatabaseManagerInstance.EditDrink(SelectedDrink);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private async Task RemoveDrink()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("Remove Drink", "Are you sure?", "Remove", "Cancel");
            if (!result)
            {
                return;
            }
            await Application.Current.MainPage.Navigation.PopModalAsync();
            History.Remove(SelectedDrink);
            await DatabaseManager.DatabaseManagerInstance.RemoveDrink(SelectedDrink);
        }
    }
}
