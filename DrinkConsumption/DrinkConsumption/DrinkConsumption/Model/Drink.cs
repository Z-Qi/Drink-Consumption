using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkConsumption
{
    public class Drink
    {
        private string _type;
        private double _volume;
        private double _stdDrinks;
        private double _price;

        public Drink (String type, double vol, double std, double price)
        {
            _type = type;
            _volume = vol;
            _stdDrinks = std;
            _price = price;
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
            }
        }

        public String Description
        {
            get => $"Volume: {_volume:N0}mL, \t\tStandard Drinks: {_stdDrinks:N1},\t\tCost: ${_price:N2}";
        }

    }

    
}
