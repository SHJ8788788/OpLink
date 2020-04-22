using Log4Ex;
using OpcClient;
using OpLink.Interface;
using PRAREServiceGX1GY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PRAREServiceGX1GY
{
    public class ServiceForPRAREGX1GY: TagServiceBase
    { 
        private IOpcClient opcClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="opcClient">opc客户端</param>
        /// <param name="runInterval">刷新时间间隔</param>
        public ServiceForPRAREGX1GY(IOpcClient opcClient,int runInterval):base(runInterval=5000)
        {
            this.opcClient = opcClient;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="opcClient">opc客户端</param>
        /// <param name="runInterval">刷新时间间隔</param>
        public ServiceForPRAREGX1GY(IOpcClient opcClient) : base(5000)
        {
            this.opcClient = opcClient;
        }
  
        /// <summary>
        /// 周期执行
        /// </summary>
        public override void InvokeService()
        {
           //opcClient["GroupData"]
           //              .GetTags()
           //              .ToList();
        }

        /// <summary>
        /// tag点发生变化后触发
        /// </summary>
        /// <param name="tag"></param>
        public override void TagChangedExecute(Tag tag)
        {
            string source_para_code = "";
            string source_app_code = "G1";
            int equip_type = 2;
            int para_value = 0;

            //出炉信号
            if (tag.TagName == "JRL_CHUGANG"|| tag.TagName =="MILL_GLTri")
            {         

            //格式转换
            if (tag.DataTypeName == "Boolean")
            {
                para_value = Convert.ToInt32(Convert.ToBoolean(tag.Value));
            }
            if (para_value == 1)
            {
                    Thread.Sleep(10000);//等待10S后执行,待二级处理完事务
                    using (var db = SugarDao.Instance)
                {  
                    //获取当前出炉材料号
                    var matNoNow = db.Queryable<L2_FURN_DATA>().Where(p=>p.DISCH_FUR_TIME !=null).OrderBy(p => p.DISCH_FUR_TIME,SqlSugar.OrderByType.Desc).First();
                    var rollPlanNoNow = matNoNow.ROLL_PLAN_NO;
                    if (rollPlanNoNow == "888888")
                     {
                        LogHelper.Debug($"当前出炉计划:{"888888"} 为虚拟计划，不处理");
                        return;
                     }
                        LogHelper.Debug($"当前出炉材料号:{matNoNow.MAT_NO} , 对应轧制计划:{matNoNow.ROLL_PLAN_NO}");
                    //出炉计划材料个数
                    int OutPlanCount = db.Queryable<L2_FURN_DATA>().Where(p=>p.ROLL_PLAN_NO == rollPlanNoNow && p.DISCH_FUR_TIME !=null).Count();
                    LogHelper.Debug($"计划出炉材料支数:{OutPlanCount} , 对应轧制计划:{matNoNow.ROLL_PLAN_NO}");

                    //根据材料号查出对应计划
                    var roll_plan = db.Queryable<L2_MILL_SCHEDU_MNG>().Where(p =>p.ROLL_PLAN_NO== matNoNow.ROLL_PLAN_NO).First();
                    LogHelper.Debug("根据计划号判断当前所有材料是否已经出炉，规则:计划支数=出炉支数+炉前剔除支数");
                    LogHelper.Debug($"轧制计划:{matNoNow.ROLL_PLAN_NO} ,总材料支数:{roll_plan.ORI_BRANCH_COUNT} ,出炉材料支数:{OutPlanCount} ,剔除材料支数:{roll_plan.REMOVE_FW_COUNT}");
                    
                    //根据计划号判断当前所有材料是否已经出炉，规则:计划支数=出炉支数+炉前剔除支数
                    if (roll_plan.ORI_BRANCH_COUNT !=(OutPlanCount + roll_plan.REMOVE_FW_COUNT))
                    {
                        LogHelper.Debug($"不满足条件规则返回");
                        return;
                    }

                    //根据计划号，查询对应流水号
                    var chargeSeqNow = db.Queryable<L2_MILL_SCHEDU>().Where(p => p.ROLL_PLAN_NO == rollPlanNoNow).First().CHARGE_SEQ;
                    LogHelper.Debug($"轧制计划:{matNoNow.ROLL_PLAN_NO} ,流水号:{chargeSeqNow}");

                    // 根据流水号，查找流水号大于当前流水号的下一个计划号
                    var rollPlanNoGY = db.Queryable<L2_MILL_SCHEDU>().Where(p => p.CHARGE_SEQ> chargeSeqNow).OrderBy(p=>p.CHARGE_SEQ, SqlSugar.OrderByType.Asc).First();
                    LogHelper.Debug($"下一个轧制计划:{rollPlanNoGY.ROLL_PLAN_NO} ,流水号:{rollPlanNoGY.CHARGE_SEQ}");

                    //根据计划号匹配对应的工艺参数
                    var planGY = db.Queryable<L2_MILL_SCHEDU_GY>().Where(p => p.ROLL_PLAN_NO == rollPlanNoGY.ROLL_PLAN_NO).First();
                    if (planGY == null)
                    {
                        return;
                    }
                    else
                    {
                        string strGdSpeed = planGY.S0.ToString().PadRight(4, '0') + " "
                               + planGY.S1.ToString().PadRight(4, '0') + " "
                                + planGY.S2.ToString().PadRight(4, '0') + " "
                                 + planGY.S3.ToString().PadRight(4, '0') + " "
                                  + planGY.S4.ToString().PadRight(4, '0') + " "
                                   + planGY.S5.ToString().PadRight(4, '0') + " "
                                    + planGY.S6.ToString().PadRight(4, '0') + " "
                                     + planGY.S7.ToString().PadRight(4, '0') + " "
                                      + planGY.S8.ToString().PadRight(4, '0') + " "
                                       + planGY.S9.ToString().PadRight(4, '0') + " "
                                        + planGY.S10.ToString().PadRight(4, '0') + " "
                                         + planGY.COLLECT_AREA.ToString().PadRight(4, '0') + " ";

                        string strFanCur = planGY.FAN_CUR_1.ToString().PadRight(2, '0') + " "
                                 + planGY.FAN_CUR_2.ToString().PadRight(2, '0') + " "
                                  + planGY.FAN_CUR_3.ToString().PadRight(2, '0') + " "
                                   + planGY.FAN_CUR_4.ToString().PadRight(2, '0') + " "
                                    + planGY.FAN_CUR_5.ToString().PadRight(2, '0') + " "
                                     + planGY.FAN_CUR_6.ToString().PadRight(2, '0') + " "
                                      + planGY.FAN_CUR_7.ToString().PadRight(2, '0') + " "
                                       + planGY.FAN_CUR_8.ToString().PadRight(2, '0') + " "
                                        + planGY.FAN_CUR_9.ToString().PadRight(2, '0') + " "
                                         + planGY.FAN_CUR_10.ToString().PadRight(2, '0') + " "
                                          + planGY.FAN_CUR_11.ToString().PadRight(2, '0') + " "
                                           + planGY.FAN_CUR_12.ToString().PadRight(2, '0') + " "
                                            + planGY.FAN_CUR_13.ToString().PadRight(2, '0') + " "
                                             + planGY.FAN_CUR_14.ToString().PadRight(2, '0') + " ";

                        string strHoldNat = planGY.HOLD_HAT_1 +
                                            planGY.HOLD_HAT_2 +
                                            planGY.HOLD_HAT_3 +
                                            planGY.HOLD_HAT_4 +
                                            planGY.HOLD_HAT_5 +
                                            planGY.HOLD_HAT_6 +
                                            planGY.HOLD_HAT_7 +
                                            planGY.HOLD_HAT_8 +
                                            planGY.HOLD_HAT_9 +
                                            planGY.HOLD_HAT_10 +
                                            planGY.HOLD_HAT_11 +
                                            planGY.HOLD_HAT_12 +
                                            planGY.HOLD_HAT_13 +
                                            planGY.HOLD_HAT_14 +
                                            planGY.HOLD_HAT_15 +
                                            planGY.HOLD_HAT_16 +
                                            planGY.HOLD_HAT_17 +
                                            planGY.HOLD_HAT_18;
                            strHoldNat=strHoldNat.Replace("开", "1");
                            strHoldNat=strHoldNat.Replace("关", "0");

                        string strFinal = strGdSpeed + strFanCur + strHoldNat;
                        LogHelper.Debug($"下发工艺参数:{strFinal}");
                        WriteValue(strFinal);
                    }
                }
                }
            }
         }

        public string strFormat(L2_MILL_SCHEDU_GY gy)
        {
            string str = gy.FAN_CUR_1.ToString();
            return "";
        }

        public void WriteValue(string value)
        {
            byte[] bytTemp = new byte[2];
            bytTemp[0] = 254;
            bytTemp[1] = 254;
            byte[] byteValue = System.Text.Encoding.Default.GetBytes(value);

            byte[] finalValue = new byte[byteValue.Length + bytTemp.Length];
            bytTemp.CopyTo(finalValue, 0);
            byteValue.CopyTo(finalValue, bytTemp.Length);             

            //opc重新绑定
            string tagName = "MILL_GY";  //将需要重载的点压入集合   
            //绑定完成后获取tag点
            string grouName = "GroupData";
            Tag bi = opcClient[grouName].GetTag(tagName);
            bi.Value = System.Text.Encoding.Default.GetString(finalValue); ;
            opcClient["GroupData"].SetTagValue(bi);

        }
    }
}
