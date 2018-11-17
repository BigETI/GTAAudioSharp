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
    public class UnitTestConfigDataContract
    {
        /// <summary>
        /// Audio files directory
        /// </summary>
        [DataMember]
        private string audioFilesDir;

        /// <summary>
        /// Audio files directory
        /// </summary>
        public string AudioFilesDir
        {
            get
            {
                if (audioFilesDir == null)
                {
                    audioFilesDir = "";
                }
                return audioFilesDir;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="audioFilesDir">Audio files directory</param>
        public UnitTestConfigDataContract(string audioFilesDir)
        {
            this.audioFilesDir = audioFilesDir;
        }
    }
}
