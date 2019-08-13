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
	public partial class PageLogOut : ContentPage
	{

        public PageLogOut()
        {
            InitializeComponent();
            LogoRM.Source = ImageSource.FromResource("RehabMaker.Picture.logo_svg.png");
            RehabMker.Source = ImageSource.FromResource("RehabMaker.Picture.RehabMaker.png");
        }

        
        private  void buttonlogout_clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new SplashPage());
        }

      
    }
}