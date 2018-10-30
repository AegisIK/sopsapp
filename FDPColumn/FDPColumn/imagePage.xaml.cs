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
        public ImagePage(int pageNumber, string categoryName)
        {
            InitializeComponent();



            NavigationPage.SetHasNavigationBar(this, false); 
            var metrics = DeviceDisplay.ScreenMetrics;//xamarin essentials may not work for lower level api's.
            double screenHeight = metrics.Height;

            string imageString = "p" + pageNumber.ToString() + ".jpg";

            string[] labelNames = new string[8] { "General", "Appendix", "Cardiac", "OB", "Trauma", "PEDS", "Respiratory", "Medical" };
            Color headerColor;
            #region check categoryName


            if (categoryName == labelNames[0])
            {
                headerColor = MainPage.patManLblColor;
            }
            else if (categoryName == labelNames[1])
            {
                headerColor = MainPage.apndxLblColor;
            }
            else if (categoryName == labelNames[2])
            {
                headerColor = MainPage.cardLblColor;
            }
            else if (categoryName == labelNames[3])
            {
                headerColor = MainPage.obLblColor;
            }
            else if (categoryName == labelNames[4])
            {
                headerColor = MainPage.traumaLblColor;
            }
            else if (categoryName == labelNames[5])
            {
                headerColor = MainPage.pedsLblColor;
            }
            else if (categoryName == labelNames[6])
            {
                headerColor = MainPage.respLblColor;
            }
            else if (categoryName == labelNames[7])
            {
                headerColor = MainPage.medLblColor;
            }
            else
            { return; }
            #endregion

            /* Label header = new Label
             {
                 Text = pageNumber.ToString(),
                 HeightRequest = screenHeight / 20,
                 HorizontalOptions = LayoutOptions.FillAndExpand,
                 HorizontalTextAlignment = TextAlignment.Center,
                 FontSize = screenHeight / 30, //something to test with when moving device resolutions
                 BackgroundColor = headerColor
             };*/

            header.Text = categoryName;
            header.HeightRequest = screenHeight / 20;
            header.HorizontalOptions = LayoutOptions.FillAndExpand;
            header.HorizontalTextAlignment = TextAlignment.Center;
            header.FontSize = screenHeight / 30;
            header.BackgroundColor = headerColor;
            
            image.Source = ImageSource.FromFile(imageString);

            /*var grid = new Grid { };
            grid.RowDefinitions.Add(new RowDefinition { });
            grid.RowDefinitions.Add(new RowDefinition { });
            grid.Children.Add(header, 0, 0);
            grid.Children.Add(new PinchAndPanContainer { Content = new Image { Source = ImageSource.FromFile(NumberToWords(pageNumber)) } }, 0, 1);*/

            /*Content = new StackLayout
            {
                Children = {
                    new StackLayout
                    {
                        Padding = new Thickness (10, 40, 10, 10),
                        Children =
                        { 
                            header,
                            new ScrollView
                            {
                                new PinchAndPanContainer
                                {

                                }
                            }
                            
                        }

                    }
                    //grid
                    
                }
            };*/
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