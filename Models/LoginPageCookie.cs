/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：865b5840-a6d2-484f-badd-c196734b070a
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：LoginPageCookie
 * 创建时间：2017/5/26 15:44:42
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSWeiXin.Models
{
    public class LoginPageCookie
    {
        public string mm_lang
        {
            get; set;
        }
        public string webwx_auth_ticket
        {
            get; set;
        }
        public string webwx_data_ticket
        {
            get;
            set;
        }
        public string webwxuvid
        {
            get;
            set;
        }
        public string wxloadtime
        {
            get;
            set;
        }
        public string wxsid
        {
            get;
            set;
        }
        public string wxuin
        {
            get;
            set;
        }


        public static LoginPageCookie Parse(CookieContainer cookies)
        {
            LoginPageCookie loginPageCookie = new LoginPageCookie();
            var cs = cookies.GetCookies(new Uri("https://wx.qq.com"));
            var type = typeof(LoginPageCookie);
            var properties = type.GetProperties();
            foreach (Cookie item in cs)
            {
                foreach (var p in properties)
                {
                    if (p.Name == item.Name)
                    {
                        p.SetValue(loginPageCookie, item.Value, null);
                    }
                }
            }
            cs = cookies.GetCookies(new Uri("https://login.wx.qq.com"));
            foreach (Cookie item in cs)
            {
                foreach (var p in properties)
                {
                    if (p.Name == item.Name)
                    {
                        p.SetValue(loginPageCookie, item.Value, null);
                    }
                }
            }
            return loginPageCookie;
        }

    }

}
