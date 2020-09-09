using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// GTA audio sharp namespace
/// </summary>
namespace GTAAudioSharp
{
    /// <summary>
    /// Decoding binary reader class
    /// </summary>
    public class DecodingBinaryReader : BinaryReader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">Input</param>
        public DecodingBinaryReader(Stream input) : base(input)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">Input</param>
        /// <param name="encoding">Encoding</param>
        public DecodingBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
        {
            // ...
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">Input</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="leaveOpen">Leave stream open</param>
        public DecodingBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
            // ...
        }

        /// <summary>
        /// Read and decode bytes
        /// </summary>
        /// <param name="count">Count</param>
        /// <returns>Decoded bytes</returns>
        public byte[] ReadDecodeBytes(int count)
        {
            byte[] ret = ReadBytes(count);
            if (ret != null)
            {
                long base_position = BaseStream.Position - ret.Length;
                Parallel.For(0, ret.Length, (index) => ret[index] ^= GTAAudio.streamsEncodingSecret[(base_position + index) % GTAAudio.streamsEncodingSecret.LongLength]);
            }
            return ret ?? Array.Empty<byte>();
        }

        /// <summary>
        /// Read and decode signed 8-bit integer
        /// </summary>
        /// <returns>Decoded signed 8-bit integer</returns>
        public sbyte ReadDecodeInt8() => (sbyte)ReadDecodeBytes(sizeof(sbyte))[0];

        /// <summary>
        /// Read and decode unsigned 8-bit integer
        /// </summary>
        /// <returns>Decoded unsigned 8-bit integer</returns>
        public byte ReadDecodeUInt8() => ReadDecodeBytes(sizeof(byte))[0];

        /// <summary>
        /// Read and decode signed 16-bit integer
        /// </summary>
        /// <returns>Decoded signed 16-bit integer</returns>
        public short ReadDecodeInt16() => BitConverter.ToInt16(ReadDecodeBytes(sizeof(short)), 0);

        /// <summary>
        /// Read and decode unsigned 16-bit integer
        /// </summary>
        /// <returns>Decoded unsigned 16-bit integer</returns>
        public ushort ReadDecodeUInt16() => BitConverter.ToUInt16(ReadDecodeBytes(sizeof(ushort)), 0);

        /// <summary>
        /// Read and decode signed 32-bit integer
        /// </summary>
        /// <returns>Decoded signed 32-bit integer</returns>
        public int ReadDecodeInt32() => BitConverter.ToInt32(ReadDecodeBytes(sizeof(int)), 0);

        /// <summary>
        /// Read and decode unsigned 32-bit integer
        /// </summary>
        /// <returns>Decoded unsigned 32-bit integer</returns>
        public uint ReadDecodeUInt32() => BitConverter.ToUInt32(ReadDecodeBytes(sizeof(uint)), 0);

        /// <summary>
        /// Read and decode signed 64-bit integer
        /// </summary>
        /// <returns>Decoded signed 64-bit integer</returns>
        public long ReadDecodeInt64() => BitConverter.ToInt64(ReadDecodeBytes(sizeof(long)), 0);

        /// <summary>
        /// Read and decode unsigned 64-bit integer
        /// </summary>
        /// <returns>Decoded unsigned 64-bit integer</returns>
        public ulong ReadDecodeUInt64() => BitConverter.ToUInt64(ReadDecodeBytes(sizeof(ulong)), 0);

        /// <summary>
        /// Read and decode signed 64-bit integer
        /// </summary>
        /// <returns>Decoded signed 64-bit integer</returns>
        public float ReadDecodeSingle() => BitConverter.ToSingle(ReadDecodeBytes(sizeof(float)), 0);

        /// <summary>
        /// Read and decode unsigned 64-bit integer
        /// </summary>
        /// <returns>Decoded unsigned 64-bit integer</returns>
        public double ReadDecodeDouble() => BitConverter.ToDouble(ReadDecodeBytes(sizeof(double)), 0);
    }
}
