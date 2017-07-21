using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace DrinkConsumption.Model
{
    public class DrinkHistory
    {
        private ObservableCollection<Drink> _drinkHistory;
        private DateTime _date;

        public DrinkHistory(DateTime date, ObservableCollection<Drink> history)
        {
            _drinkHistory = history;
            _date = date;
        }

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "History")]
        public ObservableCollection<Drink> History
        {
            get => _drinkHistory;
            set
            {
                _drinkHistory = value;
            }
        }

        [JsonProperty(PropertyName = "Date")]
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
            }
        }

        [JsonIgnore]
        public String DateString
        {
            get => Date.ToString("dd/MM/yyyy");
        }
    }
}
