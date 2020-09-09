using System;
using System.IO;

/// GTA audio sharp
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// Commitable memory stream abstract class
    /// </summary>
    /// <typeparam name="T">Open stream type</typeparam>
    public abstract class ACommitableMemoryStream<T> : MemoryStream
    {
        /// <summary>
        /// GTA audio file
        /// </summary>
        public IGTAAudioFile<T> GTAAudioFile { get; }

        /// <summary>
        /// Is stream open
        /// </summary>
        public bool IsOpen { get; private set; } = true;

        /// <summary>
        /// On close
        /// </summary>
        public GTAAudioFileClosedDelegate<T> OnClose;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        internal ACommitableMemoryStream(IGTAAudioFile<T> gtaAudioFile) : base() => GTAAudioFile = gtaAudioFile ?? throw new ArgumentNullException(nameof(gtaAudioFile));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        internal ACommitableMemoryStream(IGTAAudioFile<T> gtaAudioFile, byte[] buffer) : base(buffer) => GTAAudioFile = gtaAudioFile ?? throw new ArgumentNullException(nameof(gtaAudioFile));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="capacity">Capacity</param>
        internal ACommitableMemoryStream(IGTAAudioFile<T> gtaAudioFile, int capacity) : base(capacity) => GTAAudioFile = gtaAudioFile ?? throw new ArgumentNullException(nameof(gtaAudioFile));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="writable">Writable</param>
        internal ACommitableMemoryStream(IGTAAudioFile<T> gtaAudioFile, byte[] buffer, bool writable) : base(buffer, writable) => GTAAudioFile = gtaAudioFile ?? throw new ArgumentNullException(nameof(gtaAudioFile));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        internal ACommitableMemoryStream(IGTAAudioFile<T> gtaAudioFile, byte[] buffer, int index, int count) : base(buffer, index, count) => GTAAudioFile = gtaAudioFile ?? throw new ArgumentNullException(nameof(gtaAudioFile));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gtaAudioFile">GTA audio file</param>
        /// <param name="sampleRate">Sample rate</param>
        /// <param name="buffer">Buffer</param>
        /// <param name="index">Index</param>
        /// <param name="count">Count</param>
        /// <param name="writable">Writable</param>
        internal ACommitableMemoryStream(IGTAAudioFile<T> gtaAudioFile, byte[] buffer, int index, int count, bool writable) : base(buffer, index, count, writable) => GTAAudioFile = gtaAudioFile ?? throw new ArgumentNullException(nameof(gtaAudioFile));

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
        internal ACommitableMemoryStream(IGTAAudioFile<T> gtaAudioFile, byte[] buffer, int index, int count, bool writable, bool publiclyVisible) : base(buffer, index, count, writable, publiclyVisible) => GTAAudioFile = gtaAudioFile ?? throw new ArgumentNullException(nameof(gtaAudioFile));

        /// <summary>
        /// Close stream
        /// </summary>
        public override void Close()
        {
            if (IsOpen)
            {
                OnClose?.Invoke(GTAAudioFile, this);
                IsOpen = false;
            }
            base.Close();
        }
    }
}
