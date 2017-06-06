/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：4e0da602-42ae-4d00-b2c7-f50739aeaac3
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：RecommendInfo
 * 创建时间：2017/6/1 10:43:22
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
    public class RecommendInfo
    {

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

        [JsonProperty("QQNum")]
        public int QQNum
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

        [JsonProperty("Content")]
        public string Content
        {
            get; set;
        }

        [JsonProperty("Signature")]
        public string Signature
        {
            get; set;
        }

        [JsonProperty("Alias")]
        public string Alias
        {
            get; set;
        }

        [JsonProperty("Scene")]
        public int Scene
        {
            get; set;
        }

        [JsonProperty("VerifyFlag")]
        public int VerifyFlag
        {
            get; set;
        }

        [JsonProperty("AttrStatus")]
        public int AttrStatus
        {
            get; set;
        }

        [JsonProperty("Sex")]
        public int Sex
        {
            get; set;
        }

        [JsonProperty("Ticket")]
        public string Ticket
        {
            get; set;
        }

        [JsonProperty("OpCode")]
        public int OpCode
        {
            get; set;
        }
    }
}
