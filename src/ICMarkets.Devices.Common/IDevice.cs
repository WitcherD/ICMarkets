using System;
using System.Threading.Tasks;

namespace ICMarkets.Devices.Common
{
    /// <summary>
    /// A common device can be controlled by our application
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        Guid DeviceUid { get; }

        /// <summary>
        /// Is device active and able to receive commands
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Switch on the device
        /// </summary>
        Task SwitchOnAsync();

        /// <summary>
        /// Switch off the device
        /// </summary>
        Task SwitchOffAsync();
    }
}
