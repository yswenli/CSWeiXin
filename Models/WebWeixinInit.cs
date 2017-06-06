/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：cd0df45c-b646-43ee-86a8-0ee6c0956933
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：WebWeixinInit
 * 创建时间：2017/5/27 15:00:24
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSWeiXin.Models
{
    public class WebWeixinInit
    {
        [JsonProperty("BaseResponse")]
        public BaseResponse BaseResponse
        {
            get; set;
        }

        [JsonProperty("Count")]
        public int Count
        {
            get; set;
        }

        [JsonProperty("ContactList")]
        public IList<ContactList> ContactList
        {
            get; set;
        }

        [JsonProperty("SyncKey")]
        public SyncKey SyncKey
        {
            get; set;
        }

        [JsonProperty("User")]
        public User User
        {
            get; set;
        }

        [JsonProperty("ChatSet")]
        public string ChatSet
        {
            get; set;
        }

        [JsonProperty("SKey")]
        public string SKey
        {
            get; set;
        }

        [JsonProperty("ClientVersion")]
        public int ClientVersion
        {
            get; set;
        }

        [JsonProperty("SystemTime")]
        public int SystemTime
        {
            get; set;
        }

        [JsonProperty("GrayScale")]
        public int GrayScale
        {
            get; set;
        }

        [JsonProperty("InviteStartCount")]
        public int InviteStartCount
        {
            get; set;
        }

        [JsonProperty("MPSubscribeMsgCount")]
        public int MPSubscribeMsgCount
        {
            get; set;
        }

        [JsonProperty("MPSubscribeMsgList")]
        public IList<MPSubscribeMsgList> MPSubscribeMsgList
        {
            get; set;
        }

        [JsonProperty("ClickReportInterval")]
        public int ClickReportInterval
        {
            get; set;
        }
    }
}
