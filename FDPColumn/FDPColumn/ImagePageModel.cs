using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;
namespace FDPColumn
{
    public class ImagePageModel
    {
        public string category { get; set; }
        public string imageReference { get; set; }

        public static IList<ImagePageModel> All { get; set; }

        static ImagePageModel()
        {
            ImagePageModel[] models = new ImagePageModel[112];

            All = new ObservableCollection<ImagePageModel>
            {
                /*for (int i = 0; i < 113; i++)
                {

                }*/
            };

            for(int i = 0; i < 113; i++)
            {
                int pageNumber = i + 1;

                All[i] = new ImagePageModel { category = "resp", imageReference = string.Format("p{0}.jpg", pageNumber) };
            }
        }
    }
}
