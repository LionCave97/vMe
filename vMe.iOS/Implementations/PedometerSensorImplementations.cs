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
        public delegate void DailyStepCountChangedEventHandler(nint stepCount);

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

            Console.WriteLine("Can Count steps " + CMStepCounter.IsStepCountingAvailable);

            stepCounter.QueryStepCount(sMidnight, NSDate.Now, _queue, DailyStepQueryHandler);

            Console.WriteLine("The steps " + DailyStepCountChanged);
        }

        private void DailyStepQueryHandler(nint stepCount, NSError error)
        {
            Console.WriteLine("Step count " + stepCount);
            Console.WriteLine("Daily" + DailyStepCountChanged);

            if (DailyStepCountChanged == null)
                Console.WriteLine("Null");
                return;

            #if DEBUG
                        stepCount = 1245;

                        //stepCount = 6481;

                        //stepCount = 9328;
            #endif


            DailyStepCountChanged(stepCount);

            Console.WriteLine("Step count " + stepCount);
        }

        private void Updater(nint stepCount, NSDate date, NSError error)
        {
            NSDate sMidnight = DateTimeToNSDate(_resetTime);
            stepCounter.QueryStepCount(sMidnight, NSDate.Now, _queue, DailyStepQueryHandler);
        }


        public event DailyStepCountChangedEventHandler DailyStepCountChanged;


    }
}
