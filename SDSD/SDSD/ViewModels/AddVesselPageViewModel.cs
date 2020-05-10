using Rg.Plugins.Popup.Services;
using SDSD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SDSD.ViewModels
{
    public class AddVesselPageViewModel : BaseViewModel
    {
        public AddVesselPageViewModel()
        {
            VesselTypes = new ObservableCollection<VesselType>();
            AddVesselCommand = new Command(async () => await ExecuteAddVesselCommand());
            ClosePopUpCommand = new Command(async () => await ExecuteClosePopUpCommand());
        }

        
        public Command AddVesselCommand { get; }
        public Command ClosePopUpCommand { get; }
        public ObservableCollection<VesselType> VesselTypes { get; set; }

        private string _vesselName;
        public string VesselName
        {
            get { return _vesselName; }
            set { SetProperty(ref _vesselName, value); }
        }
        private string _regNumber;
        public string RegistrationNumber
        {
            get { return _regNumber; }
            set { SetProperty(ref _regNumber, value); }
        }

        private string _vesselType;
        public string VesselType
        {
            get { return _vesselType; }
            set { SetProperty(ref _vesselType, value); }
        }
        private async Task ExecuteAddVesselCommand()
        {
            if (string.IsNullOrEmpty(VesselName))
            {
                await DisplayAlert("Information", "Enter the name!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(RegistrationNumber))
            {
                await DisplayAlert("Information", "Enter the Registration number!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(VesselType))
            {
                await DisplayAlert("Information", "Select Vessel Type!", "OK");
                return;
            }

            var confirm = await DisplayAlert("Confirmation", "Confirm registration?", "YES", "NO");
            if (confirm)
            {
                var vessel = new Vessel()
                {
                    vesselRegNumber = RegistrationNumber,
                    vesselName = VesselName,
                    vesselType = VesselType
                };

                var response = await App.Database.SaveItemAsync(vessel);
                if (response > 0)
                    MessagingCenter.Send(this, "addVessel", vessel);

                await PopupNavigation.Instance.PopAsync(true);
            }
        }

        private async Task ExecuteClosePopUpCommand()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

    }
}
