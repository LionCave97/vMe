using System;
using Android.Hardware;
using vMe.Droid.Implementations;
using vMe.Services;

[assembly: Xamarin.Forms.Dependency(typeof(PedometerSensorImplementations))]
namespace vMe.Droid.Implementations
{
    public class PedometerSensorImplementations : PedometerSensor
    {
        private StepKeeper steps = new StepKeeper();


        public DeviceSteps GetPedometer()
        {
            Getsteps();
            return DeviceSteps.Still;
        }

        public void Getsteps()
        {
            Console.WriteLine("Can Count steps Android " + "False");
            Console.WriteLine(SensorType.StepCounter);
        }
    }
}
