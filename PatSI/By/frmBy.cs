using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.Index;

namespace PatSI.By
{
    public partial class frmBy : Form
    {
        public frmBy()
        {
            InitializeComponent();
        }

        private void frmBy_Load(object sender, EventArgs e)
        {

        }

        private void InitByTree()
        {
            trvByItmes.Nodes.Clear();

            //序号,a.id,a.idx_key as 标引项,b.Lname as 创建人, 标引词
            DataTable dt= IdexItemMag.getSysKey("", "", 1, int.MaxValue);

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow drItem in dt.Rows)
            {

            }
        }
    }
}
