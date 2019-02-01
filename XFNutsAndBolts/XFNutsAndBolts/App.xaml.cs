using global::Autofac;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFNutsAndBolts.Models;
using XFNutsAndBolts.Settings;
using XFNutsAndBolts.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFNutsAndBolts
{
    public partial class App : Application
    {
        public App(string name1, string name2, string json)
        {
            InitializeComponent();

            // Can inject it or use directly here
            Debug.WriteLine(name1);
            Debug.WriteLine(name2);
            Debug.WriteLine(json);

            var settings = new AppSettingsTest
            {
                Name1 = name1,
                Name2 = name2,
                AppSettings = JsonConvert.DeserializeObject<AppSettings>(json)
            };

            Bootstrapper.Initialize(settings);
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
