using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhereAmI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PositionPage : ContentPage
    {
        public PositionPage ()
        {
            InitializeComponent ();

                Location location = Task.Run(async ()=> await GetDeviceLocationAsync()).Result;
                if (location != null)
                {
                    latitudeValue.Text = location.Latitude.ToString();
                    logitudeValue.Text = location.Longitude.ToString();
                }
                else
                {
                    latitudeValue.Text = "0.0";
                    logitudeValue.Text = "0.0";
                }

        }

        private async Task<Location> GetDeviceLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                return location;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured: " + ex.ToString());
                return null;
            }
        }
	}
}