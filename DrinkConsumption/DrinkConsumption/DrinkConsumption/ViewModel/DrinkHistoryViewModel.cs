using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DrinkConsumption.Model;
using DrinkConsumption.View;
using System.ComponentModel;
using Xamarin.Forms;

namespace DrinkConsumption.ViewModel
{
    public class DrinkHistoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DrinkHistory> _drinkHistory;
        private static DrinkHistoryViewModel _history = null;
        private DrinkHistory _selectedHistory;

        public event PropertyChangedEventHandler PropertyChanged;

        private DrinkHistoryViewModel()
        {
            _drinkHistory = new ObservableCollection<DrinkHistory>();
            this.TestSample();
        }

        private void TestSample()
        {
            History.Add(new DrinkHistory(DateTime.Today, new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
            History.Add(new DrinkHistory(new DateTime(1990, 11, 2), new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
            History.Add(new DrinkHistory(new DateTime(2017, 11, 2), new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
            History.Add(new DrinkHistory(new DateTime(1992, 11, 2), new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
            History.Add(new DrinkHistory(new DateTime(1524, 11, 2), new ObservableCollection<Drink> { new Drink("history test", 1, 1, 1), new Drink("history test 2", 1, 1, 1) }));
        }

        public static DrinkHistoryViewModel NewHistory()
        {
            if (_history == null)
            {
                _history = new DrinkHistoryViewModel();
            }
            return _history;
        }

        public void Add(DrinkHistory drinkList)
        {
            if (!History.Any(h => h.Date == drinkList.Date))
            {
                History.Add(drinkList);
            }
        }

        public ObservableCollection<DrinkHistory> History
        {
            get => _drinkHistory;
        }

        public DrinkHistory SelectedHistory
        {
            get => _selectedHistory;
            set
            {
                _selectedHistory = value;
                if (_selectedHistory != null)
                {
                    this.HistoryDetails();
                }
                _selectedHistory = null;
            }
        }

        private void HistoryDetails()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new HistoryDetailsPage(SelectedHistory));
        }
    }
}
