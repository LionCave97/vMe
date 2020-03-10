using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using vMe.Services;
using vMe.Views;
using Xamarin.Essentials;

namespace vMe
{
    public partial class Application : Xamarin.Forms.Application
    {
        
        public Application()
        {           
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            var Accelerometer = new Services.AccelerometerSensor.AccelerometerTest();
            if (!Accelerometer.CheckAccelerometer())
            {
                Accelerometer.ToggleAccelerometer();
            }
        }

        protected override void OnSleep()
        {
            var Accelerometer = new Services.AccelerometerSensor.AccelerometerTest();
            if (Accelerometer.CheckAccelerometer())
            {
                Accelerometer.ToggleAccelerometer();
            }
        }

        protected override void OnResume()
        {
            var Accelerometer = new Services.AccelerometerSensor.AccelerometerTest();
            if (!Accelerometer.CheckAccelerometer())
            {
                Accelerometer.ToggleAccelerometer();
            }
        }
    }
}
