using System;
using Xamarin.Essentials;


namespace vMe.Services
{

    public partial class AccelerometerSensor
    {
        public AccelerometerSensor()
        {
            
        }
        

        public class AccelerometerTest
        {
            private EnergyKeeper energyLevel = new EnergyKeeper();

            // Set speed delay for monitoring changes.
            SensorSpeed speed = SensorSpeed.UI;

            public AccelerometerTest()
            {
                // Register for reading changes, be sure to unsubscribe when finished
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            }

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
                    
                }



            }

            public bool CheckAccelerometer()
            {
                bool running = false;

                if (Accelerometer.IsMonitoring)
                {
                    running = true;
                }

                return running;
            }

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

