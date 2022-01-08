using sports_appointments.ViewModels;
using Xamarin.Forms;

namespace sports_appointments.Views
{
    public partial class CourtDetailPage : ContentPage
    {
        public CourtDetailPage()
        {
            InitializeComponent();
            BindingContext = new CourtDetailViewModel();
        }
    }
}