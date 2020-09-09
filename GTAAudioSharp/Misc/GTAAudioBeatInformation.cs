/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio beat data structure
    /// </summary>
    internal readonly struct GTAAudioBeatInformation : IGTAAudioBeatInformation
    {
        /// <summary>
        /// Timing
        /// </summary>
        public uint Timing { get; }

        /// <summary>
        /// Control
        /// </summary>
        public uint Control { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timing">Timing</param>
        /// <param name="control">Control</param>
        public GTAAudioBeatInformation(uint timing, uint control)
        {
            Timing = timing;
            Control = control;
        }
    }
}
