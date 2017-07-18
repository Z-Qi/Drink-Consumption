using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DrinkConsumption.Database
{
    public class DatabaseManager
    {

        private static DatabaseManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<Drink> drinkTable;

        private DatabaseManager()
        {
            client = new MobileServiceClient("http://drinkconsumption.azurewebsites.net");
            drinkTable = client.GetTable<Drink>();
        }

        public MobileServiceClient DatabaseClient
        {
            get
            {
                return client;
            }
        }

        public static DatabaseManager DatabaseManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseManager();
                }

                return instance;
            }
        }
        
        public async Task<List<Drink>> GetDrinks()
        {
            return await drinkTable.ToListAsync();
        }

    }
}
