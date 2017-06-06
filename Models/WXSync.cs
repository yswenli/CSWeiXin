/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：19eed6d6-52dd-44d3-ab61-f7cc99166bce
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：WXSync
 * 创建时间：2017/6/1 10:39:12
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
    public class WXSync
    {

        [JsonProperty("BaseResponse")]
        public BaseResponse BaseResponse
        {
            get; set;
        }

        [JsonProperty("AddMsgCount")]
        public int AddMsgCount
        {
            get; set;
        }

        [JsonProperty("AddMsgList")]
        public IList<AddMsgList> AddMsgList
        {
            get; set;
        }

        [JsonProperty("ModContactCount")]
        public int ModContactCount
        {
            get; set;
        }

        [JsonProperty("ModContactList")]
        public IList<object> ModContactList
        {
            get; set;
        }

        [JsonProperty("DelContactCount")]
        public int DelContactCount
        {
            get; set;
        }

        [JsonProperty("DelContactList")]
        public IList<object> DelContactList
        {
            get; set;
        }

        [JsonProperty("ModChatRoomMemberCount")]
        public int ModChatRoomMemberCount
        {
            get; set;
        }

        [JsonProperty("ModChatRoomMemberList")]
        public IList<object> ModChatRoomMemberList
        {
            get; set;
        }

        [JsonProperty("Profile")]
        public Profile Profile
        {
            get; set;
        }

        [JsonProperty("ContinueFlag")]
        public int ContinueFlag
        {
            get; set;
        }

        [JsonProperty("SyncKey")]
        public SyncKey SyncKey
        {
            get; set;
        }

        [JsonProperty("SKey")]
        public string SKey
        {
            get; set;
        }

        [JsonProperty("SyncCheckKey")]
        public SyncCheckKey SyncCheckKey
        {
            get; set;
        }
    }
}
