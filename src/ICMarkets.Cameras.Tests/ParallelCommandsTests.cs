using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ICMarkets.Cameras.Tests
{
    public class ParallelCommandsTests : BaseCameraTests
    {
        [Test]
        public async Task BaseCamera_ZoomInMethod_Should_Work_In_1_Thread()
        {
            // Arrange
            var baseCameraMock1 = CreateBaseCameraMock();
            var baseCameraMock2 = CreateBaseCameraMock();
            
            var camera1 = baseCameraMock1.Object;
            var camera2 = baseCameraMock2.Object;

            await SwitchOnCameraAsync(camera1);
            await SwitchOnCameraAsync(camera2);
            await ZoomOutToMinAsync(camera1);
            await ZoomOutToMinAsync(camera2);

            var tasksCamera1 = Enumerable.Range(camera1.CurrentZoom, camera1.MaxZoom).Select(i => Task.Run(async () => await camera1.ZoomInAsync()));
            var tasksCamera2 = Enumerable.Range(camera2.CurrentZoom, camera2.MaxZoom).Select(i => Task.Run(async () => await camera2.ZoomInAsync()));
            var allTasks = tasksCamera1.Union(tasksCamera2);

            // Act
            await Task.WhenAll(allTasks);

            // Assert
            Assert.AreEqual(camera1.MaxZoom, camera1.CurrentZoom);
            Assert.AreEqual(camera2.MaxZoom, camera2.CurrentZoom);
        }
    }
}
