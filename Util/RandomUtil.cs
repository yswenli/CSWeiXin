/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：afe214d5-aaa6-47fa-9ded-016025e7ed17
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：RandomUtil
 * 创建时间：2017/5/27 15:24:43
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeiXin.Util
{
    public sealed class RandomUtil
    {
        public static string GetRandom()
        {
            Random rd = new Random();
            var result1 = rd.Next(10000000, 99999999);
            var result2 = rd.Next(10, 99);
            return result1.ToString() + result2.ToString();
        }

        public static string GetRandom(int len = 10)
        {
            var result = string.Empty;

            Random rd = new Random();

            for (int i = 0; i < len; i++)
            {
                result += rd.Next(10000000, 99999999);
            }

            return result;
        }
    }
}
