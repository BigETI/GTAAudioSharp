/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio audio clip information structure
    /// </summary>
    internal readonly struct GTAAudioAudioClipInformation : IGTAAudioAudioClipInformation
    {
        /// <summary>
        /// Sound buffer offset
        /// </summary>
        public uint SoundBufferOffset { get; }

        /// <summary>
        /// Loop offset
        /// </summary>
        public uint LoopOffset { get; }

        /// <summary>
        /// Sample rate
        /// </summary>
        public ushort SampleRate { get; }

        /// <summary>
        /// Sound headroom
        /// </summary>
        public ushort SoundHeadroom { get; }

        /// <summary>
        /// Length
        /// </summary>
        public uint Length { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="soundBufferOffset">Sound buffer offset</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="soundHeadroom">Sound headroom</param>
        /// <param name="length">Length</param>
        public GTAAudioAudioClipInformation(uint soundBufferOffset, uint loopOffset, ushort sampleRate, ushort soundHeadroom, uint length)
        {
            SoundBufferOffset = soundBufferOffset;
            LoopOffset = loopOffset;
            SampleRate = sampleRate;
            SoundHeadroom = soundHeadroom;
            Length = length;
        }
    }
}
