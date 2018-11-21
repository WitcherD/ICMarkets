using System.Threading.Tasks;
using ICMarkets.Commands;

namespace ICMarkets.Devices.Common.Commands
{
    /// <summary>
    /// Switch on a device command
    /// </summary>
    public class SwitchOnAsyncCommand : IAsyncCommand
    {
        private readonly IDevice _device;

        /// <inheritdoc />
        public SwitchOnAsyncCommand(IDevice device)
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
