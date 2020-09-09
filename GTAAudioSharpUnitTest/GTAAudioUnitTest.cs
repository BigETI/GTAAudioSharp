using GTAAudioSharp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    public class GTAAudioUnitTest
    {
        /// <summary>
        /// Configuration file path
        /// </summary>
        private static readonly string configurationFilePath = Path.Combine(Environment.CurrentDirectory, "config.json");

        /// <summary>
        /// Config serializer
        /// </summary>
        private static readonly DataContractJsonSerializer configurationJSONSerializer = new DataContractJsonSerializer(typeof(UnitTestConfigurationDataContract));

        /// <summary>
        /// Valid SFX file names
        /// </summary>
        private static readonly string[] validSFXFileNames = new string[]
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
        /// Valid streams file names
        /// </summary>
        private static readonly string[] validStreamsFileNames = new string[]
        {
            "AA",
            "ADVERTS",
            string.Empty,
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
        private UnitTestConfigurationDataContract configuration;

        /// <summary>
        /// Configuration
        /// </summary>
        public UnitTestConfigurationDataContract Configuration
        {
            get
            {
                if (configuration == null)
                {
                    if (File.Exists(configurationFilePath))
                    {
                        using FileStream configuration_file_stream = File.Open(configurationFilePath, FileMode.Open, FileAccess.Read);
                        configuration = configurationJSONSerializer.ReadObject(configuration_file_stream) as UnitTestConfigurationDataContract;
                    }
                    else
                    {
                        configuration = new UnitTestConfigurationDataContract(Path.Combine(Environment.CurrentDirectory, "audio"));
                        using FileStream configuration_file_stream = File.Open(configurationFilePath, FileMode.Create);
                        configurationJSONSerializer.WriteObject(configuration_file_stream, configuration);
                    }
                }
                return configuration;
            }
        }

        /// <summary>
        /// Read file test
        /// </summary>
        [Test]
        public void ReadFileTest()
        {
            using IGTAAudioFiles gta_audio_files = GTAAudio.OpenRead(Configuration.AudioFilesDirectoryPath);
            Assert.IsNotNull(gta_audio_files, $"Files can't be opened. Please configure \"{ configurationFilePath }\".");
            IReadOnlyList<IGTAAudioSFXFile> sfx_audio_files = gta_audio_files.SFXAudioFiles;
            Assert.AreEqual(validSFXFileNames.Length, sfx_audio_files.Count, $"Missing SFX file entries. { gta_audio_files.SFXAudioFiles.Count } files found, not { validSFXFileNames.Length }.");
            for (int i = 0, j, k; i < sfx_audio_files.Count; i++)
            {
                IGTAAudioSFXFile sfx_audio_file = sfx_audio_files[i];
                if (sfx_audio_file != null)
                {
                    IReadOnlyList<IGTAAudioBankInformation> sfx_audio_banks = sfx_audio_file.Banks;
                    Assert.AreEqual(validSFXFileNames[i], sfx_audio_file.Name, $"SFX file \"{ sfx_audio_file.Name }\" is not \"{ validSFXFileNames[i] }\" at index { i }.");
                    TestContext.WriteLine($"{ sfx_audio_file.Name }:");
                    Debug.WriteLine($"\tNumber of banks: { sfx_audio_banks.Count }");
                    for (j = 0; j < sfx_audio_banks.Count; j++)
                    {
                        IGTAAudioBankInformation sfx_audio_bank = sfx_audio_banks[j];
                        IReadOnlyList<IGTAAudioAudioClipInformation> sfx_audio_bank_audio_clips = sfx_audio_bank.AudioClips;
                        TestContext.WriteLine($"\t\t{ sfx_audio_file.Name } bank { j }:");
                        TestContext.WriteLine($"\t\t\tLength: { sfx_audio_bank.Length }");
                        TestContext.WriteLine($"\t\t\tOffset: { sfx_audio_bank.Offset }");
                        TestContext.WriteLine($"\t\t\tNumber of audio clips: { sfx_audio_bank_audio_clips.Count }");
                        for (k = 0; k < sfx_audio_bank_audio_clips.Count; k++)
                        {
                            IGTAAudioAudioClipInformation sfx_audio_bank_audio_clip = sfx_audio_bank_audio_clips[k];
                            TestContext.WriteLine($"\t\t\t{ sfx_audio_file.Name } bank { j } audio { k }:");
                            TestContext.WriteLine($"\t\t\t\tSample rate: { sfx_audio_bank_audio_clip.SampleRate }");
                            TestContext.WriteLine($"\t\t\t\tSound buffer offset: { sfx_audio_bank_audio_clip.SoundBufferOffset }");
                            TestContext.WriteLine($"\t\t\t\tLoop offset: { sfx_audio_bank_audio_clip.LoopOffset }");
                            TestContext.WriteLine($"\t\t\t\tSound headroom: { sfx_audio_bank_audio_clip.SoundHeadroom }");
                            TestContext.WriteLine($"\t\t\t\tLength: { sfx_audio_bank_audio_clip.Length }");
                        }
                    }
                }
            }
            IReadOnlyList<IGTAAudioStreamsFile> streams_audio_files = gta_audio_files.StreamsAudioFiles;
            Assert.AreEqual(validStreamsFileNames.Length, streams_audio_files.Count, $"Missing streams file entries. { gta_audio_files.SFXAudioFiles.Count } files found, not { validSFXFileNames.Length }.");
            for (int i = 0, j; i < streams_audio_files.Count; i++)
            {
                IGTAAudioStreamsFile streams_audio_file = streams_audio_files[i];
                if (streams_audio_file != null)
                {
                    IReadOnlyList<IGTAAudioBankInformation> streams_audio_file_banks = streams_audio_file.Banks;
                    IReadOnlyList<IGTAAudioBeatInformation> streams_audio_file_beats = streams_audio_file.Beats;
                    Assert.AreEqual(validStreamsFileNames[i], streams_audio_file.Name, $"Streams file \"{ streams_audio_file.Name }\" is not \"{ validStreamsFileNames[i] }\" at index { i }.");
                    TestContext.WriteLine($"{ streams_audio_file.Name }:");
                    TestContext.WriteLine($"\tNumber of banks: { streams_audio_file_banks.Count }");
                    for (j = 0; j < streams_audio_file_banks.Count; j++)
                    {
                        IGTAAudioBankInformation streams_audio_file_bank = streams_audio_file_banks[j];
                        TestContext.WriteLine($"\t\t{ streams_audio_file.Name } bank { j }:");
                        TestContext.WriteLine($"\t\t\tLength: { streams_audio_file_bank.Length }");
                        TestContext.WriteLine($"\t\t\tOffset: { streams_audio_file_bank.Offset }");
                    }
                    for (j = 0; j < streams_audio_file_beats.Count; j++)
                    {
                        IGTAAudioBeatInformation beat_data = streams_audio_file_beats[j];
                        TestContext.WriteLine($"\t\t\t{ streams_audio_file.Name } beat { j }:");
                        TestContext.WriteLine($"\t\t\t\tControl: { beat_data.Control }");
                        TestContext.WriteLine($"\t\t\t\tTiming: { beat_data.Timing }");
                    }
                }
            }
        }
    }
}
