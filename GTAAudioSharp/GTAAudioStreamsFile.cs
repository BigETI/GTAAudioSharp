using System.IO;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio stream file class
    /// </summary>
    public class GTAAudioStreamsFile : AGTAAudioFile
    {
        /// <summary>
        /// Beats data
        /// </summary>
        private GTAAudioBeatData[] beatsData;

        /// <summary>
        /// Beats data
        /// </summary>
        public GTAAudioBeatData[] BeatsData
        {
            get
            {
                if (beatsData == null)
                {
                    beatsData = new GTAAudioBeatData[0];
                }
                return beatsData.Clone() as GTAAudioBeatData[];
            }
        }

        /// <summary>
        /// Number of beats data
        /// </summary>
        public int NumBeatsData
        {
            get
            {
                return ((beatsData == null) ? 0 : beatsData.Length);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="fileStream">File stream</param>
        /// <param name="lookupData">Lookup data</param>
        /// <param name="beatsData">Beats data</param>
        internal GTAAudioStreamsFile(string name, FileStream fileStream, GTAAudioLookupData[] lookupData, GTAAudioBeatData[] beatsData) : base(name, fileStream, lookupData)
        {
            this.beatsData = beatsData;
        }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioIndex">Audio index (unused)</param>
        /// <param name="bankSlot">Bank slot (unused)</param>
        /// <returns>Audio stream</returns>
        public override Stream Open(uint bankIndex, uint audioIndex, uint bankSlot)
        {
            Stream ret = null;
            if (FileStream != null)
            {
                if (bankIndex < LookupData.Length)
                {
                    GTAAudioLookupData lookup_data = LookupData[bankIndex];
                    uint offset = lookup_data.Offset + 0x1F84;
                    if (FileStream.Length >= (offset + lookup_data.Length))
                    {
                        DecodingBinaryReader reader = new DecodingBinaryReader(FileStream);
                        FileStream.Seek(offset, SeekOrigin.Begin);
                        byte[] data = reader.ReadDecodeBytes((int)(lookup_data.Length));
                        if (data != null)
                        {
                            if (data.Length == lookup_data.Length)
                            {
                                ret = new CommitableMemoryStream(this, data);
                                ret.Seek(0L, SeekOrigin.Begin);
                            }
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <returns>Audio stream</returns>
        public Stream Open(uint bankIndex)
        {
            return Open(bankIndex, 0U, 0U);
        }
    }
}
