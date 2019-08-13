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
	public partial class PopupView : ContentPage
    {
		public PopupView ()
		{
			InitializeComponent ();
            RemDevice.Text = Settings.LastUsedDevices;
        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            try { 
            RehabMaker.Settings.LastUsedDevices = Convert.ToString(RemDevice.Text);
                RemDevice.TextColor = Color.Green;
                    }
            catch
            {
                RemDevice.TextColor = Color.Red;
            }
        }

	}
}