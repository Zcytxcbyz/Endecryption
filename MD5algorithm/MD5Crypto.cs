using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography; 

namespace MD5algorithm
{
    public class MD5Crypto
    {
        public static string MD5Encrypt(string str)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(str); //将字符串解析成字节数组，随便按照哪种解析格式都行
                MD5 md5 = MD5.Create();  //使用MD5这个抽象类的Creat()方法创建一个虚拟的MD5类的对象。
                byte[] bufferNew = md5.ComputeHash(buffer); //使用MD5实例的ComputerHash()方法处理字节数组。
                string strNew = null;
                for (int i = 0; i < bufferNew.Length; i++)
                {
                    strNew += bufferNew[i].ToString("x2");  //对bufferNew字节数组中的每个元素进行十六进制转换然后拼接成strNew字符串
                }
                return strNew;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
