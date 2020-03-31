using System;

using Xamarin.Forms;

namespace vMe.Services
{
    public class RobotState : ContentView
    {
        public RobotState()
        {

        }

        public string RobotSprite(bool lowPower, bool lowFluid, bool lowSteps)
        {

            Console.WriteLine("Robot State Power:" + lowPower);
            Console.WriteLine("Robot State Fluid: " + lowFluid );
            Console.WriteLine("Robot State Steps: " + lowSteps);
            string state = "idle_robot";

            if (lowPower)
            {
                state = "lowPower_robot";

            }

            if (lowFluid)
            {
                state = "lowWater_robot";
            }

            if (lowSteps)
            {
                state = "steps_robot";
            }

            if (lowFluid && lowPower && lowSteps)
            {
                state = "sad_robot";
            }
            else if (!lowFluid && !lowPower && !lowSteps)
            {
                state = "happy_robot";
            }
            return state;
        }

        public bool ActivityState(int val, string str)
        {
            bool low = false;

            if (str == "step")
            {
                if (val <= 1000)
                {
                    low = true;
                }
            }
            else
            {
                if (val <= 50)
                {
                    low = true;
                }
            }
            
            return low;
        }

        public string IconState(int val, string str)
        {
            string pic = "";
            if (str == "step")
            {
                if (val >= 500)
                {
                    pic = "";
                }
                if (val >= 1000)
                {
                    pic = "10";
                }
                if (val >= 5000)
                {
                    pic = "50";
                }
                if (val >= 8000)
                {
                    pic = "80";
                }
                if (val >= 10000)
                {
                    pic = "100";
                }
            }
            else
            {
                if (val >= 5)
                {
                    pic = "";
                }
                if (val >= 10)
                {
                    pic = "10";
                }
                if (val >= 50)
                {
                    pic = "50";
                }
                if (val >= 80)
                {
                    pic = "80";
                }
                if (val >= 100)
                {
                    pic = "100";
                }
            }

            return pic;
            
        }


    }
}

