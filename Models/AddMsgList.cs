/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：067df33e-2e2c-470a-acb8-4759cedd5df6
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：AddMsgList
 * 创建时间：2017/6/1 10:41:59
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
    public class AddMsgList
    {

        [JsonProperty("MsgId")]
        public string MsgId
        {
            get; set;
        }

        [JsonProperty("FromUserName")]
        public string FromUserName
        {
            get; set;
        }

        [JsonProperty("ToUserName")]
        public string ToUserName
        {
            get; set;
        }

        [JsonProperty("MsgType")]
        public int MsgType
        {
            get; set;
        }

        [JsonProperty("Content")]
        public string Content
        {
            get; set;
        }

        [JsonProperty("Status")]
        public int Status
        {
            get; set;
        }

        [JsonProperty("ImgStatus")]
        public int ImgStatus
        {
            get; set;
        }

        [JsonProperty("CreateTime")]
        public int CreateTime
        {
            get; set;
        }

        [JsonProperty("VoiceLength")]
        public int VoiceLength
        {
            get; set;
        }

        [JsonProperty("PlayLength")]
        public int PlayLength
        {
            get; set;
        }

        [JsonProperty("FileName")]
        public string FileName
        {
            get; set;
        }

        [JsonProperty("FileSize")]
        public string FileSize
        {
            get; set;
        }

        [JsonProperty("MediaId")]
        public string MediaId
        {
            get; set;
        }

        [JsonProperty("Url")]
        public string Url
        {
            get; set;
        }

        [JsonProperty("AppMsgType")]
        public int AppMsgType
        {
            get; set;
        }

        [JsonProperty("StatusNotifyCode")]
        public int StatusNotifyCode
        {
            get; set;
        }

        [JsonProperty("StatusNotifyUserName")]
        public string StatusNotifyUserName
        {
            get; set;
        }

        [JsonProperty("RecommendInfo")]
        public RecommendInfo RecommendInfo
        {
            get; set;
        }

        [JsonProperty("ForwardFlag")]
        public int ForwardFlag
        {
            get; set;
        }

        [JsonProperty("AppInfo")]
        public AppInfo AppInfo
        {
            get; set;
        }

        [JsonProperty("HasProductId")]
        public int HasProductId
        {
            get; set;
        }

        [JsonProperty("Ticket")]
        public string Ticket
        {
            get; set;
        }

        [JsonProperty("ImgHeight")]
        public int ImgHeight
        {
            get; set;
        }

        [JsonProperty("ImgWidth")]
        public int ImgWidth
        {
            get; set;
        }

        [JsonProperty("SubMsgType")]
        public int SubMsgType
        {
            get; set;
        }

        [JsonProperty("NewMsgId")]
        public long NewMsgId
        {
            get; set;
        }

        [JsonProperty("OriContent")]
        public string OriContent
        {
            get; set;
        }
    }
}
