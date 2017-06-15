/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：90973895-baf8-4409-8cf2-b8c1a391d142
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：DataUtil
 * 创建时间：2017/6/2 16:02:01
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSWeiXin.Models;

namespace CSWeiXin.Util
{
    public static class DataUtil
    {
        static readonly string DataPath = Environment.CurrentDirectory + "\\Data\\";

        static readonly string aesKey = "yswenli";

        static DataUtil()
        {
            if (!Directory.Exists(DataPath))
                Directory.CreateDirectory(DataPath);
        }

        public static string GetMD5(string fileName)
        {
            try
            {
                var file = new FileStream(fileName, FileMode.Open);
                var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString().ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
            }
        }

        public static long GetLength(string fileName)
        {
            return new FileInfo(fileName).Length;
        }

        public static void WriteData(LoginPageCookie loginPageCookie, LoginPageXml loginPageXml)
        {
            var json1 = AESUtil.Encrypt(SerializeUtil.Serialize(loginPageCookie), aesKey);
            using (var fs = File.Open(DataPath + "LoginPageCookie.json", FileMode.OpenOrCreate))
            {
                var data = Encoding.UTF8.GetBytes(json1);
                fs.Write(data, 0, data.Length);
            }
            var json2 = AESUtil.Encrypt(SerializeUtil.Serialize(loginPageXml), aesKey);
            using (var fs = File.Open(DataPath + "LoginPageXml.json", FileMode.OpenOrCreate))
            {
                var data = Encoding.UTF8.GetBytes(json2);
                fs.Write(data, 0, data.Length);
            }
        }


        public static LoginPageCookie GetLoginPageCookie()
        {
            LoginPageCookie result = null;
            try
            {
                result = SerializeUtil.Deserialize<LoginPageCookie>(AESUtil.Decrypt(File.ReadAllText(DataPath + "LoginPageCookie.json"), aesKey));
            }
            catch { }
            return result;
        }

        public static LoginPageXml GetLoginPageXml()
        {
            LoginPageXml result = null;

            try
            {
                result = SerializeUtil.Deserialize<LoginPageXml>(AESUtil.Decrypt(File.ReadAllText(DataPath + "LoginPageXml.json"), aesKey));
            }
            catch { }

            return result;
        }


        public static void WriteHistory(Dictionary<string, string> msgs)
        {
            Task.Factory.StartNew(() =>
            {
                var json = AESUtil.Encrypt(SerializeUtil.Serialize(msgs), aesKey);
                using (var fs = File.Open(DataPath + "History.json", FileMode.OpenOrCreate))
                {
                    var data = Encoding.UTF8.GetBytes(json);
                    fs.Write(data, 0, data.Length);
                }
            });
        }

        public static Dictionary<string, string> GetHistory()
        {

            Dictionary<string, string> result = null;

            try
            {
                if (File.Exists(DataPath + "History.json"))
                    result = SerializeUtil.Deserialize<Dictionary<string, string>>(AESUtil.Decrypt(File.ReadAllText(DataPath + "History.json"), aesKey));
            }
            catch { }

            return result;
        }


        public static void ClearCaches()
        {
            try
            {
                var files = Directory.GetFiles(DataPath);
                foreach (var item in files)
                {
                    File.Delete(item);
                }
            }
            catch { }

        }

    }
}
