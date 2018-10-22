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
        private const double MIN_SCALE = 1;
        private const double MAX_SCALE = 8;
        private const double OVERSHOOT = 0.15;
        private double StartX, StartY;
        private double StartScale;
        public ImagePage(int pageNumber)
        {
            InitializeComponent();



            NavigationPage.SetHasNavigationBar(this, false);
            var metrics = DeviceDisplay.ScreenMetrics;
            double screenHeight = metrics.Height;

            string page = pageNumber.ToString() + ".jpg";
        }
    }
}