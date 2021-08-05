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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        Class1 db = new Class1();
        
        /// <summary>
        /// OKボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            string userId = txtLoginId.Text;
            string password = txtPassword.Text;

            string sql;
            sql = "";
            sql += " select * from";
            sql += " mst_users";
            sql += " where login_id = '" + userId + "'";
            sql += " and password = '" + password + "'";

            DataTable dt = db.SelectSpl(sql);

            int rowCount = dt.Rows.Count;

            if (rowCount == 1)
            {
                string dealerId = dt.Rows[0]["mst_dealer_id"].ToString();

                FrmMenu f2 = new FrmMenu(dealerId);
                f2.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("IDまたはパスワードが違います");
            }
        }
    }
}
