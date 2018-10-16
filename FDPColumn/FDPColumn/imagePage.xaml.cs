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
            pinchGesture.PinchUpdated += (s, e) => OnPinchUpdated(s, e);
            image.GestureRecognizers.Add(pinchGesture);


            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (s, e) => OnPanUpdated(s, e);
            image.GestureRecognizers.Add(panGesture);


        }
        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            switch (e.Status)
            {
                case GestureStatus.Started:
                    StartScale = Scale;
                    AnchorX = e.ScaleOrigin.X;
                    AnchorY = e.ScaleOrigin.Y;
                    break;

                case GestureStatus.Running:
                    double current = Scale + (e.Scale - 1) * StartScale;
                    Scale = DoubleExtensions.Clamp(current, MIN_SCALE * (1 - OVERSHOOT), MAX_SCALE * (1 + OVERSHOOT));
                    break;

                case GestureStatus.Completed:
                    if (Scale > MAX_SCALE)
                        this.ScaleTo(MAX_SCALE, 250, Easing.SpringOut);
                    else if (Scale < MIN_SCALE)
                        this.ScaleTo(MIN_SCALE, 250, Easing.SpringOut);
                    break;
            }
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    StartX = (1 - AnchorX) * Width;
                    StartY = (1 - AnchorY) * Height;
                    break;

                case GestureStatus.Running:
                    AnchorX = DoubleExtensions.Clamp(1 - (StartX + e.TotalX) / Width, 0, 1);
                    AnchorY = DoubleExtensions.Clamp(1 - (StartY + e.TotalY) / Height, 0, 1);
                    break;
            }
        }
    }
}