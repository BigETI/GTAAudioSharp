/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio SFX data information class
    /// </summary>
    internal struct GTAAudioSFXDataInfo
    {
        /// <summary>
        /// Sound buffer offset
        /// </summary>
        public readonly uint SoundBufferOffset;

        /// <summary>
        /// Loop offset
        /// </summary>
        public readonly uint LoopOffset;

        /// <summary>
        /// Sample rate
        /// </summary>
        public readonly ushort SampleRate;

        /// <summary>
        /// Sound headroom
        /// </summary>
        public readonly ushort SoundHeadroom;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="soundBufferOffset">Sound buffer offset</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="soundHeadroom">Sound headroom</param>
        public GTAAudioSFXDataInfo(uint soundBufferOffset, uint loopOffset, ushort sampleRate, ushort soundHeadroom)
        {
            SoundBufferOffset = soundBufferOffset;
            LoopOffset = loopOffset;
            SampleRate = sampleRate;
            SoundHeadroom = soundHeadroom;
        }
    }
}
