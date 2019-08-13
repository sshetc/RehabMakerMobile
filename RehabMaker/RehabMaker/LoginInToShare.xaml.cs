using RehabMaker.ViewModels;
using RehabMaker.Services;
using RehabMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace RehabMaker 
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginInToShare : ContentPage
	{
        

        public LoginInToShare()
        {
            InitializeComponent();
            LogoRM.Source = ImageSource.FromResource("RehabMaker.Picture.logo_svg.png");
            LogItSh.Source = ImageSource.FromResource("RehabMaker.Picture.LoginInToShare.png");
            RehabMker.Source = ImageSource.FromResource("RehabMaker.Picture.RehabMaker.png");
        }

       
        private async void buttongoogle_clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Error", "ErrorPlugins","OK");
        }
        private async void buttonFacebook_clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Views.FacebookProfileCsPage());
        }
       
        private async void buttonTwitter_clicked(object sender, EventArgs e)
        {
            //var auth = new OAuth1Authenticator();
            await DisplayAlert("Error", "ErrorPlugins", "OK");

        }
    }
}