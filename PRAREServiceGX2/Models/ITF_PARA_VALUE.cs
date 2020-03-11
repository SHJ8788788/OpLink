using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRAREServiceGX2.Models
{///<summary>
 ///
 ///</summary>
    [SugarTable("ITF_PARA_VALUE")]
    public partial class ITF_PARA_VALUE
    {
        public ITF_PARA_VALUE()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(OracleSequenceName = "Itf_Tran_ID_S")]
        public decimal? TRAN_ID { get; set; }

        /// <summary>
        /// Desc:设备类型 1 作业线 2 关键设备 0 都是	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? EQUIP_TYPE { get; set; }

        /// <summary>
        /// Desc:对应系统代码	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SOURCE_APP_CODE { get; set; }

        /// <summary>
        /// Desc:对应系统参数代码	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SOURCE_PARA_CODE { get; set; }

        /// <summary>
        /// Desc:参数值	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PARA_VALUE { get; set; }

        /// <summary>
        /// Desc:发生时间	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? TRAN_DATE { get; set; }

        /// <summary>
        /// Desc:接口发送时间	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? SEND_DATE { get; set; }

        /// <summary>
        /// Desc:接口获取时间	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? GET_DATE { get; set; }

        /// <summary>
        /// Desc:0 待定，1 已获取 9 异常	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? STATUS { get; set; }

        /// <summary>
        /// Desc:备注	
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NOTE { get; set; }

    }
}
