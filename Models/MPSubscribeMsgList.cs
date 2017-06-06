/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：4d6d246f-8b83-4866-bbfb-1a1ad9e90fdc
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：MPSubscribeMsgList
 * 创建时间：2017/6/1 9:43:42
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
    public class MPSubscribeMsgList
    {

        [JsonProperty("UserName")]
        public string UserName
        {
            get; set;
        }

        [JsonProperty("MPArticleCount")]
        public int MPArticleCount
        {
            get; set;
        }

        [JsonProperty("MPArticleList")]
        public IList<MPArticleList> MPArticleList
        {
            get; set;
        }

        [JsonProperty("Time")]
        public int Time
        {
            get; set;
        }

        [JsonProperty("NickName")]
        public string NickName
        {
            get; set;
        }
    }
}
