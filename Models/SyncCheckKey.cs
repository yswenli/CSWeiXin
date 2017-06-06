/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：75966cc0-d2e3-47c4-a3e7-a702b5b595e9
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：SyncCheckKey
 * 创建时间：2017/6/1 10:44:05
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
    public class SyncCheckKey
    {
        [JsonProperty("Count")]
        public int Count
        {
            get; set;
        }

        [JsonProperty("List")]
        public IList<List> List
        {
            get; set;
        }
    }
}
