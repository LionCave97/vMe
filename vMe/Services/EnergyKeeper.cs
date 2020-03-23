using System;
using Xamarin.Forms;
using vMe.Views;
using vMe.Services;
using System.Timers;
using Xamarin.Essentials;

namespace vMe.Services
{
    class EnergyKeeper
    {
        const string energyKey = "robotEnergy";
        private TimeKeeper timeKeeper = new TimeKeeper();
        private static Timer timer;

        public EnergyKeeper()
        {
            StartTime();
        }
        
        public int RobotEnergy 
        {
            get
            {
                    
                if (Application.Current.Properties.ContainsKey(energyKey))
                {
                    
                    return (int)Application.Current.Properties[energyKey];

                    
                }
                else
                {
                    
                    return 10;
                }
            }

            set
            {
                    
                if (value > 100)
                {
                    Application.Current.Properties[energyKey] = 100;
                }
                else if (value < 0)
                {
                    Application.Current.Properties[energyKey] = 0;
                }
                else
                {
                    Application.Current.Properties[energyKey] = value;
                }
                                  

                var Activity = new ActivityDock();
                Activity.UiUpdate();
            }
        }

        public void decreaseEnergy()
        {
            RobotEnergy -= 5;
        }

        public void increaseEnergy()
        {
            ResetTimer();
            RobotEnergy += 5;
            var duration = TimeSpan.FromSeconds(1);
            var Activity = new ActivityDock();
            Activity.UiUpdate();
            //Vibration.Vibrate(duration);
        }

        public void getEnergy()
        {
            ResetTimer();
            Console.WriteLine("getEnergy");
        }

        private void StartTime()
        {
            timer = new Timer();

            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Elapsed += updateTimedData;
            timer.Start();
            Console.WriteLine("Timer started");

        }

        private void ResetTimer()
        {
            timeKeeper.StartTime = DateTime.Now;

            StartTime();
        }

        private void updateTimedData(object sender, ElapsedEventArgs e)
        {
            TimeSpan timeElapsed = e.SignalTime - timeKeeper.StartTime;
            double sec = timeElapsed.TotalSeconds;

            if (sec > 60)
            {
                Console.WriteLine("Fired");
                decreaseEnergy();
                ResetTimer();
            }
            
        }
    }
}

