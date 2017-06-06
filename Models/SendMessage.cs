/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：930e72cb-ef36-49eb-9052-b059b125005a
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：SendMessage
 * 创建时间：2017/5/27 14:16:18
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeiXin.Models
{
    public class SendMessage
    {
        public BaseRequest BaseRequest
        {
            get;
            set;
        }
        public Msg Msg
        {
            get;
            set;
        }
        public int Scene
        {
            get;
            set;
        }

        public SendMessage()
        {
            this.BaseRequest = new BaseRequest();
            this.Msg = new Msg();
        }

        public SendMessage(string uin, string sid, string skey, int type, string fromUserName, string toUserName, string content, string deviceID = "e526874475065566", string localID = "14958624652480405", string clientMsgId = "14958624652480405")
        {
            this.BaseRequest = new BaseRequest()
            {
                Uin = uin,
                Sid = sid,
                Skey = skey,
                DeviceID = deviceID
            };
            this.Msg = new Msg()
            {
                Type = type,
                FromUserName = fromUserName,
                ToUserName = toUserName,
                Content = content,
                LocalID = localID,
                ClientMsgId = clientMsgId
            };
        }
    }

    public class BaseRequest
    {
        public string Uin
        {
            get;
            set;
        }
        public string Sid
        {
            get;
            set;
        }
        public string Skey
        {
            get;
            set;
        }
        public string DeviceID
        {
            get;
            set;
        }
    }

    public class Msg
    {
        public int Type
        {
            get; set;
        }
        public string Content
        {
            get;
            set;
        }
        public string FromUserName
        {
            get;
            set;
        }
        public string ToUserName
        {
            get;
            set;
        }
        public string LocalID
        {
            get;
            set;
        }
        public string ClientMsgId
        {
            get;
            set;
        }

    }
}
