/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：a1b2aeb7-3157-433e-882f-627c9f1a119c
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Weixin
 * 类名称：LoginHelper
 * 创建时间：2017/5/25 10:07:24
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using CSWeiXin.Models;
using CSWeiXin.Util;

namespace CSWeiXin.Weixin
{
    public class LoginHelper
    {
        static string uuidUrl = "https://login.wx.qq.com/jslogin?appid=wx782c26e4c19acffb&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=zh_CN&_=1495675619027";

        static string qrUrlTemple = "https://login.weixin.qq.com/qrcode/{0}";

        static string loginUrlTemple = "https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?uuid={0}&tip=1&_=1495675619027";

        static string pingdUrlTemple = "https://pingtas.qq.com/webview/pingd?dm=wx.qq.com&pvi={0}&si={1}&url=/&arg=%26lang%3Dzh_CN&ty=&rdm=wx.qq.com&rurl=/&rarg=%26lang%3Dzh_CN&adt=&r2=43209744&r3=-1&r4=1&fl=25.0&scr=1600x900&scl=24-bit&lg=zh-cn&jv=&tz=-8&ct=&ext=adid=&pf=&random=1495867248819";


        #region wx wx2
        public static bool WX2 { private set; get; } = false;

        static string statReportUrl = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxstatreport?fun=new";

        static string loginPageUrlTemple = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxnewloginpage?ticket={0}&uuid={1}&lang=zh_CN&scan={2}&fun=new&version=v2&lang=zh_CN";
        #endregion



        public static string pgv_pvi = RandomUtil.GetRandom();

        public static string pgv_si = "s" + RandomUtil.GetRandom();

        public static string UUID = string.Empty;

        public static string Scan = string.Empty;

        public static string Ticket = string.Empty;

        public static XmlElement Result = null;


        public static LoginPageXml LoginPageXml = null;

        public static LoginPageCookie LoginPageCookie = null;




        public static string GetUUID()
        {
            CookieContainer resCookies = null;

            var uuid = WebClientUtil.GetResponseOnCookie(uuidUrl, "get", null, out resCookies);

            if (!string.IsNullOrEmpty(uuid))
            {
                uuid = uuid.Substring(uuid.IndexOf("\"") + 1);
                uuid = uuid.Substring(0, uuid.Length - 2);
                return uuid;
            }
            return null;
        }

        public static Image GetQR(string uuid)
        {
            var qrUrl = string.Format(qrUrlTemple, uuid);
            return Image.FromStream(new MemoryStream(new WebClientUtil().DownloadData(qrUrl)));
        }

        public static void PingdAsync()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string pingdUrl = string.Format(pingdUrlTemple, LoginHelper.pgv_pvi, LoginHelper.pgv_si);
                    var reqCookies = new CookieContainer();
                    reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
                    reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
                    CookieContainer resCookies = null;
                    WebClientUtil.GetResponseOnCookie(pingdUrl, "post", reqCookies, out resCookies, null, "text/html");
                }
                catch (Exception ex)
                {

                }

            });
        }

        public static void StatReport()
        {
            CookieContainer resCookies = null;
            CookieContainer reqCookies = new CookieContainer();
            reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
            WebClientUtil.GetResponseOnCookie(statReportUrl, "post", reqCookies, out resCookies, null, "application/json;charset=UTF-8");
        }


        public static void LoginPage()
        {
            var loginPageUrl = string.Format(loginPageUrlTemple, LoginHelper.Ticket, LoginHelper.UUID, LoginHelper.Scan);

            if (WX2)
            {
                loginPageUrl = loginPageUrl.Replace("//wx.", "//wx2.");
            }

            CookieContainer resCookies = null;

            CookieContainer reqCookies = null;

            reqCookies = new CookieContainer();
            reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "zh_CN", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("mm_lang", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("refreshTimes", "2", "/", "wx.qq.com"));

            LoginPageXml = LoginPageXml.Parse(WebClientUtil.GetResponseOnCookie(loginPageUrl, "get", reqCookies, out resCookies, null));

            LoginPageCookie = LoginPageCookie.Parse(resCookies);

            DataUtil.WriteData(LoginPageCookie, LoginPageXml);
        }


        public static void Login(string uuid, Action failed, Action successed)
        {
            var loginUrl = string.Format(loginUrlTemple, uuid);

            CookieContainer resCookies = null;

            CookieContainer reqCookies = null;

            var result = WebClientUtil.GetResponseOnCookie(loginUrl, "get", reqCookies, out resCookies, null);

            if (result == "window.code=201;")
            {
                do
                {
                    Thread.Sleep(500);

                    result = WebClientUtil.GetResponseOnCookie(loginUrl, "get", reqCookies, out resCookies, null);
                }
                while (result == "window.code=201;");
            }
            if (result.IndexOf("window.code=200;") > -1)
            {
                result = result.Substring(result.IndexOf("window.redirect_uri=") + 21);
                result = result.Substring(0, result.Length - 2);

                LoginHelper.UUID = uuid;
                LoginHelper.Scan = result.Substring(result.LastIndexOf("=") + 1);
                LoginHelper.Ticket = result.Substring(result.IndexOf("ticket=") + 7);
                LoginHelper.Ticket = LoginHelper.Ticket.Substring(0, LoginHelper.Ticket.IndexOf("&"));

                if (result.IndexOf("//wx2.") > -1)
                {
                    WX2 = true;
                }

                WebClientUtil.GetResponseOnCookie(result, "get", reqCookies, out resCookies, null);

                LoginHelper.PingdAsync();

                LoginHelper.StatReport();

                LoginHelper.LoginPage();

                successed?.Invoke();

                return;
            }
            failed?.Invoke();
            return;
        }


        public static bool InitWithData()
        {
            var result = false;

            var data1 = DataUtil.GetLoginPageXml();

            var data2 = DataUtil.GetLoginPageCookie();

            if (data1 != null && data2 != null && !string.IsNullOrEmpty(data1.pass_ticket) && !string.IsNullOrEmpty(data2.webwx_auth_ticket))
            {
                LoginHelper.LoginPageXml = data1;
                LoginHelper.LoginPageCookie = data2;
                result = true;
            }
            return result;
        }

        public static void ClearCaches()
        {
            DataUtil.ClearCaches();
        }



    }
}
