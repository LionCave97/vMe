using System;
using CoreMotion;
using Xamarin.Forms;
using vMe.Views;
using vMe.Services;

[assembly: ExportRenderer(typeof(iOSStep), typeof(StepManageriOS))]
namespace vMe.Services
{
    public class StepManageriOS : ContentPage
    {
    private StepKeeper steps = new StepKeeper();

        public StepManageriOS()
        {
        Console.WriteLine("Test");
        steps.Steps = 10;

        }

        public void StartUp()
        {

        }
    }
}

