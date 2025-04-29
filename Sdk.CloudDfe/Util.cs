using System.IO.Compression;
using System.Text;
using System;
using System.IO;

namespace Sdk.CloudDfe
{
    public static class Util
    {
        
        public static string ReadTextFile (string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public static byte[] ReadBinaryFile (string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public static string EncodeString (string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string EncodeBytes (byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static string DecodeString (string text)
        {
            var bytes = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] DecodeBytes (string text)
        {
            return Convert.FromBase64String(text);
        }

        public static byte[] DecompressBytes(byte[] compressedBytes)
        {
            using (var inputStream = new MemoryStream(compressedBytes))
            using (var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                gzipStream.CopyTo(outputStream);
                return outputStream.ToArray();
            }
        }

        public static string DecompressXml(byte[] compressedBytes)
        {
            using (var inputStream = new MemoryStream(compressedBytes))
            using (var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                gzipStream.CopyTo(outputStream);
                return Encoding.UTF8.GetString(outputStream.ToArray());
            }
        }

    }
}