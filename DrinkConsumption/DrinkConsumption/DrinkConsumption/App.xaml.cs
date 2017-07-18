using DrinkConsumption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DrinkConsumption.Model;
using DrinkConsumption.View;
using DrinkConsumption.ViewModel;

using Xamarin.Forms;

namespace DrinkConsumption
{
	public partial class App : Application
	{

        DrinkHistoryViewModel _history;

        public App ()
		{
			InitializeComponent();

            _history = DrinkHistoryViewModel.NewHistory();

            MainPage = new CarouselPage()
            {
                Children =
                {
                    new MainPage(),
                    new HistoryPage(_history),
                }
            };
        }

		protected override void OnStart ()
		{
            _history.Add(new DrinkHistory(DateTime.Today, new DrinkViewModel().Drinks));
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
