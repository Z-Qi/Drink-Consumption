using Xamarin.Forms;

namespace DrinkConsumption.View
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = new ViewModel.DrinkViewModel();
		}
	}
}
