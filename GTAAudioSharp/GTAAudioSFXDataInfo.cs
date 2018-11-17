/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio SFX data information class
    /// </summary>
    internal struct GTAAudioSFXDataInfo
    {
        /// <summary>
        /// Offset
        /// </summary>
        private uint offset;

        /// <summary>
        /// Sample rate
        /// </summary>
        private ushort sampleRate;

        /// <summary>
        /// Offset
        /// </summary>
        public uint Offset
        {
            get
            {
                return offset;
            }
        }

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
        /// <param name="offset">Offset</param>
        /// <param name="sampleRate">Sample rate</param>
        public GTAAudioSFXDataInfo(uint offset, ushort sampleRate)
        {
            this.offset = offset;
            this.sampleRate = sampleRate;
        }
    }
}
