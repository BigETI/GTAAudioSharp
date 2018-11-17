using System;
using System.IO;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio file abstract class
    /// </summary>
    public abstract class AGTAAudioFile : IDisposable
    {
        /// <summary>
        /// Name
        /// </summary>
        private string name;

        /// <summary>
        /// File stream
        /// </summary>
        private FileStream fileStream;

        /// <summary>
        /// Lookup data
        /// </summary>
        private GTAAudioLookupData[] lookupData;

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                if (name == null)
                {
                    name = "";
                }
                return name;
            }
        }

        /// <summary>
        /// File stream
        /// </summary>
        internal FileStream FileStream
        {
            get
            {
                return fileStream;
            }
        }

        /// <summary>
        /// Lookup data
        /// </summary>
        internal GTAAudioLookupData[] LookupData
        {
            get
            {
                if (lookupData == null)
                {
                    lookupData = new GTAAudioLookupData[0];
                }
                return lookupData;
            }
        }

        /// <summary>
        /// Number of banks
        /// </summary>
        public int NumBanks
        {
            get
            {
                return LookupData.Length;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="fileStream">File stream</param>
        /// <param name="lookupData">Lookup data</param>
        internal AGTAAudioFile(string name, FileStream fileStream, GTAAudioLookupData[] lookupData)
        {
            this.name = name;
            this.fileStream = fileStream;
            this.lookupData = lookupData;
        }

        /// <summary>
        /// Open audio stream
        /// </summary>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioIndex">Audio index</param>
        /// <returns>Audio stream</returns>
        public abstract Stream Open(uint bankIndex, uint audioIndex);

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            if (fileStream != null)
            {
                fileStream.Dispose();
                fileStream = null;
            }
        }
    }
}
