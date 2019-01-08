using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private ZoomCachedImage _zoomCachedImage;
        private CachedScaleImageView _cachedScaleImageView;
        public CachedZoomImageRenderer(Context context) : base(context)
        {

        }
        protected async override void OnElementChanged(ElementChangedEventArgs<CachedImage> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                _zoomCachedImage = (ZoomCachedImage)e.NewElement;

                //create the scale image and set it as the native control so it's available
                _cachedScaleImageView = new CachedScaleImageView(Context, null);
                _cachedScaleImageView.zoomCachedImage = _zoomCachedImage;
                SetNativeControl(_cachedScaleImageView);
                await LoadImage();
            }
        }

        protected async override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ZoomCachedImage.AspectProperty.PropertyName
                || e.PropertyName == ZoomCachedImage.HeightProperty.PropertyName
                || e.PropertyName == ZoomCachedImage.WidthProperty.PropertyName)
            {
                _cachedScaleImageView.ZoomToAspect();
            }
            else if (e.PropertyName == ZoomCachedImage.SourceProperty.PropertyName)
            {
                await LoadImage();
                _cachedScaleImageView.ZoomToAspect();
            }
            else if (e.PropertyName == ZoomCachedImage.CurrentZoomProperty.PropertyName)
            {
                _cachedScaleImageView.ZoomFromCurrentZoom();
            }
            else if (e.PropertyName == ZoomCachedImage.MaxZoomProperty.PropertyName)
            {
                _cachedScaleImageView.UpdateMaxScaleFromZoomImage();
            }
            else if (e.PropertyName == ZoomCachedImage.MinZoomProperty.PropertyName)
            {
                _cachedScaleImageView.UpdateMinScaleFromZoomImage();
            }
        }

        private async Task LoadImage()
        {
            var image = await (new FileImageSourceHandler()).LoadImageAsync(_zoomCachedImage.Source, Context);
            try
            {
                if (image != null && image.ByteCount > 0)
                    _cachedScaleImageView.SetImageBitmap(image);
            }
            catch (Exception e)
            {
                // catch an image loading failure
                Console.WriteLine($"Unable to load bitmap. Exception: {e.Message}");
            }
        }


    }
}