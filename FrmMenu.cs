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
    public partial class FrmMenu : Form
    {
        private string DealerId;
        public FrmMenu(string dealerId)
        {
            InitializeComponent();

            DealerId = dealerId;
        }

        /// <summary>
        /// 購入実績ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecord_Click(object sender, EventArgs e)
        {
            FrmRecord fr = new FrmRecord(DealerId);
            fr.ShowDialog();
        }

        /// <summary>
        /// 補充確認ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHojuCheak_Click(object sender, EventArgs e)
        {
            FrmHojuCheak fh = new FrmHojuCheak(DealerId);
            fh.ShowDialog();
        }

        /// <summary>
        /// 在庫確認ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZaikoCheak_Click(object sender, EventArgs e)
        {
            FrmZaikoCheak fz = new FrmZaikoCheak(DealerId, string.Empty);
            fz.ShowDialog();
        }

        /// <summary>
        /// 補充ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHoju_Click(object sender, EventArgs e)
        {
            FrmHoju fh = new FrmHoju(DealerId);
            fh.ShowDialog();
        }
    }
}
