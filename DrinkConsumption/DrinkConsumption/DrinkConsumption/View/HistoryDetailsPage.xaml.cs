using DrinkConsumption.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkConsumption.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryDetailsPage : ContentPage
    {
        public HistoryDetailsPage(DrinkViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}