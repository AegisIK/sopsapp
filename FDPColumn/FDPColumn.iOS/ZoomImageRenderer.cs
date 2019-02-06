/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using FDPColumn;
using FDPColumn.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ZoomCachedImage), typeof(ZoomImageRenderer))]
namespace FDPColumn.iOS
{
    [Preserve(AllMembers =true)]
    public class ZoomImageRenderer : ViewRenderer<ZoomCachedImage, UIScrollView>
    {
        private ZoomCachedImage _zoomCachedImage;
        private UIScrollView _scrollView;
        private UIImageView _imageView;
        private nfloat _baseScalingFactor;

        protected override void OnElementChanged(ElementChangedEventArgs<ZoomCachedImage> e)
        {
            base.OnElementChanged(e);
            if(this.Control == null && e.NewElement != null)
            {
                //setup the control to be a scrollview with an image in it
                _zoomCachedImage = e.NewElement;

                //create the scrollview
                _scrollView = new UIScrollView
                {
                    ClipsToBounds = true,
                    BackgroundColor = UIColor.Red,
                    ContentMode = _zoomCachedImage.Aspect.ToUIViewContentMode(),
                    ScrollEnabled = _zoomCachedImage.ScrollEnabled
                };

                //add the image view to it
                await AssignImageAsync();
                _scrollView.AddSubview(_imageView);
                // setup the zooming and double tap
                _scrollView.ViewForZoomingInScrollView += (view) => _imageView;

                _scrollView.AddGestureRecognizer(
                    new UITapGestureRecognizer((gest) =>
                    {
                        if (_zoomCachedImage.DoubleTapToZoomEnabled)
                        {
                            var location = gest.LocationOfTouch(0, _scrollView);
                            _scrollView.ZoomToRect(GenerateZoomRect(_scrollView, (float)_zoomCachedImage.TapZoomScale, location), true);
                        }
                    })
                    { NumberOfTapsRequired = 2 }
                );
                _scrollView.PinchGestureRecognizer.Enabled = _zoomCachedImage.ZoomEnabled;

                this.SetNativeControl(_scrollView);
            }
            SetNeedsDisplay();
        }


        private async Task AssignImageAsync()
        {
            //reset the scroll or the size and offsets will all be off for the new image(do this before updating the image)
            ResetScrollView();

            var fileSource = _zoomCachedImage.Source as FileImageSource;

            try
            {
                //use a file source
                using (var image = UIImage.FromFile(fileSource.File))
                {
                    if(_imageView == null)
                    {
                        _imageView = new UIImageView(image);
                    }
                    else
                    {
                        _imageView.Image = image;
                    }
                }
            }
        }
    }
}*/