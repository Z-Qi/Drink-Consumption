using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkConsumption
{
    public class Drink
    {
        private String _type { get; set; }
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
            get => String.Format("Volume: {0:N0}mL, \t\tStandard Drinks: {1:N1},\t\tCost: ${2:N2}", _volume, _stdDrinks, _price);
        }

    }

    
}
