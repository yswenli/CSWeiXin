/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：9551b502-db98-4a75-b67a-6dcea0a440f2
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：SendMessageResult
 * 创建时间：2017/5/31 16:30:01
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
    public class SendMessageResult
    {

        [JsonProperty("BaseResponse")]
        public BaseResponse BaseResponse
        {
            get; set;
        }

        [JsonProperty("MsgID")]
        public string MsgID
        {
            get; set;
        }

        [JsonProperty("LocalID")]
        public string LocalID
        {
            get; set;
        }
    }
}
