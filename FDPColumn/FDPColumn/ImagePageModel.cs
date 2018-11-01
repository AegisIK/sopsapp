using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;
namespace FDPColumn
{
    public class ImagePageModel
    {
        public string Category { get; set; }
        public string ImageReference { get; set; }

        public static IList<ImagePageModel> All { get; set; }

        static ImagePageModel()
        {

            All = new ObservableCollection<ImagePageModel>{
                new ImagePageModel
                {
                    Category = "1",
                    ImageReference = "p1.jpg"
                },
                new ImagePageModel
                {
                    Category = "2",
                    ImageReference = "p2.jpg"
                },
                new ImagePageModel
                {
                    Category ="3",
                    ImageReference = "p3.jpg"
                }

            };

            /*for(int i = 1; i <= 113; i++)
            {

                All.Add(new ImagePageModel { category = "Same Test", imageReference = string.Format("p{0}.jpg", i) });
            }*/
        }
    }
}
