using System;
using System.Collections.Generic;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio bank information structure
    /// </summary>
    internal readonly struct GTAAudioBankInformation : IGTAAudioBankInformation
    {
        /// <summary>
        /// Offset
        /// </summary>
        public uint Offset { get; }

        /// <summary>
        /// Length
        /// </summary>
        public uint Length { get; }

        /// <summary>
        /// Audio clips
        /// </summary>
        public IReadOnlyList<IGTAAudioAudioClipInformation> AudioClips { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="offset">Offset</param>
        /// <param name="length">Length</param>
        public GTAAudioBankInformation(uint offset, uint length)
        {
            Offset = offset;
            Length = length;
            AudioClips = Array.Empty<IGTAAudioAudioClipInformation>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="offset">Offset</param>
        /// <param name="length">Length</param>
        /// <param name="audioClips">Audio clips</param>
        public GTAAudioBankInformation(uint offset, uint length, IReadOnlyList<IGTAAudioAudioClipInformation> audioClips)
        {
            Offset = offset;
            Length = length;
            AudioClips = audioClips ?? throw new ArgumentNullException(nameof(audioClips));
        }
    }
}
