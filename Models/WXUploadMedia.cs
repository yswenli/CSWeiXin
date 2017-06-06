/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：527ce226-20bc-4637-a42b-1d50189216c6
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：WXUploadMedia
 * 创建时间：2017/6/2 10:44:49
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
    [JsonObject]
    public class WXUploadMedia
    {
        [JsonProperty("BaseResponse")]
        public BaseResponse BaseResponse
        {
            get; set;
        }

        [JsonProperty("MediaId")]
        public string MediaId
        {
            get; set;
        }

        [JsonProperty("StartPos")]
        public int StartPos
        {
            get; set;
        }

        [JsonProperty("CDNThumbImgHeight")]
        public int CDNThumbImgHeight
        {
            get; set;
        }

        [JsonProperty("CDNThumbImgWidth")]
        public int CDNThumbImgWidth
        {
            get; set;
        }
    }
}
