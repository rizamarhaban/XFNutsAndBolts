using System;
using global::Autofac;
using System.Collections.Generic;
using System.Text;
using XFNutsAndBolts.Settings;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFNutsAndBolts.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAppSettingsTest _settings;
        private string _name;
        private string _nameInJapanese;
        private string _appVersion;

        public MainPageViewModel(IAppSettingsTest settings)
        {
            _settings = settings;

            StartBackgroundWork();
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                RaisePropertyChanged(ref _name, value);
            }
        }

        public string NameInJapanese
        {
            get { return _nameInJapanese; }
            set
            {
                if (value == _nameInJapanese) return;
                RaisePropertyChanged(ref _nameInJapanese, value);
            }
        }

        public string AppVersion
        {
            get { return _appVersion; }
            set
            {
                if (value == _appVersion) return;
                RaisePropertyChanged(ref _appVersion, value);
            }

        }

        public void StartBackgroundWork()
        {
            Debug.WriteLine("Shows use of Start to start on a background thread:");

            var o = Observable.Start(async () =>
            {
                Name = string.Empty;
                NameInJapanese = string.Empty;
                AppVersion = string.Empty;

                await ParallelExecutionExample();

            }).Finally(() => Debug.WriteLine("Main thread completed.")); // once API call finish, render here

            Debug.WriteLine("\r\n\t In Main Thread... I still Runs...\r\n");   // This will be executed no matter what
            o.Wait();   // Wait for completion of background operation.
        }

        public async Task ParallelExecutionExample()
        {
            var o = Observable.CombineLatest(
                Observable.Start(() =>
                {
                    Debug.WriteLine("Executing 1st on Thread: {0}", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(4000);   // example of looooooong API call
                    Name = _settings.Name1;
                    Debug.WriteLine("Executing 1st Thread done");
                    return "Result A";
                }),

                Observable.Start(() =>
                {
                    Debug.WriteLine("Executing 2nd on Thread: {0}", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(6000);   // example of looooooong API call
                    NameInJapanese = _settings.Name2;
                    Debug.WriteLine("Executing 2nd Thread done");

                    return "Result B";
                }),

                Observable.Start(() =>
                {
                    Debug.WriteLine("Executing 3rd on Thread: {0}", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(3000);   // example of looooooong API call
                    AppVersion = _settings.AppSettings.AppVersion.ToString();
                    Debug.WriteLine("Executing 3rd Thread done");
                    return "Result C";
                })

            ).Finally(() => Debug.WriteLine("Done!"));

            foreach (string r in await o.FirstAsync())
                Debug.WriteLine(r);
        }
    }
}
