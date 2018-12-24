using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ListViewAsGrid
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var model = new MainViewModel
            {
                CurrentPage = new MainPage()
            };
            model.CurrentPage.BindingContext = model;
            var nav = new NavigationPage(model.CurrentPage);
            model._nav = nav.Navigation;
            MainPage = nav;

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
