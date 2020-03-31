using System;
using Xamarin.Forms;
using vMe.Views;
using vMe.Services;
using System.Timers;
//Fluid Storage
namespace vMe.Services
{
    public class FluidKeeper
    {
        const string fluidKey = "robotFluid";
        private TimeKeeper timeKeeper = new TimeKeeper();
        private static Timer timer;


        public FluidKeeper()
        {

        }            
        //Fluid state
        public int FluidCount
        {
            get
            {
                
                if (Application.Current.Properties.ContainsKey(fluidKey))
                {
                    return (int)Application.Current.Properties[fluidKey];


                }
                else
                {
                    
                    
                    return 0;
                }
            }
            set
            {

                if (value > 120)
                {
                    Application.Current.Properties[fluidKey] = 120;
                }
                else
                {
                    Application.Current.Properties[fluidKey] = value;
                }
            }
        }

        //Fluid reset on new day
        public void getFluid()
        {
            int fluid = FluidCount;

            var today = timeKeeper.getToday();
            var oldDate = timeKeeper.getOldDateFluid();

            if (today == oldDate)
            {
                timeKeeper.FluidDate = today;
            }
            else
            {
                timeKeeper.FluidDate = today;
                FluidCount = 0;
            }
            StartTime();
        }
        
        private void StartTime()
        {
            timer = new Timer();

            timer.Interval = 60000;
            timer.Enabled = true;
            timer.Elapsed += updateTimedData;
            timer.Start();

        }
        private void updateTimedData(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("TimeLoop");
            getFluid();

        }
    }
}

