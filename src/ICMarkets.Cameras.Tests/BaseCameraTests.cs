using System.Threading.Tasks;
using ICMarkets.Cameras.Common;
using ICMarkets.Cameras.Common.Communication;
using Microsoft.Extensions.Logging;
using Moq;

namespace ICMarkets.Cameras.Tests
{
    public abstract class BaseCameraTests
    {
        protected Mock<BaseCamera<ICamera>> CreateBaseCameraMock()
        {
            var loggerMock = new Mock<ILogger<ICamera>>();
            var communicationChannelMock = new Mock<ICommunicationChannel>();
            var baseCameraMock = new Mock<BaseCamera<ICamera>>(loggerMock.Object, communicationChannelMock.Object)
            {
                CallBase = true
            };

            baseCameraMock.SetupGet(i => i.MaxZoom).Returns(16);
            baseCameraMock.SetupGet(i => i.MinZoom).Returns(1);
            
            return baseCameraMock;
        }

        protected async Task SwitchOnCameraAsync(BaseCamera<ICamera> camera)
        {
            // We need to stub IsEnabled using stubs, reflection or smth else.
            // This is the simplest way for demo.

            await camera.SwitchOnAsync();
        }

        protected async Task SwitchOffCameraAsync(BaseCamera<ICamera> camera)
        {
            // We need to stub IsEnabled using stubs, reflection or smth else.
            // This is the simplest way for demo.

            await camera.SwitchOffAsync();
        }

        protected async Task ZoomInToMaxAsync(BaseCamera<ICamera> camera)
        {
            // We need to stub CurrentZoom using stubs, reflection or smth else.
            // This is the simplest way for demo.

            var zoomsCount = camera.MaxZoom - camera.CurrentZoom;

            for (var i = 0; i < zoomsCount; i++)
            {
                await camera.ZoomInAsync();
            }
        }

        protected async Task ZoomOutToMinAsync(BaseCamera<ICamera> camera)
        {
            // We need to stub CurrentZoom using stubs, reflection or smth else.
            // This is the simplest way for demo.

            var zoomsCount = camera.CurrentZoom - camera.MinZoom;

            for (var i = 0; i < zoomsCount; i++)
            {
                await camera.ZoomOutAsync();
            }
        }
    }
}
