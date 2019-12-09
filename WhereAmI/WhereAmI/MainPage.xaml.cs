using System;
using Xamarin.Forms;

namespace WhereAmI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void WhereAmI(object sender, EventArgs args)
        {
                await Navigation.PushAsync(new PositionPage());
        }

        async void WhereAmIOnMap(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }
}
