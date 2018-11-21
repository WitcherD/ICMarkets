using ICMarkets.Cameras.Common;
using ICMarkets.Cameras.Common.Communication;
using Microsoft.Extensions.Logging;

namespace ICMarkets.Cameras.Lg
{
    /// <summary>
    /// Just for example camera Lg implementation
    /// </summary>
    public class LgCamera : BaseCamera<LgCamera>
    {
        /// <inheritdoc />
        public LgCamera(ILogger<LgCamera> logger, ICommunicationChannel channel) : base(logger, channel)
        {
        }
    }
}
