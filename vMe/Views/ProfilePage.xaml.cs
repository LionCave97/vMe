﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;
using vMe.Services;

namespace vMe.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            var Accelerometer = new Services.AccelerometerSensor.AccelerometerTest();
            Accelerometer.ToggleAccelerometer();
            SensorSpeed speed = SensorSpeed.UI;
            InitializeComponent();
        }      

        

    }
}
