using System;
using System.Collections.Generic;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio files interface
    /// </summary>
    public interface IGTAAudioFiles : IDisposable
    {
        /// <summary>
        /// SFX audio files
        /// </summary>
        IReadOnlyList<IGTAAudioSFXFile> SFXAudioFiles { get; }

        /// <summary>
        /// Streams audio files
        /// </summary>
        IReadOnlyList<IGTAAudioStreamsFile> StreamsAudioFiles { get; }

        /// <summary>
        /// SFX file name to index lookup
        /// </summary>
        IReadOnlyDictionary<string, uint> SFXFileNameToIndexLookup { get; }

        /// <summary>
        /// Streams file name to index lookup
        /// </summary>
        IReadOnlyDictionary<string, uint> StreamsFileNameToIndexLookup { get; }

        /// <summary>
        /// Script event volume
        /// </summary>
        IReadOnlyList<byte> ScriptEventVolume { get; }

        /// <summary>
        /// Open SFX audio stream by index
        /// </summary>
        /// <param name="sfxIndex">SFX index</param>
        /// <param name="bankIndex">Bank Index</param>
        /// <param name="audioClipIndex">Audio clip index</param>
        /// <returns>GTA audio SFX stream</returns>
        GTAAudioSFXStream OpenSFXAudioStreamByIndex(uint sfxIndex, uint bankIndex, uint audioClipIndex);

        /// <summary>
        /// Open SFX audio stream by name
        /// </summary>
        /// <param name="sfxName">SFX name</param>
        /// <param name="bankIndex">Bank index</param>
        /// <param name="audioClipIndex">Audio clip index</param>
        /// <returns>GTA audio SFX stream</returns>
        GTAAudioSFXStream OpenSFXAudioStreamByName(string sfxName, uint bankIndex, uint audioClipIndex);

        /// <summary>
        /// Open SFX audio stream by script event index
        /// </summary>
        /// <param name="eventIndex"></param>
        /// <returns>GTA audio SFX stream</returns>
        GTAAudioSFXStream OpenSFXAudioStreamByScriptEventIndex(uint eventIndex);

        /// <summary>
        /// Open streams audio stream by index
        /// </summary>
        /// <param name="streamsIndex">Streams index</param>
        /// <param name="bankIndex">Bank Index</param>
        /// <returns>GTA audio streams stream</returns>
        GTAAudioStreamsStream OpenStreamsAudioStreamByIndex(uint streamsIndex, uint bankIndex);

        /// <summary>
        /// Open streams audio stream by name
        /// </summary>
        /// <param name="streamsName">Streams name</param>
        /// <param name="bankIndex">Bank index</param>
        /// <returns>GTA audio streams stream</returns>
        GTAAudioStreamsStream OpenStreamsAudioStreamByName(string streamsName, uint bankIndex);

        /// <summary>
        /// Get beats data by streams index
        /// </summary>
        /// <param name="streamsIndex">Streams index</param>
        /// <returns>Beats data</returns>
        IReadOnlyList<IGTAAudioBeatInformation> GetBeatsByStreamsIndex(uint streamsIndex);

        /// <summary>
        /// Get beats data by streams name
        /// </summary>
        /// <param name="streamsName">Streams name</param>
        /// <returns>Beats data</returns>
        IReadOnlyList<IGTAAudioBeatInformation> GetBeatsDataByStreamsName(string streamsName);

        /// <summary>
        /// Get SFX file index by name
        /// </summary>
        /// <param name="sfxName">SFX name</param>
        /// <returns>SFX index</returns>
        uint GetSFXFileIndexByName(string sfxName);

        /// <summary>
        /// Get streams file index by name
        /// </summary>
        /// <param name="streamsName">Streams name</param>
        /// <returns>SFX index</returns>
        uint GetStreamsFileIndexByName(string streamsName);
    }
}
