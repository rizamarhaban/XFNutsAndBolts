using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using XFNutsAndBolts.Droid.ConfigAndSettings;

namespace XFNutsAndBolts.Droid
{
    [Activity(Label = "XFNutsAndBolts", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            // Can use injection if want it, but this loads only once
            // no need to add resources just for loading one settings

            var name1 = ConfigAndSetting.GalGagot;
            var name2 = ConfigAndSetting.GalGadotInJapanese;
            var json = Encoding.UTF8.GetString(ConfigAndSetting.appsettings);

            LoadApplication(new App(name1, name2, json));
        }
    }
}