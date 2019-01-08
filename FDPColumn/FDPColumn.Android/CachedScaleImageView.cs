using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading.Forms.Platform;

namespace FDPColumn.Droid
{
    public class ScaleImageViewGestureDetector : GestureDetector.SimpleOnGestureListener
    {
        private readonly ScaleImageView m_ScaleImageView;
        public ScaleImageViewGestureDetector(ScaleImageView imageView)
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

    public class CachedScaleImageView : CachedImageView
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

        public CachedScaleImageView(Context context) : base(context)
        {
            
        }
    }
}