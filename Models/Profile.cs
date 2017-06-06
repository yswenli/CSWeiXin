/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：78230f16-fd69-45a6-9ecc-cc21389e61df
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：Profile
 * 创建时间：2017/6/1 10:42:32
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
    public class UserName
    {

        [JsonProperty("Buff")]
        public string Buff
        {
            get; set;
        }
    }

    public class NickName
    {

        [JsonProperty("Buff")]
        public string Buff
        {
            get; set;
        }
    }

    public class BindEmail
    {

        [JsonProperty("Buff")]
        public string Buff
        {
            get; set;
        }
    }

    public class BindMobile
    {

        [JsonProperty("Buff")]
        public string Buff
        {
            get; set;
        }
    }

    public class Profile
    {

        [JsonProperty("BitFlag")]
        public int BitFlag
        {
            get; set;
        }

        [JsonProperty("UserName")]
        public UserName UserName
        {
            get; set;
        }

        [JsonProperty("NickName")]
        public NickName NickName
        {
            get; set;
        }

        [JsonProperty("BindUin")]
        public int BindUin
        {
            get; set;
        }

        [JsonProperty("BindEmail")]
        public BindEmail BindEmail
        {
            get; set;
        }

        [JsonProperty("BindMobile")]
        public BindMobile BindMobile
        {
            get; set;
        }

        [JsonProperty("Status")]
        public int Status
        {
            get; set;
        }

        [JsonProperty("Sex")]
        public int Sex
        {
            get; set;
        }

        [JsonProperty("PersonalCard")]
        public int PersonalCard
        {
            get; set;
        }

        [JsonProperty("Alias")]
        public string Alias
        {
            get; set;
        }

        [JsonProperty("HeadImgUpdateFlag")]
        public int HeadImgUpdateFlag
        {
            get; set;
        }

        [JsonProperty("HeadImgUrl")]
        public string HeadImgUrl
        {
            get; set;
        }

        [JsonProperty("Signature")]
        public string Signature
        {
            get; set;
        }
    }
}
