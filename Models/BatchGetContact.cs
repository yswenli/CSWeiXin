/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：f6e4d178-3459-4200-a90e-f4b218d4206e
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：WXMemberList
 * 创建时间：2017/6/1 9:38:39
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
    public class BatchGetContact
    {

        [JsonProperty("BaseResponse")]
        public BaseResponse BaseResponse
        {
            get; set;
        }

        [JsonProperty("MemberCount")]
        public int MemberCount
        {
            get; set;
        }

        [JsonProperty("MemberList")]
        public IList<MemberList> MemberList
        {
            get; set;
        }

        [JsonProperty("Seq")]
        public int Seq
        {
            get; set;
        }
    }
}
