/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio SFX stream class
    /// </summary>
    public class GTAAudioSFXStream : ACommitableMemoryStream<GTAAudioSFXStream>
    {
        /// <summary>
        /// Sample rate
        /// </summary>
        public ushort SampleRate { get; }

        /// <summary>
        /// Loop offset
        /// </summary>
        public uint LoopOffset { get; }

        /// <summary>
        /// Sound headroom
        /// </summary>
        public uint SoundHeadroom { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="soundHeadroom">Loop offset</param>
        public GTAAudioSFXStream(IGTAAudioSFXFile gtaAudioFile, ushort sampleRate, uint loopOffset, uint soundHeadroom) : base(gtaAudioFile)
        {
            SampleRate = sampleRate;
            LoopOffset = loopOffset;
            SoundHeadroom = soundHeadroom;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="buffer">Buffer</param>
        public GTAAudioSFXStream(IGTAAudioSFXFile gtaAudioFile, ushort sampleRate, uint loopOffset, uint soundHeadroom, byte[] buffer) : base(gtaAudioFile, buffer)
        {
            SampleRate = sampleRate;
            LoopOffset = loopOffset;
            SoundHeadroom = soundHeadroom;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="capacity">Capacity</param>
        public GTAAudioSFXStream(IGTAAudioSFXFile gtaAudioFile, ushort sampleRate, uint loopOffset, uint soundHeadroom, int capacity) : base(gtaAudioFile, capacity)
        {
            SampleRate = sampleRate;
            LoopOffset = loopOffset;
            SoundHeadroom = soundHeadroom;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="writable">Writable</param>
        public GTAAudioSFXStream(IGTAAudioSFXFile gtaAudioFile, ushort sampleRate, uint loopOffset, uint soundHeadroom, byte[] buffer, bool writable) : base(gtaAudioFile, buffer, writable)
        {
            SampleRate = sampleRate;
            LoopOffset = loopOffset;
            SoundHeadroom = soundHeadroom;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        public GTAAudioSFXStream(IGTAAudioSFXFile gtaAudioFile, ushort sampleRate, uint loopOffset, uint soundHeadroom, byte[] buffer, int index, int count) : base(gtaAudioFile, buffer, index, count)
        {
            SampleRate = sampleRate;
            LoopOffset = loopOffset;
            SoundHeadroom = soundHeadroom;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        /// <param name="writable">Writable</param>
        public GTAAudioSFXStream(IGTAAudioSFXFile gtaAudioFile, ushort sampleRate, uint loopOffset, uint soundHeadroom, byte[] buffer, int index, int count, bool writable) : base(gtaAudioFile, buffer, index, count, writable)
        {
            SampleRate = sampleRate;
            LoopOffset = loopOffset;
            SoundHeadroom = soundHeadroom;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="loopOffset">Loop offset</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        /// <param name="writable">Writable</param>
        /// <param name="publiclyVisible">Publicly visible</param>
        public GTAAudioSFXStream(IGTAAudioSFXFile gtaAudioFile, ushort sampleRate, uint loopOffset, uint soundHeadroom, byte[] buffer, int index, int count, bool writable, bool publiclyVisible) : base(gtaAudioFile, buffer, index, count, writable, publiclyVisible)
        {
            SampleRate = sampleRate;
            LoopOffset = loopOffset;
            SoundHeadroom = soundHeadroom;
        }
    }
}
