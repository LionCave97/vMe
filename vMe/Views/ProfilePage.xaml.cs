using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace vMe.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            var Accelerometer = new ProfilePage.AccelerometerTest();
            Accelerometer.ToggleAccelerometer();
            SensorSpeed speed = SensorSpeed.UI;
            InitializeComponent();
        }

        public class AccelerometerTest
        {
            // Set speed delay for monitoring changes.
            SensorSpeed speed = SensorSpeed.UI;

            public AccelerometerTest()
            {
                Console.WriteLine("Test Accelerometer");
                // Register for reading changes, be sure to unsubscribe when finished
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            }

            void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
            {
                var data = e.Reading;
                Console.WriteLine("Reading Data!");
                Console.WriteLine($"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");
                // Process Acceleration X, Y, and Z
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
