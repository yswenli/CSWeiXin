/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：55053a99-5e11-4622-897c-4742a9ddfdf0
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Weixin
 * 类名称：InitHelper
 * 创建时间：2017/5/27 14:57:13
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
using System.Threading.Tasks;
using CCWin.SkinControl;
using CSWeiXin.Models;
using CSWeiXin.Util;
using System.Windows.Forms;

namespace CSWeiXin.Weixin
{
    public static class InitHelper
    {
        static string weixinInitUrlTemple = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r={1}&lang=zh_CN&pass_ticket={0}";

        static string webwxgetcontactUrlTemple = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?lang=zh_CN&r={0}&seq=0&skey={1}";

        public static bool InitWidthLogin()
        {
            try
            {
                var reqCookies = new CookieContainer();

                CookieContainer resCookies = null;

                reqCookies = new CookieContainer();
                reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("last_wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("login_frequency", "1", "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("webwx_auth_ticket", LoginHelper.LoginPageCookie.webwx_auth_ticket, "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("webwx_data_ticket", LoginHelper.LoginPageCookie.webwx_data_ticket, "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("webwxuvid", LoginHelper.LoginPageCookie.webwxuvid, "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("wxloadtime", CalcTimeUtil.GetUnixDateTime(new TimeSpan(365, 0, 0, 0)), "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("wxsid", LoginHelper.LoginPageCookie.wxsid, "/", "wx.qq.com"));
                reqCookies.Add(new Cookie("wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));

                var weixinInitUrl = string.Format(weixinInitUrlTemple, LoginHelper.LoginPageXml.pass_ticket, new object().GetHashCode());

                var data = new Dictionary<string, string>();

                var postData = "{\"BaseRequest\":{\"Uin\":\"" + LoginHelper.LoginPageCookie.wxuin + "\",\"Sid\":\"" + LoginHelper.LoginPageCookie.wxsid + "\",\"Skey\":\"" + LoginHelper.LoginPageXml.skey + "\",\"DeviceID\":\"e478901587692997\"}}";

                data.Add(WebClientUtil.JsonDataPrex, postData);

                string json = WebClientUtil.GetResponseOnCookie(weixinInitUrl, "post", reqCookies, out resCookies, data, "application/json;charset=UTF-8");

                InitHelper.WebWeixinInit = SerializeUtil.Deserialize<WebWeixinInit>(json);

                if (InitHelper.WebWeixinInit == null || WebWeixinInit.BaseResponse.Ret != 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {

            }
            return false;
        }

        public static WebWeixinInit WebWeixinInit
        {
            get;
            private set;
        }

        public static BatchGetContact BatchGetContact
        {
            get;
            private set;
        }

        public static BatchGetContact GetContactList()
        {
            var reqCookies = new CookieContainer();

            CookieContainer resCookies = null;

            reqCookies = new CookieContainer();
            reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("last_wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("login_frequency", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_auth_ticket", LoginHelper.LoginPageCookie.webwx_auth_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_data_ticket", LoginHelper.LoginPageCookie.webwx_data_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwxuvid", LoginHelper.LoginPageCookie.webwxuvid, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxloadtime", CalcTimeUtil.GetUnixDateTime(new TimeSpan(365, 0, 0, 0)), "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxsid", LoginHelper.LoginPageCookie.wxsid, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));

            var webwxgetcontactUrl = string.Format(webwxgetcontactUrlTemple, RandomUtil.GetRandom(10), LoginHelper.LoginPageXml.skey);

            string json = WebClientUtil.GetResponseOnCookie(webwxgetcontactUrl, "get", reqCookies, out resCookies);

            InitHelper.BatchGetContact = SerializeUtil.Deserialize<BatchGetContact>(json);

            return InitHelper.BatchGetContact;
        }

        public static Image GetImage(string headerImageUrl)
        {
            var reqCookies = new CookieContainer();
            reqCookies = new CookieContainer();
            reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("last_wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("login_frequency", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_auth_ticket", LoginHelper.LoginPageCookie.webwx_auth_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_data_ticket", LoginHelper.LoginPageCookie.webwx_data_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwxuvid", LoginHelper.LoginPageCookie.webwxuvid, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxloadtime", CalcTimeUtil.GetUnixDateTime(new TimeSpan(365, 0, 0, 0)), "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxsid", LoginHelper.LoginPageCookie.wxsid, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));

            return Image.FromStream(WebClientUtil.GetResponseOnCookie("https://wx.qq.com" + headerImageUrl, "get", reqCookies));
        }

        public static void SetImageAsync(ChatListSubItem sitem, string imageUrl)
        {
            Task.Factory.StartNew(() =>
            {
                var image = InitHelper.GetImage(imageUrl);
                sitem.HeadImage = image;
            });
        }

        public static void SetImageAsync(this Form form, PictureBox control, string imageUrl)
        {
            Task.Factory.StartNew(() =>
            {
                var image = InitHelper.GetImage(imageUrl);
                control.BeginInvoke(new Action(() =>
                {
                    control.Image = image;
                }));
            });
        }
    }
}
