using System.Threading.Tasks;

namespace ICMarkets.Cameras.Common.Communication
{
    /// <summary>
    /// Communication channel with cameras.
    /// </summary>
    /// <remarks>
    /// This layer represents transport abstraction for cameras, but not abstraction for transport clients!
    /// That's why this interface is located in this project rather than in some **transport** project.
    /// </remarks>
    public interface ICommunicationChannel
    {
        /// <summary>
        /// Switch on the camera
        /// </summary>
        Task SwitchOnAsync();

        /// <summary>
        /// Switch off the camera
        /// </summary>
        Task SwitchOffAsync();

        /// <summary>
        /// Zoom in the camera
        /// </summary>
        Task ZoomInAsync();

        /// <summary>
        /// Zoom out the camera
        /// </summary>
        Task ZoomOutAsync();

        /// <summary>
        /// Get current image
        /// </summary>
        Task<byte[]> SnapshotAsync();
    }
}
