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

			// clear the canvas of any old drawings
			canvas.Clear(SKColors.White);

			// create a nice 200x100 rectangle that is at an off set of 20x30
			var niceRectangle = SKRect.Create(20, 30, 200, 100);

			// create the red paint
			using var redPaint = new SKPaint
			{
				Color = 0xFFF85977, // "Uno red"
				Style = SKPaintStyle.Fill,
			};

			// draw that beautiful rectangle using the red paint
			canvas.DrawRect(niceRectangle, redPaint);
		}
	}
}
