using System.Threading.Tasks;
using ICMarkets.Commands;

namespace ICMarkets.Devices.Common.Commands
{
    /// <summary>
    /// Switch off a device command
    /// </summary>
    public class SwitchOffAsyncCommand : IAsyncCommand
    {
        private readonly IDevice _device;

        /// <inheritdoc />
        public SwitchOffAsyncCommand(IDevice device)
        {
            _device = device;
        }

        /// <inheritdoc />
        public Task ExecuteAsync()
        {
            return _device.SwitchOnAsync();
        }
    }
}