using Android.App;
using Android.Views;
using Uno.UI;

namespace DemoApp.Droid
{
	[Activity(
		MainLauncher = true,
		ConfigurationChanges = ActivityHelper.AllConfigChanges,
		WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden)]
	public class MainActivity : Windows.UI.Xaml.ApplicationActivity
	{
	}
}
