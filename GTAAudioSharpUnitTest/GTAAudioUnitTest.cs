using GTAAudioSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
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
                        using (FileStream stream = File.Open(configPath, FileMode.Open, FileAccess.Read))
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
                GTAAudioSFXFile[] sfx_audio_files = files.SFXAudioFiles;
                Assert.IsTrue(sfx_audio_files.Length == validSFXFiles.Length, "Missing SFX file entries. " + files.SFXAudioFiles.Length + " files found, not " + validSFXFiles.Length);
                for (int i = 0, j, k; i < sfx_audio_files.Length; i++)
                {
                    GTAAudioSFXFile sfx_audio_file = sfx_audio_files[i];
                    if (sfx_audio_file != null)
                    {
                        Assert.IsTrue(sfx_audio_file.Name == validSFXFiles[i], "SFX file \"" + sfx_audio_file.Name + "\" is not \"" + validSFXFiles[i] + "\" at index " + i);
                        Debug.WriteLine(sfx_audio_file.Name + ":");
                        Debug.WriteLine("\tNumber of banks: " + sfx_audio_file.NumBanks);
                        for (j = 0; j < sfx_audio_file.NumBanks; j++)
                        {
                            GTAAudioBankData bank_data = sfx_audio_file.GetBankData((uint)j);
                            Debug.WriteLine("\t\t" + sfx_audio_file.Name + " bank " + j + ":");
                            Debug.WriteLine("\t\t\tLength: " + bank_data.Length);
                            Debug.WriteLine("\t\t\tOffset: " + bank_data.Offset);
                            for (k = 0; k < bank_data.NumAudioClips; k++)
                            {
                                GTAAudioAudioClipData audio_clip_data = bank_data.GetAudioClipData((uint)k);
                                Debug.WriteLine("\t\t\t" + sfx_audio_file.Name + " bank " + j + " audio " + k + ":");
                                Debug.WriteLine("\t\t\t\tSample rate: " + audio_clip_data.SampleRate);
                                Debug.WriteLine("\t\t\t\tSound buffer offset: " + audio_clip_data.SoundBufferOffset);
                                Debug.WriteLine("\t\t\t\tLoop offset: " + audio_clip_data.LoopOffset);
                                Debug.WriteLine("\t\t\t\tSound headroom: " + audio_clip_data.SoundHeadroom);
                                Debug.WriteLine("\t\t\t\tLength: " + audio_clip_data.Length);
                            }
                        }
                    }
                }
                GTAAudioStreamsFile[] streams_audio_files = files.StreamsAudioFiles;
                Assert.IsTrue(streams_audio_files.Length == validStreamsFiles.Length, "Missing streams file entries. " + files.SFXAudioFiles.Length + " files found, not " + validSFXFiles.Length);
                for (int i = 0, j; i < streams_audio_files.Length; i++)
                {
                    GTAAudioStreamsFile streams_audio_file = streams_audio_files[i];
                    if (streams_audio_file != null)
                    {
                        Assert.IsTrue(streams_audio_file.Name == validStreamsFiles[i], "Streams file \"" + streams_audio_file.Name + "\" is not \"" + validStreamsFiles[i] + "\" at index " + i);
                        Debug.WriteLine(streams_audio_file.Name + ":");
                        Debug.WriteLine("\tNumber of banks: " + streams_audio_file.NumBanks);
                        for (j = 0; j < streams_audio_file.NumBanks; j++)
                        {
                            GTAAudioBankData bank_data = streams_audio_file.GetBankData((uint)j);
                            Debug.WriteLine("\t\t" + streams_audio_file.Name + " bank " + j + ":");
                            Debug.WriteLine("\t\t\tLength: " + bank_data.Length);
                            Debug.WriteLine("\t\t\tOffset: " + bank_data.Offset);
                        }
                        for (j = 0; j < streams_audio_file.NumBeats; j++)
                        {
                            GTAAudioBeatData beat_data = streams_audio_file.GetBeatData((uint)j);
                            Debug.WriteLine("\t\t\t" + streams_audio_file.Name + " beat " + j + ":");
                            Debug.WriteLine("\t\t\t\tControl: " + beat_data.Control);
                            Debug.WriteLine("\t\t\t\tTiming: " + beat_data.Timing);
                        }
                    }
                }
            }
        }
    }
}
