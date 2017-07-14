using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrinkConsumption.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkConsumption.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage (DrinksHistoryViewModel historyModel)
		{
			InitializeComponent ();
            BindingContext = historyModel;
		}
	}
}