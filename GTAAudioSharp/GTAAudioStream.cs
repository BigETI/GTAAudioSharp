/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio stream class
    /// </summary>
    public class GTAAudioStream : CommitableMemoryStream
    {
        /// <summary>
        /// Sample rate
        /// </summary>
        private ushort sampleRate;

        /// <summary>
        /// Sample rate
        /// </summary>
        public ushort SampleRate
        {
            get
            {
                return sampleRate;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        internal GTAAudioStream(AGTAAudioFile gtaAudioFile, ushort sampleRate) : base(gtaAudioFile)
        {
            this.sampleRate = sampleRate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        internal GTAAudioStream(AGTAAudioFile gtaAudioFile, ushort sampleRate, byte[] buffer) : base(gtaAudioFile, buffer)
        {
            this.sampleRate = sampleRate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="capacity">Capacity</param>
        internal GTAAudioStream(AGTAAudioFile gtaAudioFile, ushort sampleRate, int capacity) : base(gtaAudioFile, capacity)
        {
            this.sampleRate = sampleRate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="writable">Writable</param>
        internal GTAAudioStream(AGTAAudioFile gtaAudioFile, ushort sampleRate, byte[] buffer, bool writable) : base(gtaAudioFile, buffer, writable)
        {
            this.sampleRate = sampleRate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        internal GTAAudioStream(AGTAAudioFile gtaAudioFile, ushort sampleRate, byte[] buffer, int index, int count) : base(gtaAudioFile, buffer, index, count)
        {
            this.sampleRate = sampleRate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        /// <param name="writable">Writable</param>
        internal GTAAudioStream(AGTAAudioFile gtaAudioFile, ushort sampleRate, byte[] buffer, int index, int count, bool writable) : base(gtaAudioFile, buffer, index, count, writable)
        {
            this.sampleRate = sampleRate;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        /// <param name="writable">Writable</param>
        /// <param name="publiclyVisible">Publicly visible</param>
        internal GTAAudioStream(AGTAAudioFile gtaAudioFile, ushort sampleRate, byte[] buffer, int index, int count, bool writable, bool publiclyVisible) : base(gtaAudioFile, buffer, index, count, writable, publiclyVisible)
        {
            this.sampleRate = sampleRate;
        }
    }
}
