using System.Threading.Tasks;

namespace ICMarkets.Commands
{
    /// <summary>
    /// Standart implementation of command pattern <see cref="https://en.wikipedia.org/wiki/Command_pattern#C#"/>
    /// </summary>
    /// <remarks>
    /// 1. Make it async instead of sync, because it's really usefull and for CPU-bound (UI intefaces)
    /// and for IO-bound (all IO communications) operations.
    ///
    /// 2. Use command pattern because camera can be controlled by many UI elements
    ///    (button's arrows, mouse, console app).
    ///
    /// 3. Invoker in our scenario is a user, no need more layers.
    /// </remarks>
    public interface IAsyncCommand
    {
        /// <summary>
        /// Execute command
        /// </summary>
        Task ExecuteAsync();
    }
}
