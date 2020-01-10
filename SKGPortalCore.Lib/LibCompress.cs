using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SKGPortalCore.Lib
{
    /// <summary>
    /// 壓縮
    /// </summary>
    public static class LibCompress
    {
        #region Public
        /// <summary>
        /// 將傳入的字串以GZip演算法壓縮後，傳回Base64編碼字串
        /// </summary>
        /// <param name="rawString">要壓縮的字串</param>
        /// <returns>壓縮後的字串(Base64)</returns>
        public static string GZipCompressString(string rawString)
        {
            if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rawString.ToString());
                byte[] zippedData = Compress(rawData);
                return (string)(Convert.ToBase64String(zippedData));
            }

        }
        /// <summary>
        /// GZip壓縮
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] rawData)
        {
            MemoryStream ms = new MemoryStream();
            using GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedzipStream.Write(rawData, 0, rawData.Length);
            compressedzipStream.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// 將傳入的二進位字串資料以GZip演算法解壓縮
        /// </summary>
        /// <param name="zippedString">傳入經GZip壓縮後的二進位字串資料</param>
        /// <returns>傳回原後的未壓縮原始字串資料</returns>
        public static string GZipDecompressString(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
                return string.Empty;
            else
            {
                byte[] zippedData = Convert.FromBase64String(zippedString.ToString());
                return Encoding.UTF8.GetString(Decompress(zippedData));
            }
        }
        /// <summary>
        /// GZip解壓縮
        /// </summary>
        /// <param name="zippedData"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            using MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }
        /// <summary>
        /// 將Bytes轉換成String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConvertBytesToString(byte[] bytes)
        {
            BinaryFormatter format = new BinaryFormatter();
            MemoryStream memory = new MemoryStream(bytes);
            return format.Deserialize(memory) as string;
        }
        /// <summary>
        /// 將String轉換成Bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] ConvertStringToBytes(string str)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            binFormat.Serialize(mStream, str);
            return mStream.ToArray();
        }
        #endregion
    }
}
