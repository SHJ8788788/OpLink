using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRAREService.Models
{
    public partial class ITF_PARA_VALUE
    {
        public ITF_PARA_VALUE()
        { }
        public double TRAN_ID { get; set; }
        public double EQUIP_TYPE { get; set; }
        public string SOURCE_APP_CODE { get; set; }
        public string SOURCE_PARA_CODE { get; set; }
        public double PARA_VALUE { get; set; }
        public DateTime TRAN_DATE { get; set; }

        public DateTime SEND_DATE { get; set; }
        public DateTime GET_DATE { get; set; }

        public double STATUS { get; set; }
        public string NOTE { get; set; }
    }
}
