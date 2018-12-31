using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio bank slot data structure
    /// </summary>
    public struct GTAAudioBankSlotData
    {
        /// <summary>
        /// Buffer size
        /// </summary>
        public readonly uint BufferSize;

        /// <summary>
        /// GTA audio bank slot data
        /// </summary>
        /// <param name="bufferSize"></param>
        public GTAAudioBankSlotData(uint bufferSize)
        {
            BufferSize = bufferSize;
        }
    }
}
