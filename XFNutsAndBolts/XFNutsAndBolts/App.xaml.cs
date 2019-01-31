using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
