/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：faeae8a6-8e0b-4699-943c-81920d05aa4c
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：MessageUtil
 * 创建时间：2017/6/1 17:02:26
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSWeiXin.Controls;

namespace CSWeiXin.Util
{
    public static class MessageUtil
    {
        static Dictionary<string, string> msgs = new Dictionary<string, string>();


        static object locker = new object();


        static MessageUtil()
        {
            msgs = DataUtil.GetHistory();
            if (msgs == null)
                msgs = new Dictionary<string, string>();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    DataUtil.WriteHistory(msgs);
                    Thread.Sleep(10 * 1000);
                }
            });

        }


        public static void Set(string fromUserName, string msg)
        {
            lock (locker)
            {
                if (msgs.ContainsKey(fromUserName))
                {
                    var m = msgs[fromUserName] + msg;
                    msgs[fromUserName] = m;
                }
                else
                {
                    msgs.Add(fromUserName, Displayer.GoBottomScript + msg);
                }
            }
        }


        public static string Get(string fromUserName)
        {
            lock (locker)
            {
                if (msgs.ContainsKey(fromUserName))
                {
                    return msgs[fromUserName];
                }
                else
                {
                    return Displayer.GoBottomScript;
                }
            }
        }

    }
}
