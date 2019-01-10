using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpcClient
{
    /// <summary>
    /// 提供string的扩展
    /// </summary>
    public static class StringExtend
    {
        //
        // 摘要:
        //     指示指定的字符串是 null 还是 System.String.Empty 字符串。
        //
        // 参数:
        //   value:
        //     要测试的字符串。
        //
        // 返回结果:
        //     如果 value 参数为 null 或空字符串 ("")，则为 true；否则为 false。
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        //
        // 摘要:
        //     指示指定的字符串是 null、空还是仅由空白字符组成。
        //
        // 参数:
        //   value:
        //     要测试的字符串。
        //
        // 返回结果:
        //     如果 value 参数为 null 或 System.String.Empty，或者如果 value 仅由空白字符组成，则为 true。
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
