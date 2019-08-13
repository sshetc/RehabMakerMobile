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
	public partial class PrivacyPolicy : ContentPage
	{
		public PrivacyPolicy ()
		{
			InitializeComponent ();
            LogoRM.Source = ImageSource.FromResource("RehabMaker.Picture.logo_svg.png");
        }
	}
}