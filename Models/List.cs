/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：94682eda-df7f-4cf4-86e2-0828cd716605
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Models
 * 类名称：List
 * 创建时间：2017/6/1 9:41:59
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
    public class List
    {

        [JsonProperty("Key")]
        public int Key
        {
            get; set;
        }

        [JsonProperty("Val")]
        public int Val
        {
            get; set;
        }
    }
}
