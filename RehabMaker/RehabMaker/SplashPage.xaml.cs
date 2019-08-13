using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RehabMaker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
      

        public SplashPage()
        {
            InitializeComponent();
            splashImage.Source = ImageSource.FromResource("RehabMaker.Picture.logo_svg.png");
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();

            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            
            

            sub.Children.Add(splashImage);
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(0.9, 1500, Easing.Linear);
            await splashImage.ScaleTo(1, 1200, Easing.Linear);
            await splashImage.ScaleTo(0.9, 1500, Easing.Linear);
            await splashImage.ScaleTo(1, 1200, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

    }
}