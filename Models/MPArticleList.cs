/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：3094ef4d-dc8f-4e3f-8f54-8fef1177fa93
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：MPArticleList
 * 创建时间：2017/6/1 9:43:18
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
    public class MPArticleList
    {

        [JsonProperty("Title")]
        public string Title
        {
            get; set;
        }

        [JsonProperty("Digest")]
        public string Digest
        {
            get; set;
        }

        [JsonProperty("Cover")]
        public string Cover
        {
            get; set;
        }

        [JsonProperty("Url")]
        public string Url
        {
            get; set;
        }
    }
}
