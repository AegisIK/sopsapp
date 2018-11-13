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
        string categoryPageName;
        IDictionary<string, int> dictionary = new Dictionary<string, int>();
        public categoryPage(string myText)
        {
            categoryPageName = myText;
            //Create the listview and populate it
            #region create listview
            NavigationPage.SetHasNavigationBar(this, false);
            var metrics = DeviceDisplay.ScreenMetrics;
            double screenHeight = metrics.Height;

            string[] labelNames = new string[8] { "General", "Appendix", "Cardiac", "OB", "Trauma", "PEDS", "Respiratory", "Medical" };
            string[] procedures;
            Color headerColor;
            #region check mytext

            
            if (myText == labelNames[0])
            {
                procedures = CategoryClasses.patMan.components;
                headerColor = MainPage.patManLblColor;
                dictionary = DictionaryClasses.generalDictionary.dictionary;
            }
            else if (myText == labelNames[1])
            {
                procedures = CategoryClasses.apndx.components;
                headerColor = MainPage.apndxLblColor;
                dictionary = DictionaryClasses.apndxDictionary.dictionary;
            }
            else if (myText == labelNames[2])
            {
                procedures = CategoryClasses.card.components;
                headerColor = MainPage.cardLblColor;
                dictionary = DictionaryClasses.cardDictionary.dictionary;
            }
            else if (myText == labelNames[3])
            {
                procedures = CategoryClasses.ob.components;
                headerColor = MainPage.obLblColor;
                dictionary = DictionaryClasses.obDictionary.dictionary;
            }
            else if (myText == labelNames[4])
            {
                procedures = CategoryClasses.trauma.components;
                headerColor = MainPage.traumaLblColor;
                dictionary = DictionaryClasses.traumaDictionary.dictionary;
            }
            else if (myText == labelNames[5])
            {
                procedures = CategoryClasses.peds.components;
                headerColor = MainPage.pedsLblColor;
                dictionary = DictionaryClasses.pedsDictionary.dictionary;
            }
            else if (myText == labelNames[6])
            {
                procedures = CategoryClasses.resp.components;
                headerColor = MainPage.respLblColor;
                dictionary = DictionaryClasses.respDictionary.dictionary;
            }
            else if (myText == labelNames[7])
            {
                procedures = CategoryClasses.med.components;
                headerColor = MainPage.medLblColor;
                dictionary = DictionaryClasses.medDictionary.dictionary;
            }
            else
            { return; }
            #endregion

            Label header = new Label
            {
                Text = myText,
                HeightRequest = screenHeight / 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = screenHeight/30, //something to test with when moving device resolutions
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

            listView.ItemTapped += (s, e) => procedureTapped(s, e);
            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listView
                }
            };
            #endregion

            //Set up swipe recognition in header
            
        }

        async void procedureTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ImagePageSwipeAnimated(dictionary[e.Item.ToString()], categoryPageName));
        }

    }               
}