using DrinkConsumption.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkConsumption.Database
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;
        private MobileServiceClient _client;
        private IMobileServiceTable<Drink> _drinkTable;
        private IMobileServiceTable<DrinkHistory> _drinkHistoryTable;

        private DatabaseManager()
        {
            _client = new MobileServiceClient("http://drinkconsumption.azurewebsites.net");
            _drinkTable = _client.GetTable<Drink>();
            _drinkHistoryTable = _client.GetTable<DrinkHistory>();
        }

        public MobileServiceClient DatabaseClient
        {
            get => _client;
        }

        public static DatabaseManager DatabaseManagerInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseManager();
                }

                return _instance;
            }
        }

        public async Task<List<Drink>> GetDrinks()
        {
            return await _drinkTable.ToListAsync();
        }

        public async Task<List<Drink>> GetDrinks(DrinkHistory history)
        {
            return await _drinkTable.Where(d => d.Guid.ToString() == history.Guid.ToString()).ToListAsync();
        }

        public async Task PostDrink(Drink drink)
        {
            await _drinkTable.InsertAsync(drink);
        }

        public async Task EditDrink(Drink drink)
        {
            await _drinkTable.UpdateAsync(drink);
        }

        public async Task RemoveDrink(Drink drink)
        {
            await _drinkTable.DeleteAsync(drink);
        }

        public async Task ClearDrinks(DrinkHistory history)
        {
            List<Drink> drinks = await DatabaseManagerInstance.GetDrinks(history);
            foreach (Drink drink in drinks)
            {
                await _drinkTable.DeleteAsync(drink);
            }
        }

        public async Task<List<DrinkHistory>> GetHistory()
        {
            return await _drinkHistoryTable.ToListAsync();
        }

        public async Task<DrinkHistory> GetTodaysHistory()
        {
            List<DrinkHistory> histories = await DatabaseManagerInstance.GetHistory();
            DrinkHistory history = histories.FirstOrDefault((h => h.Date == DateTime.Today));
            if (history == null)
            {
                history = new DrinkHistory(DateTime.Today);
                await PostHistory(history);
            }
            return history;
        }

        public async Task PostHistory(DrinkHistory history)
        {
            await _drinkHistoryTable.InsertAsync(history);
        }

        public async Task RemoveHistory(DrinkHistory history)
        {
            await _drinkHistoryTable.DeleteAsync(history);
        }

        public async Task ClearHistory()
        {
            List<DrinkHistory> histories = await DatabaseManagerInstance.GetHistory();
            foreach (DrinkHistory history in histories)
            {
                await _drinkHistoryTable.DeleteAsync(history);
            }

            List<Drink> allDrinks = await DatabaseManagerInstance.GetDrinks();
            foreach (Drink drink in allDrinks)
            {
                await _drinkTable.DeleteAsync(drink);
            }
        }
    }
}
