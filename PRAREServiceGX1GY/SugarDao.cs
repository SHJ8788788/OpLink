using Log4Ex;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAREServiceGX1GY
{
    /// <summary>
    /// SqlSugar
    /// </summary>
    public class SugarDao
    {
        //禁止实例化
        private SugarDao()
        {

        }        

        public static string ConnectionString
        {
            get
            {
                string reval = @"Data Source= (DESCRIPTION=
    (ADDRESS=
      (PROTOCOL=TCP)
      (HOST=172.15.1.25)
      (PORT=1522)
    )
    (CONNECT_DATA=
      (SERVER=dedicated)
      (SERVICE_NAME=G1L2)
    )
  );User ID=G1L2;Password = G1L2;";
                return reval;
            }
        }


        /// <summary>
        /// 获取实例
        /// SqlSugar和.NET ADO的 SqlConnection是一样的，一个线程只能是一个实例，单实例跨线程使用是错误用法
        /// 属性每次会new新实例
        /// </summary>
        public static SqlSugarClient Instance
        {
            get
            {
                var db = new SqlSugarClient(ConnectionConfig);
                //用来打印Sql方便你调式    
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    string str = "\r\n" + sql + "\r\n" +
                    db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value));
                    LogHelper.Info(str.Replace("\n", "\n                                                                       "));
                    //Console.WriteLine(sql + "\r\n" +
                    //db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                    //Console.WriteLine();
                };
                return db;
            }          
        }

        //public static SqlSugarClient GetInstance()
        //{
        //    var db = new SqlSugarClient(ConnectionString);
        //    db.IsEnableLogEvent = true;//启用日志事件
        //    db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par + "\r\n"); };
        //    return db;
        //}

        //ORACLE
        public static ConnectionConfig ConnectionConfig
        {
            get
            {
                return new ConnectionConfig
                {
                    ConnectionString = ConnectionString,
                    DbType = SqlSugar.DbType.Oracle,
                    IsAutoCloseConnection = true
                };
            }
        }
    }
}
