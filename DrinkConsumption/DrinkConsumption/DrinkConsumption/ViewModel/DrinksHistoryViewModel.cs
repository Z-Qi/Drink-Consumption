using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DrinkConsumption.ViewModel
{
    public class DrinksHistoryViewModel
    {
        private SortedDictionary<DateTime, List<Drink>> _drinksHistory;
        private static DrinksHistoryViewModel _history = null;

        private DrinksHistoryViewModel()
        {
            _drinksHistory = new SortedDictionary<DateTime, List<Drink>>(Comparer<DateTime>.Create((a, b) => b.CompareTo(a)));
            this.testSample();
        }

        private void testSample()
        {
            History.Add(DateTime.Today, new List<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) });
            History.Add(new DateTime(1990, 11, 2), new List<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) });
            History.Add(new DateTime(2017, 11, 2), new List<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) });
            History.Add(new DateTime(1992, 11, 2), new List<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) });
            History.Add(new DateTime(1524, 11, 2), new List<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) });
        }

        public static DrinksHistoryViewModel newHistory()
        {
            if (_history == null)
            {
                _history = new DrinksHistoryViewModel();
            }
            return _history;
        }

        public void add(DateTime date, List<Drink> drinksToday)
        {
            if (date == DateTime.Today)
            {
                if (!History.ContainsKey(DateTime.Today))
                {
                    History.Add(DateTime.Today, drinksToday);
                }
            }
        }

        public SortedDictionary<DateTime, List<Drink>> History
        {
            get => _drinksHistory;
            set
            {
                _drinksHistory = value;
            }
        }
        
        public List<DateTime> Date
        {
            get => _drinksHistory.Keys.ToList();
        }

        public List<Drink> details(DateTime date)
        {
            return History[date];
        }

        /*
        public String Day
        {
            get => ((DateTime)this.Date.ToString("d");
        }*/
    }
}
