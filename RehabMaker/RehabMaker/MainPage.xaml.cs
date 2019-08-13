using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RehabMaker
{

    public partial class JsonParams
    {
        [JsonProperty("idParams")]
        public decimal IdParams { get; set; }

        [JsonProperty("speed")]
        public decimal Speed { get; set; }

        [JsonProperty("distance")]
        public decimal Distance { get; set; }

        [JsonProperty("сalories")]
        public decimal Сalories { get; set; }

        [JsonProperty("idDevice")]
        public int IdDevice { get; set; }

        [JsonProperty("idDeviceNavigation")]
        public object IdDeviceNavigation { get; set; }
    }
    public partial class AverageJsonParams
    {
        [JsonProperty("IdStatitics")]
        public decimal IdStatitics { get; set; }

        [JsonProperty("AverageSpeed")]
        public string AverageSpeed { get; set; }

        [JsonProperty("TotalDistance")]
        public string TotalDistance { get; set; }

        [JsonProperty("TotalCalories")]
        public string TotalCalories { get; set; }

        [JsonProperty("IdDevice")]
        public int IdDevice { get; set; }

        [JsonProperty("idDeviceNavigation")]
        public object IdDeviceNavigation { get; set; }
    }

    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            
            BletoothRM.Source = ImageSource.FromResource("RehabMaker.Picture.bletooth.png");
            LogoRM.Source = ImageSource.FromResource("RehabMaker.Picture.logo_min_svg.png");
            LogoRM.HorizontalOptions = LayoutOptions.Center;
            button1.BackgroundColor = Color.Red;
            button1.TextColor = Color.White;
            stackLayout2.IsVisible = false;
            DatePicker1.IsEnabled = false;
            button3.Source = ImageSource.FromResource("RehabMaker.Picture.brows.png");
            button4.Source = ImageSource.FromResource("RehabMaker.Picture.brows.png");
            StopRM.IsVisible = false;
            StartRM.Opacity = 0;

            StartRM.Source = ImageSource.FromResource("RehabMaker.Picture.start_svg.png");
            StopRM.Source = ImageSource.FromResource("RehabMaker.Picture.stop.png");
            bool IsBusy = false;
        }
        public MainPage(string name, Models.Picture picture)
        {
            InitializeComponent();


            stackLayoutFB.Orientation = StackOrientation.Horizontal;
            LogoRM.Source = ImageSource.FromResource("RehabMaker.Picture.logo_min_svg.png");
            LogoRM.HorizontalOptions = LayoutOptions.CenterAndExpand;
            NameRM.Text = name;
            NameRM.HorizontalOptions = LayoutOptions.EndAndExpand;


            NameRM.TextDecorations = TextDecorations.Underline;

            BletoothRM.Source = ImageSource.FromResource("RehabMaker.Picture.bletooth.png");
            button1.BackgroundColor = Color.Red;
            button1.TextColor = Color.White;
            stackLayout2.IsVisible = false;
            DatePicker1.IsEnabled = false;
            button3.Source = ImageSource.FromResource("RehabMaker.Picture.brows.png");
            button4.Source = ImageSource.FromResource("RehabMaker.Picture.brows.png");
            StopRM.IsVisible = false;
            StartRM.Opacity = 0;
            DatePicker1.IsVisible = false;
            StartRM.Source = ImageSource.FromResource("RehabMaker.Picture.start_svg.png");
            StopRM.Source = ImageSource.FromResource("RehabMaker.Picture.stop.png");
           
        }
      

        Random rnd = new Random();

        private void button1_clicked(object sender, EventArgs e)
        {

            button1.BackgroundColor = Color.Red;
            button1.TextColor = Color.White;
            button2.BackgroundColor = Color.White;
            button2.TextColor = Color.Red;
            stackLayout1.IsVisible = true;
            stackLayout2.IsVisible = false;
            DatePicker1.IsVisible = false;
        }
        private void button2_clicked(object sender, EventArgs e)
        {

            button2.BackgroundColor = Color.Red;
            button2.TextColor = Color.White;
            button1.BackgroundColor = Color.White;
            button1.TextColor = Color.Red;
            stackLayout1.IsVisible = false;
            stackLayout2.IsVisible = true;
            DatePicker1.IsVisible = true;
        }

        private async void button3_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginInToShare());

        }

        private async void BletoothRM_clicked(object sender, EventArgs e)
        {

            activityIndicator.IsRunning = true;

            try
            {
                //await BletoothRM.ScaleTo(0.9, 1500, Easing.Linear);
                string LastDevices = Settings.LastUsedDevices;

                string Get = "";
                try
                {
                    Get = GETApi("api/web/ok/1");
                }
                catch
                {
                    label1.Text = "Server is not available. Check your Internet connection,";
                    activityIndicator.IsRunning = false;
                }
                if (Get == "\"Oks\"")
                {
                    //await BletoothRM.ScaleTo(1, 2000, Easing.Linear);
                    if(LastDevices == null)
                    {

                        activityIndicator.IsRunning = false;
                        await  DisplayAlert("Device error", "Check the device number in 'Settings'", "Ok");
                        return;
                    }
                    string json13 = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=1&ugo=ugo");
                    AverageJsonParams JParams = JsonConvert.DeserializeObject<AverageJsonParams>(json13);
                    AvSpeed.Text = JParams.AverageSpeed.ToString();
                    AvCal.Text = JParams.TotalDistance.ToString();
                    AvDis.Text = JParams.TotalCalories.ToString();
                    await stackLayout3.FadeTo(0, 1000);
                    stackLayout3.IsVisible = false;
                    await StartRM.FadeTo(1, 1000);
                    DatePicker1.IsEnabled = true;
                    activityIndicator.IsRunning = false;
                }
                else
                    await DisplayAlert("Error", "Server is not available. Check your Internet connection", "Ok");
                    activityIndicator.IsRunning = false;
            }
            catch
            {
                await DisplayAlert("Device error", "Check the device number in 'Settings'", "Ok");
                activityIndicator.IsRunning = false;
            }
        }

        private static string GETApi(string Data)
        {
            string Url = "http://rehabmaker-001-site1.dtempurl.com";
            string Out = "";

            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(Url + "/" + Data);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.Stream stream = resp.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                Out = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {

            }
            return Out;
        }

        private void StartRM_clicked(object sender, EventArgs e)
        {
           
            activityIndicator.HorizontalOptions = LayoutOptions.FillAndExpand;
            
            StartRM.IsVisible = false;
            string LastDevices = Settings.LastUsedDevices;
            string json = "";
            string Get = "";
            try
            {
                Get = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=0&ugo=ugo");
            }
            catch
            {
                DisplayAlert("Server is not available", "Check your Internet connection", "Ok");
            }
            if (Get != null)
            {
                StopRM.IsVisible = true;
                ApiConnect(LastDevices, json, 1);
            }
            else
                DisplayAlert("Device error", "Check the device number in 'Settings'", "Ok");
        }

        private async void StopRM_clicked(object sender, EventArgs e)
        {
            StartRM.IsVisible = true;
            StopRM.IsVisible = false;
            await StopRM.RotateTo(0, 0);
        }

        private async void ApiConnect(string LastDevices, string date, int destin)
        {

            try
            {
                if (destin == 0)
                {
                    string json4Date = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=2&ugo=" + date);
                    AverageJsonParams JParamss = JsonConvert.DeserializeObject<AverageJsonParams>(json4Date);
                    AvDateSp.Text = JParamss.AverageSpeed.ToString();
                    AvDateDist.Text = JParamss.TotalDistance.ToString();
                    AvDateCal.Text = JParamss.TotalCalories.ToString();
                }
            }
            catch
            {
                await DisplayAlert("Error", "No data found for this date", "Ok");
            }
            while (StopRM.IsVisible == true)
            {
                try
                {
                    if (destin != 0)
                    {
                        string json = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=0&ugo=ugo");
                        JsonParams JParams = JsonConvert.DeserializeObject<JsonParams>(json);
                        LSpeed.Text = JParams.Speed.ToString();
                        LDistance.Text = JParams.Distance.ToString();
                        LCalories.Text = JParams.Сalories.ToString();

                        string json4Average = GETApi("api/params/paraparams?id=" + LastDevices + "&simbol=1&ugo=ugo");

                        AverageJsonParams JParamss = JsonConvert.DeserializeObject<AverageJsonParams>(json4Average);
                        AvSpeed.Text = JParamss.AverageSpeed.ToString();
                        AvCal.Text = JParamss.TotalDistance.ToString();
                        AvDis.Text = JParamss.TotalCalories.ToString();

                    }


                    int i = rnd.Next(1, 3);
                    if (i == 1)
                    {
                        await StopRM.RotateTo(720, 4000, Easing.CubicInOut);
                        await StopRM.RotateTo(0, 0);
                    }
                    if (i == 2)
                    {
                        await StopRM.RotateTo(1080, 6000, Easing.CubicInOut);
                        await StopRM.RotateTo(0, 0);
                    }
                }
                catch
                {
                    StopRM.IsVisible = false;
                    StartRM.IsVisible = true;
                    await DisplayAlert("Server is not available", "Check your Internet connection", "Ok");
                    break;
                }
            }
        }

        private async void TermsOfService_tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermsOfService());
        }

        private async void PrivacyPolicy_tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrivacyPolicy());
        }

        private async void NameFB_tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageLogOut());
        }

        private async void SettingsDevice_tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PopupView());
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            string date = e.NewDate.ToString("MM/dd/yyyy");

            try
            {
                string LastDevices = Settings.LastUsedDevices;
                DateTime data = Convert.ToDateTime(date);
                date = data.ToShortDateString();
                date = date.Replace("/", ".");
                ApiConnect(LastDevices, date, 0);
            }
            catch
            {
                label1.Text = "Server is not available. Check your Internet connection,";
            }


        }

        private async void buttonviewdate_clicked(object sender, EventArgs e)
        {
            string LastDevices = Settings.LastUsedDevices;
            string date = DatePicker1.Date.ToString("MM/dd/yyyy");
            try
            {
                if (date[0] == Convert.ToChar("0"))
                {
                    date = date.Remove(0, 1);
                }
                date = date.Replace("/", ".");
                ApiConnect(LastDevices, date, 0);
            }
            catch
            {
                await DisplayAlert("Error", "Server is not available", "Ok");
            }
        }



    }
}










