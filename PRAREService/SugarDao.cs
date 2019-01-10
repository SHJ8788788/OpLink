using OracleSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAREService
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
                //string reval = "Data Source=localhost/orcl;User ID=system;Password=JHL52771jhl;"; //这里可以动态根据cookies或session实现多库切换
  //              string reval = @"Data Source=(DESCRIPTION=
  //  (ADDRESS=
  //    (PROTOCOL=TCP)
  //    (HOST=10.100.200.142)
  //    (PORT=1521)
  //  )
  //  (CONNECT_DATA=
  //    (SERVER=dedicated)
  //    (SERVICE_NAME=SGSBGL)
  //  )
  //);User ID=klapp;Password = klapp;";

                string reval = @"Data Source= (DESCRIPTION=
    (ADDRESS=
      (PROTOCOL=TCP)
      (HOST=172.16.6.30)
      (PORT=1521)
    )
    (CONNECT_DATA=
      (SERVER=dedicated)
      (SERVICE_NAME=LZGX2L2)
    )
  );User ID=INFO;Password = ceri;";
                return reval;
            }
        }

        ///// <summary>
        ///// 唯一实例
        ///// </summary>
        private static readonly Lazy<SqlSugarClient> instance = new Lazy<SqlSugarClient>(() => new SqlSugarClient(ConnectionString));

        /// <summary>
        /// 获取唯一实例
        /// </summary>
        public static SqlSugarClient Instance
        {
            get { return instance.Value; }
        }

        //public static SqlSugarClient GetInstance()
        //{
        //    var db = new SqlSugarClient(ConnectionString);
        //    db.IsEnableLogEvent = true;//启用日志事件
        //    db.LogEventStarting = (sql, par) => { Console.WriteLine(sql + " " + par + "\r\n"); };
        //    return db;
        //}
    }
}
