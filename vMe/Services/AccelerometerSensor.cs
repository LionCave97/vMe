using System;
using Xamarin.Essentials;
using vMe.Views;

//Manages the Accelerometer and will increase the energy of the robot.

namespace vMe.Services
{

    public partial class AccelerometerSensor
    {
        public AccelerometerSensor()
        {
            
        }
        

        public class AccelerometerTest
        {
            //
            private EnergyKeeper energyLevel = new EnergyKeeper();

            // Set speed delay for monitoring changes.
            SensorSpeed speed = SensorSpeed.UI;

            public AccelerometerTest()
            {
                // Register for reading changes, be sure to unsubscribe when finished
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            }

            //This will check the Y Axis and call the increaseEnergy Funcion if needed
            void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
            {
                 
                float yFloat = 0;
                var data = e.Reading;
                //Console.WriteLine("Reading Data!");
                //Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
                yFloat = data.Acceleration.Y;
                int y = (int)yFloat;
                // Process Acceleration X, Y, and Z

                if (y == 2)
                {
                    energyLevel.increaseEnergy();
                    var Activity = new ActivityDock();
                    Activity.UiUpdate();
                }
            }

            //Checks Accelerometer state used in the App.xaml.cs to stop and start the sensor according to OnStart/OnSleep/OnResume
            public bool CheckAccelerometer()
            {
                bool running = false;

                if (Accelerometer.IsMonitoring)
                {
                    running = true;
                }

                return running;
            }
            //This will Toggle the Accelerometer On or Off
            public void ToggleAccelerometer()
            {
                Console.WriteLine("Toggle Accelerometer");

                try
                {
                    if (Accelerometer.IsMonitoring)
                    {
                        Console.WriteLine("AccelerometerInActive");
                        Accelerometer.Stop();

                    }
                    else
                    {
                        Console.WriteLine("AccelerometerActive");
                        Accelerometer.Start(speed);

                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Feature not supported on device
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                }
            }
        }
    }
}

