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
    public partial class ImagePageSwipeAnimated : ContentPage
    {
        int currentPageNumber;
        ScreenMetrics metrics = DeviceDisplay.ScreenMetrics;//xamarin essentials may not work for lower level api's.

        int currentImageCounter = 1;
        public ImagePageSwipeAnimated(int pageNumber, string categoryName)
        {
            InitializeComponent();

            

            currentPageNumber = pageNumber; // create pageNumber accessible throughout the code
            Setup(categoryName);
            

            image1.Source = FileImageSource.FromFile("p" + pageNumber + ".jpg");



        }

        public async void AlertSomething(double swipeX)
        {
            
            if(swipeX > 100)
            {
                await DisplayAlert("title", "something happeened", swipeX.ToString());
                int test = currentPageNumber + 1;
                
                switch(currentImageCounter)
                {
                    case (1):
                        double xDim1 = view1.Width;
                        double yDim1 = view1.Height;
                        double xDim2 = view2.Width;
                        double yDim2 = view2.Height;
                        image2.Source = FileImageSource.FromFile("p" + test.ToString() + ".jpg");
                        List<Task> transition = new List<Task>();
                        transition.Add(view1.TranslateTo(-360, view1.TranslationY));
                        transition.Add(view2.TranslateTo(0, view2.TranslationY));
                        await Task.WhenAll(transition);

                        view1.WidthRequest = 0;
                        break;
                    case (2):

                        break;
                    case (3):

                        break;
                }
                

                



            }
        }

        void Setup(string categoryName)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            
            /*double screenHeight = metrics.Height;

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

            header.Text = categoryName;
            header.HeightRequest = screenHeight / 20;
            header.HorizontalOptions = LayoutOptions.FillAndExpand;
            header.HorizontalTextAlignment = TextAlignment.Center;
            header.FontSize = screenHeight / 30;
            header.BackgroundColor = headerColor;*/
        }
    }
}