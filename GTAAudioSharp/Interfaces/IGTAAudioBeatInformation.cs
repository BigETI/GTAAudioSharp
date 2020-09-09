/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio beat information interface
    /// </summary>
    public interface IGTAAudioBeatInformation
    {
        /// <summary>
        /// Timing
        /// </summary>
        uint Timing { get; }

        /// <summary>
        /// Control
        /// </summary>
        uint Control { get; }
    }
}
