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
[SugarTable("L2_MILL_SCHEDU_MNG")]
public partial class L2_MILL_SCHEDU_MNG
{
    public L2_MILL_SCHEDU_MNG()
    {


    }
    /// <summary>
    /// Desc:<--> 轧号
    /// Default:
    /// Nullable:False
    /// </summary>           
    [SugarColumn(IsPrimaryKey = true)]
    public string ROLL_PLAN_NO { get; set; }

    /// <summary>
    /// Desc:<> 原材料来料支数(3)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? ORI_BRANCH_COUNT { get; set; }

    /// <summary>
    /// Desc:<> 生产开始时间（装炉）
    /// Default:
    /// Nullable:True
    /// </summary>           
    public DateTime? PROD_START { get; set; }

    /// <summary>
    /// Desc:<> 生产结束时间（装炉）
    /// Default:
    /// Nullable:True
    /// </summary>           
    public DateTime? PROD_END { get; set; }

    /// <summary>
    /// Desc:<> 生产班次（装炉）
    /// Default:
    /// Nullable:True
    /// </summary>           
    public string PROD_SHIFT_NO { get; set; }

    /// <summary>
    /// Desc:<> 生产班别（装炉）
    /// Default:
    /// Nullable:True
    /// </summary>           
    public string PROD_CREW_NO { get; set; }

    /// <summary>
    /// Desc:<> 材料生产支数(3)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? PROD_BRANCH_COUNT { get; set; }

    /// <summary>
    /// Desc:<> 材料撤销支数(3)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? REMOVE_BRANCH_COUNT { get; set; }

    /// <summary>
    /// Desc:<> 炉前剔料支数(3)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? REMOVE_FW_COUNT { get; set; }

    /// <summary>
    /// Desc:<> 炉后剔料支数(3)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? REMOVE_BW_COUNT { get; set; }

    /// <summary>
    /// Desc:<> 轧制计划的状态(0:初始,1:计划开始,2:计划结束,3:计划强制终止,4:轧批卸卷完成)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? ORDER_STATUS { get; set; }

    /// <summary>
    /// Desc:<> 装炉状态（0:未装炉，1:已装炉）
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? CHARGE_STATUS { get; set; }

    /// <summary>
    /// Desc:<> 装炉支数
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? CHARGE_BRANCH { get; set; }

    /// <summary>
    /// Desc:<> 轧制计划轧制状态(0:等待轧制 1:开始轧制 2:轧制结束)位数：1
    /// Default:
    /// Nullable:True
    /// </summary>           
    public string MILL_STATUS { get; set; }

    /// <summary>
    /// Desc:<> 开始轧制时间(批号中第一根进入1机架)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public DateTime? MILL_START { get; set; }

    /// <summary>
    /// Desc:<> 结束轧制时间(批号中最后一根进离开Kocks轧机)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public DateTime? MILL_END { get; set; }

    /// <summary>
    /// Desc:<> 轧制计划顺序(5)
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? ORDER_SEQ { get; set; }

    /// <summary>
    /// Desc:<> 用户定义的3位代码
    /// Default:
    /// Nullable:True
    /// </summary>           
    public string CHECKCODE { get; set; }

    /// <summary>
    /// Desc:<> 预定库位
    /// Default:
    /// Nullable:True
    /// </summary>           
    public string STOCK_PLACE_NO { get; set; }

    /// <summary>
    /// Desc:<> 称重标记,0:未称重,1:称重开始,2:称重完成
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? WEIGHTFLAG { get; set; }

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

    /// <summary>
    /// Desc:生产计划顺序
    /// Default:
    /// Nullable:True
    /// </summary>           
    public decimal? PROD_SEQ { get; set; }

}
}
