﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using FDPColumn;
using FDPColumn.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using CoreGraphics;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ZoomCachedImage), typeof(ZoomImageRenderer))]
namespace FDPColumn.iOS
{
    [Preserve(AllMembers = true)]
    public class ZoomImageRenderer : ViewRenderer<ZoomCachedImage, UIScrollView>
    {
        private ZoomCachedImage _zoomCachedImage;
        private UIScrollView _scrollView;
        private UIImageView _imageView;
        private nfloat _baseScalingFactor;
        private bool _isSwiping = false;


        protected override void OnElementChanged(ElementChangedEventArgs<ZoomCachedImage> e)
        {
            if (Control == null && e.NewElement != null)
            {
                //setup the control to be a scrollview with an image in it
                _zoomCachedImage = e.NewElement;

                //create the scrollview
                _scrollView = new UIScrollView
                {
                    ClipsToBounds = true,
                    BackgroundColor = UIColor.White,
                    ContentMode = _zoomCachedImage.Aspect.ToUIViewContentMode(),
                    ScrollEnabled = _zoomCachedImage.ScrollEnabled
                };

                //add the image view to it
                AssignImage();
                _scrollView.AddSubview(_imageView);
                // setup the zooming and double tap
                _scrollView.ViewForZoomingInScrollView += (view) => _imageView;

                if (_zoomCachedImage.DoubleTapToZoomEnabled)
                {
                    _scrollView.AddGestureRecognizer(
                        new UITapGestureRecognizer((gest) =>
                        {
                            var location = gest.LocationOfTouch(0, _scrollView);
                            //If we are 100% zoomed out, then zoom in :
                            if (_scrollView.ZoomScale == _scrollView.MinimumZoomScale)
                            {
                                _scrollView.ZoomToRect(GenerateZoomRect(_scrollView, (float)_zoomCachedImage.TapZoomScale, location), true);

                            }
                            else if (_scrollView.ZoomScale == _scrollView.MaximumZoomScale) //We are 100% zoomed in
                            {
                                //Zoom to 50000 scale, if curious as to why that is look at GenerateZoomRect function
                                _scrollView.ZoomToRect(GenerateZoomRect(_scrollView, (float)50000, location), true);
                            }
                            else //If we are not 100% zoomed out, then zoom out
                            {
                                _scrollView.ZoomToRect(GenerateZoomRect(_scrollView, (float)0.000005, location), true);
                            }

                        })
                        { NumberOfTapsRequired = 2 }
                    );
                }
                _scrollView.PinchGestureRecognizer.Enabled = _zoomCachedImage.ZoomEnabled;

                this.SetNativeControl(_scrollView);
            }
            SetNeedsDisplay();
            base.OnElementChanged(e);
        }


        private void AssignImage()
        {
            //reset the scroll or the size and offsets will all be off for the new image(do this before updating the image)
            ResetScrollView();

            var fileSource = _zoomCachedImage.Source as FileImageSource;

            try
            {
                //use a file source
                using (var image = UIImage.FromFile(fileSource.File))
                {
                    if (_imageView == null)
                    {
                        _imageView = new UIImageView(image);
                    }
                    else
                    {
                        _imageView.Image = image;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading image from file {fileSource.File}. Exception: {e.Message}");
            }


            _imageView.SizeToFit();
            _scrollView.ContentSize = _imageView.Frame.Size;

        }

        private void ResetScrollView()
        {
            _scrollView.ContentOffset = new CGPoint(0, 0);
            _scrollView.ContentInset = new UIEdgeInsets(0, 0, 0, 0);
            //the min and max don't really matter, they will be reset, just need 1.0 to be in the range so that when the new image is set the sizing is correct
            _scrollView.MinimumZoomScale = 1.0f;
            _scrollView.MaximumZoomScale = 2.0f;
            _scrollView.ZoomScale = 1.0f;
        }

        private void SetZoomToAspect(bool reapplyCurrentScale = false)
        {
            // the min and max zoom provided by the zoom control will be based on whatever initial scale is determined here
            // so 10X max will be 10 x original zoom and similiarly for min zoom

            if (_scrollView == null || _imageView == null || _imageView.Image == null)
                return;

            // if the scroll view doesn't have any size, just exit
            if (_scrollView.Frame.Width == 0 || _scrollView.Frame.Height == 0)
                return;

            if (_baseScalingFactor == 0)
                reapplyCurrentScale = false;

            // if reapplying the current scale, hold on to what it currently is without the base scaling factor (which may change)
            nfloat oldScale = 0;
            if (reapplyCurrentScale)
                oldScale = _scrollView.ZoomScale / _baseScalingFactor;

            // get the scale for each dimension
            var wScale = _scrollView.Frame.Width / _imageView.Image.Size.Width;
            var hScale = _scrollView.Frame.Height / _imageView.Image.Size.Height;


            // determine the base scaling factor to use based on the requested aspect
            /*switch (_zoomCachedImage.Aspect)
            {
                case Aspect.AspectFill:
                case Aspect.Fill:
                    // fill the view, so scale to the larger of the two scales
                    _baseScalingFactor = (nfloat)Math.Max(wScale, hScale);
                    break;
                default:
                    // fit the full image, so scale to the smaller of the two scales
                    _baseScalingFactor = (nfloat)Math.Min(wScale, hScale);
                    break;
            }*/
            _baseScalingFactor = wScale;

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                if(UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeLeft || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeRight)
                {
                    _baseScalingFactor = wScale;
                }                    
                else
                {
                    _baseScalingFactor = hScale;
                }
            }

            // assign the min and max zooms based on the user request and base scaling factor
            _scrollView.MinimumZoomScale = (nfloat)_zoomCachedImage.MinZoom * _baseScalingFactor;
            _scrollView.MaximumZoomScale = (nfloat)_zoomCachedImage.MaxZoom * _baseScalingFactor;

            // center image when filling the screen
            var widthDiff = (_imageView.Bounds.Width * _baseScalingFactor) - _scrollView.Bounds.Width;
            var heightDiff = (_imageView.Bounds.Height * _baseScalingFactor) - _scrollView.Bounds.Height;
            var widthOffset = Math.Max(widthDiff / 2, 0);
            double heightOffset;
            if(UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeLeft || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeRight)
            {
                heightOffset = 0;
            }
            else
            {
                heightOffset = Math.Max(heightDiff / 2, 0);
            }
            // if offset is 0, apply it immediately - it won't change and this allows the inset to be correct
            // logically this is == 0, but since using floats there are warnings about rounding errors
            //if (widthOffset < 0.1 && heightOffset < 0.1)
            if (widthOffset <= float.Epsilon && heightOffset <= float.Epsilon)
                _scrollView.SetContentOffset(new CGPoint(0, 0), false);
                //_scrollView.ContentOffset = new CGPoint(0, 0);


            // center the image in the scroll when image is smaller than the scroll view
            var inset = new UIEdgeInsets();
            if (widthDiff < 0)
                inset.Left = (nfloat)Math.Abs(widthDiff) / 2;
            if (heightDiff < 0)
                inset.Top = (nfloat)Math.Abs(heightDiff) / 4;
                //inset.Top = 0;
            _scrollView.ContentInset = inset;

            // set the current scale
            if (reapplyCurrentScale)
                _scrollView.SetZoomScale(oldScale * _baseScalingFactor, false);
            else
                _scrollView.SetZoomScale(_baseScalingFactor, false);

            // if non-zero offset, apply that animated (so it completes after the zoom) HERE'S THE CULPRIT <---!
            if (widthOffset > 0 || heightOffset > 0)
                _scrollView.SetContentOffset(new CGPoint(widthOffset, heightOffset), false);

            // updating the zoom scale resets the pinch gesture recognizer, so set it back to the current zoom enabled state
            _scrollView.PinchGestureRecognizer.Enabled = _zoomCachedImage.ZoomEnabled;
        }


        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                Console.WriteLine(e.PropertyName + " property has changed.");
                base.OnElementPropertyChanged(sender, e);

                if (e.PropertyName == Image.AspectProperty.PropertyName)
                {
                    SetZoomToAspect();
                }
                else if (e.PropertyName == ZoomCachedImage.CurrentZoomProperty.PropertyName)
                {
                    var scale = (nfloat)_zoomCachedImage.Scale * _baseScalingFactor;
                    _scrollView.SetZoomScale(scale, false);
                }
                else if (e.PropertyName == VisualElement.WidthProperty.PropertyName)
                {
                    await Task.Delay(1); // give a short delay for changes to be applied to the frame THE CULPRIT V2
                    SetZoomToAspect(true); // reapply the current scale
                    SetNeedsDisplay();
                }
                else if (e.PropertyName == ZoomCachedImage.MaxZoomProperty.PropertyName)
                {
                    _scrollView.MaximumZoomScale = (nfloat)_zoomCachedImage.MaxZoom * _baseScalingFactor;
                }
                else if (e.PropertyName == ZoomCachedImage.MinZoomProperty.PropertyName)
                {
                    _scrollView.MaximumZoomScale = (nfloat)_zoomCachedImage.MinZoom * _baseScalingFactor;
                }
                else if (e.PropertyName == ZoomCachedImage.ScrollEnabledProperty.PropertyName)
                {
                    _scrollView.ScrollEnabled = _zoomCachedImage.ScrollEnabled;
                }
                else if (e.PropertyName == Image.SourceProperty.PropertyName)
                {
                    AssignImage();
                    SetZoomToAspect();
                    SetNeedsDisplay();
                }
                else if (e.PropertyName == ZoomCachedImage.ZoomEnabledProperty.PropertyName)
                {
                    _scrollView.PinchGestureRecognizer.Enabled = _zoomCachedImage.ZoomEnabled;
                    // if zoom is disabled, return to aspect view
                    if (!_zoomCachedImage.ZoomEnabled)
                        SetZoomToAspect();
                }//Detect if carousel is swiping in order to disable back navigation
                /*else if (e.PropertyName == ZoomCachedImage.carouselIsSwipingProperty.PropertyName)
                {
                    var navctrl = this.ViewController.NavigationController;

                    if (_zoomCachedImage.carouselIsSwiping != _isSwiping && _isSwiping == true)
                    {
                        _isSwiping = false;
                        Console.WriteLine("Enable back nav here.");
                        navctrl.InteractivePopGestureRecognizer.Enabled = true;
                    }
                    else if(_zoomCachedImage.carouselIsSwiping != _isSwiping && _isSwiping == false)
                    {
                        _isSwiping = true;
                        Console.WriteLine("Disable back nav here.");
                        navctrl.InteractivePopGestureRecognizer.Enabled = false;
                    }
                }*/
            }
            catch (Exception ex)
            {
                // nothing we can really do here, but will catch it because it can be difficult
                // with bindings for the caller to catch
                Debug.WriteLine($"ZoomImageRenderer: Error: {ex.Message}\nStack: {ex.StackTrace}");
            }
        }

        private CGRect GenerateZoomRect(UIScrollView scrollView, float scaleFactor, CGPoint point)
        {
            nfloat scale;
            if (scrollView.ZoomScale < scrollView.MaximumZoomScale)
            {
                // not at max zoom so zoom in
                scale = (nfloat)Math.Min(scrollView.ZoomScale * scaleFactor, scrollView.MaximumZoomScale);
            }
            else
            {
                // already at max zoom so zoom out
                scale = (nfloat)Math.Max(scrollView.ZoomScale / scaleFactor, scrollView.MinimumZoomScale);
            }

            // note that the point location is from the top left of the image and is measured in the scaled size
            CGRect zoomRect = new CGRect
            {
                Height = scrollView.Frame.Height / scale,
                Width = scrollView.Frame.Width / scale,
                X = (point.X / scrollView.ZoomScale) - (scrollView.Frame.Width / (scale * 2.0f)), // half the width
                Y = (point.Y / scrollView.ZoomScale) - (scrollView.Frame.Height / (scale * 2.0f)) // half the height
            };

            return zoomRect;
        }

    }
}