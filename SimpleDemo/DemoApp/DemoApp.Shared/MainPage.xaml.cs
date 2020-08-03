using SkiaSharp;
using SkiaSharp.Views.UWP;
using Windows.UI.Xaml.Controls;

namespace DemoApp
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var canvas = e.Surface.Canvas;
			var info = e.Info;

			canvas.Clear();
		}
	}
}
