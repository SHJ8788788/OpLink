using OpcClient;
using OpLink.Interface;
using PRAREServiceGX1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAREServiceGX1
{
    public class ServiceForPRAREGX1: TagServiceBase
    { 
        private IOpcClient opcClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="opcClient">opc客户端</param>
        /// <param name="runInterval">刷新时间间隔</param>
        public ServiceForPRAREGX1(IOpcClient opcClient,int runInterval):base(runInterval=5000)
        {
            this.opcClient = opcClient;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="opcClient">opc客户端</param>
        /// <param name="runInterval">刷新时间间隔</param>
        public ServiceForPRAREGX1(IOpcClient opcClient) : base(5000)
        {
            this.opcClient = opcClient;
        }
  
        /// <summary>
        /// 周期执行
        /// </summary>
        public override void InvokeService()
        {
           opcClient["GroupData"]
                         .GetTags()
                         .ToList();
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

            switch (tag.TagName)
            {
                case "H1YAOGANG":
                    source_para_code = "G1_MILL_1_STATUS";
                    break;
                case "H7YAOGANG":
                    source_para_code = "G1_MILL_7_STATUS";
                    break;
                case "H13YAOGANG":
                    source_para_code = "G1_MILL_13_STATUS";
                    break;
                case "PL_RULU":
                    source_para_code = "G1_FURN_IN_STATUS";
                    break;
                case "JRL_CHUGANG":
                    source_para_code = "G1_FURN_OUT_STATUS";
                    break;
                default:
                    source_para_code = "UNKNOW";
                    return;
            }
            //格式转换
            if (tag.DataTypeName == "Boolean")
            {
                para_value = Convert.ToInt32(Convert.ToBoolean(tag.Value));
            }
            if (para_value == 1)
            {
                using (var db = SugarDao.Instance)
                {
                    var sucess = db.Insertable(new ITF_PARA_VALUE() { EQUIP_TYPE = equip_type, SOURCE_APP_CODE = source_app_code, SOURCE_PARA_CODE = source_para_code, PARA_VALUE = para_value, TRAN_DATE = DateTime.Now, SEND_DATE = DateTime.Now, STATUS = 0 }).ExecuteCommand();
                } 
             }
         }
    }
}
