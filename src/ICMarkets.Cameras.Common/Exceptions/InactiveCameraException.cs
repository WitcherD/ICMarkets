using System;

namespace ICMarkets.Cameras.Common.Exceptions
{
    /// <summary>
    /// Camera is inactive and can not receive commands
    /// </summary>
    /// <remarks>
    /// In our case it means that camera is switched off
    /// </remarks>
    public class InactiveCameraException : Exception
    {
    }
}
