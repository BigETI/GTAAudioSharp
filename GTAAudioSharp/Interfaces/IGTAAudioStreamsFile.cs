using System.Collections.Generic;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio streams file interface
    /// </summary>
    public interface IGTAAudioStreamsFile : IGTAAudioFile<GTAAudioStreamsStream>
    {
        /// <summary>
        /// Beats
        /// </summary>
        IReadOnlyList<IGTAAudioBeatInformation> Beats { get; }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <returns>Audio stream</returns>
        GTAAudioStreamsStream Open(uint bankIndex);
    }
}
