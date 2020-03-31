using System;
namespace vMe.Services
{
    public partial interface PedometerSensor
    {
        //Runs the GetPedometer Xamarin.iOS or Xamarin.Android
        //This executes the native code
       DeviceSteps GetPedometer();
    }
}
