using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using XFNutsAndBolts.Droid.ConfigAndSettings;
using System.Linq;

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
            var data = CleanByteOrderMark(ConfigAndSetting.appsettings);
            var json = Encoding.UTF8.GetString(data);

            LoadApplication(new App(name1, name2, json));
        }

        // helper if using file resource. not needed if us string only.
        // can make  as extensions as well
        public byte[] CleanByteOrderMark(byte[] bytes)
        {
            var bom = new byte[] { 0xEF, 0xBB, 0xBF };
            var empty = Enumerable.Empty<byte>();
            if (bytes.Take(3).SequenceEqual(bom))
                return bytes.Skip(3).ToArray();

            return bytes;
        }
    }


}