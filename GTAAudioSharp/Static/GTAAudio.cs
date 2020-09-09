using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio class
    /// </summary>
    public static class GTAAudio
    {
        /// <summary>
        /// Maximal sound entry count
        /// </summary>
        private static readonly ushort maximalSoundEntryCount = 400;

        /// <summary>
        /// Maximal beat count
        /// </summary>
        private static readonly int maximalBeatCount = 1000;

        /// <summary>
        /// Streams encoding secret
        /// </summary>
        internal static readonly byte[] streamsEncodingSecret = new byte[] { 0xEA, 0x3A, 0xC4, 0xA1, 0x9A, 0xA8, 0x14, 0xF3, 0x48, 0xB0, 0xD7, 0x23, 0x9D, 0xE8, 0xFF, 0xF1 };

        /// <summary>
        /// Get null terminated byte string length
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Number of characters in byte string</returns>
        private static int GetNullTerminatedByteStringLength(byte[] bytes)
        {
            int ret = 0;
            if (bytes != null)
            {
                ret = bytes.Length;
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] == 0)
                    {
                        ret = i;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Open GTA audio files
        /// </summary>
        /// <param name="gtaAudioFilesDirectoryPath">GTA audio files directory</param>
        /// <param name="filesAccessMode">GTA audio files access mode</param>
        /// <returns>GTA audio files</returns>
        public static IGTAAudioFiles Open(string gtaAudioFilesDirectoryPath, EGTAAudioFilesAccessMode filesAccessMode)
        {
            if (gtaAudioFilesDirectoryPath == null)
            {
                throw new ArgumentNullException(nameof(gtaAudioFilesDirectoryPath));
            }
            IGTAAudioFiles ret = null;
            List<Stream> dispose_on_error_streams = new List<Stream>();
            try
            {
                string config_directory_path = Path.Combine(gtaAudioFilesDirectoryPath, "CONFIG");
                string sfx_directory_path = Path.Combine(gtaAudioFilesDirectoryPath, "SFX");
                string streams_directory_path = Path.Combine(gtaAudioFilesDirectoryPath, "streams");
                string audio_event_history_dot_txt_path = Path.Combine(config_directory_path, "AudioEventHistory.txt");
                string bank_lookup_dot_dat_path = Path.Combine(config_directory_path, "BankLkup.dat");
                string bank_slot_dot_dat_path = Path.Combine(config_directory_path, "BankSlot.dat");
                string event_volume_dot_dat_path = Path.Combine(config_directory_path, "EventVol.dat");
                string pak_files_dot_dat_path = Path.Combine(config_directory_path, "PakFiles.dat");
                string stream_paks_dot_dat_path = Path.Combine(config_directory_path, "StrmPaks.dat");
                string trak_lookup_dot_dat_path = Path.Combine(config_directory_path, "TrakLkup.dat");
                switch (filesAccessMode)
                {
                    case EGTAAudioFilesAccessMode.Read:
                        if (!Directory.Exists(gtaAudioFilesDirectoryPath))
                        {
                            throw new DirectoryNotFoundException($"Directory \"{ gtaAudioFilesDirectoryPath }\" doesn't exist.");
                        }
                        if (!Directory.Exists(config_directory_path))
                        {
                            throw new DirectoryNotFoundException($"Directory \"{ config_directory_path }\" doesn't exist.");
                        }
                        if (!Directory.Exists(sfx_directory_path))
                        {
                            throw new DirectoryNotFoundException($"Directory \"{ sfx_directory_path }\" doesn't exist.");
                        }
                        if (!Directory.Exists(streams_directory_path))
                        {
                            throw new DirectoryNotFoundException($"Directory \"{ streams_directory_path }\" doesn't exist.");
                        }
                        if (!File.Exists(audio_event_history_dot_txt_path))
                        {
                            throw new FileNotFoundException($"File doesn't exist.", audio_event_history_dot_txt_path);
                        }
                        if (!File.Exists(bank_lookup_dot_dat_path))
                        {
                            throw new FileNotFoundException($"File doesn't exist.", bank_lookup_dot_dat_path);
                        }
                        if (!File.Exists(bank_slot_dot_dat_path))
                        {
                            throw new FileNotFoundException($"File doesn't exist.", bank_slot_dot_dat_path);
                        }
                        if (!File.Exists(event_volume_dot_dat_path))
                        {
                            throw new FileNotFoundException($"File doesn't exist.", event_volume_dot_dat_path);
                        }
                        if (!File.Exists(pak_files_dot_dat_path))
                        {
                            throw new FileNotFoundException($"File doesn't exist.", pak_files_dot_dat_path);
                        }
                        if (!File.Exists(stream_paks_dot_dat_path))
                        {
                            throw new FileNotFoundException($"File doesn't exist.", stream_paks_dot_dat_path);
                        }
                        if (!File.Exists(trak_lookup_dot_dat_path))
                        {
                            throw new FileNotFoundException($"File doesn't exist.", trak_lookup_dot_dat_path);
                        }
                        string[] sfx_file_names = Array.Empty<string>();
                        string[] streams_file_names = Array.Empty<string>();
                        IGTAAudioSFXFile[] sfx_audio_files = Array.Empty<IGTAAudioSFXFile>();
                        IGTAAudioStreamsFile[] streams_audio_files = Array.Empty<IGTAAudioStreamsFile>();
                        List<IGTAAudioBankInformation>[] sfx_audio_files_banks;
                        List<IGTAAudioBankInformation>[] streams_audio_files_banks;
                        Dictionary<string, uint> sfx_file_name_to_index_lookup = new Dictionary<string, uint>();
                        Dictionary<string, uint> streams_file_name_to_index_lookup = new Dictionary<string, uint>();
                        byte[] script_event_volume = Array.Empty<byte>();
                        Exception raised_exception = null;
                        using (FileStream pak_files_dat_file_stream = File.OpenRead(pak_files_dot_dat_path))
                        {
                            long pak_files_dat_file_stream_length = pak_files_dat_file_stream.Length;
                            if ((pak_files_dat_file_stream_length % 52L) != 0L)
                            {
                                throw new InvalidDataException($"File size in bytes of \"{ pak_files_dot_dat_path }\" is not a multiple of 52. File size in bytes: { pak_files_dat_file_stream_length }.");
                            }
                            byte[] data = new byte[52];
                            long count = pak_files_dat_file_stream_length / 52L;
                            sfx_file_names = new string[count];
                            sfx_audio_files = new IGTAAudioSFXFile[count];
                            for (int index = 0; index < sfx_file_names.Length; index++)
                            {
                                int read_bytes = pak_files_dat_file_stream.Read(data, 0, data.Length);
                                if (read_bytes != data.Length)
                                {
                                    throw new EndOfStreamException($"Couldn't read { data.Length } bytes from file \"{ pak_files_dot_dat_path }\". Read bytes: { read_bytes }.");
                                }
                                int len = GetNullTerminatedByteStringLength(data);
                                if (len > 0)
                                {
                                    string sfx_file = Encoding.UTF8.GetString(data, 0, len);
                                    sfx_file_names[index] = sfx_file;
                                    sfx_file = sfx_file.ToLower();
                                    if (sfx_file_name_to_index_lookup.ContainsKey(sfx_file))
                                    {
                                        throw new InvalidDataException($"Duplicate SFX file entry \"{ sfx_file }\" in file \"{ pak_files_dot_dat_path }\".");
                                    }
                                    sfx_file_name_to_index_lookup.Add(sfx_file, (uint)index);
                                }
                                else
                                {
                                    sfx_file_names[index] = string.Empty;
                                }
                            }
                        }
                        using (FileStream stream_paks_dat_file_stream = File.OpenRead(stream_paks_dot_dat_path))
                        {
                            long stream_paks_dat_file_stream_length = stream_paks_dat_file_stream.Length;
                            if ((stream_paks_dat_file_stream_length % 16L) != 0L)
                            {
                                throw new InvalidDataException($"File size in bytes of \"{ stream_paks_dot_dat_path }\" is not a multiple of 16. File size in bytes: { stream_paks_dat_file_stream_length }.");
                            }
                            byte[] data = new byte[16];
                            long count = stream_paks_dat_file_stream_length / 16L;
                            streams_file_names = new string[count];
                            streams_audio_files = new GTAAudioStreamsFile[count];
                            for (int i = 0; i < streams_file_names.Length; i++)
                            {
                                int read_bytes = stream_paks_dat_file_stream.Read(data, 0, data.Length);
                                if (read_bytes != data.Length)
                                {
                                    throw new EndOfStreamException($"Couldn't read { data.Length } bytes from file \"{ stream_paks_dot_dat_path }\". Read bytes: { read_bytes }.");
                                }
                                int len = GetNullTerminatedByteStringLength(data);
                                if (len > 0)
                                {
                                    string streams_file = Encoding.UTF8.GetString(data, 0, len);
                                    streams_file_names[i] = streams_file;
                                    streams_file = streams_file.ToLower();
                                    if (streams_file_name_to_index_lookup.ContainsKey(streams_file))
                                    {
                                        throw new InvalidDataException($"Duplicate streams file entry \"{ streams_file }\" in file \"{ stream_paks_dot_dat_path }\".");
                                    }
                                    streams_file_name_to_index_lookup.Add(streams_file, (uint)i);
                                }
                                else
                                {
                                    streams_file_names[i] = string.Empty;
                                }
                            }
                        }
                        sfx_audio_files_banks = new List<IGTAAudioBankInformation>[sfx_file_names.Length];
                        Parallel.For(0, sfx_audio_files_banks.Length, (index) => sfx_audio_files_banks[index] = new List<IGTAAudioBankInformation>());
                        using (FileStream bank_lookup_dat_file_stream = File.OpenRead(bank_lookup_dot_dat_path))
                        {
                            using (BinaryReader bank_lookup_dat_file_stream_reader = new BinaryReader(bank_lookup_dat_file_stream))
                            {
                                long bank_lookup_dat_file_stream_length = bank_lookup_dat_file_stream.Length;
                                if ((bank_lookup_dat_file_stream_length % 12L) != 0L)
                                {
                                    throw new InvalidDataException($"File size in bytes of \"{ bank_lookup_dat_file_stream }\" is not a multiple of 12. File size in bytes: { bank_lookup_dat_file_stream_length }.");
                                }
                                long count = bank_lookup_dat_file_stream_length / 12L;
                                for (long i = 0; i < count; i++)
                                {
                                    byte index = bank_lookup_dat_file_stream_reader.ReadByte();
                                    bank_lookup_dat_file_stream.Seek(3L, SeekOrigin.Current);
                                    uint offset = bank_lookup_dat_file_stream_reader.ReadUInt32();
                                    uint length = bank_lookup_dat_file_stream_reader.ReadUInt32();
                                    if (index >= sfx_audio_files_banks.Length)
                                    {
                                        throw new IndexOutOfRangeException($"Invalid bank index { index } in file \"{ bank_lookup_dot_dat_path }\".");
                                    }
                                    sfx_audio_files_banks[index].Add(new GTAAudioBankInformation(offset, length));
                                }
                            }
                        }
                        streams_audio_files_banks = new List<IGTAAudioBankInformation>[streams_file_names.Length];
                        Parallel.For(0, streams_audio_files_banks.Length, (index) => streams_audio_files_banks[index] = new List<IGTAAudioBankInformation>());
                        using (FileStream trak_lookup_dat_file_stream = File.OpenRead(trak_lookup_dot_dat_path))
                        {
                            using (BinaryReader trak_lookup_dat_file_stream_reader = new BinaryReader(trak_lookup_dat_file_stream))
                            {
                                long trak_lookup_dat_file_stream_length = trak_lookup_dat_file_stream.Length;
                                if ((trak_lookup_dat_file_stream_length % 12L) != 0L)
                                {
                                    throw new InvalidDataException($"File size in bytes of \"{ trak_lookup_dot_dat_path }\" is not a multiple of 12. File size in bytes: { trak_lookup_dat_file_stream_length }.");
                                }
                                long count = trak_lookup_dat_file_stream_length / 12L;
                                for (long i = 0; i < count; i++)
                                {
                                    byte index = trak_lookup_dat_file_stream_reader.ReadByte();
                                    trak_lookup_dat_file_stream.Seek(3L, SeekOrigin.Current);
                                    uint offset = trak_lookup_dat_file_stream_reader.ReadUInt32();
                                    uint length = trak_lookup_dat_file_stream_reader.ReadUInt32();
                                    if (index >= streams_audio_files_banks.Length)
                                    {
                                        throw new IndexOutOfRangeException($"Invalid streams index { index } in file \"{ trak_lookup_dot_dat_path }\".");
                                    }
                                    streams_audio_files_banks[index].Add(new GTAAudioBankInformation(offset, length));
                                }
                            }
                        }
                        Parallel.For(0, sfx_file_names.Length, (i) =>
                        {
                            try
                            {
                                string sfx_file_name = sfx_file_names[i];
                                if (sfx_file_name == null)
                                {
                                    throw new NullReferenceException($"\"{ nameof(sfx_file_name) }\" is null.");
                                }
                                string sfx_file_path = Path.Combine(sfx_directory_path, sfx_file_name);
                                if (!File.Exists(sfx_file_path))
                                {
                                    throw new FileNotFoundException("SFX file doesn't exist.", sfx_file_path);
                                }
                                FileStream sfx_file_stream = File.OpenRead(sfx_file_path);
                                if (sfx_file_stream == null)
                                {
                                    throw new NullReferenceException($"\"{ nameof(sfx_file_stream) }\" is null.");
                                }
                                lock (dispose_on_error_streams)
                                {
                                    dispose_on_error_streams.Add(sfx_file_stream);
                                }
                                using (BinaryReader sfx_file_stream_reader = new BinaryReader(sfx_file_stream, Encoding.UTF8, true))
                                {
                                    List<IGTAAudioBankInformation> sfx_audio_file_banks = sfx_audio_files_banks[i];
                                    for (int j = 0, k; j < sfx_audio_file_banks.Count; j++)
                                    {
                                        IGTAAudioBankInformation sfx_audio_file_bank = sfx_audio_file_banks[j];
                                        IGTAAudioAudioClipInformation[] sfx_audio_file_bank_audio_clips = Array.Empty<IGTAAudioAudioClipInformation>();
                                        HashSet<uint> offset_set = new HashSet<uint>();
                                        sfx_file_stream.Seek(sfx_audio_file_bank.Offset, SeekOrigin.Begin);
                                        ushort audio_clip_count = sfx_file_stream_reader.ReadUInt16();
                                        sfx_file_stream.Seek(sizeof(ushort), SeekOrigin.Current);
                                        if (audio_clip_count <= maximalSoundEntryCount)
                                        {
                                            sfx_audio_file_bank_audio_clips = new IGTAAudioAudioClipInformation[audio_clip_count];
                                            for (k = 0; k < audio_clip_count; k++)
                                            {
                                                uint sound_buffer_offset = sfx_file_stream_reader.ReadUInt32();
                                                if (offset_set.Contains(sound_buffer_offset))
                                                {
                                                    throw new InvalidDataException($"Duplicate sound buffer offset { sound_buffer_offset } found in SFX file \"{ sfx_file_path }\".");
                                                }
                                                uint loop_offset = sfx_file_stream_reader.ReadUInt32();
                                                ushort sample_rate = sfx_file_stream_reader.ReadUInt16();
                                                ushort sound_headroom = sfx_file_stream_reader.ReadUInt16();
                                                sfx_audio_file_bank_audio_clips[k] = new GTAAudioAudioClipInformation(sound_buffer_offset, loop_offset, sample_rate, sound_headroom, 0U);
                                                offset_set.Add(sound_buffer_offset);
                                            }
                                        }
                                        List<uint> offsets = new List<uint>(offset_set);
                                        offset_set.Clear();
                                        offsets.Sort();
                                        for (k = 0; k < sfx_audio_file_bank_audio_clips.Length; k++)
                                        {
                                            IGTAAudioAudioClipInformation sfx_audio_file_bank_audio_clip = sfx_audio_file_bank_audio_clips[k];
                                            int offset_index = offsets.IndexOf(sfx_audio_file_bank_audio_clip.SoundBufferOffset);
                                            sfx_audio_file_bank_audio_clips[k] = new GTAAudioAudioClipInformation(sfx_audio_file_bank_audio_clip.SoundBufferOffset, sfx_audio_file_bank_audio_clip.LoopOffset, sfx_audio_file_bank_audio_clip.SampleRate, sfx_audio_file_bank_audio_clip.SoundHeadroom, ((offset_index < (offsets.Count - 1)) ? offsets[offset_index + 1] : sfx_audio_file_bank.Length) - sfx_audio_file_bank_audio_clip.SoundBufferOffset);
                                        }
                                        offsets.Clear();
                                        sfx_audio_files_banks[i][j] = new GTAAudioBankInformation(sfx_audio_file_bank.Offset, sfx_audio_file_bank.Length, sfx_audio_file_bank_audio_clips);
                                    }
                                }
                                sfx_audio_files[i] = new GTAAudioSFXFile(sfx_file_name, sfx_file_stream, sfx_audio_files_banks[i]);
                            }
                            catch (Exception e)
                            {
                                raised_exception = e;
                                Console.Error.WriteLine(e);
                            }
                        });
                        if (raised_exception != null)
                        {
                            throw raised_exception;
                        }
                        Parallel.For(0, streams_file_names.Length, (i) =>
                        {
                            try
                            {
                                string streams_file_name = streams_file_names[i];
                                if (streams_file_name == null)
                                {
                                    throw new NullReferenceException($"\"{ nameof(streams_file_name) }\" is null.");
                                }
                                if (streams_file_name.Trim().Length > 0)
                                {
                                    string streams_file_path = Path.Combine(streams_directory_path, streams_file_name);
                                    if (!File.Exists(streams_file_path))
                                    {
                                        throw new FileNotFoundException("Streams file doesn't exist.", streams_file_path);
                                    }
                                    FileStream streams_file_stream = File.OpenRead(streams_file_path);
                                    if (streams_file_stream == null)
                                    {
                                        throw new NullReferenceException($"\"{ nameof(streams_file_stream) }\" is null.");
                                    }
                                    lock (dispose_on_error_streams)
                                    {
                                        dispose_on_error_streams.Add(streams_file_stream);
                                    }
                                    List<IGTAAudioBeatInformation> streams_file_beats = new List<IGTAAudioBeatInformation>();
                                    using (DecodingBinaryReader streams_file_stream_decoding_binary_reader = new DecodingBinaryReader(streams_file_stream, Encoding.UTF8, true))
                                    {
                                        streams_file_stream.Seek(8064L, SeekOrigin.Begin);
                                        if (streams_file_stream_decoding_binary_reader.ReadDecodeUInt32() != 0xCDCD0001)
                                        {
                                            throw new InvalidDataException();
                                        }
                                        streams_file_stream.Seek(0L, SeekOrigin.Begin);
                                        for (int j = 0; j < maximalBeatCount; j++)
                                        {
                                            uint timing = streams_file_stream_decoding_binary_reader.ReadDecodeUInt32();
                                            uint control = streams_file_stream_decoding_binary_reader.ReadDecodeUInt32();
                                            if (timing == uint.MaxValue)
                                            {
                                                break;
                                            }
                                            streams_file_beats.Add(new GTAAudioBeatInformation(timing, control));
                                        }
                                    }
                                    streams_audio_files[i] = new GTAAudioStreamsFile(streams_file_name, streams_file_stream, streams_audio_files_banks[i], streams_file_beats);
                                }
                                else
                                {
                                    streams_audio_files[i] = new GTAAudioStreamsFile(string.Empty, null, streams_audio_files_banks[i], Array.Empty<IGTAAudioBeatInformation>());
                                }
                            }
                            catch (Exception e)
                            {
                                raised_exception = e;
                                Console.Error.WriteLine(e);
                            }
                        });
                        if (raised_exception != null)
                        {
                            throw raised_exception;
                        }
                        using (FileStream event_volume_dot_dat_file_stream = File.OpenRead(event_volume_dot_dat_path))
                        {
                            script_event_volume = new byte[event_volume_dot_dat_file_stream.Length];
                            int read_bytes = event_volume_dot_dat_file_stream.Read(script_event_volume, 0, script_event_volume.Length);
                            if (read_bytes != script_event_volume.Length)
                            {
                                throw new EndOfStreamException($"Couldn't read { script_event_volume.Length } bytes from file \"{ event_volume_dot_dat_path }\". Read bytes: { read_bytes }.");
                            }
                        }
                        ret = new GTAAudioFiles(sfx_audio_files, streams_audio_files, sfx_file_name_to_index_lookup, streams_file_name_to_index_lookup, script_event_volume);
                        break;
                    case EGTAAudioFilesAccessMode.Create:
                        // TODO: Implement creating GTA audio files
                        throw new NotSupportedException("Creating GTA audio files isn't supported yet.");
                    case EGTAAudioFilesAccessMode.Update:
                        // TODO: Implement updating GTA audio files
                        throw new NotSupportedException("Updating GTA audio files isn't supported yet.");
                }
            }
            catch (Exception e)
            {
                if (ret == null)
                {
                    Parallel.ForEach(dispose_on_error_streams, (dispose_stream) => dispose_stream.Dispose());
                }
                else
                {
                    ret.Dispose();
                }
                dispose_on_error_streams.Clear();
                throw e;
            }
            dispose_on_error_streams.Clear();
            return ret;
        }

        /// <summary>
        /// Open GTA audio files in read only mode
        /// </summary>
        /// <param name="directory">Audio files directory</param>
        /// <returns>GTA audio files</returns>
        public static IGTAAudioFiles OpenRead(string directory) => Open(directory, EGTAAudioFilesAccessMode.Read);
    }
}
