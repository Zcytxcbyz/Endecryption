using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.IO;

namespace DESalgorithm
{
    public class DES
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">需要加密的</param>
        /// <param name="sKey">密匙</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static string Encrypt(string str, string sKey, string iv)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.Default.GetBytes(str);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);// 密匙
                des.IV = ASCIIEncoding.ASCII.GetBytes(iv);// 初始化向量
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var retB = Convert.ToBase64String(ms.ToArray());
                return retB;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="pToDecrypt">需要解密的</param>
        /// <param name="sKey">密匙</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static string Decrypt(string pToDecrypt, string sKey, string iv)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                // 如果两次密匙不一样，这一步可能会引发异常
                cs.FlushFinalBlock();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string Encrypt(string str, string sKey)
        {
            return Encrypt(str, sKey, sKey);
        }
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            return Decrypt(pToDecrypt, sKey, sKey);
        }
    }
}
