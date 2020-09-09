using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio SFX file class
    /// </summary>
    internal class GTAAudioSFXFile : AGTAAudioFile<GTAAudioSFXStream>, IGTAAudioSFXFile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="fileStream">File stream</param>
        /// <param name="banks">Bank data</param>
        public GTAAudioSFXFile(string name, FileStream fileStream, IReadOnlyList<IGTAAudioBankInformation> banks) : base(name, fileStream, banks)
        {
            // ...
        }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioIndex">Audio index</param>
        /// <returns>GTA audio SFX stream</returns>
        public override GTAAudioSFXStream Open(uint bankIndex, uint audioIndex)
        {
            GTAAudioSFXStream ret = null;
            if (bankIndex >= Banks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(bankIndex), bankIndex, "Invalid bank index.");
            }
            IGTAAudioBankInformation bank_data = Banks[(int)bankIndex];
            if (audioIndex >= bank_data.AudioClips.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(audioIndex), audioIndex, "Invalid audio index.");
            }
            IGTAAudioAudioClipInformation audio_clip_data = bank_data.AudioClips[(int)audioIndex];
            uint offset = bank_data.Offset + audio_clip_data.SoundBufferOffset + 0x12C4;
            if (Stream.Length < (offset + audio_clip_data.Length))
            {
                throw new InvalidDataException($"Stream can't contain { audio_clip_data.Length } bytes at stream offset { offset }.");
            }
            byte[] data = new byte[audio_clip_data.Length];
            Stream.Seek(offset, SeekOrigin.Begin);
            if (Stream.Read(data, 0, data.Length) == data.Length)
            {
                ret = new GTAAudioSFXStream(this, audio_clip_data.SampleRate, audio_clip_data.LoopOffset, audio_clip_data.SoundHeadroom, data);
                ret.Seek(0L, SeekOrigin.Begin);
            }
            return ret;
        }
    }
}
