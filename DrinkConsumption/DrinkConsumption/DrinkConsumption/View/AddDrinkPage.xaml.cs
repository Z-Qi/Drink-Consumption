using DrinkConsumption.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkConsumption.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDrinkPage : ContentPage
	{
		public AddDrinkPage (AddDrinkViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
		}
	}
}