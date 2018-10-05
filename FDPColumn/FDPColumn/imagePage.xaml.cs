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
	public partial class imagePage : ContentPage
	{
		public imagePage (int pageNumber)
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);

            var metrics = DeviceDisplay.ScreenMetrics;
            double screenHeight = metrics.Height;

            header.Text = pageNumber.ToString();
            header.HeightRequest = screenHeight / 20;
            header.HorizontalOptions = LayoutOptions.FillAndExpand;
            header.HorizontalTextAlignment = TextAlignment.Center;
            header.FontSize = screenHeight / 30;
            

            pageImage.Source = ImageSource.FromFile(pageNumber + ".jpg");
        }
	}
}