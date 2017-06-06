/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：62478fb6-22d6-49e9-8442-e40e02dae086
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：AESUtil
 * 创建时间：2017/6/2 16:09:52
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSWeiXin.Util
{
    public class AESUtil
    {
        /// <summary>
        ///     AES加密
        /// </summary>
        /// <param name="encryptString">待加密的密文</param>
        /// <param name="encryptKey">加密密匙</param>
        /// <returns></returns>
        public static string Encrypt(string encryptString, string encryptKey)
        {
            string returnValue;
            var temp = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            var AESProvider = Rijndael.Create();
            try
            {
                var defaultKey = "3B2hb2oYHpmZrFflfdmSon1x";
                if (string.IsNullOrEmpty(encryptKey))
                    encryptKey = defaultKey;
                if (encryptKey.Length < 24)
                    encryptKey = encryptKey + defaultKey.Substring(0, 24 - encryptKey.Length);
                if (encryptKey.Length > 24)
                    encryptKey = encryptKey.Substring(0, 24);
                var byteEncryptString = Encoding.UTF8.GetBytes(encryptString);
                using (var memoryStream = new MemoryStream())
                {
                    using (
                        var cryptoStream = new CryptoStream(memoryStream,
                            AESProvider.CreateEncryptor(Encoding.UTF8.GetBytes(encryptKey), temp),
                            CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(byteEncryptString, 0, byteEncryptString.Length);
                        cryptoStream.FlushFinalBlock();
                        returnValue = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

        /// <summary>
        ///     AES 解密
        /// </summary>
        /// <param name="decryptString">待解密密文</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public static string Decrypt(string decryptString, string decryptKey)
        {
            if (string.IsNullOrEmpty(decryptString))
                return string.Empty;

            var returnValue = "";
            var temp = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            var AESProvider = Rijndael.Create();
            try
            {
                var defaultKey = "3B2hb2oYHpmZrFflfdmSon1x";
                if (string.IsNullOrEmpty(decryptKey))
                    decryptKey = defaultKey;
                if (decryptKey.Length < 24)
                    decryptKey = decryptKey + defaultKey.Substring(0, 24 - decryptKey.Length);
                if (decryptKey.Length > 24)
                    decryptKey = decryptKey.Substring(0, 24);
                var byteDecryptString = Convert.FromBase64String(decryptString);
                using (var memoryStream = new MemoryStream())
                {
                    using (
                        var cryptoStream = new CryptoStream(memoryStream,
                            AESProvider.CreateDecryptor(Encoding.UTF8.GetBytes(decryptKey), temp),
                            CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(byteDecryptString, 0, byteDecryptString.Length);
                        cryptoStream.FlushFinalBlock();
                        returnValue = Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
    }
}
