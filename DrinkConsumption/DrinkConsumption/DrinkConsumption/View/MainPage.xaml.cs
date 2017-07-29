using DrinkConsumption.ViewModel;
using Xamarin.Forms;

namespace DrinkConsumption.View
{
    public partial class MainPage : ContentPage
	{
		public MainPage(DrinkViewModel viewModel)
		{
			InitializeComponent();
            BindingContext = viewModel;
		}
	}
}
