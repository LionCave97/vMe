using System;
namespace vMe.Services
{
    public partial interface PedometerSensor
    {
       DeviceSteps GetPedometer();
    }
}
