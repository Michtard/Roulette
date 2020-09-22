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
    public partial class ScorePage : ContentPage
    {
        public ScorePage()
        {
            InitializeComponent();
            var users =  DAL.GetUserScoreList();

            for (int i = 0; i < users.Count(); i++)
            {
                users.ElementAt(i).Classement = i + 1;
            }

            maListeView.ItemsSource = users;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
}