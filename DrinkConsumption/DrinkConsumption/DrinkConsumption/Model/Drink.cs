using Newtonsoft.Json;
using System;

namespace DrinkConsumption.Model
{
    public class Drink
    {
        private string _type;
        private double _volume;
        private double _stdDrinks;
        private double _price;
        private readonly Guid _guid;
        private DateTime _time;

        public Drink (String type, double vol, double std, double price, Guid guid)
        {
            _type = type;
            _volume = vol;
            _stdDrinks = std;
            _price = price;
            _guid = guid;
            _time = DateTime.UtcNow;
        }

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
            }
        }

        [JsonProperty(PropertyName = "Volume")]
        public double Volume
        {
            get => _volume;
            set
            {
                _volume = value;
            }
        }

        [JsonProperty(PropertyName = "StandDrinks")]
        public double StandardDrinks
        {
            get => _stdDrinks;
            set
            {
                _stdDrinks = value;
            }
        }

        [JsonProperty(PropertyName = "Price")]
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
            }
        }

        [JsonProperty(PropertyName = "Guid")]
        public Guid Guid
        {
            get => _guid;
            set { }
        }

        [JsonProperty(PropertyName = "Time")]
        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
            }
        }

        public String Description
        {
            get => $"Volume: {_volume:N0}mL\t\tStandard Drink: {_stdDrinks:N1}\t\tCost: ${_price:N2}";
        }
    }

    
}
