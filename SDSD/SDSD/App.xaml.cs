using Plugin.SharedTransitions;
using SDSD.Bootstrap;
using SDSD.Views;
using Syncfusion.Licensing;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//initializing app fonts
[assembly: ExportFont("Poppins-Light.ttf", Alias = "ThemeFontLight")]
[assembly: ExportFont("Poppins-Medium.ttf", Alias = "ThemeFontMedium")]
[assembly: ExportFont("Poppins-Bold.ttf", Alias = "ThemeFontBold")]
[assembly: ExportFont("Poppins-SemiBold.ttf", Alias = "ThemeFontSemiBold")]
namespace SDSD
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           
            SyncfusionLicenseProvider.RegisterLicense("MjU0NTA1QDMxMzcyZTM0MmUzMEFQZTFGUkJ3SFFsYWtlaXc0NVdnUWswOHdnYWs5a3FMMnlwRzI0OGF0c1U9");
            //MainPage = new MainPage();
            MainPage = new SharedTransitionNavigationPage(new LoginPage());
        }
        // creating an instance of the SDSDItemDatabase class
        static SDSDItemDatabase database;
        public static SDSDItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SDSDItemDatabase();
                }
                return database;
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
