using System.Threading.Tasks;
using ICMarkets.Commands;

namespace ICMarkets.Cameras.Common.Commands
{
    /// <summary>
    /// Zoom in camera command
    /// </summary>
    public class ZoomInAsyncCommand : IAsyncCommand
    {
        private readonly ICamera _camera;

        /// <inheritdoc />
        public ZoomInAsyncCommand(ICamera camera)
        {
            _camera = camera;
        }

        /// <inheritdoc />
        public Task ExecuteAsync()
        {
            return _camera.ZoomInAsync();
        }
    }
}
