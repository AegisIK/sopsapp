using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using XFExtensions;

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
            //InitializeComponent();



            NavigationPage.SetHasNavigationBar(this, false); 
            var metrics = DeviceDisplay.ScreenMetrics;//xamarin essentials may not work for lower level api's.
            double screenHeight = metrics.Height;

            string page = "_" + pageNumber.ToString() + ".jpg";

            /*Label header = new Label
            {
                Text = myText,
                HeightRequest = screenHeight / 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = screenHeight / 30, //something to test with when moving dev+ice resolutions
                BackgroundColor = headerColor
            };*/
            Image image = new Image
            {
                Source = ImageSource.FromFile(NumberToWords(pageNumber))
            };

            #region gesture recognizer - no math anchor method
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => OnTapped(s, e);
            image.GestureRecognizers.Add(tapGestureRecognizer);

            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += (s, e) => OnPinchUpdated(s, e);
            image.GestureRecognizers.Add(pinchGesture);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (s, e) => OnPanUpdated(s, e);
            image.GestureRecognizers.Add(panGesture);
            #endregion

            
    
           Content = new StackLayout
            {
                Children = {
                        /*new PinchAndPanContainer
                        {
                            Content = new Image {Source = ImageSource.FromFile(NumberToWords(pageNumber))}
                        }*/
                        image
                        

                }
            };
        }


        #region gesture work
        private void OnTapped(object sender, EventArgs e)
        {
            if (Scale > MIN_SCALE)
            {
                this.ScaleTo(MIN_SCALE, 250, Easing.CubicInOut);
                this.TranslateTo(0, 0, 250, Easing.CubicInOut);
            }
            else
            {
                AnchorX = AnchorY = 0.5;
                this.ScaleTo(MAX_SCALE, 250, Easing.CubicInOut);
            }
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



        #endregion
        static string NumberToWords(int number)
        {
            string words = "";
            if( (number/100) > 0)
            {
                words += "hundred";
            }

            string[] unitMap = new string[20] {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
                "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"};
            string[] tensMap = new string[10] { "zero", "ten", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninety" };


            if (number > 0)
            {
                if (number < 20)
                {
                    words += unitMap[number];
                }
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) != 0)
                    {
                        words += " " + unitMap[number % 10];
                    }
                }
            }

            return words;
        }
    }
}