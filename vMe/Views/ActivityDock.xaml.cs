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
using Xamarin.Essentials;

namespace vMe.Views
{
    [DesignTimeVisible(false)]
    public partial class ActivityDock : ContentView
    {
        //Update
        private FluidKeeper fluid = new FluidKeeper();
        private StepKeeper step = new StepKeeper();
        private EnergyKeeper energy = new EnergyKeeper();        


        //Setup variable
        double widthActive = 0;
        double heightActive = 0;


        //Menu config
        bool waterTappedState = false;
        bool runningTappedState = false;
        bool batteryTappedState = false;

        public ActivityDock()
        {            
            InitializeComponent();

            Setup();
            fluid.getFluid();
            energy.getEnergy();
            UiUpdate();
        }

        public void Setup()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width / mainDisplayInfo.Density;
            var height = mainDisplayInfo.Height / mainDisplayInfo.Density;

            dockContainer.WidthRequest = width;
            widthActive = (int)Math.Round(width);
            var v = width / 3;
            v -= 15;
            widthActive = v;
            waterLevel.WidthRequest = widthActive;
            runningLevel.WidthRequest = widthActive;
            batteryLevel.WidthRequest = widthActive;

            heightActive = (int)Math.Round(height);
            var h = height / 5;
            double height30 = h;
            Console.WriteLine(heightActive);
            Console.WriteLine(height30);

            dockContainer.HeightRequest = height30;
        }

        public void UiUpdate()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Console.WriteLine(DependencyService.Get<PedometerSensor>().GetPedometer().ToString());
            Console.WriteLine("UpdateUi");

                //Battery/Energy Ui Update
                int battery = energy.RobotEnergy;
                string sBatteryCount = battery.ToString();
                
                if (battery >= 5)
                {
                    batteryPic.Source = "battery";
                    batteryLabel.Text = "Battery is at "+sBatteryCount+"% you need to recharge now!";
                }
                if (battery >= 10)
                {
                    batteryPic.Source = "battery10";
                    batteryLabel.Text = "Battery is at " + sBatteryCount + "%";
                }
                if (battery >= 50)
                {
                    batteryPic.Source = "battery50";
                    batteryLabel.Text = "Battery is at " + sBatteryCount + "%";
                }
                if (battery >= 80)
                {
                    batteryPic.Source = "battery80";
                    batteryLabel.Text = "Battery is at " + sBatteryCount + "%";
                }
                if (battery >= 100)
                {
                    batteryPic.Source = "battery100";
                    batteryLabel.Text = "Battery is at " + sBatteryCount + "%";
                }
                batteryPic.Margin = new Thickness(12, 40, 0, 20);
                batteryPic.WidthRequest = -1;

                //Steps Ui Update
                int stepCount = step.RobotCounts;
                string stepCounts = stepCount.ToString();
                Console.WriteLine("Steps taken " + stepCounts);
                runningLabel.Text = "You have taken " + stepCounts + " steps today";

                //Fluid Ui Update
                int fluidCount = fluid.FluidCount;
                string sFluidCount = fluidCount.ToString();
                waterLabel.Text = "You have drink " + sFluidCount + "% of your daily intake";
                if (fluidCount >= 5)
                {
                    waterDropPic.Source = "waterDrop";
                }
                if (fluidCount >= 10)
                {
                    waterDropPic.Source = "water10";
                    waterLabel.Text = "You have drink " + sFluidCount + "% of your daily intake. I feel a bit thirsty at this stage.";
                }
                if (fluidCount >= 50)
                {
                    waterDropPic.Source = "water50";
                    waterLabel.Text = "You have drink " + sFluidCount + "% of your daily intake";
                }
                if (fluidCount >= 80)
                {
                    waterDropPic.Source = "water80";
                    waterLabel.Text = "You have drink " + sFluidCount + "% of your daily intake. Almost there!";
                }
                if (fluidCount >= 100)
                {
                    waterDropPic.Source = "water100";
                    waterLabel.Text = "You have drink " + sFluidCount + "% of your daily intake. You have reached your goal!";
                }
                if (fluidCount >= 110)
                {
                    waterDropPic.Source = "water100";
                    waterLabel.Text = "You have drink " + sFluidCount + "% of your daily intake. I think that may be enough for now.";
                }
                waterDropPic.Margin = new Thickness(12, 40, 0, 20);
                waterDropPic.WidthRequest = -1;



            });
        }       

               
        void waterTapped(System.Object sender, System.EventArgs e)
        {
            UiUpdate();
            Console.WriteLine("Tapped");
            if (waterTappedState)
            {
                Console.WriteLine("true");
                waterLevel.WidthRequest = widthActive;
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
            Console.WriteLine("Tapped");
            if (runningTappedState)
            {
                Console.WriteLine("true");
                runningLevel.WidthRequest = widthActive;
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
                batteryLevel.WidthRequest = widthActive;
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
