using System.IO;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio SFX file class
    /// </summary>
    public class GTAAudioSFXFile : AGTAAudioFile
    {
        /// <summary>
        /// Audio data
        /// </summary>
        private GTAAudioSFXDataInfo[] audioData;

        /// <summary>
        /// Bank slots
        /// </summary>
        private GTAAudioBankSlotData[] bankSlots;

        /// <summary>
        /// Audio data
        /// </summary>
        private GTAAudioSFXDataInfo[] AudioData
        {
            get
            {
                if (audioData == null)
                {
                    audioData = new GTAAudioSFXDataInfo[0];
                }
                return audioData;
            }
        }

        /// <summary>
        /// Bank slots
        /// </summary>
        private GTAAudioBankSlotData[] BankSlots
        {
            get
            {
                if (bankSlots == null)
                {
                    bankSlots = new GTAAudioBankSlotData[0];
                }
                return bankSlots;
            }
        }

        /// <summary>
        /// Number of audios
        /// </summary>
        public int NumAudios
        {
            get
            {
                return AudioData.Length;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileStream">File stream</param>
        /// <param name="lookupData">Lookup data</param>
        /// <param name="name">Name</param>
        /// <param name="audioData">Audio data</param>
        internal GTAAudioSFXFile(string name, FileStream fileStream, GTAAudioLookupData[] lookupData, GTAAudioSFXDataInfo[] audioData, GTAAudioBankSlotData[] bankSlots) : base(name, fileStream, lookupData)
        {
            this.audioData = audioData;
            this.bankSlots = bankSlots;
        }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioIndex">Audio index</param>
        /// <param name="bankSlot">Bank slot</param>
        /// <returns>GTA audio stream</returns>
        public override Stream Open(uint bankIndex, uint audioIndex, uint bankSlot)
        {
            GTAAudioStream ret = null;
            if (FileStream != null)
            {
                if ((bankIndex < LookupData.Length) && (audioIndex < AudioData.Length) && (bankSlot < BankSlots.Length))
                {
                    GTAAudioLookupData lookup_data = LookupData[bankIndex];
                    GTAAudioSFXDataInfo audio_data = AudioData[audioIndex];
                    GTAAudioBankSlotData bank_slot = BankSlots[bankSlot];
                    uint offset = lookup_data.Offset + audio_data.SoundBufferOffset + 0x12C4;
                    uint len = ((bank_slot.BufferSize < lookup_data.Length) ? bank_slot.BufferSize : lookup_data.Length);
                    if (FileStream.Length >= (offset + len))
                    {
                        byte[] data = new byte[len];
                        FileStream.Seek(offset, SeekOrigin.Begin);
                        if (FileStream.Read(data, 0, data.Length) == data.Length)
                        {
                            ret = new GTAAudioStream(this, audio_data.SampleRate, audio_data.LoopOffset, audio_data.SoundHeadroom, data);
                            ret.Seek(0L, SeekOrigin.Begin);
                        }
                    }
                }
            }
            return ret;
        }
    }
}
