using System;
using Android.App;
using Android.Hardware;
using Android.Content;
using vMe.Droid.Implementations;
using vMe.Services;


[assembly: Xamarin.Forms.Dependency(typeof(PedometerSensorImplementations))]
namespace vMe.Droid.Implementations
{
    public class PedometerSensorImplementations : PedometerSensor
    {
        private StepKeeper steps = new StepKeeper();
        private StepService stepGet = new StepService();

        public DeviceSteps GetPedometer()
        {

            Startup();
            Getsteps();
            return DeviceSteps.Still;

            
        }

        private void Startup()
        {
            var stepServiceIntent = new Intent("using Android.Content");
            
            Console.WriteLine("It has booted");
        }

        public void Getsteps()
        {
            Console.WriteLine("Can Count steps Android " + "False");
            Console.WriteLine(Convert.ToInt32(stepGet.StepsToday));
            
            
        }



    }
}
