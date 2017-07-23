using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DrinkConsumption.Model
{
    public class DrinkHistory
    {
        private List<Drink> _drinks;
        private DateTime _date;
        private Guid _guid;

        public DrinkHistory(DateTime date)
        {
            _drinks = new List<Drink>();
            _date = date;
            _guid = Guid.NewGuid();
        }

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
            }
        }

        [JsonProperty(PropertyName = "Guid")]
        public Guid Guid
        {
            get => _guid;
            set { }
        }

        public String DateString
        {
            get => Date == DateTime.Today ? "Today" : Date.ToString("dd/MM/yyyy");
        }

        [JsonIgnore]
        public List<Drink> Drinks
        {
            get => _drinks;
            set
            {
                _drinks = value;
            }
        }

        public void Add(Drink drink)
        {
            Drinks.Add(drink);
        }

        public void Remove(Drink drink)
        {
            Drinks.Remove(drink);
        }
    }
}
