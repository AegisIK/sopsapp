﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;
namespace FDPColumn
{
    public class ImagePageModel
    {

        public ObservableCollection<Image> All { get; set; }

        public ImagePageModel()
        {

            All = new ObservableCollection<Image>{
                new Image
                {
                    Category = "Resp",
                    ImageReference = "p1.jpg"
                },
                new Image
                {
                    Category = "OB",
                    ImageReference = "p2.jpg"
                },
                new Image
                {
                    Category ="General",
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
