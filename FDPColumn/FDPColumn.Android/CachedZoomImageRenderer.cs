using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FDPColumn;
using FDPColumn.Droid;
using FFImageLoading;
using FFImageLoading.Forms;
using FFImageLoading.Forms.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ZoomCachedImage), typeof(CachedZoomImageRenderer))]
namespace FDPColumn.Droid
{
    public class CachedZoomImageRenderer : CachedImageRenderer
    {

        private ZoomImage _zoomImage;
        private ScaleImageView _scaleImage; //replace with my own cachedscaleimage
        public CachedZoomImageRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<CachedImage> e)
        {
            base.OnElementChanged(e);
        }
    }
}