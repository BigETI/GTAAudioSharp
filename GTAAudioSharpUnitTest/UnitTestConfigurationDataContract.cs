using System.Runtime.Serialization;

/// <summary>
/// GTA audio sharp unit test namespace
/// </summary>
namespace GTAAudioSharpUnitTest
{
    /// <summary>
    /// Unit test configuration data contract class
    /// </summary>
    [DataContract]
    public class UnitTestConfigurationDataContract
    {
        /// <summary>
        /// Audio files directory
        /// </summary>
        [DataMember]
        private string audioFilesDirectoryPath;

        /// <summary>
        /// Audio files directory
        /// </summary>
        public string AudioFilesDirectoryPath
        {
            get
            {
                if (audioFilesDirectoryPath == null)
                {
                    audioFilesDirectoryPath = string.Empty;
                }
                return audioFilesDirectoryPath;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="audioFilesDirectoryPath">Audio files directory path</param>
        public UnitTestConfigurationDataContract(string audioFilesDirectoryPath)
        {
            this.audioFilesDirectoryPath = audioFilesDirectoryPath;
        }
    }
}
