using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAREServiceGX1GY.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("L2_FURN_DATA")]
    public partial class L2_FURN_DATA
    {
        public L2_FURN_DATA()
        {


        }
        /// <summary>
        /// Desc:<> 轧制计划号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string ROLL_PLAN_NO { get; set; }

        /// <summary>
        /// Desc:<> 材料号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string MAT_NO { get; set; }

        /// <summary>
        /// Desc:<> 试批号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SAMPLE_LOT_NO { get; set; }

        /// <summary>
        /// Desc:<> PONO号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PONO { get; set; }

        /// <summary>
        /// Desc:<> 加热炉号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FUR_NO { get; set; }

        /// <summary>
        /// Desc:<> 装炉时刻
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LOAD_FUR_TIME { get; set; }

        /// <summary>
        /// Desc:<> 入炉班次号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CHARGE_SHIFT_NO { get; set; }

        /// <summary>
        /// Desc:<> 入炉班组
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CHARGE_SHIFT_GROUP { get; set; }

        /// <summary>
        /// Desc:<> 生产状态（0: 正常; 1:撤消）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PROD_STATUS { get; set; }

        /// <summary>
        /// Desc:<> 称重时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? WEIGHT_TIME { get; set; }

        /// <summary>
        /// Desc:<> 实际入炉数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ACT_IN_NUM { get; set; }

        /// <summary>
        /// Desc:<KG> 实际入炉重量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? ACT_IN_WT { get; set; }

        /// <summary>
        /// Desc:<> 入炉长度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? CHARGING_LENGTH { get; set; }

        /// <summary>
        /// Desc:<℃> 实际入炉温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? CHARGE_TEMP_ACT { get; set; }

        /// <summary>
        /// Desc:<> 库位号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string STOCK_PLACE_NOV { get; set; }

        /// <summary>
        /// Desc:<> 加热炉类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FUR_TYPE { get; set; }

        /// <summary>
        /// Desc:<> 加热模式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HEAT_MODE { get; set; }

        /// <summary>
        /// Desc:<> 炉内列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FUR_LINE_NO { get; set; }

        /// <summary>
        /// Desc:<> 列内顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LINE_SEQ_NO { get; set; }

        /// <summary>
        /// Desc:<min> 加热时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? HEAT_TIME { get; set; }

        /// <summary>
        /// Desc:<> 出炉时刻
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DISCH_FUR_TIME { get; set; }

        /// <summary>
        /// Desc:<> 出炉班次号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DISCHA_SHIFT_NO { get; set; }

        /// <summary>
        /// Desc:<> 出炉班组
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DISCH_SHIFT_GROUP { get; set; }

        /// <summary>
        /// Desc:<> 实际出炉数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ACT_OUTPUT_NUM { get; set; }

        /// <summary>
        /// Desc:<KG> 实际出炉重量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? ACT_OUTPUT_WT { get; set; }

        /// <summary>
        /// Desc:<℃> 实际出炉温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DISCH_TEMP_ACT { get; set; }

        /// <summary>
        /// Desc:<> 炉前剔料轧号内流水号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ROLL_RETURN_SEQ_NO { get; set; }

        /// <summary>
        /// Desc:<> 实绩处理区分（1炉前剔料，2炉后剔料）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OPERATE_DIV { get; set; }

        /// <summary>
        /// Desc:<> 剔料时刻
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? RETURN_TIME { get; set; }

        /// <summary>
        /// Desc:<> 剔料班次号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RETURN_SHIFT_NO { get; set; }

        /// <summary>
        /// Desc:<> 剔料班组
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RETURN_SHIFT_GROUP { get; set; }

        /// <summary>
        /// Desc:<> 剔料数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? RETURN_NUM { get; set; }

        /// <summary>
        /// Desc:<> 剔料重量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? RETURN_WT { get; set; }

        /// <summary>
        /// Desc:<> 剔料原因
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RETURN_REASON { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N1
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N1 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N2 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N3
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N3 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N4
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N4 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N5
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N5 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N6
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N6 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N7
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N7 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N8
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N8 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_N9
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? SPARE_ITEM_N9 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C1
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C1 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C2 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C3
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C3 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C4
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C4 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C5
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C5 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C6
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C6 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C7
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C7 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C8
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C8 { get; set; }

        /// <summary>
        /// Desc:<> 备用字段_C9
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPARE_ITEM_C9 { get; set; }

        /// <summary>
        /// Desc:<--> Time of Create
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? TOC { get; set; }

        /// <summary>
        /// Desc:<--> Time of Modify
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? TOM { get; set; }

        /// <summary>
        /// Desc:<--> Program name
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MOP { get; set; }

        /// <summary>
        /// Desc:<--> Program name
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MOU { get; set; }

    }
}
