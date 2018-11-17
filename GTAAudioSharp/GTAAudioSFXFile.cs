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
        internal GTAAudioSFXFile(string name, FileStream fileStream, GTAAudioLookupData[] lookupData, GTAAudioSFXDataInfo[] audioData) : base(name, fileStream, lookupData)
        {
            this.audioData = audioData;
        }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioIndex">Audio index</param>
        /// <returns>GTA audio stream</returns>
        public override Stream Open(uint bankIndex, uint audioIndex)
        {
            GTAAudioStream ret = null;
            if (FileStream != null)
            {
                if ((bankIndex < LookupData.Length) && (audioIndex < AudioData.Length))
                {
                    GTAAudioLookupData lookup_data = LookupData[bankIndex];
                    GTAAudioSFXDataInfo audio_data = AudioData[audioIndex];
                    uint offset = lookup_data.Offset + audio_data.Offset + 0x12C4;
                    if (FileStream.Length >= (offset + lookup_data.Length))
                    {
                        byte[] data = new byte[lookup_data.Length];
                        FileStream.Seek(offset, SeekOrigin.Begin);
                        if (FileStream.Read(data, 0, data.Length) == data.Length)
                        {
                            ret = new GTAAudioStream(this, audio_data.SampleRate, data);
                            ret.Seek(0L, SeekOrigin.Begin);
                        }
                    }
                }
            }
            return ret;
        }
    }
}
