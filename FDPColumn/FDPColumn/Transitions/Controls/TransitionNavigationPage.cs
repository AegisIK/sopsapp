using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using FDPColumn.Transitions.Enums;

namespace FDPColumn.Transitions.Controls
{
    class TransitionNavigationPage : NavigationPage
    {
        public static readonly BindableProperty TransitionTypeProperty = BindableProperty.Create("TransitionType", typeof(TransitionType), typeof(TransitionNavigationPage), TransitionType.SlideFromLeft);

        public TransitionType TransitionType
        {
            get { return (TransitionType)GetValue(TransitionTypeProperty); }
            set { SetValue(TransitionTypeProperty, value); }
        }

        public TransitionNavigationPage() : base()
        {
        }

        public TransitionNavigationPage(Page root) : base(root)
        {
        }
    }
}
