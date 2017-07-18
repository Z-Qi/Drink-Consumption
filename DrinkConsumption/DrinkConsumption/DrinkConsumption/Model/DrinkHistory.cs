using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

        public ObservableCollection<Drink> History
        {
            get => _drinkHistory;
        }

        public String Date
        {
            get => _date.ToString("dd/MM/yyyy");
        }
    }
}
