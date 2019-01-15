using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using CarouselView.FormsPlugin.Abstractions;

namespace FDPColumn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePageSwipeAnimated : ContentPage
    {
        private string _categoryName;

        string[] labelNames = new string[8] { "General", "Appendix", "Cardiac", "OB", "Trauma", "PEDS", "Respiratory", "Medical" };
        public ImagePageSwipeAnimated(int pageNumber, string categoryName)
        {
            InitializeComponent();

            Setup(categoryName);

            List<string> pageList = new List<string>();
            for (int i = 1; i <= 113; i++)
            {
                pageList.Add("p" + i + ".jpg");
            }
            view.Position = pageNumber;
            view.ItemsSource = pageList;


            
        }

        private double width = 0;
        private double height = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            App.ScreenHeight = height;
            App.ScreenWidth = width;

            if(!Equals(this.width, width) || !Equals(this.height, height))
            {
                this.width = width;
                this.height = height;

                //reconfigure layout if android
                if (Device.RuntimePlatform == Device.Android)
                {
                    view.Orientation = CarouselView.FormsPlugin.Abstractions.CarouselViewOrientation.Vertical;
                    view.Orientation = CarouselView.FormsPlugin.Abstractions.CarouselViewOrientation.Horizontal;
                }
            }
        }

        public void CarouselSwipeController(bool zoomedIn)
        {
            if (zoomedIn)
            {
                view.IsSwipeEnabled = false;
            }
            else
            {
                view.IsSwipeEnabled = true;
            }
        }

        void Setup(string categoryName)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            //var metrics = DeviceDisplay.ScreenMetrics;//xamarin essentials may not work for lower level api's.
            var metrics = DeviceDisplay.MainDisplayInfo;
            double screenHeight = metrics.Height;

            
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

            _categoryName = categoryName;
            header.Text = _categoryName;
            header.HeightRequest = screenHeight / 20;
            header.HorizontalOptions = LayoutOptions.FillAndExpand;
            header.HorizontalTextAlignment = TextAlignment.Center;
            header.FontSize = screenHeight / 30;
            header.BackgroundColor = headerColor;

            row0.Height = screenHeight / 20;

            view.ShowIndicators = false;
            view.ShowArrows = false;

            view.PositionSelected += (s, e) => CarouselPositionChanged(s, e);
        }

        void CarouselPositionChanged(object sender, PositionSelectedEventArgs e)
        {
            int currentPage = e.NewValue + 1;
            if (currentPage.ToString() == DictionaryClasses.generalDictionary.dictionary.First().Value.ToString() && _categoryName != labelNames[0])
            {
                _categoryName = labelNames[0];
                header.Text = _categoryName;
            }
            else if (currentPage.ToString() == DictionaryClasses.apndxDictionary.dictionary.First().Value.ToString() && _categoryName != labelNames[1])
            {
                _categoryName = labelNames[1];
                header.Text = _categoryName;
            }
            else if (currentPage.ToString() == DictionaryClasses.cardDictionary.dictionary.First().Value.ToString() && _categoryName != labelNames[2])
            {
                _categoryName = labelNames[2];
                header.Text = _categoryName;
            }
            else if (currentPage.ToString() == DictionaryClasses.obDictionary.dictionary.First().Value.ToString() && _categoryName != labelNames[3])
            {
                _categoryName = labelNames[3];
                header.Text = _categoryName;
            }
            else if (currentPage.ToString() == DictionaryClasses.traumaDictionary.dictionary.First().Value.ToString() && _categoryName != labelNames[4])
            {
                _categoryName = labelNames[4];
                header.Text = _categoryName;
            }
            else if (currentPage.ToString() == DictionaryClasses.pedsDictionary.dictionary.First().Value.ToString() && _categoryName != labelNames[5])
            {
                _categoryName = labelNames[5];
                header.Text = _categoryName;
            }
            else if (currentPage.ToString() == DictionaryClasses.respDictionary.dictionary.First().Value.ToString() && _categoryName != labelNames[6])
            {
                _categoryName = labelNames[6];
                header.Text = _categoryName;
            }
            else if (currentPage.ToString() == DictionaryClasses.medDictionary.dictionary.First().Value.ToString() && _categoryName != labelNames[7])
            {
                _categoryName = labelNames[7];
                header.Text = _categoryName;
            }

        }
    
    

        
        
    }
}