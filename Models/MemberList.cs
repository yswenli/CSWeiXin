/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：93278c9d-6439-4832-80ce-cd8e1642697e
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：MemberList
 * 创建时间：2017/6/1 9:39:35
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
    public class MemberList
    {

        [JsonProperty("Uin")]
        public int Uin
        {
            get; set;
        }

        [JsonProperty("UserName")]
        public string UserName
        {
            get; set;
        }

        [JsonProperty("NickName")]
        public string NickName
        {
            get; set;
        }

        [JsonProperty("HeadImgUrl")]
        public string HeadImgUrl
        {
            get; set;
        }

        [JsonProperty("ContactFlag")]
        public int ContactFlag
        {
            get; set;
        }

        [JsonProperty("MemberCount")]
        public int SMemberCount
        {
            get; set;
        }

        [JsonProperty("MemberList")]
        public IList<object> SMemberList
        {
            get; set;
        }

        [JsonProperty("RemarkName")]
        public string RemarkName
        {
            get; set;
        }

        [JsonProperty("HideInputBarFlag")]
        public int HideInputBarFlag
        {
            get; set;
        }

        [JsonProperty("Sex")]
        public int Sex
        {
            get; set;
        }

        [JsonProperty("Signature")]
        public string Signature
        {
            get; set;
        }

        [JsonProperty("VerifyFlag")]
        public int VerifyFlag
        {
            get; set;
        }

        [JsonProperty("OwnerUin")]
        public int OwnerUin
        {
            get; set;
        }

        [JsonProperty("PYInitial")]
        public string PYInitial
        {
            get; set;
        }

        [JsonProperty("PYQuanPin")]
        public string PYQuanPin
        {
            get; set;
        }

        [JsonProperty("RemarkPYInitial")]
        public string RemarkPYInitial
        {
            get; set;
        }

        [JsonProperty("RemarkPYQuanPin")]
        public string RemarkPYQuanPin
        {
            get; set;
        }

        [JsonProperty("StarFriend")]
        public int StarFriend
        {
            get; set;
        }

        [JsonProperty("AppAccountFlag")]
        public int AppAccountFlag
        {
            get; set;
        }

        [JsonProperty("Statues")]
        public int Statues
        {
            get; set;
        }

        [JsonProperty("AttrStatus")]
        public object AttrStatus
        {
            get; set;
        }

        [JsonProperty("Province")]
        public string Province
        {
            get; set;
        }

        [JsonProperty("City")]
        public string City
        {
            get; set;
        }

        [JsonProperty("Alias")]
        public string Alias
        {
            get; set;
        }

        [JsonProperty("SnsFlag")]
        public int SnsFlag
        {
            get; set;
        }

        [JsonProperty("UniFriend")]
        public int UniFriend
        {
            get; set;
        }

        [JsonProperty("DisplayName")]
        public string DisplayName
        {
            get; set;
        }

        [JsonProperty("ChatRoomId")]
        public int ChatRoomId
        {
            get; set;
        }

        [JsonProperty("KeyWord")]
        public string KeyWord
        {
            get; set;
        }

        [JsonProperty("EncryChatRoomId")]
        public string EncryChatRoomId
        {
            get; set;
        }

        [JsonProperty("IsOwner")]
        public int IsOwner
        {
            get; set;
        }
    }
}
