/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio streams stream class
    /// </summary>
    /// <typeparam name="T">Open stream type</typeparam>
    public class GTAAudioStreamsStream : ACommitableMemoryStream<GTAAudioStreamsStream>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        public GTAAudioStreamsStream(IGTAAudioFile<GTAAudioStreamsStream> gtaAudioFile) : base(gtaAudioFile)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        public GTAAudioStreamsStream(IGTAAudioFile<GTAAudioStreamsStream> gtaAudioFile, byte[] buffer) : base(gtaAudioFile, buffer)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="capacity">Capacity</param>
        public GTAAudioStreamsStream(IGTAAudioFile<GTAAudioStreamsStream> gtaAudioFile, int capacity) : base(gtaAudioFile, capacity)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="writable">Writable</param>
        public GTAAudioStreamsStream(IGTAAudioFile<GTAAudioStreamsStream> gtaAudioFile, byte[] buffer, bool writable) : base(gtaAudioFile, buffer, writable)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        public GTAAudioStreamsStream(IGTAAudioFile<GTAAudioStreamsStream> gtaAudioFile, byte[] buffer, int index, int count) : base(gtaAudioFile, buffer, index, count)
        {
            // ...
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
        public GTAAudioStreamsStream(IGTAAudioFile<GTAAudioStreamsStream> gtaAudioFile, byte[] buffer, int index, int count, bool writable) : base(gtaAudioFile, buffer, index, count, writable)
        {
            // ...
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
        public GTAAudioStreamsStream(IGTAAudioFile<GTAAudioStreamsStream> gtaAudioFile, byte[] buffer, int index, int count, bool writable, bool publiclyVisible) : base(gtaAudioFile, buffer, index, count, writable, publiclyVisible)
        {
            // ...
        }
    }
}
