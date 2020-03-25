using System;
using vMe.Services;
using CoreMotion;
using Foundation;
using vMe.iOS.Implementations;
using vMe.Views;

[assembly: Xamarin.Forms.Dependency(typeof(PedometerSensorImplementations))]
namespace vMe.iOS.Implementations
{
    public class PedometerSensorImplementations : PedometerSensor
    {

        private StepKeeper steps = new StepKeeper();

        private NSOperationQueue _queue;
        private DateTime _resetTime;

        private CMStepCounter stepCounter;

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

        public void Getsteps()
        {
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

        private void DailyStepQueryHandler(nint stepCount, NSError error)
        {            
            
            

            if (steps.RobotCounts != (Int32)stepCount)
            {
                steps.RobotCounts = (Int32)stepCount;
                Console.WriteLine("Step count " + (Int32)stepCount);

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
