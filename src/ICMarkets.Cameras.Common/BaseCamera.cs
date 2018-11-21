using System;
using System.Threading;
using System.Threading.Tasks;
using ICMarkets.Cameras.Common.Communication;
using ICMarkets.Cameras.Common.Exceptions;
using ICMarkets.Cameras.Common.Photos;
using Microsoft.Extensions.Logging;

namespace ICMarkets.Cameras.Common
{
    /// <summary>
    /// Base class for cameras with default implementations.
    /// Using this class is not necessary, but can be usefull.
    /// </summary>
    public abstract class BaseCamera<TCamera> : ICamera
    {
        #region ### Properties ###

        /// <inheritdoc />
        public virtual Guid DeviceUid { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public virtual int MinZoom { get; } = 1;

        /// <inheritdoc />
        public virtual int MaxZoom { get; } = 16;

        /// <inheritdoc />
        public virtual int CurrentZoom { get; private set; } = 4;

        /// <inheritdoc />
        public virtual bool IsEnabled { get; private set; } = false;

        /// <summary>
        /// Communication channel
        /// </summary>
        protected ICommunicationChannel CommunicationChannel { get; }

        /// <summary>
        /// Current logger
        /// </summary>
        protected ILogger<TCamera> Logger { get; }
        
        /// <summary>
        /// All commands to the camera are asynchronous, so we need to queue commands to prevent multithreading exceptions
        /// </summary>
        /// <remarks>
        /// It's defenetly not the best solution and code looks messy as well.
        /// But for demo it's good compromise between productivity and code quality. 
        /// </remarks>
        protected readonly SemaphoreSlim Locker = new SemaphoreSlim(1, 1);

        #endregion

        /// <inheritdoc />
        protected BaseCamera(ILogger<TCamera> logger, ICommunicationChannel communicationChannel)
        {
            CommunicationChannel = communicationChannel ?? throw new ArgumentNullException(nameof(communicationChannel));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region ### ICamera Implementation ###

        /// <inheritdoc />
        public virtual async Task ZoomInAsync()
        {
            await Locker.WaitAsync();
            try
            {
                AssertCameraAvailability();

                if (CurrentZoom < MaxZoom)
                {
                    CurrentZoom++;
                    await CommunicationChannel.ZoomInAsync();
                    Logger.LogInformation($"{this} zoomed in. Current zoom is {CurrentZoom}");
                }
                else
                {
                    Logger.LogInformation($"Current zoom {CurrentZoom} is already maximum");
                }
            }
            finally
            {
                Locker.Release();
            }
        }

        /// <inheritdoc />
        public virtual async Task ZoomOutAsync()
        {
            await Locker.WaitAsync();
            try
            {
                AssertCameraAvailability();

                if (CurrentZoom > MinZoom)
                {
                    CurrentZoom--;
                    await CommunicationChannel.ZoomOutAsync();
                    Logger.LogInformation($"{this} zoomed out. Current zoom is {CurrentZoom}");
                }
                else
                {
                    Logger.LogInformation($"Current zoom {CurrentZoom} is already minimum");
                }
            }
            finally
            {
                Locker.Release();
            }
        }

        /// <inheritdoc />
        public virtual async Task<IPhoto> TakePhotoAsync()
        {
            await Locker.WaitAsync();
            try
            {
                AssertCameraAvailability();
                var image = await CommunicationChannel.SnapshotAsync();
                // Converting, enrichment, etc.

                var result = new Photo();
                Logger.LogInformation($"{this}. Current image: {Environment.NewLine}{result}");
                return result;
            }
            finally
            {
                Locker.Release();
            }
        }

        #endregion

        #region ### IDevice Implementation ###

        /// <inheritdoc />
        public virtual async Task SwitchOnAsync()
        {
            await Locker.WaitAsync();
            try
            {
                IsEnabled = true;
                await CommunicationChannel.SwitchOnAsync();
                Logger.LogInformation($"{this} switched on");
            }
            finally
            {
                Locker.Release();
            }
        }

        /// <inheritdoc />
        public virtual async Task SwitchOffAsync()
        {
            await Locker.WaitAsync();
            try
            {
                IsEnabled = false;
                await CommunicationChannel.SwitchOffAsync();
                Logger.LogInformation($"{this} switched off");
            }
            finally
            {
                Locker.Release();
            }
        }

        #endregion

        #region ### Private Methods ###

        private void AssertCameraAvailability()
        {
            if (!IsEnabled)
                throw new InactiveCameraException();
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{typeof(TCamera).Name} {DeviceUid}";
        }
    }
}
