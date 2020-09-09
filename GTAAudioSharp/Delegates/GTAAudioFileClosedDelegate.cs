/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// GTA audio file closed delegate
    /// </summary>
    /// <typeparam name="T">Open stream type</typeparam>
    /// <param name="file">GTA audio file</param>
    /// <param name="stream">Commitable memory stream</param>
    public delegate void GTAAudioFileClosedDelegate<T>(IGTAAudioFile<T> file, ACommitableMemoryStream<T> stream);
}
