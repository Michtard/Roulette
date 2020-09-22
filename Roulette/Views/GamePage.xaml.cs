using Roulette.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Roulette.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private readonly User user = new User();
        private string colorSelectByUser = "";
        private string mise = "";

        public GamePage()
        {
            InitializeComponent();
            DAL.SetSectorColor();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Content.IsEnabled = false;

            user.Credit = 200;

            user.Name = await DisplayPromptAsync(null, "Entrer votre nom de joueur", "OK", null);

            while (user.Name == "" || user.Name == null)
            {
                await DisplayAlert("Attention", "Entrer un nom", "OK");
                user.Name = await DisplayPromptAsync(null, "Entrer votre nom de joueur", "OK", null);
            }
            VerifAdmin(user.Name);
            lblUserName.Text = user.Name;

            PopUpMise();

            Content.IsEnabled = true;
        }

        private async void PlayGame_Clicked(object sender, EventArgs e)
        {
            btnLancerRoue.IsEnabled = false;

            if (colorSelectByUser != "")
            {
                Random random = new Random();
                int aleatoireRotate = random.Next(5, 356);

                await roulette.RotateTo((aleatoireRotate + 720), 3000, Easing.SinOut);

                await Task.Delay(100);

                if (colorSelectByUser != DAL.FindColor(aleatoireRotate))
                {
                    user.Credit += int.Parse(mise) * 2;
                    lblCredit.Text = user.Credit.ToString();

                    PopUpTryAgain("Gagné");
                }

                else
                {
                    PopUpTryAgain("Perdu");
                }
            }
            else
                await DisplayAlert("Attention", "Veuillez sélectionner une couleur", "OK");

            btnLancerRoue.IsEnabled = true;

        }

        private void RedBouton_Clicked(object sender, EventArgs e)
        {
            RedBouton.BorderColor = Color.Yellow;
            BlackBouton.BorderColor = Color.FromHex("#5e4138");
            colorSelectByUser = "red";
        }

        private void BlackBouton_Clicked(object sender, EventArgs e)
        {
            BlackBouton.BorderColor = Color.Yellow;
            RedBouton.BorderColor = Color.FromHex("#5e4138");
            colorSelectByUser = "black";
        }

        public async void VerifAdmin(string user)
        {
            if (user == "kk")
            {
                string mdp = await DisplayPromptAsync("MDP", "admin", "OK", null);

                if (mdp == "xx")
                {
                    await Navigation.PushAsync(new HomePage(mdp));
                }
            }
        }

        public async void PopUpMise()
        {
            mise = await DisplayPromptAsync($"Vous avez {user.Credit} credits", "Quel est votre mise?", "OK", null);
            while (mise == "" || mise == "0" || mise == null || int.Parse(mise) > user.Credit)
            {
                await DisplayAlert("Attention", "Mise incorrect", "OK");
                mise = await DisplayPromptAsync($"Vous avez {user.Credit} credits", "Quel est votre mise?", "OK", null);
            }

            lblMise.Text = mise;
            user.Credit -= int.Parse(mise);
            lblCredit.Text = user.Credit.ToString();
        }

        public async void PopUpTryAgain(string result)
        {
            lblMise.Text = "0";

            if (user.Credit > 0)
            {
                bool answer = await DisplayAlert(result, "Voulez vous rejouer?", "Oui", "Non");

                if (answer)
                {
                    PopUpMise();
                }
                else
                {
                    bool answer2 = await DisplayAlert(null, "Voulez-vous enregistré votre score?", "Oui", "Non");

                    if (answer2)
                    {
                        DAL.SetUser(user);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await Navigation.PopAsync();
                    }
                }
            }
            else
            {
                await DisplayAlert("Perdu", "Navré vous n'avez plus de crédit", "Retour au Menu Principal");
                await Navigation.PopAsync();
            }

            roulette.Rotation = 0;
        }
    }
}