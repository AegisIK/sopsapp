using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FDPColumn
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnTapSetup();

            colorSetup();
        }

        public static Color patManLblColor = Color.FromHex("#F3F3F3"); //move this to it's own class later, it's being referenced in category page, 
        public static Color apndxLblColor = Color.FromHex("#DDDDDD");
        public static Color respLblColor = Color.FromHex("#80E5E2");
        public static Color obLblColor = Color.FromHex("#F896B2");
        public static Color cardLblColor = Color.FromHex("#F9EB73");
        public static Color traumaLblColor = Color.FromHex("#84EC9D");
        public static Color pedsLblColor = Color.FromHex("#FEEB9A");
        public static Color medLblColor = Color.FromHex("#FF9C71");


        void colorSetup()
        {

            patManLbl.BackgroundColor = patManLblColor;
            apndxLbl.BackgroundColor = apndxLblColor;
            respLbl.BackgroundColor = respLblColor;
            obLbl.BackgroundColor = obLblColor;
            cardLbl.BackgroundColor = cardLblColor;
            traumaLbl.BackgroundColor = traumaLblColor;
            pedsLbl.BackgroundColor = pedsLblColor;
            medLbl.BackgroundColor = medLblColor;
        }

        void btnTapSetup()
        {

            Label[] labels = new Label[8] { patManLbl, apndxLbl, cardLbl, obLbl, traumaLbl, pedsLbl, respLbl, medLbl };
            string[] labelNames = new string[8] { "General", "Appendix", "Cardiac", "OB", "Trauma", "PEDS", "Respiratory", "Medical" };

            //so, I was trying to put this in a for loop, but it wouldnt work, it would attach the last iteration of tap to all of the labels, took me a week, still couldn't figure it out, so
            //now, we're just gonna hard code it and solve it later if I have the time
            #region tapSetup
            var tgr0 = new TapGestureRecognizer();
            tgr0.Tapped += (s, e) => categoryPage(labelNames[0]);
            labels[0].GestureRecognizers.Add(tgr0);

            var tgr1 = new TapGestureRecognizer();
            tgr1.Tapped += (s, e) => categoryPage(labelNames[1]);
            labels[1].GestureRecognizers.Add(tgr1);

            var tgr2 = new TapGestureRecognizer();
            tgr2.Tapped += (s, e) => categoryPage(labelNames[2]);
            labels[2].GestureRecognizers.Add(tgr2);

            var tgr3 = new TapGestureRecognizer();
            tgr3.Tapped += (s, e) => categoryPage(labelNames[3]);
            labels[3].GestureRecognizers.Add(tgr3);

            var tgr4 = new TapGestureRecognizer();
            tgr4.Tapped += (s, e) => categoryPage(labelNames[4]);
            labels[4].GestureRecognizers.Add(tgr4);

            var tgr5 = new TapGestureRecognizer();
            tgr5.Tapped += (s, e) => categoryPage(labelNames[5]);
            labels[5].GestureRecognizers.Add(tgr5);

            var tgr6 = new TapGestureRecognizer();
            tgr6.Tapped += (s, e) => categoryPage(labelNames[6]);
            labels[6].GestureRecognizers.Add(tgr6);

            var tgr7 = new TapGestureRecognizer();
            tgr7.Tapped += (s, e) => categoryPage(labelNames[7]);
            labels[7].GestureRecognizers.Add(tgr7);
            #endregion 

        } 

        async void categoryPage(string categoryName)
        {
            await Navigation.PushAsync(new categoryPage(categoryName));
        }

    }
}
