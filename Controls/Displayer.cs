/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：fadf7250-b861-4c15-b449-fc20c8058898
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Controls
 * 类名称：Displayer
 * 创建时间：2017/6/5 15:54:32
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSWeiXin.Controls
{
    public partial class Displayer : UserControl
    {


        public Displayer()
        {
            InitializeComponent();
        }

        public static string GoBottomScript
        {
            get
            {
                return "<script type='text/javascript'>function GoBottom(){window.scrollBy(0, 60000);}</script>";
            }
        }

        public string Html
        {
            get
            {
                return this.webBrowser1.DocumentText;
            }
            set
            {
                this.webBrowser1.ScriptErrorsSuppressed = false;
                this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
                this.webBrowser1.DocumentText = value;
            }
        }


        public string AppendMyMsg(string userName, string msg)
        {
            var html = Displayer.GenerateMsgHtml(userName, msg, true);

            if (this.webBrowser1.InvokeRequired)
            {
                this.webBrowser1.BeginInvoke(new Func<string, string, string>(AppendMyMsg), userName, msg);
            }
            else
            {
                this.webBrowser1.ScriptErrorsSuppressed = false;
                this.webBrowser1.IsWebBrowserContextMenuEnabled = false;

                if (string.IsNullOrEmpty(this.webBrowser1.DocumentText))
                {
                    this.webBrowser1.DocumentText = Displayer.GoBottomScript;
                }
                this.webBrowser1.DocumentText += html;
            }
            return html;
        }

        public string AppendMsg(string userName, string msg)
        {
            var html = Displayer.GenerateMsgHtml(userName, msg);
            if (this.webBrowser1.InvokeRequired)
            {
                this.webBrowser1.BeginInvoke(new Func<string, string, string>(AppendMsg), userName, msg);
            }
            else
            {
                this.webBrowser1.ScriptErrorsSuppressed = false;
                this.webBrowser1.IsWebBrowserContextMenuEnabled = false;

                if (string.IsNullOrEmpty(this.webBrowser1.DocumentText))
                {
                    this.webBrowser1.DocumentText = Displayer.GoBottomScript;
                }
                this.webBrowser1.DocumentText += html;
            }
            return html;
        }

        public void GoBottom()
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                this.webBrowser1.BeginInvoke(new Action(()=>
                {
                    this.webBrowser1.Document.InvokeScript("GoBottom", null);
                }));
            });
            
        }

        public static string GenerateMsgHtml(string userName, string msg, bool isMe = false)
        {
            var html = "<div style='display:inline-block;min-width:10px;border-radius:25px; background-color:#FAFAFA;background-color: #f5f5f5;padding:5px;margin:5px;font-family: \"Microsoft YaHei\", 微软雅黑, Verdana, sans-serif, 宋体;'><p style='font-size:12px;font-weight:bold;line-height:15px;'>" + userName + "</p><p style='font-size:11px;'>" + msg + "</p></div>";
            if (isMe)
            {
                html = "<div style='display:inline-block;min-width:10px;border-radius:25px;background-color:#FAFAFA;background-color: #f5f5f5;padding:5px;margin:5px;font-family: \"Microsoft YaHei\", 微软雅黑, Verdana, sans-serif, 宋体;'><p style='font-size:12px;font-weight:bold;text-align:right;line-height:15px;'>" + userName + "</p><p style='text-align:right;font-size:11px;'>" + msg + "</p></div>";
            }
            return html;
        }
    }
}
