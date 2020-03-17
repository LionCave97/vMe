using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using vMe.Views;
using vMe.Services;
using System.Timers;

namespace vMe.Views
{
    [DesignTimeVisible(false)]
    public partial class ActivityDock : ContentView
    {
        private FluidKeeper fluid = new FluidKeeper();
        private EnergyKeeper energy = new EnergyKeeper();
        private StepKeeper steps = new StepKeeper();

        //Menu config
        bool waterTappedState = false;
        bool runningTappedState = false;
        bool batteryTappedState = false;

        public ActivityDock()
        {            
            InitializeComponent();
            fluid.getFluid();
            energy.getEnergy();
            UiUpdate();
        }

        public void UiUpdate()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string sBatteryCount = energy.RobotEnergy.ToString();
                batteryLabel.Text = sBatteryCount;

                string sFluidCount = fluid.FluidCount.ToString();
                waterLabel.Text = "You have drank "+sFluidCount+"% of your daily intake";
            });
            
            
            
        }       

               
        void waterTapped(System.Object sender, System.EventArgs e)
        {
            UiUpdate();
            Console.WriteLine("Tapped");
            if (waterTappedState)
            {
                Console.WriteLine("true");
                waterLevel.WidthRequest = -1;
                waterTappedState = false;

                waterLevel.IsVisible = true;
                runningLevel.IsVisible = true;
                batteryLevel.IsVisible = true;

                waterLevel.LowerChild(waterDropPic);
                waterLabel.IsVisible = false;
                waterButton.IsVisible = false;
            }
            else if (!waterTappedState)
            {
                Console.WriteLine("false");
                waterTappedState = true;

                waterLevel.IsVisible = true;
                runningLevel.IsVisible = false;
                batteryLevel.IsVisible = false;
                waterLevel.WidthRequest = 394;

                waterLevel.RaiseChild(waterDropPic);
                waterLabel.IsVisible = true;
                waterButton.IsVisible = true;



            }
        }

        void runningTapped(System.Object sender, System.EventArgs e)
        {
            UiUpdate();
            runningLabel.Text = steps.Steps.ToString();
            Console.WriteLine("Tapped");
            if (runningTappedState)
            {
                Console.WriteLine("true");
                runningLevel.WidthRequest = -1;
                runningTappedState = false;

                waterLevel.IsVisible = true;
                runningLevel.IsVisible = true;
                batteryLevel.IsVisible = true;

                runningLevel.LowerChild(waterDropPic);
                runningLabel.IsVisible = false;
            }
            else if (!runningTappedState)
            {
                Console.WriteLine("false");
                runningTappedState = true;

                waterLevel.IsVisible = false;
                runningLevel.IsVisible = true;
                batteryLevel.IsVisible = false;
                runningLevel.WidthRequest = 394;

                runningLevel.RaiseChild(waterDropPic);
                runningLabel.IsVisible = true;

            }
        }

        void batteryTapped(System.Object sender, System.EventArgs e)
        {
            UiUpdate();
            Console.WriteLine("Tapped");
            if (batteryTappedState)
            {
                Console.WriteLine("true");
                batteryLevel.WidthRequest = -1;
                batteryTappedState = false;

                waterLevel.IsVisible = true;
                runningLevel.IsVisible = true;

                batteryLevel.LowerChild(waterDropPic);
                batteryLabel.IsVisible = false;

            }
            else if (!batteryTappedState)
            {
                Console.WriteLine("false");
                batteryTappedState = true;

                waterLevel.IsVisible = false;
                runningLevel.IsVisible = false;
                batteryLevel.IsVisible = true;
                batteryLevel.WidthRequest = 394;

                batteryLevel.RaiseChild(waterDropPic);
                batteryLabel.IsVisible = true;
            }
        }

        //Action Buttons

        void drinkWaterTapped(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine(fluid.FluidCount);
            fluid.FluidCount += 10;
            Console.WriteLine(fluid.FluidCount);
            UiUpdate();
            
        }
    }
}
