using System;
using vMe.Views;
using Xamarin.Forms;

namespace vMe.Services
{
    public partial class StepKeeper 
    {
        const string stepKey = "robotSteps";

        public StepKeeper()
        {
            
        }

        public int RobotCounts
        {
            
            get
            {

                if (Application.Current.Properties.ContainsKey(stepKey))
                {
                    Console.WriteLine("Get steps " + (int)Application.Current.Properties[stepKey]);
                    return (int)Application.Current.Properties[stepKey];


                }
                else
                {
                    Console.WriteLine("Get 0 steps ");
                    return 0;
                }
            }

            set
            {
                Console.WriteLine("Add steps " + value);
                Application.Current.Properties[stepKey] = value;         


                
            }
        }

        public void GetSteps()
        {
        }

    }
}

