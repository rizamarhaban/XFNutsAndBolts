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
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace XFNutsAndBolts.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAppSettingsTest _settings;
        private ObservableCollection<string> _theProcess;
        private string _name;
        private string _nameInJapanese;
        private string _appVersion;
        private bool _isReady;

        private delegate void InitViewModel();

        public MainPageViewModel(IAppSettingsTest settings)
        {
            _settings = settings;

            _theProcess = new ObservableCollection<string>();
            new InitViewModel(StartBackgroundWork).Invoke();
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

        public bool IsReady
        {
            get { return _isReady; }
            set
            {
                RaisePropertyChanged(ref _isReady, value);
            }
        }

        public ICommand RunCommand => new Command(() =>
        {
            new InitViewModel(StartBackgroundWork).Invoke();
        });

        public ObservableCollection<string> TheProcess
        {
            get
            {
                return _theProcess;
            }
        }

        public void StartBackgroundWork()
        {
            _theProcess.Clear();
            _theProcess.Add("Shows use of Start to start on a background thread");

            IsReady = false;
            Name = string.Empty;
            NameInJapanese = string.Empty;
            AppVersion = string.Empty;

            Observable.Start(async () => await ParallelExecutionExample())
                .Finally(() => _theProcess.Add("Main thread completed"));

            _theProcess.Add("In Main Thread... I still Runs..."); // This will be executed no matter what

        }

        public async Task ParallelExecutionExample()
        {
            MessagingCenter.Send<MainPageViewModel>(this, "Start");

            var o = Observable.CombineLatest(
                Observable.Start(() =>
                {
                    _theProcess.Add($"Executing 1st on Thread: {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(4000);   // example of looooooong API call
                    Name = _settings.Name1;
                    _theProcess.Add("Executing 1st Thread done");
                    return "Result A";
                }),

                Observable.Start(() =>
                {
                    _theProcess.Add($"Executing 2nd on Thread: {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(6000);   // example of looooooong API call
                    NameInJapanese = _settings.Name2;
                    _theProcess.Add("Executing 2nd Thread done");
                    return "Result B";
                }),

                Observable.Start(() =>
                {
                    _theProcess.Add($"Executing 3rd on Thread: {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(3000);   // example of looooooong API call
                    AppVersion = _settings.AppSettings.AppVersion.ToString();
                    _theProcess.Add("Executing 3rd Thread done");
                    return "Result C";
                })

            ).Finally(() =>
            {
                _theProcess.Add("Executing All Threads Done");
                IsReady = true;
            });

            foreach (string r in await o.FirstAsync()) _theProcess.Add(r);

        }
    }
}
