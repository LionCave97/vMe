using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;
using vMe.Services;
using System.Timers;

namespace vMe.Views
{
    public partial class ProfilePage : ContentPage
    {
        private FluidKeeper fluidK = new FluidKeeper();
        private StepKeeper stepK = new StepKeeper();
        private EnergyKeeper energyK = new EnergyKeeper();

        private static Timer timer;

        public ProfilePage()
        {
            InitializeComponent();
            Setup();
            FastStartTime();
            Update();
        }

        public void Update()
        {
            int battery = energyK.RobotEnergy;
            int fluidCount = fluidK.FluidCount;
            int stepCount = stepK.RobotCounts;
            bool lowPower = false;
            bool lowFluid = false;
            bool lowSteps = false;

            var state = "happy";

            if (battery <= 50)
            {
                lowPower = true;

            }


            if (fluidCount <= 50)
            {
                lowFluid = true;
            }

            if (stepCount <= 1000)
            {
                lowSteps = true;
            }

            


            MainThread.BeginInvokeOnMainThread(() =>
            {
            fluid.Text = "Fluid: " + fluidK.FluidCount.ToString() + "%";
            step.Text = "Steps: " + stepK.RobotCounts.ToString() + " out of 10000";
            energy.Text = "Energy: " + energyK.RobotEnergy.ToString() + "%" ;

                if (lowFluid || lowPower || lowSteps)
                {
                    robotSprite.Source = "sad_robot";
                }
                else
                {
                    robotSprite.Source = "heppy_robot";
                }
            });


        }

        private void FastStartTime()
        {
            timer = new Timer();

            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Elapsed += updateFast;
            timer.Start();

        }
        private void updateFast(object sender, ElapsedEventArgs e)
        {
            Update();

        }

        private void Setup()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width / mainDisplayInfo.Density;
            var height = mainDisplayInfo.Height / mainDisplayInfo.Density;

            var heightActive = (int)Math.Round(height);
            double heightSet1 = height * 0.55;
            double heightSet2 = height * 0.25;

            Console.WriteLine(heightSet1);

            robotSprite.HeightRequest = heightSet1;
            details.HeightRequest = heightSet2;
        }


    }
}
