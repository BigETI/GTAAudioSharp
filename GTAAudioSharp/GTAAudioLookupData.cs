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
        public readonly uint Offset;

        /// <summary>
        /// Length
        /// </summary>
        public readonly uint Length;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="offset">Offset</param>
        /// <param name="length">Length</param>
        public GTAAudioLookupData(uint offset, uint length)
        {
            Offset = offset;
            Length = length;
        }
    }
}
