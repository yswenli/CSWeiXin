/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：f6e737af-121d-4c60-8a24-4d967bce87f3
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：LoginPageXml
 * 创建时间：2017/5/27 13:45:40
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CSWeiXin.Models
{
    public class LoginPageXml
    {
        public string skey
        {
            get;
            set;
        }

        public string wxsid
        {
            get;
            set;
        }

        public string pass_ticket
        {
            get;
            set;
        }

        public static LoginPageXml Parse(string xmlStr)
        {
            var xml = XElement.Parse(xmlStr);
            var instance = new LoginPageXml();
            instance.skey = xml.Element("skey").Value;
            instance.wxsid = xml.Element("wxsid").Value;
            instance.pass_ticket = xml.Element("pass_ticket").Value;
            return instance;
        }
    }
}
