using System.Threading.Tasks;
using ICMarkets.Commands;

namespace ICMarkets.Cameras.Common.Commands
{
    /// <summary>
    /// Take a photo camera command
    /// </summary>
    public class TakePhotoAsyncCommand : IAsyncCommand
    {
        private readonly ICamera _camera;

        /// <inheritdoc />
        public TakePhotoAsyncCommand(ICamera camera)
        {
            _camera = camera;
        }

        /// <inheritdoc />
        public Task ExecuteAsync()
        {
            return _camera.TakePhotoAsync();
        }
    }
}
