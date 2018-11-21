using System.Threading.Tasks;
using ICMarkets.Cameras.Common.Photos;
using ICMarkets.Devices.Common;

namespace ICMarkets.Cameras.Common
{
    /// <summary>
    /// A camera device
    /// </summary>
    public interface ICamera : IDevice
    {
        /// <summary>
        /// Min suppotred zoom
        /// </summary>
        int MinZoom { get; }

        /// <summary>
        /// Max suppotred zoom
        /// </summary>
        int MaxZoom { get; }

        /// <summary>
        /// Current zoom
        /// </summary>
        int CurrentZoom { get; }

        /// <summary>
        /// Zoom in
        /// </summary>
        Task ZoomInAsync();

        /// <summary>
        /// Zoom out
        /// </summary>
        Task ZoomOutAsync();

        /// <summary>
        /// Take a photo
        /// </summary>
        Task<IPhoto> TakePhotoAsync();
    }
}
