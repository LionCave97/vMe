using System;
using vMe.Services;
using CoreMotion;
using Foundation;
using vMe.iOS.Implementations;

[assembly: Xamarin.Forms.Dependency(typeof(PedometerSensorImplementations))]
namespace vMe.iOS.Implementations
{
    public class PedometerSensorImplementations : PedometerSensor
    {
        private StepKeeper steps = new StepKeeper();

        private CMStepCounter stepCounter;
        

        public DeviceSteps GetPedometer()
        {
            Getsteps();
            return DeviceSteps.Still;
        }

        public void Getsteps()
        {

            Console.WriteLine("Can Count steps " + CMStepCounter.IsStepCountingAvailable);
        }
        
    }
}
