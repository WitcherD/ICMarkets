using System.Threading.Tasks;
using ICMarkets.Commands;

namespace ICMarkets.Cameras.Common.Commands
{
    /// <summary>
    /// Zoom out camera command
    /// </summary>
    public class ZoomOutAsyncCommand : IAsyncCommand
    {
        private readonly ICamera _camera;

        /// <inheritdoc />
        public ZoomOutAsyncCommand(ICamera camera)
        {
            _camera = camera;
        }

        /// <inheritdoc />
        public Task ExecuteAsync()
        {
            return _camera.ZoomOutAsync();
        }
    }
}