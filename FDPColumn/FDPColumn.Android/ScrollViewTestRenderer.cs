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
using Xamarin.Forms.Platform.Android;
using static Android.Views.ScaleGestureDetector;
using Android.Views.Animations;
using Xamarin.Forms;
using FDPColumn.Droid;
using FDPColumn;

[assembly: ExportRenderer(typeof(RendyTestView), typeof(ZoomScrollViewRenderer))]
namespace FDPColumn.Droid
{

    public class ZoomScrollViewRenderer : ViewRenderer, IOnScaleGestureListener
    {
        public ZoomScrollViewRenderer(Context context) : base(context)
        {
            
        }

        private float mScale = 1f;
        private ScaleGestureDetector mScaleDetector;

        private const int INVALID_POINTER_ID = -1;

        private float mLastTouchX;
        private float mLastTouchY;

        private float mPosX;
        private float mPosY;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            mScaleDetector = new ScaleGestureDetector(Context, this);
        }

        private int mActivePointerID = INVALID_POINTER_ID;
        public override bool DispatchTouchEvent(MotionEvent e)
        {
            base.DispatchTouchEvent(e);
            mScaleDetector.OnTouchEvent(e);
            if(e.PointerCount > 1)
            {
                return true;
            }
            MotionEventActions action = e.ActionMasked;

            switch(action)
            {
                case MotionEventActions.Down:
                    int pointerIndex = e.ActionIndex;
                    float x = e.GetX(pointerIndex);
                    float y = e.GetY(pointerIndex);

                    //Remember where we started (for dragging)
                    mLastTouchX = x;
                    mLastTouchY = y;
                    //Save the ID of this pointer (for dragging)
                    mActivePointerID = e.GetPointerId(0);
                    break;

                case MotionEventActions.Move:
                    //Find the index of the active pointer and fetch its position
                    int pointerIndex1 = e.FindPointerIndex(mActivePointerID);

                    float x1 = e.GetX(pointerIndex1);
                    float y1 = e.GetY(pointerIndex1);

                    //Calculate the distance moved
                    float dx = x1 - mLastTouchX;
                    float dy = y1 - mLastTouchY;

                    mPosX += dx;
                    mPosY += dy;

                    TranslateAnimation translateAnimation = new TranslateAnimation(0, mPosX, 0, mPosY);
                    translateAnimation.Duration = 0;
                    translateAnimation.FillAfter = true;
                    StartAnimation(translateAnimation);

                    mLastTouchX = x1;
                    mLastTouchY = y1;
                    break;

                case MotionEventActions.Up:
                    mActivePointerID = INVALID_POINTER_ID;
                    break;

                case MotionEventActions.Cancel:
                    mActivePointerID = INVALID_POINTER_ID;
                    break;

                case MotionEventActions.PointerUp:
                    int pointerIndex2 = e.ActionIndex;
                    int pointerId = e.GetPointerId(pointerIndex2);

                    if(pointerId == mActivePointerID)
                    {
                        //This was our active pointer going up. Choose a new active pointer and adjust accordingly
                        int newPointerIndex = pointerIndex2 == 0 ? 1 : 0;
                        mLastTouchX = e.GetX(newPointerIndex);
                        mLastTouchY = e.GetY(newPointerIndex);
                        mActivePointerID = e.GetPointerId(newPointerIndex);
                    }
                    break;

            }
            return true;
        }

        public bool OnScale(ScaleGestureDetector detector)
        {
            float scale = 1 - detector.ScaleFactor;

            float prevScale = mScale;
            mScale += scale;

            if (mScale < 0.5f) // Minimum scale condition:
                mScale = 0.5f;

            if (mScale > 1f) // Maximum scale condition:
                mScale = 1f;
            ScaleAnimation scaleAnimation = new ScaleAnimation(1f / prevScale, 1f / mScale, 1f / prevScale, 1f / mScale, detector.FocusX, detector.FocusY);
            scaleAnimation.Duration = 0;
            scaleAnimation.FillAfter = true;
            scaleAnimation.FillEnabled = true;
            scaleAnimation.FillBefore = true;
            StartAnimation(scaleAnimation);
            return true;
        }

        public bool OnScaleBegin(ScaleGestureDetector detector)
        {
            return true;
        }

        public void OnScaleEnd(ScaleGestureDetector detector)
        {

        }
    }
}