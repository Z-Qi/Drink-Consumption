using DrinkConsumption.Database;
using DrinkConsumption.Model;
using DrinkConsumption.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Web;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace DrinkConsumption.ViewModel
{
    public class DrinkViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Drink> _drinks;
        private string _searchEntry;
        private SearchSuggestion _selectedSearch;
        private Drink _selectedDrink;
        private DateTime _date;
        private DrinkHistory _history;
        private List<SearchSuggestion> _suggestions;

        private bool _isRefreshing;

        public ICommand MakeRequestCommand { get; private set; }
        public ICommand AddDrinkCommand { get; private set; }
        public ICommand ClearDrinksCommand { get; private set; }
        public ICommand PullToRefreshCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DrinkViewModel()
        {
            _drinks = new ObservableCollection<Drink>();
            _date = DateTime.Today;

            MakeRequestCommand = new Command(async () => await MakeRequest());
            AddDrinkCommand = new Command(async () => await AddDrink());
            ClearDrinksCommand = new Command(async () => await ClearDrinks());
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());

            PullToRefreshCommand.Execute(null);
            _isRefreshing = false;
        }

        public DrinkViewModel(DrinkHistory history)
        {
            _drinks = new ObservableCollection<Drink>();
            _date = history.Date;
            _history = history;

            MakeRequestCommand = new Command(async () => await MakeRequest());
            AddDrinkCommand = new Command(async () => await AddDrink());
            PullToRefreshCommand = new Command(async () => await OnPullToRefresh());
            ClearDrinksCommand = new Command(async () => await ClearDrinks());

            PullToRefreshCommand.Execute(null);
            _isRefreshing = false;
        }

        public ObservableCollection<Drink> Drinks
        {
            get => _drinks;
            set
            {
                _drinks = value;
                OnPropertyChanged();
            }
        }

        public string SearchEntry
        {
            get => _searchEntry;
            set
            {
                _searchEntry = value;
                OnPropertyChanged();
            }
        }

        public SearchSuggestion SelectedSearch
        {
            get => _selectedSearch;
            set
            {
                _selectedSearch = value;
                if (value != null)
                {
                    RequestedDrink();
                }
                OnPropertyChanged();
            }
        }

        public Drink SelectedDrink
        {
            get => _selectedDrink;
            set
            {
                _selectedDrink = value;
                if (value != null)
                {
                    EditDrink();
                }
                _selectedDrink = null;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _date;
        }

        public DrinkHistory History
        {
            get => _history;
            set
            {
                _history = value;
            }
        }

        public List<SearchSuggestion> Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value;
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

        private async Task MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "333c22b2e5e24c0f8dde963849946f3a");
            queryString["q"] = SearchEntry;
            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/suggestions/?" + queryString;

            var response = await client.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();
            Autosuggest results = JsonConvert.DeserializeObject<Autosuggest>(responseString);
            List<Suggestion> groups = results.SuggestionGroups;
            Suggestions = groups.FirstOrDefault(s => string.Equals(s.Name, "Web", StringComparison.OrdinalIgnoreCase)).SearchSuggestions;

            await Application.Current.MainPage.Navigation.PushModalAsync(new SearchListPage(this));
            SearchEntry = null;
        }

        private async Task AddDrink()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddDrinkPage(new AddDrinkViewModel(SearchEntry, History, Drinks)));
            SearchEntry = null;
        }

        private async void RequestedDrink()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddDrinkPage(new AddDrinkViewModel(SelectedSearch.DisplayText, History, Drinks)));
            SelectedSearch = null;
        }

        private async void EditDrink()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditDrinkPage(new EditDrinkViewModel(SelectedDrink, Drinks)));
        }

        private async Task ClearDrinks()
        {
            bool remove = false;

            if (History.Date == DateTime.Today)
                remove = await Application.Current.MainPage.DisplayAlert("Remove all drinks from today", "Are you sure?", "Remove", "Cancel");
            else
                remove = await Application.Current.MainPage.DisplayAlert("Remove all drinks from this day", "Are you sure?", "Remove", "Cancel");

            if (!remove)
            {
                return;
            }

            await DatabaseManager.DatabaseManagerInstance.ClearDrinks(History);

            if (History.Date != DateTime.Today)
            {
                await DatabaseManager.DatabaseManagerInstance.RemoveHistory(History);
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        private async Task OnPullToRefresh()
        {
            Refreshing = true;
            History = await DatabaseManager.DatabaseManagerInstance.GetHistory(Date);
            Drinks = new ObservableCollection<Drink>((await DatabaseManager.DatabaseManagerInstance.GetDrinks(History)).OrderByDescending(d => d.Time).ToList());
            Refreshing = false;
        }
    }
}
