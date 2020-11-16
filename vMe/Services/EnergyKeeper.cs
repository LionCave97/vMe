using System;
using Xamarin.Forms;
using vMe.Views;
using vMe.Services;
using System.Timers;
using Xamarin.Essentials;
//Energy storage
namespace vMe.Services
{
    public partial class EnergyKeeper
    {
        const string energyKey = "robotEnergy";
        const string OldenergyKey = "oldrobotEnergy";
        private TimeKeeper timeKeeper = new TimeKeeper();
        private static Timer timer;

        public EnergyKeeper()
        {
            //StartTime();
        }

        //RobotEnergy state get and set
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
        //Stores old energy value - used for Robot charging animation
        public int OldRobotEnergy
        {
            get
            {

                if (Application.Current.Properties.ContainsKey(OldenergyKey))
                {
                    Console.WriteLine("Get Old Battery" + (int)Application.Current.Properties[OldenergyKey]);
                    return (int)Application.Current.Properties[OldenergyKey];                    

                }
                else
                {

                    return 0;
                }
            }

            set
            {
                Console.WriteLine("Set Old Battery" + value);
                Application.Current.Properties[OldenergyKey] = value;
                
            }
        }


        //Decrease Energy state
        public void decreaseEnergy()
        {
            RobotEnergy -= 5;
            OldRobotEnergy = RobotEnergy;
        }

        //Increase Energy state
        public void increaseEnergy()
        {
            ResetTimer();
            RobotEnergy += 5;
            var Activity = new ActivityDock();
            Activity.UiUpdate();
            var duration = TimeSpan.FromSeconds(1);
            Vibration.Vibrate(duration);
        }

        //Get initial energy and activates the timer
        public void getEnergy()
        {
            ResetTimer();
            Console.WriteLine("getEnergy");
        }

        //Timers used to decrease Energy states
        private void StartTime()
        {
            timer = new Timer();

            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Elapsed += updateTimedData;
            timer.Start();
            Console.WriteLine("Energy Timer started");

        }

        private void ResetTimer()
        {
            Console.WriteLine("Reset Timer");
            timeKeeper.StartTime = DateTime.Now;
            //timer.Stop();
            //timer.Dispose();
            //timer = null;

            //StartTime();
        }

        private void updateTimedData(object sender, ElapsedEventArgs e)
        {
            TimeSpan timeElapsed = e.SignalTime - timeKeeper.StartTime;
            double sec = timeElapsed.TotalSeconds;
            Console.WriteLine(sec + " Time");

            if (sec > 60)
            {
                
                Console.WriteLine("Energy Time");
                decreaseEnergy();
                ResetTimer();

            }
            
        }
    }
}

