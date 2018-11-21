using System.Threading.Tasks;
using ICMarkets.Cameras.Common.Exceptions;
using NUnit.Framework;

namespace ICMarkets.Cameras.Tests
{
    public class ZoomCameraTests : BaseCameraTests
    {
        [Test]
        public async Task BaseCamera_ZoomInAsync_Should_Increment()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            var camera = baseCameraMock.Object;
            await SwitchOnCameraAsync(camera);
            await ZoomOutToMinAsync(camera);

            var zoomBeforeTest = camera.CurrentZoom;

            // Act
            await camera.ZoomInAsync();

            // Assert
            baseCameraMock.Verify(m => m.ZoomInAsync());
            Assert.AreEqual(zoomBeforeTest + 1, camera.CurrentZoom);
        }

        [Test]
        public async Task BaseCamera_ZoomInAsync_MaxZoom_Shouldnt_Change()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            var camera = baseCameraMock.Object;
            await SwitchOnCameraAsync(camera);
            await ZoomOutToMinAsync(camera);
            var zoomsCount = camera.MaxZoom - camera.CurrentZoom + 1;

            // Act
            for (var i = 0; i < zoomsCount; i++)
            {
                await camera.ZoomInAsync();
            }

            // Assert
            baseCameraMock.Verify(m => m.ZoomInAsync());
            Assert.AreEqual(camera.MaxZoom, camera.CurrentZoom);
        }

        [Test]
        public void BaseCamera_ZoomInAsync_CameraInactive_Should_Throw_InactiveCameraException()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            baseCameraMock.SetupGet(i => i.IsEnabled).Returns(false);
            var camera = baseCameraMock.Object;

            // Act, Assert
            Assert.ThrowsAsync<InactiveCameraException>(() => camera.ZoomInAsync());
        }

        [Test]
        public async Task BaseCamera_ZoomOutAsync_Should_Decrement()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            var camera = baseCameraMock.Object;
            await SwitchOnCameraAsync(camera);
            await ZoomInToMaxAsync(camera);

            var zoomBeforeTest = camera.CurrentZoom;

            // Act
            await camera.ZoomOutAsync();

            // Assert
            baseCameraMock.Verify(m => m.ZoomOutAsync());
            Assert.AreEqual(zoomBeforeTest - 1, camera.CurrentZoom);
        }

        [Test]
        public async Task BaseCamera_ZoomOutAsync_MinZoom_Shouldnt_Change()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            var camera = baseCameraMock.Object;
            await SwitchOnCameraAsync(camera);
            await ZoomInToMaxAsync(camera);

            var zoomsCount = camera.CurrentZoom - camera.MinZoom + 1;

            // Act
            for (var i = 0; i < zoomsCount; i++)
            {
                await camera.ZoomOutAsync();
            }

            // Assert
            baseCameraMock.Verify(m => m.ZoomOutAsync());
            Assert.AreEqual(camera.MinZoom, camera.CurrentZoom);
        }

        [Test]
        public void BaseCamera_ZoomOutAsync_CameraInactive_Should_Throw_InactiveCameraException()
        {
            // Arrange
            var baseCameraMock = CreateBaseCameraMock();
            baseCameraMock.SetupGet(i => i.IsEnabled).Returns(false);
            var camera = baseCameraMock.Object;

            // Act, Assert
            Assert.ThrowsAsync<InactiveCameraException>(() => camera.ZoomOutAsync());
        }
    }
}
