﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FFImageLoading.Forms.Platform;

namespace FDPColumn.Droid
{
    public class CachedScaleImageViewGestureDetector : GestureDetector.SimpleOnGestureListener
    {
        private readonly CachedScaleImageView m_ScaleImageView;
        public CachedScaleImageViewGestureDetector(CachedScaleImageView imageView)
        {
            m_ScaleImageView = imageView;
        }

        public override bool OnDown(MotionEvent e)
        {
            return true;
        }

        public override bool OnDoubleTap(MotionEvent e)
        {
            m_ScaleImageView.TapZoomTo((int)e.GetX(), (int)e.GetY());
            m_ScaleImageView.Cutting();
            return true;
        }
    }

    public class CachedScaleImageView : CachedImageView, View.IOnTouchListener
    {

        private Context m_Context;

        private Matrix m_Matrix = new Matrix();
        private float[] m_MatrixValues = new float[9];
        private int m_Width;
        private int m_Height;
        private int m_IntrinsicWidth;
        private int m_IntrinsicHeight;
        private float _baseScale;
        private float m_Scale;
        private float m_MinScale;
        private float m_MaxScale = 4.0f;
        private float m_PreviousDistance;
        private int m_PreviousMoveX;
        private int m_PreviousMoveY;

        private bool m_IsScaling;
        private GestureDetector m_GestureDetector;


        ImagePageModel model = new ImagePageModel();

        private SurfaceOrientation _currentRotation = SurfaceOrientation.Rotation0;


        public CachedScaleImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            m_Context = context;
            this.Initialize();
        }

        public ZoomCachedImage zoomCachedImage { get; set; }

        public override void SetImageBitmap(Bitmap bm)
        {
            base.SetImageBitmap(bm);
            this.Initialize();
        }

        private void Initialize()
        {
            this.SetScaleType(ScaleType.Matrix);

            if (Drawable != null)
            {
                m_IntrinsicWidth = Drawable.IntrinsicWidth;
                m_IntrinsicHeight = Drawable.IntrinsicHeight;
                this.SetOnTouchListener(this);
            }

            m_GestureDetector = new GestureDetector(m_Context, new CachedScaleImageViewGestureDetector(this));
            

            
        }

        protected override bool SetFrame(int l, int t, int r, int b)
        {
            //set the view size variables
            m_Width = r - l;
            m_Height = b - t;

            ZoomToAspect();
            
            return base.SetFrame(l, t, r, b);
        }

        public void UpdateMinScaleFromZoomImage()
        {
            m_MinScale = (float)zoomCachedImage.MinZoom * _baseScale;
        }

        public void UpdateMaxScaleFromZoomImage()
        {
            m_MaxScale = (float)zoomCachedImage.MaxZoom * _baseScale;
        }

        private float GetValue(Matrix matrix, int whichValue)
        {
            matrix.GetValues(m_MatrixValues);
            return m_MatrixValues[whichValue];
        }

        public void ZoomToAspect()
        {
            // make sure there is a drawable size and container size to continue
            if (m_IntrinsicWidth > 1 && m_IntrinsicHeight > 1 && m_Width > 1 && m_Height > 1)
            {
                // ensure starting from a clean matrix to prevent multiple calls from throwing off the scale
                m_Matrix.Reset();
                this.SetScaleType(ScaleType.Matrix);

                // calculate the scale that should be used
                var hScale = m_Width / (float)m_IntrinsicWidth;
                //var vScale = m_Height / (float)m_IntrinsicHeight;
                /*if (zoomCachedImage.Aspect == Xamarin.Forms.Aspect.AspectFit)
                    m_Scale = Math.Min(hScale, vScale);
                else
                    m_Scale = Math.Max(hScale, vScale);*/
                m_Scale = hScale;
                // set the min and max scales
                _baseScale = m_Scale;
                m_MinScale = _baseScale * (float)zoomCachedImage.MinZoom;
                m_MaxScale = _baseScale * (float)zoomCachedImage.MaxZoom;
                // perform the zoom, todo (orientation)
                //Display display = Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>().DefaultDisplay;
                ZoomTo(m_Scale, m_IntrinsicWidth / 2, m_IntrinsicHeight / 2);
                m_Matrix.PostTranslate(0, m_IntrinsicHeight/2);
                Cutting();
            }
        }
        public float Scale
        {
            get { return this.GetValue(m_Matrix, Matrix.MscaleX); }
        }
        public float TranslateX
        {
            get { return this.GetValue(m_Matrix, Matrix.MtransX); }
        }

        public float TranslateY
        {
            get { return this.GetValue(m_Matrix, Matrix.MtransY); }
        }

        public void ZoomFromCurrentZoom()
        {
            // make sure there is an actual change
            if (Math.Abs(Scale - zoomCachedImage.CurrentZoom) < .1)
                return;

            var scale = (float)zoomCachedImage.CurrentZoom / Scale;
            ZoomTo(scale, m_Width / 2, m_Height / 2);
        }

        public void TapZoomTo(int x, int y)
        {
            if (zoomCachedImage.ZoomEnabled && zoomCachedImage.DoubleTapToZoomEnabled)
            {
                // if at any zoom, zoom out, I use 0.0005 scale here to simulate a very small relative scale    
                if (Scale != m_MinScale)
                    ZoomTo((float)0.0005, x, y);
                else//if not zoomed at all, zoom in
                    ZoomTo((float)zoomCachedImage.TapZoomScale, x, y);
            }
        }

        public void ZoomTo(float scale, float x, float y)
        {
            if (Scale * scale < m_MinScale)
            {
                scale = m_MinScale / Scale;
            }
            else
            {
                if (scale >= 1 && Scale * scale > m_MaxScale)
                {
                    scale = m_MaxScale / Scale;
                }
            }
            m_Matrix.PostScale(scale, scale);
            //move to center
            m_Matrix.PostTranslate(-(m_Width * scale - m_Width) / 2, -(m_Height * scale - m_Height) / 2);


            //move x and y distance
            m_Matrix.PostTranslate(-(x - (m_Width / 2)) * scale, 0);
            m_Matrix.PostTranslate(0, -(y - (m_Height / 2)) * scale);

            //mine
            //float dx = x/Scale;
            //float dy = y/Scale;
            //m_Matrix.PostTranslate(-dx, -dy);
            ImageMatrix = m_Matrix;
            zoomCachedImage.CurrentZoom = Scale;
            model.ImageZoomedIn(Scale != _baseScale);
        }

        public void Cutting()
        {
            var width = (int)(m_IntrinsicWidth * Scale);
            var height = (int)(m_IntrinsicHeight * Scale);
            if (TranslateX < -(width - m_Width))
            {
                m_Matrix.PostTranslate(-(TranslateX + width - m_Width), 0);
            }

            if (TranslateX > 0)
            {
                m_Matrix.PostTranslate(-TranslateX, 0);
            }

            if (TranslateY < -(height - m_Height))
            {
                m_Matrix.PostTranslate(0, -(TranslateY + height - m_Height));
            }

            if (TranslateY > 0)
            {
                m_Matrix.PostTranslate(0, -TranslateY);
            }

            if (width < m_Width)
            {
                m_Matrix.PostTranslate((m_Width - width) / 2, 0);
            }

            if (height < m_Height)
            {
                m_Matrix.PostTranslate(0, (m_Height - height) / 2);
            }

            ImageMatrix = m_Matrix;
        }

        private float Distance(float x0, float x1, float y0, float y1)
        {
            var x = x0 - x1;
            var y = y0 - y1;
            return (float)Math.Sqrt(x * x + y * y);
        }

        private Tuple<float, float> MidPoint(float x0, float x1, float y0, float y1)
        {
            var x = (x0 + x1) / 2;
            var y = (y0 + y1) / 2;
            return Tuple.Create(x, y);
        }

        private float DispDistance()
        {
            return (float)Math.Sqrt(m_Width * m_Width + m_Height * m_Height);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            // only handle the touch if either scroll or zoom is enabled
            if (!zoomCachedImage.ZoomEnabled && !zoomCachedImage.ScrollEnabled)
                return false;

            if (m_GestureDetector.OnTouchEvent(e))
            {
                m_PreviousMoveX = (int)e.GetX();
                m_PreviousMoveY = (int)e.GetY();
                return true;
            }

            var touchCount = e.PointerCount;
            var handled = false;
            switch (e.Action)
            {
                case MotionEventActions.Down:
                case MotionEventActions.Pointer1Down:
                case MotionEventActions.Pointer2Down:
                    if (touchCount >= 2)
                    {
                        if (zoomCachedImage.ZoomEnabled)
                        {
                            var distance = this.Distance(e.GetX(0), e.GetX(1), e.GetY(0), e.GetY(1));
                            m_PreviousDistance = distance;
                            m_IsScaling = true;
                            handled = true;
                        }
                    }
                    break;

                case MotionEventActions.Move:
                    if (touchCount >= 2 && m_IsScaling)
                    {
                        if (zoomCachedImage.ZoomEnabled)
                        {
                            var distance = this.Distance(e.GetX(0), e.GetX(1), e.GetY(0), e.GetY(1));
                            var scale = (distance - m_PreviousDistance) / this.DispDistance();
                            m_PreviousDistance = distance;
                            scale += 1;
                            scale = scale * scale;

                            //var focus = MidPoint(e.GetX(0), e.GetX(1), e.GetY(0), e.GetY(1));
                            //float xdist = focus.Item1 - m_Width / 2;
                            //float ydist = focus.Item2 - m_Height / 2;
                            ZoomTo(scale, m_Width / 2, m_Height / 2);
                            //ZoomTo(scale, xdist/m_Width, ydist/m_Height); 
                            Cutting();

                            handled = true;
                        }
                    }
                    else if (!m_IsScaling)
                    {
                        if (zoomCachedImage.ScrollEnabled)
                        {
                            var distanceX = m_PreviousMoveX - (int)e.GetX();
                            var distanceY = m_PreviousMoveY - (int)e.GetY();
                            m_PreviousMoveX = (int)e.GetX();
                            m_PreviousMoveY = (int)e.GetY();

                            m_Matrix.PostTranslate(-distanceX, -distanceY);
                            this.Cutting();

                            handled = true;
                        }
                    }
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Pointer1Up:
                case MotionEventActions.Pointer2Up:
                    if (touchCount <= 1)
                    {
                        m_IsScaling = false;
                        handled = true;
                    }
                    break;
            }
            return handled;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            return OnTouchEvent(e);
        }
    }
}