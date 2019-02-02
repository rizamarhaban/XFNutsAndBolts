using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using XFNutsAndBolts.iOS.ConfigAndSettings;

namespace XFNutsAndBolts.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Can use injection if want it, but this loads only once
            // no need to add resources just for loading one settings

            var name1 = ConfigAndSetting.GalGagot;
            var name2 = ConfigAndSetting.GalGadotInJapanese;
            var data = CleanByteOrderMark(ConfigAndSetting.appsettings);
            var json = Encoding.UTF8.GetString(data);

            LoadApplication(new App(name1, name2, json));

            return base.FinishedLaunching(app, options);
        }

        // helper if using file resource. not needed if use string only.
        // can make as extensions as well
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
