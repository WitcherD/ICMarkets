using System.Threading.Tasks;
using NUnit.Framework;

namespace ICMarkets.Cameras.Tests
{
    public class CameraActivityTests : BaseCameraTests
    {
        [Test]
        public async Task BaseCamera_SwitchOnAsync_Device_Should_Be_Active()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            var camera = baseCameraMock.Object;
            await SwitchOffCameraAsync(camera);

            // Act
            await camera.SwitchOnAsync();

            // Assert
            baseCameraMock.Verify(m => m.SwitchOnAsync());
            Assert.AreEqual(true, camera.IsEnabled);
        }

        [Test]
        public async Task BaseCamera_SwitchOnAsync_Twice_No_Exceptions()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            var camera = baseCameraMock.Object;
            await SwitchOffCameraAsync(camera);

            // Act
            await camera.SwitchOnAsync();
            await camera.SwitchOnAsync();

            // Assert
            baseCameraMock.Verify(m => m.SwitchOnAsync());
            Assert.AreEqual(true, camera.IsEnabled);
        }

        [Test]
        public async Task BaseCamera_SwitchOffAsync_Device_Should_Be_Inactive()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            var camera = baseCameraMock.Object;
            await SwitchOnCameraAsync(camera);

            // Act
            await camera.SwitchOffAsync();

            // Assert
            baseCameraMock.Verify(m => m.SwitchOffAsync());
            Assert.AreEqual(false, camera.IsEnabled);
        }

        [Test]
        public async Task BaseCamera_SwitchOffAsync_Twice_Device_Should_Be_Inactive()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            var camera = baseCameraMock.Object;
            await SwitchOnCameraAsync(camera);

            // Act
            await camera.SwitchOffAsync();
            await camera.SwitchOffAsync();

            // Assert
            baseCameraMock.Verify(m => m.SwitchOffAsync());
            Assert.AreEqual(false, camera.IsEnabled);
        }
    }
}
