using OpcSBForGX.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpcSBForGX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var db = SugarDao.Instance;
            //ServerLog r2 = db.GetSingle<ServerLog>("select   * from ServerLog where rownum<=1");
            db.Insert(new ServerLog() { START_DATE=DateTime.Now, SERVER_INFO="55667" }); //插入一条记录 (有主键也好，没主键也好，有自增列也好都可以插进去)
            //var r1 = db.GetDataTable("select * from ServerLog");
            //var select1 = db.Queryable<ServerLog>();
        }
    }
}
