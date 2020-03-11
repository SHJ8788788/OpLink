using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasySocketService
{
    public class SysTimeSetting
    {   
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool SetLocalTime(ref SYSTEMTIME lpSystemTime);
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;    // ignored for the SetLocalTime function
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
    public static bool SetLocalTimeByStr(string timestr)
    {
        bool flag = false;
        SYSTEMTIME sysTime = new SYSTEMTIME();
        string SysTime = timestr.Trim();   //此步骤多余，为方便程序而用直接用timestr即可
        sysTime.wYear = Convert.ToUInt16(SysTime.Substring(0, 4));
        sysTime.wMonth = Convert.ToUInt16(SysTime.Substring(4, 2));
        sysTime.wDay = Convert.ToUInt16(SysTime.Substring(6, 2));
        sysTime.wHour = Convert.ToUInt16(SysTime.Substring(8, 2));
        sysTime.wMinute = Convert.ToUInt16(SysTime.Substring(10, 2));
        sysTime.wSecond = Convert.ToUInt16(SysTime.Substring(12, 2));
        //注意：
        //结构体的wDayOfWeek属性一般不用赋值，函数会自动计算，写了如果不对应反而会出错
        //wMiliseconds属性默认值为一，可以赋值
        try
        {
            flag = SetLocalTime(ref sysTime);
        }
        //由于不是C#本身的函数，很多异常无法捕获
        //函数执行成功则返回true，函数执行失败返回false
        //经常不返回异常，不提示错误，但是函数返回false，给查找错误带来了一定的困难
        catch (Exception ex1)
        {
            Console.WriteLine("SetLocalTime函数执行异常" + ex1.Message);
        }
        return flag;
    }
}
}
