using sports_appointments.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace sports_appointments.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourtsPage : ContentPage
    {
        private CourtViewModel _viewModel;

        public CourtsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CourtViewModel();
        }
    }
}