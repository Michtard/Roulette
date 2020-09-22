using Roulette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Roulette.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage(string access)
        {
            InitializeComponent();

            if (access == "xx")
                BtnAdmin.IsVisible = true;
            else
                BtnAdmin.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void PlayGame_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new GamePage());
        }

        private void ListScores_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ScorePage());
        }

        private void Admin_Clicked(object sender, EventArgs e)
        {
            DAL.ResetUserTable();
        }
    }
}