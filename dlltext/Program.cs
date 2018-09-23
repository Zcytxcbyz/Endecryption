using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using DESalgorithm;
using Base64code;
using AESalgorithm;
using RSAalgorithm;
using MD5algorithm;

namespace dlltext
{  
    class Program
    { 
        static void Main(string[] args)
        {
            RSA rsa = new RSA();
            string key = "12345678";
            string content = "123456";
            string str1 = DES.Encrypt(content, key);
            string arr1 = DES.Decrypt(str1, key);
            string str2 = AES.AESEncrypt(content, key);
            string arr2 = AES.AESDecrypt(str2, key);
            string privatekey, publickey;
            rsa.RSAKey(out privatekey,out publickey);
            string str3 = rsa.RSAEncrypt(publickey, content);
            string arr3 = rsa.RSADecrypt(privatekey, str3);
            string str4 = MD5Crypto.MD5Encrypt(content);
            string str5 = Base64code.Base64.ToBase64String(content);
            string arr5 = Base64code.Base64.UnBase64String(str5);
            Console.WriteLine(str1);
            Console.WriteLine(arr1);
            Console.WriteLine(str2);
            Console.WriteLine(arr2);
            Console.WriteLine(str3);
            Console.WriteLine(arr3);
            Console.WriteLine(publickey);
            Console.WriteLine(privatekey);
            Console.WriteLine(str4);
            Console.WriteLine(str5);
            Console.WriteLine(arr5);
            Console.Read();
        }
    }
}
