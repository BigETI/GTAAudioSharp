using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio file abstract class
    /// </summary>
    /// <typeparam name="T">Open stream type</typeparam>
    internal abstract class AGTAAudioFile<T> : IGTAAudioFile<T>
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Stream
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Banks
        /// </summary>
        public IReadOnlyList<IGTAAudioBankInformation> Banks { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="stream">Stream</param>
        /// <param name="banks">Banks</param>
        public AGTAAudioFile(string name, Stream stream, IReadOnlyList<IGTAAudioBankInformation> banks)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Stream = stream;
            Banks = banks ?? throw new ArgumentNullException(nameof(banks));
        }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioIndex">Audio index</param>
        /// <param name="bankSlot">Bank slot</param>
        /// <returns>Audio stream</returns>
        public abstract T Open(uint bankIndex, uint audioIndex);

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose() => Stream?.Dispose();
    }
}
