using System.Collections.Generic;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio bank information interface
    /// </summary>
    public interface IGTAAudioBankInformation
    {
        /// <summary>
        /// Offset
        /// </summary>
        uint Offset { get; }

        /// <summary>
        /// Length
        /// </summary>
        uint Length { get; }

        /// <summary>
        /// Audio clips
        /// </summary>
        IReadOnlyList<IGTAAudioAudioClipInformation> AudioClips { get; }
    }
}
