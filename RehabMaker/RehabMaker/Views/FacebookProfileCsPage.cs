using System.Linq;
using RehabMaker.Models;
using RehabMaker.ViewModels;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

namespace RehabMaker.Views
{
    public class FacebookProfileCsPage : ContentPage
    {

        /// <summary>
        /// Make sure to get a new ClientId from:
        /// https://developers.facebook.com/apps/
        /// </summary>
        private string ClientId = "443508269806056";

        public FacebookProfileCsPage()
        {

            BindingContext = new FacebookViewModel();

            Title = "Facebook Profile";
            BackgroundColor = Color.White;

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&redirect_uri=https://www.facebook.com/connect/login_success.html"
                + "&response_type=token";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);
          
                if (accessToken != "")
                {
                   Content.IsVisible = false;
                    var vm = BindingContext as FacebookViewModel;

                    await vm.SetFacebookUserProfileAsync(accessToken);
                    accessToken = "";
                    SetPageContent(vm.FacebookProfile);
                    
                }
            
        }

        private void SetPageContent(FacebookProfile facebookProfile)
        {

            Navigation.PushAsync(new MainPage(facebookProfile.Name,facebookProfile.Picture));

        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

            //    if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
           //     {
            //        at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", " ");
           //     }
                
                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}
