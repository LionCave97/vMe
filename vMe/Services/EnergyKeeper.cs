using System;
using System.Collections.Generic;
using System.Text;
using vMe.Views;
using Xamarin.Essentials;

using Xamarin.Forms;

namespace vMe.Services
{
    class EnergyKeeper
    {
        const string energyKey = "robotEnergy";

        public EnergyKeeper()
        {
            
        }
        
        public int RobotEnergy 
        {
            get
            {
                    
                if (Application.Current.Properties.ContainsKey(energyKey))
                {
                    Console.WriteLine("Reading Energy " + (int)Application.Current.Properties[energyKey]);
                    return (int)Application.Current.Properties[energyKey];

                    
                }
                else
                {
                    
                    return 10;
                }
            }

            set
            {
                    Console.WriteLine("Writing Energy!!" + value);
                   Application.Current.Properties[energyKey] = value;

                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);

                var Activity = new ActivityDock();
                Activity.UiUpdate();

            }
        }

    }
}

