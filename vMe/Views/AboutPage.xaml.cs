using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using vMe.Services;
using vMe.Views;

namespace vMe.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private FluidKeeper fluid = new FluidKeeper();
        private EnergyKeeper energy = new EnergyKeeper();

        //Developer button just to reset the app for testing
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Reset Settings");

            fluid.FluidCount = 0;
            energy.RobotEnergy = 0;
        }
    }
}