using Rg.Plugins.Popup.Pages;
using SDSD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SDSD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddVesselPage : PopupPage
    {
        public AddVesselPage()
        {
            InitializeComponent();
            BindingContext = new AddVesselPageViewModel();
        }
    }
}