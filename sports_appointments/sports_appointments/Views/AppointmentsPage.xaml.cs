using sports_appointments.Models;
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
    public partial class AppointmentsPage : ContentPage
    {
        private AppointmentsViewModel _viewModel;

        public AppointmentsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AppointmentsViewModel();
        }
        
    }
}