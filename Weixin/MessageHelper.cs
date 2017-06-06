/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：58454ebd-f65a-4c90-945c-e58aa805998b
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Weixin
 * 类名称：MessageHelper
 * 创建时间：2017/5/27 14:12:14
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSWeiXin.Models;
using CSWeiXin.Util;

namespace CSWeiXin.Weixin
{

    public delegate void OnMessageHander(IList<AddMsgList> msgs);

    public delegate void OnTokenFaiedHander();

    public static class MessageHelper
    {

        static SendMessageResult SendMessageResult = new SendMessageResult();

        static string SendMsgTemple = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?lang=zh_CN&pass_ticket={0}";

        static string SendImpageUrlTemple = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsgimg?fun=async&f=json&lang=zh_CN&pass_ticket={0}";

        static string UploadMediaUrl = "https://file.wx.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json";

        static string CheckeUrlTemple = "https://webpush.wx.qq.com/cgi-bin/mmwebwx-bin/synccheck?r={0}&skey={1}&sid={2}&uin={3}&deviceid=e139881526627243&synckey={4}&_=1496220248449";

        static string SyncUrlTemple = "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid={0}&skey={1}&lang=zh_CN&pass_ticket={2}";

        static MessageHelper()
        {
            MessageHelper.ReceiveMsg();
        }


        public static void SendMsg(string fromUserName, string toUserName, string content)
        {
            var msg = new SendMessage(LoginHelper.LoginPageCookie.wxuin,
                LoginHelper.LoginPageCookie.wxsid,
                LoginHelper.LoginPageXml.skey,
                1,
                fromUserName,
                toUserName,
                content, "e526874475065566", MessageHelper.SendMessageResult.LocalID, MessageHelper.SendMessageResult.LocalID);

            var json = SerializeUtil.Serialize(msg);

            var reqCookies = new CookieContainer();
            reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("refreshTimes", "2", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwxuvid", LoginHelper.LoginPageCookie.webwxuvid, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_auth_ticket", LoginHelper.LoginPageCookie.webwx_auth_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("login_frequency", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("last_wxuin", "LoginHelper.LoginPageCookie.wxuin", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxloadtime", "1495855277_expired", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxsid", LoginHelper.LoginPageCookie.wxsid, "/", "wx.qq.com"));

            CookieContainer resCookies = null;

            var SendMsg = string.Format(SendMsgTemple, LoginHelper.LoginPageXml.pass_ticket);

            var dic = new Dictionary<string, string>();

            dic.Add(WebClientUtil.JsonDataPrex, json);

            MessageHelper.SendMessageResult = SerializeUtil.Deserialize<SendMessageResult>(WebClientUtil.GetResponseOnCookie(SendMsg, "post", reqCookies, out resCookies, dic, "application/json;charset=UTF-8"));
        }

        public static void UploadMedia(string fromUserName, string toUserName, string mediaName)
        {
            var len = DataUtil.GetLength(mediaName);

            var md5 = DataUtil.GetMD5(mediaName);

            string uploadmediarequest = "{\"UploadType\":2,\"BaseRequest\":{\"Uin\":" + LoginHelper.LoginPageCookie.wxuin + ",\"Sid\":\"" + LoginHelper.LoginPageCookie.wxsid + "\",\"Skey\":\"" + LoginHelper.LoginPageXml.skey + "\",\"DeviceID\":\"e828447528404774\"},\"ClientMediaId\":" + RandomUtil.GetRandom(13) + ",\"TotalLen\":" + len + ",\"StartPos\":0,\"DataLen\":" + len + ",\"MediaType\":4,\"FromUserName\":\"" + fromUserName + "\",\"ToUserName\":\"" + toUserName + "\",\"FileMd5\":\"" + md5 + "\"}";

            var wxUploadMedia = SerializeUtil.Deserialize<WXUploadMedia>(WebClientUtil.UploadMediaOnCookie(UploadMediaUrl, mediaName, uploadmediarequest, LoginHelper.LoginPageCookie.webwx_data_ticket, LoginHelper.LoginPageXml.pass_ticket));

            if (wxUploadMedia != null && !string.IsNullOrEmpty(wxUploadMedia.MediaId))
            {
                SendImage(fromUserName, toUserName, wxUploadMedia.MediaId);
            }
        }

        private static void SendImage(string fromUserName, string toUserName, string mediaId)
        {

            var reqCookies = new CookieContainer();
            reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("refreshTimes", "2", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwxuvid", LoginHelper.LoginPageCookie.webwxuvid, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_auth_ticket", LoginHelper.LoginPageCookie.webwx_auth_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_data_ticket", LoginHelper.LoginPageCookie.webwx_data_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("login_frequency", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("last_wxuin", "LoginHelper.LoginPageCookie.wxuin", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxloadtime", "1495855277_expired", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxsid", LoginHelper.LoginPageCookie.wxsid, "/", "wx.qq.com"));

            CookieContainer resCookies = null;

            var SendImpageUrl = string.Format(SendImpageUrlTemple, LoginHelper.LoginPageXml.pass_ticket);

            var json = "{\"BaseRequest\":{\"Uin\":" + LoginHelper.LoginPageCookie.wxuin + ",\"Sid\":\"" + LoginHelper.LoginPageCookie.wxsid + "\",\"Skey\":\"" + LoginHelper.LoginPageXml.skey + "\",\"DeviceID\":\"e856174200109311\"},\"Msg\":{\"Type\":3,\"MediaId\":\"" + mediaId + "\",\"Content\":\"\",\"FromUserName\":\"" + fromUserName + "\",\"ToUserName\":\"" + toUserName + "\",\"LocalID\":\"14963106567540883\",\"ClientMsgId\":\"14963106567540883\"},\"Scene\":0}";

            var dic = new Dictionary<string, string>();

            dic.Add(WebClientUtil.JsonDataPrex, json);

            MessageHelper.SendMessageResult = SerializeUtil.Deserialize<SendMessageResult>(WebClientUtil.GetResponseOnCookie(SendImpageUrl, "post", reqCookies, out resCookies, dic, "application/json;charset=UTF-8"));
        }


        public static event OnMessageHander OnMessage;

        public static event OnTokenFaiedHander OnTokenFailed;

        private static void ReceiveMsg()
        {
            Task.Factory.StartNew(() =>
            {
                var result = string.Empty;

                while (true)
                {
                    try
                    {
                        result = CheckeSync();

                        if (!string.IsNullOrEmpty(result) && result.IndexOf("retcode:\"0\"") > -1)
                        {
                            if (result.IndexOf("selector:\"0\"") == -1)
                            {
                                var wxsync = SyncMessage();
                                if (wxsync != null && wxsync.SyncKey.Count > 0)
                                {
                                    preSyncKey = wxsync.SyncKey;

                                    if (wxsync.AddMsgCount > 0)
                                    {
                                        OnMessage?.Invoke(wxsync.AddMsgList);
                                        continue;
                                    }
                                }
                                else
                                {
                                    Thread.Sleep(1000);
                                }
                            }
                            else
                            {
                                Thread.Sleep(1000);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(result) && result.IndexOf("retcode:\"0\"") == -1)
                            {
                                OnTokenFailed?.Invoke();
                                Thread.Sleep(15000);
                            }
                            else
                            {
                                Thread.Sleep(1000);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(1000);
                    }
                }
            });
        }


        private static SyncKey preSyncKey = new SyncKey();



        private static string CheckeSync()
        {
            var reqCookies = new CookieContainer();

            reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("refreshTimes", "2", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwxuvid", LoginHelper.LoginPageCookie.webwxuvid, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_auth_ticket", LoginHelper.LoginPageCookie.webwx_auth_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_data_ticket", LoginHelper.LoginPageCookie.webwx_data_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("login_frequency", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("last_wxuin", "LoginHelper.LoginPageCookie.wxuin", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxloadtime", "1495855277_expired", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxsid", LoginHelper.LoginPageCookie.wxsid, "/", "wx.qq.com"));

            CookieContainer resCookies = null;

            var synckey = string.Empty;

            if ((preSyncKey == null || preSyncKey.Count == 0) && InitHelper.WebWeixinInit.SyncKey != null && InitHelper.WebWeixinInit.SyncKey.Count > 0)
            {
                foreach (var item in InitHelper.WebWeixinInit.SyncKey.List)
                {
                    synckey += item.Key + "_" + item.Val + "|";
                }
                synckey = synckey.Substring(0, synckey.Length - 1);
            }
            else if (preSyncKey != null && preSyncKey.Count > 0 && preSyncKey.Count > 0)
            {
                foreach (var item in preSyncKey.List)
                {
                    synckey += item.Key + "_" + item.Val + "|";
                }
                synckey = synckey.Substring(0, synckey.Length - 1);
            }

            var checkeUrl = string.Format(CheckeUrlTemple, CalcTimeUtil.GetUnixDateTime() + "000", LoginHelper.LoginPageXml.skey, LoginHelper.LoginPageCookie.wxsid, LoginHelper.LoginPageCookie.wxuin, synckey);

            return WebClientUtil.GetResponseOnCookie(checkeUrl, "get", reqCookies, out resCookies);
        }

        private static WXSync SyncMessage()
        {
            var synckey = string.Empty;

            int count = 0;

            if (preSyncKey == null || preSyncKey.Count == 0)
            {
                foreach (var item in InitHelper.WebWeixinInit.SyncKey.List)
                {
                    synckey += "{\"Key\":" + item.Key + ",\"Val\":" + item.Val + "},";
                }
                if (!string.IsNullOrEmpty(synckey))
                    synckey = synckey.Substring(0, synckey.Length - 1);
                count = InitHelper.WebWeixinInit.SyncKey.Count;
            }
            else if (preSyncKey.List != null && preSyncKey.List.Count > 0)
            {
                foreach (var item in preSyncKey.List)
                {
                    synckey += "{\"Key\":" + item.Key + ",\"Val\":" + item.Val + "},";
                }
                if (!string.IsNullOrEmpty(synckey))
                    synckey = synckey.Substring(0, synckey.Length - 1);
                count = preSyncKey.Count;
            }


            var json = "{\"BaseRequest\":{\"Uin\":" + LoginHelper.LoginPageCookie.wxuin + ",\"Sid\":\"" + LoginHelper.LoginPageCookie.wxsid + "\",\"Skey\":\"" + LoginHelper.LoginPageXml.skey + "\",\"DeviceID\":\"e370515259583130\"},\"SyncKey\":{\"Count\":" + count + ",\"List\":[" + synckey + "]},\"rr\":-" + CalcTimeUtil.GetUnixDateTime() + "}";


            var reqCookies = new CookieContainer();
            reqCookies.Add(new Cookie("pgv_pvi", LoginHelper.pgv_pvi, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("pgv_si", LoginHelper.pgv_si, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("refreshTimes", "2", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwxuvid", LoginHelper.LoginPageCookie.webwxuvid, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_auth_ticket", LoginHelper.LoginPageCookie.webwx_auth_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("webwx_data_ticket", LoginHelper.LoginPageCookie.webwx_data_ticket, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("login_frequency", "1", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("last_wxuin", "LoginHelper.LoginPageCookie.wxuin", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxloadtime", "1495855277_expired", "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxuin", LoginHelper.LoginPageCookie.wxuin, "/", "wx.qq.com"));
            reqCookies.Add(new Cookie("wxsid", LoginHelper.LoginPageCookie.wxsid, "/", "wx.qq.com"));

            CookieContainer resCookies = null;

            var SyncUrl = string.Format(SyncUrlTemple, LoginHelper.LoginPageCookie.wxsid, LoginHelper.LoginPageXml.skey, LoginHelper.LoginPageXml.pass_ticket);

            var dic = new Dictionary<string, string>();

            dic.Add(WebClientUtil.JsonDataPrex, json);

            return SerializeUtil.Deserialize<WXSync>(WebClientUtil.GetResponseOnCookie(SyncUrl, "post", reqCookies, out resCookies, dic, "application/json;charset=UTF-8"));
        }

    }
}
