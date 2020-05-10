using Rg.Plugins.Popup.Extensions;
using SDSD.Bootstrap;
using SDSD.Models;
using SDSD.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SDSD.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Vessels = new ObservableCollection<Vessel>();
            Crews = new ObservableCollection<Crew>();
            NavigateToAddVesselPageCommand = new Command(async () => await ExecuteNavigateToAddVesselPageCommand());
            NavigateToAddCrewPageCommand = new Command(async () => await ExecuteNavigateToAddCrewPageCommand());
            DeleteVesselCommand = new Command(async () => await ExecuteDeleteVesselCommand());
            GetVessels();
            GetCrews();
        }
        private string _username = Settings.Username.ToUpper();
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private async Task ExecuteNavigateToAddCrewPageCommand()
        {
            await Navigation.PushPopupAsync(new AddCrewPage());
        }
        
        Vessel _vesselSelectedItem;
        public Vessel VesselSelectedItem
        {
            get { return _vesselSelectedItem; }
            set { SetProperty(ref _vesselSelectedItem, value); }
        }
        private async Task ExecuteDeleteVesselCommand()
        {
            await App.Database.DeleteItemAsync(_vesselSelectedItem);
            
        }

        private async Task ExecuteNavigateToAddVesselPageCommand()
        {
            await Navigation.PushPopupAsync(new AddVesselPage());
        }

        public ObservableCollection<Vessel> Vessels { get; set; }
        public ObservableCollection<Crew> Crews { get; set; }
        public Command NavigateToAddVesselPageCommand { get; }
        public Command NavigateToAddCrewPageCommand { get; }
        public Command DeleteVesselCommand { get; }
        void GetVessels()
        {
            var r =  App.Database.GetItemsAsync().Result;
            if (r != null)
            {
                foreach (var item in r)
                {
                    Vessels.Add(item);
                }
                SetVesselBackground();
            }
            
        }

        void GetCrews()
        {
            var r = App.Database.GetCrewItemsAsync().Result;
            if (r != null)
            {
                foreach (var item in r)
                {
                    item.firstName = item.firstName + " " + item.lastName;
                    Crews.Add(item);
                }
                SetCrewImage();
            }

        }
        void SetVesselBackground()
        {
            for (int i = 0; i < Vessels.Count(); i++)
            {
                if (IsOddNumber(i))
                    Vessels[i].vesselBackground = "mask.png";
                else
                    Vessels[i].vesselBackground = "mask2.png";
            }
        }

        void SetCrewImage()
        {
            for (int i = 0; i < Crews.Count(); i++)
            {
                if (Crews[i].role == "Master")
                    Crews[i].crewImage = "mcdonalds.png";
                else if(Crews[i].role == "Chief Engineer")
                    Crews[i].crewImage = "careem.png";
                else if (Crews[i].role == "Technical Manager")
                    Crews[i].crewImage = "centrepoint.png";
                else
                    Crews[i].crewImage = "starbucks.png";
            }
        }
        public void SubscribeAddVessel()
        {
            MessagingCenter.Subscribe<AddVesselPageViewModel, Vessel>(this, "addVessel", (s, param) =>
            {
                Vessels.Add(param);
                SetVesselBackground();
            });
        }
        public void SubscribeAddCrew()
        {
            MessagingCenter.Subscribe<AddCrewPageViewModel, Crew>(this, "addCrew", (s, param) =>
            {
                param.firstName = param.firstName + " " + param.lastName;
                Crews.Add(param);
                SetCrewImage();
            });
        }
        public  bool IsOddNumber(int value)
        {
            return value % 2 != 0;
        }
    }
}
