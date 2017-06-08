/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：7fdcf7af-23e8-49fa-90d9-9c6f7f1241d2
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：LogUtil
 * 创建时间：2017/6/7 16:27:52
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSWeiXin.Util
{

    class Log
    {
        public string SavePath
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Params
        {
            get;
            set;
        }
        public string Message
        {
            get; set;
        }
        public DateTime DateTime
        {
            get;
            set;
        }
    }

    public static class LogUtil
    {
        static string path = Environment.CurrentDirectory + "\\Logs\\";

        static Queue<Log> logQueue = new Queue<Log>();


        static LogUtil()
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Task.Factory.StartNew(() =>
            {
                List<Log> logs = new List<Log>();

                var mark = DateTime.Now;

                while (true)
                {
                    if (logQueue != null && logQueue.Count > 0)
                    {
                        var log = logQueue.Dequeue();
                        if (log != null)
                        {
                            logs.Add(log);

                            if (logs.Count >= 100)
                            {
                                mark = DateTime.Now;

                                foreach (var item in logs)
                                {
                                    WriteFile(item);
                                }
                                logs.Clear();
                            }
                        }
                        else
                            Thread.Sleep(500);
                    }
                    else
                        Thread.Sleep(500);

                    if (logs.Count > 0 && mark.AddSeconds(3) <= DateTime.Now)
                    {
                        foreach (var item in logs)
                        {
                            WriteFile(item);
                        }
                        logs.Clear();
                    }
                }
            });
        }

        static void WriteFile(Log log)
        {
            File.AppendAllText(log.SavePath, Environment.NewLine + log.DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  " + log.Name + Environment.NewLine + "  Parmas:" + log.Params + Environment.NewLine + "  Message:" + log.Message + Environment.NewLine);
        }

        public static void WriteLog(string name, string msg, params object[] @params)
        {
            var log = new Log()
            {
                Name = name,
                Message = msg,
                SavePath = path,
                DateTime = DateTime.Now
            };

            if (@params != null)
            {
                foreach (var item in @params)
                {
                    log.Params += item.GetType().Name + ":" + item.ToString();
                }
            }

            log.SavePath += log.DateTime.ToString("yyyyMMddHH") + ".txt";

            logQueue.Enqueue(log);
        }

        public static void WriteLog(string name, Exception ex, params object[] @params)
        {
            string msg = ex.Source + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            WriteLog(name, msg, @params);
        }


    }
}
