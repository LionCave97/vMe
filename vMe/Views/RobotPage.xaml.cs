using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using vMe.Models;
using vMe.Services;
using System.Timers;

using Xamarin.Forms;

namespace vMe.Views
{
    public partial class RobotPage : ContentPage
    {
        private static Timer timer;
        private FluidKeeper fluid = new FluidKeeper();
        private EnergyKeeper energy = new EnergyKeeper();


        public RobotPage()
        {
            InitializeComponent();
            Startup();
            Console.WriteLine("RobotState Start");
        }

        public void Update()
        {
            
        }

        public void Startup()
        {
            HoverRobot();
            StartTime();
        }        

        public async void HoverRobot()
        {
            await robotSprite.TranslateTo(0, 10, 500);
            await robotSprite.TranslateTo(0, 0, 1000);
            await robotSprite.TranslateTo(0, -10, 500);
            await robotSprite.TranslateTo(0, 0, 1000);
        }


        private void StartTime()
        {
            timer = new Timer();

            timer.Interval = 20000;
            timer.Enabled = true;
            timer.Elapsed += updateTimedData;
            timer.Start();

        }
        private void updateTimedData(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("RobotAnimate");

            HoverRobot();
        }

    }
}
