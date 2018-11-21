using System;
using System.Threading.Tasks;

namespace ICMarkets.Cameras.Common.Communication
{
    /// <summary>
    /// Example of communication channel implementation <see cref="https://en.wikipedia.org/wiki/Real_Time_Streaming_Protocol"/>
    /// </summary>
    /// <remarks>
    /// I don't know if it really used for =) just googled it.
    /// </remarks>
    public class RtspCommunicationChannel : ICommunicationChannel
    {
        // We will use RTSP client here, control connections, send bytes, whatever needed.

        /// <inheritdoc />
        public Task SwitchOnAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task SwitchOffAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task ZoomInAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task ZoomOutAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<byte[]> SnapshotAsync()
        {
            return Task.FromResult(Array.Empty<byte>());
        }
    }
}
