using Sirenita4_app;
using Sirenita4_app.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sirenita4_app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new lateral());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
