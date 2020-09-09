/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio sudio clip information interface
    /// </summary>
    public interface IGTAAudioAudioClipInformation
    {
        /// <summary>
        /// Sound buffer offset
        /// </summary>
        uint SoundBufferOffset { get; }

        /// <summary>
        /// Loop offset
        /// </summary>
        uint LoopOffset { get; }

        /// <summary>
        /// Sample rate
        /// </summary>
        ushort SampleRate { get; }

        /// <summary>
        /// Sound headroom
        /// </summary>
        ushort SoundHeadroom { get; }

        /// <summary>
        /// Length
        /// </summary>
        uint Length { get; }
    }
}
