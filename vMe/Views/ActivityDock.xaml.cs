using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using vMe.Views;
using vMe.Services;
using System.Timers;

namespace vMe.Views
{
    [DesignTimeVisible(false)]
    public partial class ActivityDock : ContentView
    {
        private TimeKeeper timeKeeper = new TimeKeeper();

        private static Timer timer;

        bool waterTappedState = false;
        bool runningTappedState = false;
        bool batteryTappedState = false;
        public ActivityDock()
        {
            InitializeComponent();

            StartTime();
        }

        private void StartTime()
        {
            timer = new Timer();

            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Elapsed += updateTimedData;
            timer.Start();

        }

        private void ResetTimer()
        {
            timeKeeper.StartTime = DateTime.Now;

            StartTime();
        }

        private void updateTimedData(object sender, ElapsedEventArgs e)
        {
            TimeSpan timeElapsed = e.SignalTime - timeKeeper.StartTime;
                        
        }

        void waterTapped(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine(timeKeeper.GetTimeElapsed());
            Console.WriteLine("Tapped");
            if (waterTappedState)
            {
                Console.WriteLine("true");
                waterLevel.WidthRequest = -1;
                waterTappedState = false;

                waterLevel.IsVisible = true;
                runningLevel.IsVisible = true;
                batteryLevel.IsVisible = true;

                waterLevel.LowerChild(waterDropPic);
                waterLabel.IsVisible = false;
                waterButton.IsVisible = false;
            }
            else if (!waterTappedState)
            {
                Console.WriteLine("false");
                waterTappedState = true;

                waterLevel.IsVisible = true;
                runningLevel.IsVisible = false;
                batteryLevel.IsVisible = false;
                waterLevel.WidthRequest = 394;

                waterLevel.RaiseChild(waterDropPic);
                waterLabel.IsVisible = true;
                waterButton.IsVisible = true;



            }
        }

        void runningTapped(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Tapped");
            if (runningTappedState)
            {
                Console.WriteLine("true");
                runningLevel.WidthRequest = -1;
                runningTappedState = false;

                waterLevel.IsVisible = true;
                runningLevel.IsVisible = true;
                batteryLevel.IsVisible = true;

                runningLevel.LowerChild(waterDropPic);
                runningLabel.IsVisible = false;
            }
            else if (!runningTappedState)
            {
                Console.WriteLine("false");
                runningTappedState = true;

                waterLevel.IsVisible = false;
                runningLevel.IsVisible = true;
                batteryLevel.IsVisible = false;
                runningLevel.WidthRequest = 394;

                runningLevel.RaiseChild(waterDropPic);
                runningLabel.IsVisible = true;

            }
        }

        void batteryTapped(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Tapped");
            if (batteryTappedState)
            {
                Console.WriteLine("true");
                batteryLevel.WidthRequest = -1;
                batteryTappedState = false;

                waterLevel.IsVisible = true;
                runningLevel.IsVisible = true;

                batteryLevel.LowerChild(waterDropPic);
                batteryLabel.IsVisible = false;

            }
            else if (!batteryTappedState)
            {
                Console.WriteLine("false");
                batteryTappedState = true;

                waterLevel.IsVisible = false;
                runningLevel.IsVisible = false;
                batteryLevel.IsVisible = true;
                batteryLevel.WidthRequest = 394;

                batteryLevel.RaiseChild(waterDropPic);
                batteryLabel.IsVisible = true;
            }
        }

        void drinkWaterTapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}
