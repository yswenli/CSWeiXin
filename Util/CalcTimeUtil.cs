/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：c4f08ade-9048-486e-a627-7479141da2e4
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：CalcTimeUtil
 * 创建时间：2017/5/26 11:15:50
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Threading;

namespace CSWeiXin.Util
{
    public class CalcTimeUtil
    {
        static bool Playing = false;

        public void Start(int maxSeconds, Action<int> OnChanged)
        {
            while (Playing)
            {
                Thread.Sleep(10);
            }
            var td = new Thread(new ThreadStart(() =>
            {
                Playing = true;
                int i = maxSeconds + 1;
                while (i > 1)
                {
                    i--;
                    OnChanged?.Invoke(i);
                    Thread.Sleep(1000);
                }
                Playing = false;
            }));
            td.IsBackground = true;
            td.Start();
        }


        public static string GetUnixDateTime()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtNow = DateTime.Now;
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            return timeStamp.Substring(0, timeStamp.Length - 7);
        }

        public static string GetUnixDateTime(TimeSpan ts)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtNow = DateTime.Now;
            TimeSpan toNow = dtNow.Subtract(dtStart);
            toNow = toNow.Add(ts);
            string timeStamp = toNow.Ticks.ToString();
            return timeStamp.Substring(0, timeStamp.Length - 7);
        }

        public static DateTime GetCSDateTime(string unixDateTime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(unixDateTime + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }

    }
}
