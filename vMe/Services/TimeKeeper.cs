using System;
using System.Collections.Generic;
using System.Text;

namespace vMe.Services
{
    public class TimeKeeper
    {

        const string startTimeKey = "startTime";
        const string storedTimeKey = "storedTime";
        const string FluidDateKey = "fluiddate";

        public DateTime StartTime
        {         
            get
            {
                if (Application.Current.Properties.ContainsKey(startTimeKey))
                {
                    Console.WriteLine("TimeKeeper get");
                    return new DateTime((long)Application.Current.Properties[startTimeKey]);
                }
                else
                {
                    Console.WriteLine("TimeKeeper get now");
                    return DateTime.Now;
                }
            }

            set
            {
                Console.WriteLine("TimeKeeper set");
                Application.Current.Properties[startTimeKey] = value.Ticks;
            }
        }

        public DateTime StoredTime
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(storedTimeKey))
                {
                    return new DateTime((long)Application.Current.Properties[storedTimeKey]);
                }
                else
                {
                    return DateTime.Now;
                }
            }

            set
            {
                Application.Current.Properties[storedTimeKey] = value.Ticks;
            }
        }

        public double GetTimeElapsed()
        {
            return (StoredTime - StartTime).TotalSeconds;
        }

        public String FluidDate
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(FluidDateKey))
                {                    
                    return (string)Application.Current.Properties[FluidDateKey];
                }
                else
                {
                    var date = DateTime.Today.Date;
                    return date.ToString("dd-MM-yyyy");
                }
            }

            set
            {
                Application.Current.Properties[FluidDateKey] = value;
            }
        }

        public String getToday()
        {
            var date = DateTime.Today.Date;
            return date.ToString("dd-MM-yyyy");
        }

        public String getOldDateFluid()
        {
            return FluidDate;
        }

    }
}
