using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace StusTools
{
    public class Encrypt
    {
        public string ToEncrypt(string str)
        {
            try
            {
                //将密钥字符串转换为字节序列
                byte[] P_byte_key = Encoding.Unicode.GetBytes("@#gf");
                //将字符串转换为字节序列
                byte[] P_byte_data = Encoding.Unicode.GetBytes(str);
                //创建内存流对象
                MemoryStream P_Stream_MS = new MemoryStream();
                //创建加密流对象
                CryptoStream P_CryptStream_Stream = new CryptoStream(P_Stream_MS, new DESCryptoServiceProvider().CreateEncryptor(P_byte_key, P_byte_key), CryptoStreamMode.Write);
                //向加密流中写入字节序列
                P_CryptStream_Stream.Write(P_byte_data, 0, P_byte_data.Length);
                //将数据压入基础流
                P_CryptStream_Stream.FlushFinalBlock();
                //从内存流中获取字节序列
                byte[] P_bt_temp = P_Stream_MS.ToArray();
                //关闭加密流
                P_CryptStream_Stream.Close();
                //关闭内存流
                P_Stream_MS.Close();
                //方法返回加密后的字符串
                return Convert.ToBase64String(P_bt_temp);
            }
            catch (CryptographicException ce)
            {
                throw new Exception(ce.Message);
            }
        }

        public string ToDecrypt(string str)
        {
            try
            {
                //将密钥字符串转换为字节序列
                byte[] P_byte_key = Encoding.Unicode.GetBytes("@#gf");
                //将加密后的字符串转换为字节序列
                byte[] P_byte_data = Convert.FromBase64String(str);
                //创建内存流对象并写入数据
                MemoryStream P_Stream_MS = new MemoryStream(P_byte_data);
                //创建加密流对象
                CryptoStream P_CryptStream_Stream = new CryptoStream(P_Stream_MS, new DESCryptoServiceProvider().CreateDecryptor(P_byte_key, P_byte_key), CryptoStreamMode.Read);
                //创建字节序列对象
                byte[] P_bt_temp = new byte[200];
                //创建内存流对象
                MemoryStream P_MemoryStream_temp = new MemoryStream();
                //创建记数器
                int i = 0;
                //使用while循环得到解密数据
                while ((i = P_CryptStream_Stream.Read(P_bt_temp, 0, P_bt_temp.Length)) > 0)
                {
                    //将解密后的数据放入内存流
                    P_MemoryStream_temp.Write(P_bt_temp, 0, i);
                }
                //方法返回解密后的字符串
                return Encoding.Unicode.GetString(P_MemoryStream_temp.ToArray());
            }
            catch (CryptographicException ce)
            {
                throw new Exception(ce.Message);
            }
        }
    }
}
