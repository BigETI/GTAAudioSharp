/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio beat data
    /// </summary>
    public struct GTAAudioBeatData
    {
        /// <summary>
        /// Timing
        /// </summary>
        private uint timing;

        /// <summary>
        /// Control
        /// </summary>
        private uint control;

        /// <summary>
        /// Timing
        /// </summary>
        private uint Timing
        {
            get
            {
                return timing;
            }
        }

        /// <summary>
        /// Control
        /// </summary>
        private uint Control
        {
            get
            {
                return control;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timing">Timing</param>
        /// <param name="control">Control</param>
        public GTAAudioBeatData(uint timing, uint control)
        {
            this.timing = timing;
            this.control = control;
        }
    }
}
