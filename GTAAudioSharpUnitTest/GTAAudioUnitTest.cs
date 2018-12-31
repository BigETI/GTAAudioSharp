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
                for (int i = 0, j; i < sfx_audio_files.Length; i++)
                {
                    GTAAudioSFXFile sfx_audio_file = sfx_audio_files[i];
                    if (sfx_audio_file != null)
                    {
                        Assert.IsTrue(sfx_audio_file.Name == validSFXFiles[i], "SFX file \"" + sfx_audio_file.Name + "\" is not \"" + validSFXFiles[i] + "\" at index " + i);
                        /*Debug.WriteLine(sfx_audio_file.Name);
                        Debug.WriteLine("\tNumber of audios: " + sfx_audio_file.NumAudios);
                        Debug.WriteLine("\tNumber of banks: " + sfx_audio_file.NumBanks);
                        for (j = 0; j < sfx_audio_file.AudioData.Length; j++)
                        {
                            GTAAudioSFXDataInfo audio_data = sfx_audio_file.AudioData[j];
                            Debug.WriteLine("\t\t" + sfx_audio_file.Name + " audio " + j + ":");
                            Debug.WriteLine("\t\t\tSample rate: " + audio_data.SampleRate);
                            Debug.WriteLine("\t\t\tSound buffer offset: " + audio_data.SoundBufferOffset);
                            Debug.WriteLine("\t\t\tLoop offset: " + audio_data.LoopOffset);
                            Debug.WriteLine("\t\t\tSound headroom: " + audio_data.SoundHeadroom);
                        }
                        for (j = 0; j < sfx_audio_file.LookupData.Length; j++)
                        {
                            GTAAudioLookupData lookup_data = sfx_audio_file.LookupData[j];
                            Debug.WriteLine("\t\t" + sfx_audio_file.Name + " bank " + j + ":");
                            Debug.WriteLine("\t\t\tLength: " + lookup_data.Length);
                            Debug.WriteLine("\t\t\tOffset: " + lookup_data.Offset);
                        }*/
                    }
                }
                GTAAudioStreamsFile[] streams_audio_files = files.StreamsAudioFiles;
                Assert.IsTrue(streams_audio_files.Length == validStreamsFiles.Length, "Missing streams file entries. " + files.SFXAudioFiles.Length + " files found, not " + validSFXFiles.Length);
                for (int i = 0; i < streams_audio_files.Length; i++)
                {
                    GTAAudioStreamsFile streams_audio_file = streams_audio_files[i];
                    if (streams_audio_file != null)
                    {
                        Assert.IsTrue(streams_audio_file.Name == validStreamsFiles[i], "Streams file \"" + streams_audio_file.Name + "\" is not \"" + validStreamsFiles[i] + "\" at index " + i);
                    }
                }
                /*Debug.WriteLine("SFX bank slots: " + files.NumSFXBankSlots);
                GTAAudioBankSlotData[] sfx_bank_slots = files.SFXBankSlots;
                for (int i = 0; i < sfx_bank_slots.Length; i++)
                {
                    GTAAudioBankSlotData sfx_bank_slot = sfx_bank_slots[i];
                    Debug.WriteLine("\tBank slot " + i + " size: " + sfx_bank_slot.BufferSize);
                }*/
            }
        }
    }
}
