using System;

using Xamarin.Forms;

namespace vMe.Services
{
    public class StepKeeper
    {
        const string stepsKey = "steps";
        public StepKeeper()
        {

        }

        public int Steps
        {
            get
            {

                if (Application.Current.Properties.ContainsKey(stepsKey))
                {
                    Console.WriteLine("Reading Steps  " + (int)Application.Current.Properties[stepsKey]);
                    return (int)Application.Current.Properties[stepsKey];


                }
                else
                {


                    return 0;
                }
            }
            set
            {
                Console.WriteLine("Writing Steps!!" + value);
                Application.Current.Properties[stepsKey] = value;
            }
        }
    }
}

