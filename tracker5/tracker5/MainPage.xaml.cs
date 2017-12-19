using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;


namespace tracker5
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btnGetLocation.Clicked += btnGetLocation_Clicked;
        }

        private async void btnGetLocation_Clicked(object sender, EventArgs e)
        {
            await RetreiveLocation();
        }

        private async Task RetreiveLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var position = await locator.GetPositionAsync();

            txtLat.Text = "Latitude: " + position.Latitude.ToString();
            txtLong.Text = "Longitude: " + position.Longitude.ToString();

            MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
               Distance.FromMiles(1)));

            var pin = new Pin
            {
                Position = new Position(position.Latitude, position.Longitude),
                Label = "Current location",
                
            };
            MainMap.Pins.Add(pin);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           

           

            var pin2 = new Pin
            {
                Position = new Position(36.892, 10.182),
                Label = "Position #2",
                Address = "Address #2"
            };

            var pin3 = new Pin
            {
                Position = new Position(36.893, 10.183),
                Label = "Position #3",
                Address = "Address #3"
            };

           
            pin2.Clicked += Pin_Clicked;
            pin3.Clicked += Pin_Clicked;

          
            MainMap.Pins.Add(pin2);
            MainMap.Pins.Add(pin3);
        }

        private void Pin_Clicked(object sender, EventArgs e)
        {

            var selectedPin = sender as Pin;

            DisplayAlert(selectedPin.Label, selectedPin.Address, "OK");
        }
    }
}

