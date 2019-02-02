using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFNutsAndBolts
{
    public partial class MainPage : ContentPage
    {
        private readonly Animation _labelFadingAnimation;
        private const string AnimHandle = "Spinner";

        public MainPage()
        {
            InitializeComponent();

            _labelFadingAnimation = new Animation(x =>
            {
                lblScanning.TextColor = Color.FromHsla(x, 1, 0.5);
                lblScanning.Opacity = x;
            }, 0, 1);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AnimationLoop();
        }

        private void AnimationLoop()
        {
            var sw = new Stopwatch();
            sw.Start();

            _labelFadingAnimation.Commit(this, AnimHandle, 12, 750, Easing.Linear,
                (d, b) =>
                {
                    lblScanning.TextColor = Color.Red;
                },
                () =>
                {
                    if (sw.ElapsedMilliseconds > 10000)
                    {
                        sw.Stop();
                        return false;
                    }
                    return true;
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.AbortAnimation(AnimHandle);
        }
    }
}
