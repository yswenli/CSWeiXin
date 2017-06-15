using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSWeiXin.Util;

namespace CSWeiXin
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.MaxServicePoints = 0;
                ServicePointManager.DefaultConnectionLimit = 1024;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
            }
            catch(Exception ex)
            {
                LogUtil.WriteLog("Program.Main", ex);
                MessageBox.Show("出现严重异常，已记录到日志");
            }

            
        }
    }
}
