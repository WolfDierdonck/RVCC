using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RVCC;

namespace RVCC
{
    public partial class App : Application
    {

        public static MainViewModel ViewModel { get; set; }
        public App()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            MainPage = new MainPage();
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
