using DrinkConsumption.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkConsumption.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryDetailsPage : ContentPage
    {
        public HistoryDetailsPage(DrinkHistory history)
        {
            InitializeComponent();
            BindingContext = history;
        }
    }
}