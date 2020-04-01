using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

using Xamarin.Forms;


namespace FDPColumn
{
    public class ImagePageModel
    {
        
        public ImagePageModel()
        {
        }

        public void ImageZoomedIn (bool zoomedIn)
        {

            ImagePageSwipeAnimated currPage;

            int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;

            currPage = (ImagePageSwipeAnimated)Application.Current.MainPage.Navigation.NavigationStack[index];

            currPage.CarouselSwipeController(zoomedIn);
        }
    }
}
