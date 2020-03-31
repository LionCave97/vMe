using System;
using vMe.Views;
using Xamarin.Forms;

//Step Storage
namespace vMe.Services
{
    public partial class StepKeeper 
    {
        //Key for storage
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
                    return (int)Application.Current.Properties[stepKey];
                }
                else
                {                    
                    return 0;
                }
            }

            set
            {              
                Application.Current.Properties[stepKey] = value;
            }
        }

        

    }
}

