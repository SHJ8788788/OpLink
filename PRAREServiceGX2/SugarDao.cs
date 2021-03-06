﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForPRAREGX2
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
                //              string reval = @"Data Source= (DESCRIPTION=
                //  (ADDRESS=
                //    (PROTOCOL=TCP)
                //    (HOST=172.22.197.45)
                //    (PORT=1521)
                //  )
                //  (CONNECT_DATA=
                //    (SERVER=dedicated)
                //    (SERVICE_NAME=ORCLFORTEST)
                //  )
                //);User ID=INFO;Password = ceri;";
                string reval = @"Data Source=(DESCRIPTION=
                  (ADDRESS=
                    (PROTOCOL=TCP)
                    (HOST=10.100.200.142)
                    (PORT=1521)
                  )
                  (CONNECT_DATA=
                    (SERVER=dedicated)
                    (SERVICE_NAME=SGSBGL)
                  )
                );User ID=klapp;Password = klapp;";
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
            get { return new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.Oracle,
                IsAutoCloseConnection = true
            }); }
            //get { return instance.Value; }
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
