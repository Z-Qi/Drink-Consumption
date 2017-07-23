using DrinkConsumption.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkConsumption.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent ();
            BindingContext = DrinkHistoryViewModel.HistoryInstance();
		}
	}
}