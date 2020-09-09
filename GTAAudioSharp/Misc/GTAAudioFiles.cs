using System;
using System.Collections.Generic;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio files class
    /// </summary>
    internal class GTAAudioFiles : IGTAAudioFiles
    {
        /// <summary>
        /// SFX file name to index lookup
        /// </summary>
        private readonly Dictionary<string, uint> sfxFileNameToIndexLookup;

        /// <summary>
        /// Streams file name to index lookup
        /// </summary>
        private readonly Dictionary<string, uint> streamsFileNameToIndexLookup;

        /// <summary>
        /// SFX audio files
        /// </summary>
        public IReadOnlyList<IGTAAudioSFXFile> SFXAudioFiles { get; private set; }

        /// <summary>
        /// Streams audio files
        /// </summary>
        public IReadOnlyList<IGTAAudioStreamsFile> StreamsAudioFiles { get; private set; }

        /// <summary>
        /// SFX file name to index lookup
        /// </summary>
        public IReadOnlyDictionary<string, uint> SFXFileNameToIndexLookup => sfxFileNameToIndexLookup;

        /// <summary>
        /// Streams file name to index lookup
        /// </summary>
        public IReadOnlyDictionary<string, uint> StreamsFileNameToIndexLookup => streamsFileNameToIndexLookup;

        /// <summary>
        /// Script event volume
        /// </summary>
        public IReadOnlyList<byte> ScriptEventVolume { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sfxAudioFiles">SFX audio banks files</param>
        /// <param name="streamsAudioFiles">Streams audio banks files</param>
        /// <param name="sfxFileNameToIndexLookup">SFX file name to index lookup</param>
        /// <param name="streamsFileNameToIndexLookup">Streams file name to index lookup</param>
        /// <param name="scriptEventVolume">Script event volume</param>
        public GTAAudioFiles(IReadOnlyList<IGTAAudioSFXFile> sfxAudioFiles, IReadOnlyList<IGTAAudioStreamsFile> streamsAudioFiles, Dictionary<string, uint> sfxFileNameToIndexLookup, Dictionary<string, uint> streamsFileNameToIndexLookup, IReadOnlyList<byte> scriptEventVolume)
        {
            SFXAudioFiles = sfxAudioFiles ?? throw new ArgumentNullException(nameof(sfxAudioFiles));
            StreamsAudioFiles = streamsAudioFiles ?? throw new ArgumentNullException(nameof(streamsAudioFiles));
            this.sfxFileNameToIndexLookup = sfxFileNameToIndexLookup ?? throw new ArgumentNullException(nameof(sfxFileNameToIndexLookup));
            this.streamsFileNameToIndexLookup = streamsFileNameToIndexLookup ?? throw new ArgumentNullException(nameof(streamsFileNameToIndexLookup));
            ScriptEventVolume = scriptEventVolume ?? throw new ArgumentNullException(nameof(scriptEventVolume));
        }

        /// <summary>
        /// Open SFX audio stream by index
        /// </summary>
        /// <param name="sfxIndex">SFX index</param>
        /// <param name="bankIndex">Bank Index</param>
        /// <param name="audioClipIndex">Audio clip index</param>
        /// <returns>GTA audio SFX stream</returns>
        public GTAAudioSFXStream OpenSFXAudioStreamByIndex(uint sfxIndex, uint bankIndex, uint audioClipIndex)
        {
            if (sfxIndex >= SFXAudioFiles.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(sfxIndex), sfxIndex, "Invalid SFX index.");
            }
            GTAAudioSFXStream ret = SFXAudioFiles[(int)sfxIndex].Open(bankIndex, audioClipIndex);
            if (ret == null)
            {
                throw new NullReferenceException($"{ nameof(ret) } is null.");
            }
            return ret;
        }

        /// <summary>
        /// Open SFX audio stream by name
        /// </summary>
        /// <param name="sfxName">SFX name</param>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioClipIndex">Audio clip index</param>
        /// <returns>GTA audio SFX stream</returns>
        public GTAAudioSFXStream OpenSFXAudioStreamByName(string sfxName, uint bankIndex, uint audioClipIndex)
        {
            if (sfxName == null)
            {
                throw new ArgumentNullException(nameof(sfxName));
            }
            string key = sfxName.Trim().ToLower();
            if (!sfxFileNameToIndexLookup.ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(sfxName), sfxName, "SFX name doesn't exist.");
            }
            return OpenSFXAudioStreamByIndex(sfxFileNameToIndexLookup[key], bankIndex, audioClipIndex);
        }

        /// <summary>
        /// Open SFX audio stream by script event index
        /// </summary>
        /// <param name="eventIndex"></param>
        /// <returns>GTA audio SFX stream</returns>
        public GTAAudioSFXStream OpenSFXAudioStreamByScriptEventIndex(uint eventIndex)
        {
            if (eventIndex < 2000U)
            {
                throw new ArgumentOutOfRangeException(nameof(eventIndex), eventIndex, "Invalid event index. Event index must start at atleast index 2000.");
            }
            return OpenSFXAudioStreamByName("SCRIPT", (eventIndex - 2000U) / 200U, (eventIndex - 2000) % 200);
        }

        /// <summary>
        /// Open streams audio stream by index
        /// </summary>
        /// <param name="streamsIndex">Streams index</param>
        /// <param name="bankIndex">Bank Index</param>
        /// <returns>GTA audio streams stream</returns>
        public GTAAudioStreamsStream OpenStreamsAudioStreamByIndex(uint streamsIndex, uint bankIndex)
        {
            if (streamsIndex >= StreamsAudioFiles.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(streamsIndex), streamsIndex, "Invalid streams index.");
            }
            return StreamsAudioFiles[(int)streamsIndex]?.Open(bankIndex);
        }

        /// <summary>
        /// Open streams audio stream by name
        /// </summary>
        /// <param name="streamsName">Streams name</param>
        /// <param name="bankIndex">Bank index</param>
        /// <returns>GTA audio streams stream</returns>
        public GTAAudioStreamsStream OpenStreamsAudioStreamByName(string streamsName, uint bankIndex)
        {
            if (streamsName == null)
            {
                throw new ArgumentNullException(nameof(streamsName));
            }
            string key = streamsName.Trim().ToLower();
            if (!streamsFileNameToIndexLookup.ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(streamsName), streamsName, "Streams name doesn't exist.");
            }
            return OpenStreamsAudioStreamByIndex(streamsFileNameToIndexLookup[key], bankIndex);
        }

        /// <summary>
        /// Get beats by streams index
        /// </summary>
        /// <param name="streamsIndex">Streams index</param>
        /// <returns>Beats data</returns>
        public IReadOnlyList<IGTAAudioBeatInformation> GetBeatsByStreamsIndex(uint streamsIndex)
        {
            if (streamsIndex >= StreamsAudioFiles.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(streamsIndex), streamsIndex, "Invalid streams index.");
            }
            return StreamsAudioFiles[(int)streamsIndex].Beats;
        }

        /// <summary>
        /// Get beats by streams name
        /// </summary>
        /// <param name="streamsName">Streams name</param>
        /// <returns>Beats data</returns>
        public IReadOnlyList<IGTAAudioBeatInformation> GetBeatsDataByStreamsName(string streamsName)
        {
            if (streamsName == null)
            {
                throw new ArgumentNullException(nameof(streamsName));
            }
            string key = streamsName.Trim().ToLower();
            if (!streamsFileNameToIndexLookup.ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(streamsName), streamsName, "Invalid streams name.");
            }
            return GetBeatsByStreamsIndex(streamsFileNameToIndexLookup[streamsName]);
        }

        /// <summary>
        /// Get SFX file index by name
        /// </summary>
        /// <param name="sfxName">SFX name</param>
        /// <returns>SFX index</returns>
        public uint GetSFXFileIndexByName(string sfxName)
        {
            if (sfxName == null)
            {
                throw new ArgumentNullException(nameof(sfxName));
            }
            string key = sfxName.Trim().ToLower();
            if (!sfxFileNameToIndexLookup.ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(sfxName), sfxName, "Invalid SFX name.");
            }
            return sfxFileNameToIndexLookup[key];
        }

        /// <summary>
        /// Get streams file index by name
        /// </summary>
        /// <param name="streamsName">Streams name</param>
        /// <returns>SFX index if successful, otherwise "-1"</returns>
        public uint GetStreamsFileIndexByName(string streamsName)
        {
            if (streamsName == null)
            {
                throw new ArgumentNullException(nameof(streamsName));
            }
            string key = streamsName.Trim().ToLower();
            if (!streamsFileNameToIndexLookup.ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(streamsName), streamsName, "Invalid streams name.");
            }
            return streamsFileNameToIndexLookup[key];
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            foreach (IGTAAudioSFXFile sfx_file in SFXAudioFiles)
            {
                sfx_file?.Dispose();
            }
            foreach (IGTAAudioStreamsFile streams_file in StreamsAudioFiles)
            {
                streams_file?.Dispose();
            }
            StreamsAudioFiles = Array.Empty<IGTAAudioStreamsFile>();
            SFXAudioFiles = Array.Empty<IGTAAudioSFXFile>();
            sfxFileNameToIndexLookup.Clear();
            streamsFileNameToIndexLookup.Clear();
        }
    }
}
