using System;
using CoreMotion;
using Xamarin.Forms;
using vMe.Views;
using vMe.Services;

namespace vMe.Services
{
    public class StepManager : ContentPage
    {
    private StepKeeper steps = new StepKeeper();

        public StepManager()
        {
        Console.WriteLine("Test");
        steps.Steps = 10;

        }

        public void StartUp()
        {

        }
    }
}

