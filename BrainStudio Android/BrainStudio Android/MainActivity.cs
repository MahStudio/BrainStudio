using Android.App;
using Android.Widget;
using Android.OS;

namespace BrainStudio_Android
{
    [Activity(Label = "BrainStudio_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //newproj
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

