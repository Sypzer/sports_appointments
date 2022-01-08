using sports_appointments.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace sports_appointments.ViewModels
{
    [QueryProperty(nameof(CourtId), nameof(CourtId))]
    public class CourtDetailViewModel : BaseViewModel
    {
        private int courtId;
        private string name;
        private string address;
        private int capacity;
        public string Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public int Capacity
        {
            get => capacity;
            set => SetProperty(ref capacity, value);
        }
        public int CourtId
        {
            get
            {
                return courtId;
            }
            set
            {
                courtId = value;
                LoadCourtId(value);
            }
        }

        public async void LoadCourtId(int courtId)
        {
            try
            {
                var item = await CourtService.GetCourt(courtId);
                CourtId = item.courtId;
                Name = item.name;
                Address = item.address;
                Capacity = item.capacity;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}

