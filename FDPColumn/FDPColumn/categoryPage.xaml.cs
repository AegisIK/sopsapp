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
    public partial class categoryPage : ContentPage
    {
        
        public categoryPage(string myText)
        {
            var metrics = DeviceDisplay.ScreenMetrics;
            double headerHeight = metrics.Height;

            string[] labelNames = new string[8] { "General Patient Management", "Appendix", "Cardiac", "OB", "Trauma", "PEDS", "Respiratory", "Medical" };
            string[] procedures;
            Color headerColor;
            #region check mytext
            if (myText == labelNames[0])
            {
                procedures = CategoryClasses.patMan.components;
                headerColor = MainPage.patManLblColor;
            }
            else if (myText == labelNames[1])
            {
                procedures = CategoryClasses.apndx.components;
                headerColor = MainPage.apndxLblColor;
            }
            else if (myText == labelNames[2])
            {
                procedures = CategoryClasses.card.components;
                headerColor = MainPage.cardLblColor;
            }
            else if (myText == labelNames[3])
            {
                procedures = CategoryClasses.ob.components;
                headerColor = MainPage.obLblColor;
            }
            else if (myText == labelNames[4])
            {
                procedures = CategoryClasses.trauma.components;
                headerColor = MainPage.traumaLblColor;
            }
            else if (myText == labelNames[5])
            {
                procedures = CategoryClasses.peds.components;
                headerColor = MainPage.pedsLblColor;
            }
            else if (myText == labelNames[6])
            {
                procedures = CategoryClasses.resp.components;
                headerColor = MainPage.respLblColor;
            }
            else if (myText == labelNames[7])
            {
                procedures = CategoryClasses.med.components;
                headerColor = MainPage.medLblColor;
            }
            else
            { return; }
            #endregion

            Label header = new Label
            {
                Text = myText,
                HeightRequest = headerHeight / 25,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = headerHeight/30, //something to test with when moving device resolutions
                BackgroundColor = headerColor
            };

            
            

            //Checks to see what the inputted myText is, to determine which category was selected and display the corresponding category
           

            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items. Going to hard code with inefficient if else statements, find better way if have time.
                ItemsSource = procedures,
                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                HasUnevenRows = true,
                
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label procedureLabel = new Label();
                    procedureLabel.SetBinding(Label.TextProperty, ".");
                    procedureLabel.FontSize = 20;


                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(10, 20),
                            //Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                                {
                                    new StackLayout
                                    {
                                        
                                        //HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Children =
                                        {
                                            procedureLabel,
                                        }
                                    }
                                }
                        }
                    };
                })
            };


            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listView
                }
            };

        }
    }               
}