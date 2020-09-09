using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio stream file class
    /// </summary>
    internal class GTAAudioStreamsFile : AGTAAudioFile<GTAAudioStreamsStream>, IGTAAudioStreamsFile
    {
        /// <summary>
        /// Beats
        /// </summary>
        public IReadOnlyList<IGTAAudioBeatInformation> Beats { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="fileStream">File stream</param>
        /// <param name="banks">Banks</param>
        /// <param name="beats">Beats</param>
        public GTAAudioStreamsFile(string name, FileStream fileStream, IReadOnlyList<IGTAAudioBankInformation> banks, IReadOnlyList<IGTAAudioBeatInformation> beats) : base(name, fileStream, banks) => Beats = beats ?? throw new ArgumentNullException(nameof(beats));

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioIndex">Audio index (unused)</param>
        /// <returns>Audio stream</returns>
        public override GTAAudioStreamsStream Open(uint bankIndex, uint audioIndex)
        {
            GTAAudioStreamsStream ret = null;
            if (bankIndex >= Banks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(bankIndex), bankIndex, "Invalid bank index.");
            }
            IGTAAudioBankInformation bank_data = Banks[(int)bankIndex];
            uint offset = bank_data.Offset + 0x1F84;
            if (Stream.Length < (offset + bank_data.Length))
            {
                throw new InvalidDataException($"Stream can't contain { bank_data.Length } bytes at stream offset { offset }.");
            }
            using (DecodingBinaryReader stream_reader = new DecodingBinaryReader(Stream, Encoding.UTF8, true))
            {
                Stream.Seek(offset, SeekOrigin.Begin);
                byte[] data = stream_reader.ReadDecodeBytes((int)bank_data.Length);
                if (data.Length == bank_data.Length)
                {
                    ret = new GTAAudioStreamsStream(this, data);
                    ret.Seek(0L, SeekOrigin.Begin);
                }
            }
            return ret;
        }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <returns>Audio stream</returns>
        public GTAAudioStreamsStream Open(uint bankIndex) => Open(bankIndex, 0U);
    }
}
