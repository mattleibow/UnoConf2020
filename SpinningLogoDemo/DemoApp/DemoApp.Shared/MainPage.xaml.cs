using System;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DemoApp
{
	public sealed partial class MainPage : Page
	{
		// a single "link" in the Uno Platform logo: https://s3.amazonaws.com/uno-website-assets/wp-content/uploads/2018/08/22113759/UnoLogoSmall.png
		private const string LogoLink = "M -12.006307,2.6154826 6.31,20.57 c 9.32,8.18 16.04,13.36 26.33,6.68 L 51.08,9.41 c 5.46,-5.67 10.18,-12.64 2.07,-23.39 0,0 -9.05,-9.28 -14.57,-13.1 -6.24,-4.71 -7.72,-4.68 -11.09,-4.34";

		// load the link path from SVG path data
		private SKPath logoLinkPath = SKPath.ParseSvgPathData(LogoLink);

		// add all the colors we want to use
		private SKColor[] logoColors =
		{
			0xff7a67f8, // purple
			0xfff85977, // red
			0xff159bff, // blue
			0xff67e5ad, // green
		};

		private DispatcherTimer timer;

		private float rotation;

		public MainPage()
		{
			InitializeComponent();

			// create a timer to slowly spin the logo
			timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(1000.0 / 30.0), // 30 FPS
			};
			timer.Tick += OnTimerTick;
			void OnTimerTick(object sender, object e)
			{
				rotation = (rotation + 1) % 360;
				skiaView.Invalidate();
			}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			// start the timer
			timer.Start();
		}

		protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
			// stop the timer
			timer.Stop();

			base.OnNavigatingFrom(e);
		}

		private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var canvas = e.Surface.Canvas;
			var info = e.Info;

			// clear the canvas of any old drawings
			canvas.Clear(SKColors.White);

			// move the canvas so the center of the canvas is under our brush
			canvas.Translate(info.Width / 2, info.Height / 2);

			// zoom in on the canvas
			canvas.Scale(5);

			// rotate the canvas by the number
			canvas.RotateDegrees(rotation);

			// create the paint brush that we will use to paint with
			using var paint = new SKPaint
			{
				IsAntialias = true,
				StrokeWidth = 7,
				Style = SKPaintStyle.Stroke,
				StrokeCap = SKStrokeCap.Butt,
				StrokeJoin = SKStrokeJoin.Round,
			};

			// we have 4 links
			for (var i = 0; i < 4; i++)
			{
				// dip the paint brush into the paint color
				paint.Color = logoColors[i];

				// draw that path onto the canvas
				canvas.DrawPath(logoLinkPath, paint);

				// rotate the canvas 90* clockwise
				canvas.RotateDegrees(-90);
			}

			// because the links always overlap, we need to draw half the first one again
			// we don't need to rotate as the loop above just did

			// cover the canvas so we can't draw on anything but a small hole that we know won't overlap
			var stencilHole = SKRect.Create(0, 0, logoLinkPath.Bounds.Width, logoLinkPath.Bounds.Height);
			canvas.ClipRect(stencilHole);

			// dip the paint brush into first paint color again
			paint.Color = logoColors[0];

			// draw that first path onto the canvas again
			canvas.DrawPath(logoLinkPath, paint);
		}
	}
}
