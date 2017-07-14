using DrinkConsumption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DrinkConsumption.View;
using DrinkConsumption.ViewModel;

using Xamarin.Forms;

namespace DrinkConsumption
{
	public partial class App : Application
	{

        DrinksHistoryViewModel _history;

        public App ()
		{
			InitializeComponent();

            _history = DrinksHistoryViewModel.newHistory();

            MainPage = new CarouselPage()
            {
                Children =
                {
                    new MainPage(),
                    new HistoryPage(_history),
                    new HistoryDetailsPage(_history, new DateTime(2017,07,14,0,0,0))
                }
            };
        }

		protected override void OnStart ()
		{
            _history.add(DateTime.Today, new DrinksViewModel().Drinks);
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
