using System.IO.Compression;
using System.Text;
using System;
using System.IO;

namespace Sdk.CloudDfe
{
    public static class Util
    {
        public static string Encode(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }

        public static string Decode(string data)
        {
            var decodedBytes = Convert.FromBase64String(data);
            try
            {
                using (var compressedStream = new MemoryStream(decodedBytes))
                using (var decompressedStream = new MemoryStream())
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    gzipStream.CopyTo(decompressedStream);
                    var decompressedBytes = decompressedStream.ToArray();
                    return Encoding.UTF8.GetString(decompressedBytes);
                }
            }
            catch (InvalidDataException)
            {
                return Encoding.UTF8.GetString(decodedBytes);
            }
        }

        public static string ReadFile(string file)
        {
            return File.ReadAllText(file);
        }
    }
}