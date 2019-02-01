using System;
using global::Autofac;
using System.Collections.Generic;
using System.Text;
using XFNutsAndBolts.Settings;

namespace XFNutsAndBolts.ViewModels
{
    public class MainPageViewModel
    {
        private readonly IAppSettingsTest _settings;

        public MainPageViewModel(IAppSettingsTest settings)
        {
            _settings = settings;
        }

        public string Name => _settings.Name1;

        public string NameInJapanese => _settings.Name2;

        public string AppVersion => _settings.AppSettings.AppVersion.ToString();
    }
}
