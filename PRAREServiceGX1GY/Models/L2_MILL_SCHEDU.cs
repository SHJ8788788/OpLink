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
    [SugarTable("L2_MILL_SCHEDU")]
    public partial class L2_MILL_SCHEDU
    {
        public L2_MILL_SCHEDU()
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
        /// Desc:<> 轧制计划顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? CHARGE_SEQ { get; set; }

        /// <summary>
        /// Desc:<> 制造命令号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PONO { get; set; }

        /// <summary>
        /// Desc:<> 出钢记号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ST_NO { get; set; }

        /// <summary>
        /// Desc:<> 合计材料件数(位数：5)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TOTAL_MAT_NUM { get; set; }

        /// <summary>
        /// Desc:<> 合计材料重量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? TOTAL_MAT_WT { get; set; }

        /// <summary>
        /// Desc:<mm> 入口材料厚度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? IN_MAT_THICK { get; set; }

        /// <summary>
        /// Desc:<mm> 入口材料宽度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? IN_MAT_WIDTH { get; set; }

        /// <summary>
        /// Desc:<mm> 出口材料厚度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? OUT_MAT_THICK { get; set; }

        /// <summary>
        /// Desc:<mm> 出口材料宽度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? OUT_MAT_WIDTH { get; set; }

        /// <summary>
        /// Desc:<> 椭圆度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? OVAL { get; set; }

        /// <summary>
        /// Desc:<> 生产日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? PROD_DATE { get; set; }

        /// <summary>
        /// Desc:<> 分卷标志
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DIVIDE_FLAG { get; set; }

        /// <summary>
        /// Desc:<> 检验材号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TEST_NO { get; set; }

        /// <summary>
        /// Desc:<> 归并合同号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MERG_ORDER_NO { get; set; }

        /// <summary>
        /// Desc:<> 紧急合同标记
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string URG_ORDER_FLAG { get; set; }

        /// <summary>
        /// Desc:<> 产品最终用途码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string APN { get; set; }

        /// <summary>
        /// Desc:<> 取样要求代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SAMPLE_REQ_CODE { get; set; }

        /// <summary>
        /// Desc:<> 牌号（钢级）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SG_SIGN { get; set; }

        /// <summary>
        /// Desc:<> 标准
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SG_STD { get; set; }

        /// <summary>
        /// Desc:<> 特殊标记类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MARK_TYPE_SPECAIL { get; set; }

        /// <summary>
        /// Desc:<> 特殊包装类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PACK_TYPE_SPECAIL { get; set; }

        /// <summary>
        /// Desc:<> 包装类型代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PACK_TYPE_CODE { get; set; }

        /// <summary>
        /// Desc:<> 出口标记
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EXPORT_FLAG { get; set; }

        /// <summary>
        /// Desc:<> 合同特殊要求
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ORDER_SPECIAL { get; set; }

        /// <summary>
        /// Desc:<> 取样代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SAMPLE_CODE { get; set; }

        /// <summary>
        /// Desc:<> 试样代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TEST_CODE { get; set; }

        /// <summary>
        /// Desc:<mm> 直径/边径公差下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? DIA_BORDER_TOL_MIN { get; set; }

        /// <summary>
        /// Desc:<mm> 直径/边径公差上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? DIA_BORDER_TOL_MAX { get; set; }

        /// <summary>
        /// Desc:<> 椭圆度公差上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? OVAL_TOL_MAX { get; set; }

        /// <summary>
        /// Desc:<> 出炉温度目标值(位数：4)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HEAT_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<min> 均热时间(位数：4)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SOAK_TIME { get; set; }

        /// <summary>
        /// Desc:<> 脱碳控制代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DECARBON_CTRL_CODE { get; set; }

        /// <summary>
        /// Desc:<> 轧制作业代号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ROLLING_PRAC_CODE { get; set; }

        /// <summary>
        /// Desc:<℃> 进NTM温度最小值(位数：4)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? NTM_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<℃> 进NTM温度目标值(位数：4)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? NTM_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<℃> 进NTM温度最大值(位数：4)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? NTM_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<℃> 进RSM温度最小值(位数：4)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? RSM_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<℃> 进RSM温度目标值(位数：4)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? RSM_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<℃> 进RSM温度最大值(位数：4)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? RSM_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 空冷作业代号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AIR_COOLING_PRAC_CODE { get; set; }

        /// <summary>
        /// Desc:<> 水冷作业代号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WATER_COOLING_PRAC_CODE { get; set; }

        /// <summary>
        /// Desc:<> 产品等级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PROD_LEVEL { get; set; }

        /// <summary>
        /// Desc:<> 检验要求
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string INSPECT_REQ { get; set; }

        /// <summary>
        /// Desc:<> 均热作业代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SOAK_TASK_CODE { get; set; }

        /// <summary>
        /// Desc:<> 新产品标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NEW_PROD_FLAG { get; set; }

        /// <summary>
        /// Desc:<> 系列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SERIES_NO { get; set; }

        /// <summary>
        /// Desc:<> 子系列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SUB_SERIES_NO { get; set; }

        /// <summary>
        /// Desc:<> 技术通知书号(合集)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TECH_TEXT_TOL { get; set; }

        /// <summary>
        /// Desc:<> 计划备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REMARK_PS { get; set; }

        /// <summary>
        /// Desc:<> 计划执行顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PLAN_EXEC_SEQ_NO { get; set; }

        /// <summary>
        /// Desc:<> 转料标志（1：转料 0：非转料）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALLOCATE_FLAG { get; set; }

        /// <summary>
        /// Desc:<> 原轧制计划号（REALLOCATE_FLAG=1时，此字段为转料过来的原始计划号）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OLD_ROLL_PLAN_NO { get; set; }

        /// <summary>
        /// Desc:<> 钢种大类
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string STEEL_TYPE { get; set; }

        /// <summary>
        /// Desc:<> 空冷方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AIR_COLD { get; set; }

        /// <summary>
        /// Desc:<> 最终用户单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FINAL_CUSTOMER { get; set; }

        /// <summary>
        /// Desc:<> 标牌类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SIGNS_TYPE { get; set; }

        /// <summary>
        /// Desc:<> 头尾标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HEAD_TAIL_SIGN { get; set; }

        /// <summary>
        /// Desc:<> 捆带类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BAND_TYPE { get; set; }

        /// <summary>
        /// Desc:<> 吊运方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LIFTING_MODE { get; set; }

        /// <summary>
        /// Desc:<> 小卷率（订货短尺率）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? ORDER_SHORT_TATE { get; set; }

        /// <summary>
        /// Desc:<> 卷重目标值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ORDER_UNIT_AIM_WT { get; set; }

        /// <summary>
        /// Desc:<> 卷重最大值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ORDER_UNIT_MAX_WT { get; set; }

        /// <summary>
        /// Desc:<> 卷重最小值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ORDER_UNIT_MIN_WT { get; set; }

        /// <summary>
        /// Desc:<> 小卷最大卷重
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DMAX_PACK_WT { get; set; }

        /// <summary>
        /// Desc:<> 小卷最小卷重
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DMIN_PACK_WT { get; set; }

        /// <summary>
        /// Desc:<> 轧制计划号(变更后)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ROLL_PLAN_NO_NEW { get; set; }

        /// <summary>
        /// Desc:<> 试批号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SAMPLE_LOT_NO { get; set; }

        /// <summary>
        /// Desc:<> 原检验批号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SAMPLE_LOT_NO_NEW { get; set; }

        /// <summary>
        /// Desc:<> 熔炼号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HEAT_NO { get; set; }

        /// <summary>
        /// Desc:<> 炉座号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FURNACE_NO { get; set; }

        /// <summary>
        /// Desc:<> 牌号代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SG_CODE { get; set; }

        /// <summary>
        /// Desc:<> 标准代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string STD_CODE { get; set; }

        /// <summary>
        /// Desc:<mm> 入口材料长度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? IN_MAT_LEN { get; set; }

        /// <summary>
        /// Desc:<mm> 入口材料最大长度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? IN_MAT_MAX_LEN { get; set; }

        /// <summary>
        /// Desc:<mm> 入口材料最小长度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? IN_MAT_MIN_LEN { get; set; }

        /// <summary>
        /// Desc:<> 截面形状代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CROSS_SHAPE_CODE { get; set; }

        /// <summary>
        /// Desc:<mm> 出口材料长度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? OUT_MAT_LEN { get; set; }

        /// <summary>
        /// Desc:<mm> 出口材料最小长度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? OUT_MAT_MIN_LEN { get; set; }

        /// <summary>
        /// Desc:<mm> 出口材料最大长度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? OUT_MAT_MAX_LEN { get; set; }

        /// <summary>
        /// Desc:<> 冷热标志
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string COLD_HOT_FLAG { get; set; }

        /// <summary>
        /// Desc:<> 纵肋高下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? RIB_HEIGHT_UP_LIMIT { get; set; }

        /// <summary>
        /// Desc:<> 纵肋高上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? RIB_HEIGHT_DOWN_LIMIT { get; set; }

        /// <summary>
        /// Desc:<> 纵肋宽下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? RIB_WIDTH_DOWN_LIMIT { get; set; }

        /// <summary>
        /// Desc:<> 纵肋宽上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? RIB_WIDT_DOWN_LIMIT { get; set; }

        /// <summary>
        /// Desc:<> 横肋高下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? TRAN_HEIGHT_DOWN_LIMIT { get; set; }

        /// <summary>
        /// Desc:<> 横肋高上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? TRAN_HEIGHT_UP_LIMIT { get; set; }

        /// <summary>
        /// Desc:<> 横肋宽下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? TRAN_WIDTH_DOWN_LIMIT { get; set; }

        /// <summary>
        /// Desc:<> 横肋宽上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? TRAN_WIDTH_UP_LIMIT { get; set; }

        /// <summary>
        /// Desc:<> 横肋间距最大值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? TRAN_RIB_MAX_SPACE { get; set; }

        /// <summary>
        /// Desc:<> 横肋间距最小值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? TRAN_RIB_MIN_SPACE { get; set; }

        /// <summary>
        /// Desc:<> 不圆度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? OUT_OF_ROUND_MAX { get; set; }

        /// <summary>
        /// Desc:<> 不圆度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? OUT_OF_ROUND_MIN { get; set; }

        /// <summary>
        /// Desc:<> 预热段时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_TIME { get; set; }

        /// <summary>
        /// Desc:<> 预热段温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_TEM_MIN { get; set; }

        /// <summary>
        /// Desc:<> 预热段温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_TEM_MAX { get; set; }

        /// <summary>
        /// Desc:<> 一加上加热段温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TEMP_HG_MIN_01 { get; set; }

        /// <summary>
        /// Desc:<> 一加上加热段温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TEMP_HG_MAX_01 { get; set; }

        /// <summary>
        /// Desc:<> 一加上加热段温度目标值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TEMP_HG_AIM_01 { get; set; }

        /// <summary>
        /// Desc:<> 一加热段时间下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TIME_MIN_01 { get; set; }

        /// <summary>
        /// Desc:<> 一加热段时间上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TIME_MAX_01 { get; set; }

        /// <summary>
        /// Desc:<> 二加上加热段温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TEMP_HG_MIN_02 { get; set; }

        /// <summary>
        /// Desc:<> 二加上加热段温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TEMP_HG_MAX_02 { get; set; }

        /// <summary>
        /// Desc:<> 二加上加热段温度目标值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TEMP_HG_AIM_02 { get; set; }

        /// <summary>
        /// Desc:<> 均热温度温度最小值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OAK_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<> 均热温度温度最大值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OAK_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 均热温度目标值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OAK_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 高温段时间上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HIGH_HEAT_TIME_MAX { get; set; }

        /// <summary>
        /// Desc:<> 高温段时间下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HIGH_HEAT_TIME_MIN { get; set; }

        /// <summary>
        /// Desc:<> 加热时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? BIL_MELT_TIME { get; set; }

        /// <summary>
        /// Desc:<> 出炉温度最小值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HEAT_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<> 出炉温度最大值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HEAT_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 加热炉曲线代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HEAT_DIAGRAM_CODE { get; set; }

        /// <summary>
        /// Desc:<> 加热炉曲线描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HEAT_DIAGRAM_DESC { get; set; }

        /// <summary>
        /// Desc:<> 出钢速度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? TAP_SPEED { get; set; }

        /// <summary>
        /// Desc:<> 上加热段温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TEMP_HG_MIN { get; set; }

        /// <summary>
        /// Desc:<> 上加热段温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HT_TEMP_HG_MAX { get; set; }

        /// <summary>
        /// Desc:<> 均热温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OAK_TEMP { get; set; }

        /// <summary>
        /// Desc:<> 加热时间最小值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? HEAT_TIME_MIN { get; set; }

        /// <summary>
        /// Desc:<> 加热时间最大值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? HEAT_TIME_MAX { get; set; }

        /// <summary>
        /// Desc:<> 连轧工艺规程号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ROLLING_REGULATE_NO { get; set; }

        /// <summary>
        /// Desc:<> 坯料截面形状代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string STUFF_CROSS_SHP_CODE { get; set; }

        /// <summary>
        /// Desc:<> 坯料来源代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BILLET_ORIGIN_CODE { get; set; }

        /// <summary>
        /// Desc:<> 坯料规格厚度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? STUFF_SPEC_THICK { get; set; }

        /// <summary>
        /// Desc:<> 坯料规格宽度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? STUFF_SPEC_WIDTH { get; set; }

        /// <summary>
        /// Desc:<> 除鳞压力
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? DESCALE_PRESS { get; set; }

        /// <summary>
        /// Desc:<> 开轧温度目标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ROLL_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 开轧温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ROLL_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<> 开轧温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ROLL_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 中轧温度目标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TEMP_ZM_AIM { get; set; }

        /// <summary>
        /// Desc:<> 中轧温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TEMP_ZM_MIN { get; set; }

        /// <summary>
        /// Desc:<> 中轧温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TEMP_ZM_MAX { get; set; }

        /// <summary>
        /// Desc:<> 预穿水出口温度最小值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_WATER_IN_TM_MIN { get; set; }

        /// <summary>
        /// Desc:<> 预穿水出口温度最大值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_WATER_IN_TM_MAX { get; set; }

        /// <summary>
        /// Desc:<> 精轧温度目标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TEMP_FM_AIM { get; set; }

        /// <summary>
        /// Desc:<> 精轧温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TEMP_FM_MIN { get; set; }

        /// <summary>
        /// Desc:<> 精轧温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TEMP_FM_MAX { get; set; }

        /// <summary>
        /// Desc:<> 减定径入口目标温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? JDJ_IN_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 减定径入口温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? JDJ_IN_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 减定径入口温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? JDJ_IN_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<> 吐丝温度最小值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LAYING_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<> 吐丝温度目标值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LAYING_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 吐丝温度最大值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LAYING_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 轧制速度 目标值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? ROLL_AIM_SPEED { get; set; }

        /// <summary>
        /// Desc:<> 终轧温度目标值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? AFFT_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 轧制速度平均值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? ROLL_SPEED_AVG { get; set; }

        /// <summary>
        /// Desc:<> 轧制速度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? ROLL_SPEED_MIN { get; set; }

        /// <summary>
        /// Desc:<> 轧制速度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? ROLL_SPEED_MAX { get; set; }

        /// <summary>
        /// Desc:<> 终轧温度最小值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? MIN_AFFT_TEMP { get; set; }

        /// <summary>
        /// Desc:<> 终轧温度最大值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? MAX_AFFT_TEMP { get; set; }

        /// <summary>
        /// Desc:<> 回复温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_IN_COOL_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<> 回复温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_IN_COOL_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 终轧速度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? AFFT_SPEED { get; set; }

        /// <summary>
        /// Desc:<> 辊道速度代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GD_SPEED_TYPE { get; set; }

        /// <summary>
        /// Desc:<> 保温罩开/关代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HOLD_HAT_CODE { get; set; }

        /// <summary>
        /// Desc:<> 风机风量代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FAN_CUR_CODE { get; set; }

        /// <summary>
        /// Desc:<> 集卷温度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public short? COLLECT_TEMP { get; set; }

        /// <summary>
        /// Desc:<> 冷却方式代码描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CLDN_PATTERN_DESC { get; set; }

        /// <summary>
        /// Desc:<> 冷却方式代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CLDN_PATTERN_CODE { get; set; }

        /// <summary>
        /// Desc:<> 上冷床温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ON_COOLBED_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<> 上冷床温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ON_COOLBED_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 上冷床温度目标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ON_COOLBED_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 下冷床温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OFF_COOLBED_TEMP_MIN { get; set; }

        /// <summary>
        /// Desc:<> 下冷床温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OFF_COOLBED_TEMP_MAX { get; set; }

        /// <summary>
        /// Desc:<> 下冷床温度目标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OFF_COOLBED_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 入坑温度目标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? IN_PIT_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 入坑温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? IN_PIT_TEMP_DOWN { get; set; }

        /// <summary>
        /// Desc:<> 入坑温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? IN_PIT_TEMP_UP { get; set; }

        /// <summary>
        /// Desc:<> 在坑时间下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? ON_PIT_TIME_DOWN { get; set; }

        /// <summary>
        /// Desc:<> 在坑时间上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public Single? ON_PIT_TIME_UP { get; set; }

        /// <summary>
        /// Desc:<> 出坑温度目标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OUT_PIT_TEMP_AIM { get; set; }

        /// <summary>
        /// Desc:<> 出坑温度下限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OUT_PIT_TEMP_DOWN { get; set; }

        /// <summary>
        /// Desc:<> 出坑温度上限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? OUT_PIT_TEMP_UP { get; set; }

        /// <summary>
        /// Desc:<> 除鳞后温度（平均）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? R_SQUA_TEMP_AVE { get; set; }

        /// <summary>
        /// Desc:<> C实际值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? C_VALUE { get; set; }

        /// <summary>
        /// Desc:<> SI实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SI_VALUE { get; set; }

        /// <summary>
        /// Desc:<> MN实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? MN_VALUE { get; set; }

        /// <summary>
        /// Desc:<> P实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? P_VALUE { get; set; }

        /// <summary>
        /// Desc:<> S实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? S_VALUE { get; set; }

        /// <summary>
        /// Desc:<> CU实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? CU_VALUE { get; set; }

        /// <summary>
        /// Desc:<> NI实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? NI_VALUE { get; set; }

        /// <summary>
        /// Desc:<> CR实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? CR_VALUE { get; set; }

        /// <summary>
        /// Desc:<> AS实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? AS_VALUE { get; set; }

        /// <summary>
        /// Desc:<> SN实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SN_VALUE { get; set; }

        /// <summary>
        /// Desc:<> NB实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? NB_VALUE { get; set; }

        /// <summary>
        /// Desc:<> V实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? V_VALUE { get; set; }

        /// <summary>
        /// Desc:<> AL实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? AL_VALUE { get; set; }

        /// <summary>
        /// Desc:<> TI实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TI_VALUE { get; set; }

        /// <summary>
        /// Desc:<> MO实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? MO_VALUE { get; set; }

        /// <summary>
        /// Desc:<> B实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? B_VALUE { get; set; }

        /// <summary>
        /// Desc:<> W实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? W_VALUE { get; set; }

        /// <summary>
        /// Desc:<> ZR实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ZR_VALUE { get; set; }

        /// <summary>
        /// Desc:<> CA实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? CA_VALUE { get; set; }

        /// <summary>
        /// Desc:<> CE实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? CE_VALUE { get; set; }

        /// <summary>
        /// Desc:<> H实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? H_VALUE { get; set; }

        /// <summary>
        /// Desc:<> O实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? O_VALUE { get; set; }

        /// <summary>
        /// Desc:<> N实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? N_VALUE { get; set; }

        /// <summary>
        /// Desc:<> SOL_AL实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SOL_AL_VALUE { get; set; }

        /// <summary>
        /// Desc:<> SB实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SB_VALUE { get; set; }

        /// <summary>
        /// Desc:<> MG实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? MG_VALUE { get; set; }

        /// <summary>
        /// Desc:<> PB实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PB_VALUE { get; set; }

        /// <summary>
        /// Desc:<> LA实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LA_VALUE { get; set; }

        /// <summary>
        /// Desc:<> CO实绩值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? CO_VALUE { get; set; }

        /// <summary>
        /// Desc:<> 特殊元素1
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TS1_VALUE { get; set; }

        /// <summary>
        /// Desc:<> 特殊元素2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TS2_VALUE { get; set; }

        /// <summary>
        /// Desc:<> 特殊元素3
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TS3_VALUE { get; set; }

        /// <summary>
        /// Desc:<> 标牌类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string STEEL_TYPE_4 { get; set; }

        /// <summary>
        /// Desc:<> 质保书标准
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string STEEL_MARKING_METHOD { get; set; }

        /// <summary>
        /// Desc:<> 计重方式1-理重0-实重
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WEIGHT_TYPE { get; set; }

        /// <summary>
        /// Desc:<> 米重
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double? WT_PER_METER { get; set; }

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
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BAK1 { get; set; }

        /// <summary>
        /// Desc:打印用牌号信息
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BAK2 { get; set; }

        /// <summary>
        /// Desc:特殊标牌标记0-不需要，1-需要
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BAK3 { get; set; }

        /// <summary>
        /// Desc:产品名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BAK4 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BAK5 { get; set; }

    }
}
