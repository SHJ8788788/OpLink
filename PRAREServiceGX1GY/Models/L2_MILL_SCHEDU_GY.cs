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
    [SugarTable("L2_MILL_SCHEDU_GY")]
    public partial class L2_MILL_SCHEDU_GY
    {
        public L2_MILL_SCHEDU_GY()
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
        /// Desc:风机风量代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FAN_CUR_CODE { get; set; }

        /// <summary>
        /// Desc:1#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_1 { get; set; }

        /// <summary>
        /// Desc:2#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_2 { get; set; }

        /// <summary>
        /// Desc:3#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_3 { get; set; }

        /// <summary>
        /// Desc:4#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_4 { get; set; }

        /// <summary>
        /// Desc:5#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_5 { get; set; }

        /// <summary>
        /// Desc:6#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_6 { get; set; }

        /// <summary>
        /// Desc:7#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_7 { get; set; }

        /// <summary>
        /// Desc:8#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_8 { get; set; }

        /// <summary>
        /// Desc:9#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_9 { get; set; }

        /// <summary>
        /// Desc:10#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_10 { get; set; }

        /// <summary>
        /// Desc:11#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_11 { get; set; }

        /// <summary>
        /// Desc:12#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_12 { get; set; }

        /// <summary>
        /// Desc:13#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_13 { get; set; }

        /// <summary>
        /// Desc:14#风机风量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short FAN_CUR_14 { get; set; }

        /// <summary>
        /// Desc:保温罩开/关代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_CODE { get; set; }

        /// <summary>
        /// Desc:保温罩1#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_1 { get; set; }

        /// <summary>
        /// Desc:保温罩2#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_2 { get; set; }

        /// <summary>
        /// Desc:保温罩3#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_3 { get; set; }

        /// <summary>
        /// Desc:保温罩4#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_4 { get; set; }

        /// <summary>
        /// Desc:保温罩5#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_5 { get; set; }

        /// <summary>
        /// Desc:保温罩6#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_6 { get; set; }

        /// <summary>
        /// Desc:保温罩7#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_7 { get; set; }

        /// <summary>
        /// Desc:保温罩8#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_8 { get; set; }

        /// <summary>
        /// Desc:保温罩9#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_9 { get; set; }

        /// <summary>
        /// Desc:保温罩10#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_10 { get; set; }

        /// <summary>
        /// Desc:保温罩11#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_11 { get; set; }

        /// <summary>
        /// Desc:保温罩12#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_12 { get; set; }

        /// <summary>
        /// Desc:保温罩13#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_13 { get; set; }

        /// <summary>
        /// Desc:保温罩14#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_14 { get; set; }

        /// <summary>
        /// Desc:保温罩15#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_15 { get; set; }

        /// <summary>
        /// Desc:保温罩16#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_16 { get; set; }

        /// <summary>
        /// Desc:保温罩17#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_17 { get; set; }

        /// <summary>
        /// Desc:保温罩18#
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HOLD_HAT_18 { get; set; }

        /// <summary>
        /// Desc:辊道速度代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GD_SPEED_TYPE { get; set; }

        /// <summary>
        /// Desc:S0段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S0 { get; set; }

        /// <summary>
        /// Desc:S1段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S1 { get; set; }

        /// <summary>
        /// Desc:S2段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S2 { get; set; }

        /// <summary>
        /// Desc:S3段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S3 { get; set; }

        /// <summary>
        /// Desc:S4段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S4 { get; set; }

        /// <summary>
        /// Desc:S5段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S5 { get; set; }

        /// <summary>
        /// Desc:S6段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S6 { get; set; }

        /// <summary>
        /// Desc:S7段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S7 { get; set; }

        /// <summary>
        /// Desc:S8段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S8 { get; set; }

        /// <summary>
        /// Desc:S9段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S9 { get; set; }

        /// <summary>
        /// Desc:S10段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single S10 { get; set; }

        /// <summary>
        /// Desc:集卷段
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Single COLLECT_AREA { get; set; }

    }
}
