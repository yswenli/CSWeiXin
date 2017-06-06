/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：18f3f953-fe16-4e93-8e85-63829d7c8e60
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：SerializeUtil
 * 创建时间：2017/5/25 9:41:49
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSWeiXin.Util
{
    public class SerializeUtil
    {
        /// <summary>
        ///     二进制序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ByteSerialize(object obj)
        {
            using (var m = new MemoryStream())
            {
                var bin = new BinaryFormatter();

                bin.Serialize(m, obj);

                return m.ToArray();
            }
        }

        /// <summary>
        ///     二进制反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static T ByteDeserialize<T>(byte[] buffer)
        {
            using (var m = new MemoryStream())
            {
                m.Write(buffer, 0, buffer.Length);
                m.Position = 0;

                var bin = new BinaryFormatter();

                return (T)bin.Deserialize(m);
            }
        }

        /// <summary>
        ///     newton.json序列化,日志参数专用
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            settings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        ///     newton.json反序列化,日志参数专用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            settings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}
