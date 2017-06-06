/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：fc15f20f-ade2-4c00-9387-459f8ecc9664
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：User
 * 创建时间：2017/6/1 9:42:55
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
    public class User
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

        [JsonProperty("RemarkName")]
        public string RemarkName
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

        [JsonProperty("HideInputBarFlag")]
        public int HideInputBarFlag
        {
            get; set;
        }

        [JsonProperty("StarFriend")]
        public int StarFriend
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

        [JsonProperty("AppAccountFlag")]
        public int AppAccountFlag
        {
            get; set;
        }

        [JsonProperty("VerifyFlag")]
        public int VerifyFlag
        {
            get; set;
        }

        [JsonProperty("ContactFlag")]
        public int ContactFlag
        {
            get; set;
        }

        [JsonProperty("WebWxPluginSwitch")]
        public int WebWxPluginSwitch
        {
            get; set;
        }

        [JsonProperty("HeadImgFlag")]
        public int HeadImgFlag
        {
            get; set;
        }

        [JsonProperty("SnsFlag")]
        public int SnsFlag
        {
            get; set;
        }
    }
}
