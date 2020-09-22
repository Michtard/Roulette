using Roulette.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Roulette
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage("ok"));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
