using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio file interface
    /// </summary>
    /// <typeparam name="T">Open stream type</typeparam>
    public interface IGTAAudioFile<T> : IDisposable
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Stream
        /// </summary>
        Stream Stream { get; }

        /// <summary>
        /// Banks
        /// </summary>
        IReadOnlyList<IGTAAudioBankInformation> Banks { get; }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioIndex">Audio index</param>
        /// <param name="bankSlot">Bank slot</param>
        /// <returns>Audio stream</returns>
        T Open(uint bankIndex, uint audioIndex);
    }
}
