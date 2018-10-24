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
                FontSize = screenHeight / 30, //something to test with when moving device resolutions
                BackgroundColor = headerColor
            };*/

            Content = new StackLayout
            {
                Children = {
                    new PinchAndPanContainer
                    {
                        Content = new Image {Source = ImageSource.FromFile(NumberToWords(pageNumber))}
                    }
                }
            };
        }


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