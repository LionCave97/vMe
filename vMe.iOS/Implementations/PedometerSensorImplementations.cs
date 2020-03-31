using System;
using vMe.Services;
using CoreMotion;
using Foundation;
using vMe.iOS.Implementations;
using vMe.Views;

//iOS Pedometer
[assembly: Xamarin.Forms.Dependency(typeof(PedometerSensorImplementations))]
namespace vMe.iOS.Implementations
{
    public class PedometerSensorImplementations : PedometerSensor
    {

        private StepKeeper steps = new StepKeeper();

        private NSOperationQueue _queue;
        private DateTime _resetTime;

        private CMStepCounter stepCounter;

        //Checks if Pedometer is availible
        public DeviceSteps GetPedometer()
        {

            Getsteps();
            stepCounter.StartStepCountingUpdates(_queue, 1, Updater);
            return DeviceSteps.Still;
        }

        public static NSDate DateTimeToNSDate(DateTime date)
        {
            return NSDate.FromTimeIntervalSinceReferenceDate((date - (new DateTime(2001, 1, 1, 0, 0, 0))).TotalSeconds);
        }

        //Get steps of today
        public void Getsteps()
        {
            Console.WriteLine("Get todays steps");
            if (_resetTime.Date.Day != DateTime.Now.Date.Day)
            {
                _resetTime = DateTime.Today; //Forces update as the day may have changed.
            }
            NSDate sMidnight = DateTimeToNSDate(_resetTime);
            _queue = _queue ?? NSOperationQueue.CurrentQueue;

            if (stepCounter == null)
                stepCounter = new CMStepCounter();

            //Console.WriteLine("Can Count steps " + CMStepCounter.IsStepCountingAvailable);

            stepCounter.QueryStepCount(sMidnight, NSDate.Now, _queue, DailyStepQueryHandler);
        }

        //Updates StepKeeper state
        private void DailyStepQueryHandler(nint stepCount, NSError error)
        {   
            if (steps.RobotCounts != (Int32)stepCount)
            {
                steps.RobotCounts = (Int32)stepCount;
                Console.WriteLine("iOS Steps "+(Int32)stepCount);

                var Activity = new ActivityDock();
                Activity.UiUpdate();
            }

        }

        private void Updater(nint stepCount, NSDate date, NSError error)
        {
            NSDate sMidnight = DateTimeToNSDate(_resetTime);
            stepCounter.QueryStepCount(sMidnight, NSDate.Now, _queue, DailyStepQueryHandler);
        }



    }
}
