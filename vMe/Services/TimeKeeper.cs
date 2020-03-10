using System;
using System.Collections.Generic;
using System.Text;

namespace vMe.Views
{
    public class TimeKeeper
    {

        const string startTimeKey = "startTime";
        const string storedTimeKey = "storedTime";

        public DateTime StartTime
        {
            get
            {
                if (Application.Current.Properties.ContainsKey(startTimeKey))
                {
                    return new DateTime((long)Application.Current.Properties[startTimeKey]);
                }
                else
                {
                    return DateTime.Now;
                }
            }

            set
            {
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

    }
}
