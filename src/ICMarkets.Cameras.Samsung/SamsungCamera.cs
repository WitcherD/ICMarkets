using ICMarkets.Cameras.Common;
using ICMarkets.Cameras.Common.Communication;
using Microsoft.Extensions.Logging;

namespace ICMarkets.Cameras.Samsung
{
    /// <summary>
    /// Just for example camera Samsung implementation
    /// </summary>
    public class SamsungCamera : BaseCamera<SamsungCamera>
    {
        /// <inheritdoc />
        public SamsungCamera(ILogger<SamsungCamera> logger, ICommunicationChannel communicationChannel) : base(logger, communicationChannel)
        {
        }
    }
}
