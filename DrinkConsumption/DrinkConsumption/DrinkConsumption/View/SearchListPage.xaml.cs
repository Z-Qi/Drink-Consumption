using DrinkConsumption.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkConsumption.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchListPage : ContentPage
	{
        public SearchListPage(DrinkViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}