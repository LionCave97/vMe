using Microsoft.VisualStudio.TestTools.UnitTesting;
using vMe.Services;
using Xamarin.Forms;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace vMeTest
{
    [TestClass]
    public class VMeTests
    {
        //Check Accelermeter
        [TestMethod]
        public void AcelerometerTest()
        {
            //Arrange
            var Accelerometer = new vMe.Services.AccelerometerSensor.AccelerometerTest();

            //Act
            bool result = Accelerometer.CheckAccelerometer();

            //Assert
            Assert.IsTrue(!result);
        }

        //Check RobotState for its sprite
        [TestMethod]
        public void RobotStateCheck()
        {
            RobotState state = new RobotState();
            //Arrange            
            string expected = "happy_robot";            


            //Act
            
            string result = state.RobotSprite(false, false, false);

            //Assert
            Assert.AreEqual(expected, result);
        }

        //Check max Energy allowed
        [TestMethod]
        public void ActivityStateCheck()
        {
            //Arrange
            RobotState state = new RobotState();

            //Act
            bool result = state.ActivityState(500, "step");

            //Assert
            Assert.IsTrue(result);
        }

        //Check max Fluid allowed
        [TestMethod]
        public void IconStateCheck()
        {
            //Arrange
            RobotState state = new RobotState();
            string expected = "80";

            //Act
            
            string result = state.IconState(95, "null");

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
