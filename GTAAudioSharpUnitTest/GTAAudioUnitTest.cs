using GTAAudioSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.Serialization.Json;

/// <summary>
/// GTA audio sharp unit test namespace
/// </summary>
namespace GTAAudioSharpUnitTest
{
    /// <summary>
    /// GTA audio unit test class
    /// </summary>
    [TestClass]
    public class GTAAudioUnitTest
    {
        /// <summary>
        /// Config serializer
        /// </summary>
        private static readonly string configPath = Path.Combine(Environment.CurrentDirectory, "config.json");

        /// <summary>
        /// Config serializer
        /// </summary>
        private static readonly DataContractJsonSerializer configSerializer = new DataContractJsonSerializer(typeof(UnitTestConfigDataContract));

        /// <summary>
        /// Valid SFX files
        /// </summary>
        private static readonly string[] validSFXFiles = new string[]
        {
            "FEET",
            "GENRL",
            "PAIN_A",
            "SCRIPT",
            "SPC_EA",
            "SPC_FA",
            "SPC_GA",
            "SPC_NA",
            "SPC_PA"
        };

        /// <summary>
        /// Valid streams files
        /// </summary>
        private static readonly string[] validStreamsFiles = new string[]
        {
            "AA",
            "ADVERTS",
            "",
            "AMBIENCE",
            "BEATS",
            "CH",
            "CO",
            "CR",
            "CUTSCENE",
            "DS",
            "HC",
            "MH",
            "MR",
            "NJ",
            "RE",
            "RG",
            "TK"
        };

        /// <summary>
        /// Configuration
        /// </summary>
        private UnitTestConfigDataContract config;

        /// <summary>
        /// Configuration
        /// </summary>
        public UnitTestConfigDataContract Config
        {
            get
            {
                if (config == null)
                {
                    if (File.Exists(configPath))
                    {
                        using (FileStream stream = File.Open(configPath, FileMode.Open))
                        {
                            config = configSerializer.ReadObject(stream) as UnitTestConfigDataContract;
                        }
                    }
                    else
                    {
                        config = new UnitTestConfigDataContract(Path.Combine(Environment.CurrentDirectory, "audio"));
                        using (FileStream stream = File.Open(configPath, FileMode.Create))
                        {
                            configSerializer.WriteObject(stream, config);
                        }
                    }
                }
                return config;
            }
        }

        /// <summary>
        /// Read file test
        /// </summary>
        [TestMethod]
        public void ReadFileTest()
        {
            using (GTAAudioFiles files = GTAAudio.OpenRead(Config.AudioFilesDir))
            {
                Assert.IsNotNull(files, "Files can't be opened. Please configure \"" + configPath + "\".");
                Assert.IsTrue(files.SFXAudioFiles.Length == validSFXFiles.Length, "Missing SFX file entries. " + files.SFXAudioFiles.Length + " files found, not " + validSFXFiles.Length);
                for (int i = 0; i < files.SFXAudioFiles.Length; i++)
                {
                    GTAAudioSFXFile sfx_audio_file = files.SFXAudioFiles[i];
                    if (sfx_audio_file != null)
                    {
                        Assert.IsTrue(sfx_audio_file.Name == validSFXFiles[i], "SFX file \"" + sfx_audio_file.Name + "\" is not \"" + validSFXFiles[i] + "\" at index " + i);
                    }
                }
                Assert.IsTrue(files.StreamsAudioFiles.Length == validStreamsFiles.Length, "Missing streams file entries. " + files.SFXAudioFiles.Length + " files found, not " + validSFXFiles.Length);
                for (int i = 0; i < files.SFXAudioFiles.Length; i++)
                {
                    GTAAudioStreamsFile streams_audio_file = files.StreamsAudioFiles[i];
                    if (streams_audio_file != null)
                    {
                        Assert.IsTrue(streams_audio_file.Name == validStreamsFiles[i], "Streams file \"" + streams_audio_file.Name + "\" is not \"" + validStreamsFiles[i] + "\" at index " + i);
                    }
                }
            }
        }
    }
}
