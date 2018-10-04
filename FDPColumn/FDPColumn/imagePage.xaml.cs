using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FDPColumn
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class imagePage : ContentPage
	{
		public imagePage (int pageNumber)
		{
			InitializeComponent ();
            moilabel.Text = pageNumber.ToString();
            pageImage.Source = "waterfront.jpg";
        }
	}
}