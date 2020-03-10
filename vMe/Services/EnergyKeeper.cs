using System;
using System.Collections.Generic;
using System.Text;
using vMe.Views;

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
                    Console.WriteLine("Reading Energy!!");
                if (Application.Current.Properties.ContainsKey(energyKey))
                {
                    Console.WriteLine("Reading again!! " + (int)Application.Current.Properties[energyKey]);
                    return (int)Application.Current.Properties[energyKey];

                    
                }
                else
                {
                    Console.WriteLine("Reading first read!!");
                    return 10;
                }
            }

            set
            {
                    Console.WriteLine("Writing Energy!!" + value);
                   Application.Current.Properties[energyKey] = value;
                
            }
        }

    }
}

