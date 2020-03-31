using System;
using Android.App;
using Android.Hardware;
using Android.Content;
using vMe.Droid.Implementations;
using vMe.Services;
using Plugin.DeviceSensors;

//Android Pedometer
[assembly: Xamarin.Forms.Dependency(typeof(PedometerSensorImplementations))]
namespace vMe.Droid.Implementations
{
    public class PedometerSensorImplementations : PedometerSensor
    {
        private StepKeeper steps = new StepKeeper();

        public DeviceSteps GetPedometer()
        {
            Startup();
            return DeviceSteps.Still;            
        }

        //Setup and monitor Pedometer
        private void Startup()
        {
            if (CrossDeviceSensors.Current.Pedometer.IsSupported)
            {
                CrossDeviceSensors.Current.Pedometer.OnReadingChanged += (s, a) => {

                    Console.WriteLine("Step updated");

                    steps.RobotCounts = CrossDeviceSensors.Current.Pedometer.LastReading;
                };
                
                if (!CrossDeviceSensors.Current.Pedometer.IsActive)
                {
                    CrossDeviceSensors.Current.Pedometer.StartReading();
                }
                
                Console.WriteLine("Latest steps " + CrossDeviceSensors.Current.Pedometer.LastReading);

            }

        }


    }
}
