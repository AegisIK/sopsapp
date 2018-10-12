using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace FDPColumn
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImagePage : ContentPage
	{
		public ImagePage (int pageNumber)
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);
            var metrics = DeviceDisplay.ScreenMetrics;
            double screenHeight = metrics.Height;

            string page = pageNumber.ToString() + ".jpg";



            header.Text = page;
            header.HorizontalOptions = LayoutOptions.FillAndExpand;
            header.HorizontalTextAlignment = TextAlignment.Center;
            header.HeightRequest = screenHeight / 20;
            header.FontSize = screenHeight / 30;

            image.Source = page;


            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += (s, e) => {
                // Handle the pinch
            };

            image.GestureRecognizers.Add(pinchGesture);


        }
	}
}