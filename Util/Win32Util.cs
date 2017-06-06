/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：89c37cf4-6e23-41dd-9c04-1faa71c7b3b9
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：CSWeiXin.Util
 * 类名称：Win32Util
 * 创建时间：2017/6/5 17:06:12
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CSWeiXin.Util
{
    public class Win32Util
    {
        #region flashwindow
        [DllImport("user32.dll")]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr handle, bool invert);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeOut;
        }

        public const UInt32 FLASHW_STOP = 0x00000000;
        public const UInt32 FLASHW_CAPTION = 0x00000001;
        public const UInt32 FLASHW_TRAY = 0x00000002;
        public const UInt32 FLASHW_ALL = 0x00000003;
        public const UInt32 FLASHW_TIMER = 0x00000004;
        public const UInt32 FLASHW_TIMERNOFG = 0x0000000C;
        #endregion
    }
}
