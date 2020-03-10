using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vMeTest
{
    [TestClass]
    public class VMeTests
    {
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
    }
}
