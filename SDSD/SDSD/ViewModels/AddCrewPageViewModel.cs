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
    public class AddCrewPageViewModel : BaseViewModel
    {
        #region Ctor
        public AddCrewPageViewModel()
        {
            VesselTypes = new List<string>();
            AddCrewCommand = new Command(async () => await ExecuteAddCrewCommand());
            ClosePopUpCommand = new Command(async () => await ExecuteClosePopUpCommand());
            GetVesselType();
        }
        #endregion

        #region Close Pop Up Method
        private async Task ExecuteClosePopUpCommand()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion

        #region Add Crew Method
        private async Task ExecuteAddCrewCommand()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await DisplayAlert("Information", "Enter the name!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await DisplayAlert("Information", "Enter the Last name!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(Age))
            {
                await DisplayAlert("Information", "Select Age!", "OK");
                return;
            }
            if (string.IsNullOrEmpty(Role))
            {
                await DisplayAlert("Information", "Select Role!", "OK");
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
                var crew = new Crew()
                {
                    firstName = FirstName,
                    lastName = LastName,
                    age = int.Parse(Age),
                    role = Role,
                    vesselType = VesselType
                };

                var response = await App.Database.SaveCrewItemAsync(crew);
                if (response > 0)
                    MessagingCenter.Send(this, "addCrew", crew);

                await PopupNavigation.Instance.PopAsync(true);
            }
        }
        #endregion

        #region Global Variables
        public List<string> VesselTypes { get; set; }
        public Command AddCrewCommand { get; }
        public Command ClosePopUpCommand { get; }
        #endregion

        #region Load Vessel Type from SQL DB
        void GetVesselType()
        {
            var r = App.Database.GetItemsAsync().Result;
            foreach (var item in r)
            {
                VesselTypes.Add(item.vesselName);
            }
        }
        #endregion

        #region Crew Params
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        private string _age;
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }
        private string _role;
        public string Role
        {
            get { return _role; }
            set { SetProperty(ref _role, value); }
        }
        private string _vesselType;
        public string VesselType
        {
            get { return _vesselType; }
            set { SetProperty(ref _vesselType, value); }
        }
        #endregion

    }
}
