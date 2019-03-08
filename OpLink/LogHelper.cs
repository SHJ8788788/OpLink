using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpLink
{
    //log4net二次封装，支持动态文件名，按日期和大小自动分割文件
    public static class LogHelper
    {
        private static ILog _log = log4net.LogManager.GetLogger("AppLogger");

        public static void Info(object message)
        {
            if (_log == null)
            {
                return;
            }
            message = " Msg: " + message;
            _log.Info(message);
        }
        public static void Debug(object message)
        {
            if (_log == null)
            {
                return;
            }
            message = " Msg: " + message;
            _log.Debug(message);
        }
        public static void Error(object message)
        {
            if (_log == null)
            {
                return;
            }
            message = " Msg: " + message;
            _log.Error(message);
        }
        public static void Fatal(object message)
        {
            if (_log == null)
            {
                return;
            }
            message = " Msg: " + message;
            _log.Fatal(message);
        }
    }
}
