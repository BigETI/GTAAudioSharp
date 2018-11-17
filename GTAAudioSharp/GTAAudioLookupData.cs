/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio lookup data structure
    /// </summary>
    internal struct GTAAudioLookupData
    {
        /// <summary>
        /// Offset
        /// </summary>
        private uint offset;

        /// <summary>
        /// Length
        /// </summary>
        private uint length;

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
        /// Length
        /// </summary>
        public uint Length
        {
            get
            {
                return length;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="offset">Offset</param>
        /// <param name="length">Length</param>
        public GTAAudioLookupData(uint offset, uint length)
        {
            this.offset = offset;
            this.length = length;
        }
    }
}
