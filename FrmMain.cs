using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 自販機
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 購入画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuy_Click(object sender, EventArgs e)
        {
            FrmBuy f2 = new FrmBuy();
            f2.ShowDialog();
        }

        /// <summary>
        /// 管理画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManagement_Click(object sender, EventArgs e)
        {
            FrmLogin fl = new FrmLogin();
            fl.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }
    }
}
