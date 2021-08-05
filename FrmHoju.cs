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
    public partial class FrmHoju : Form
    {
        Class1 db = new Class1();

        private int DealerId;

        public FrmHoju(string dealerId)
        {
            InitializeComponent();

            DealerId = int.Parse(dealerId);
        }

        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmHoju_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = GetData();

                if (dt.Rows.Count != 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("商品が存在しません");
                    return;
                }
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 補充ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRep_Click(object sender, EventArgs e)
        {
            Rep();

            DataTable dt = GetData();
            dataGridView1.DataSource = dt;
        }

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetData()
        {
            string sql;
            sql = "";
            sql += " select";
            sql += " a.jan_code as JANコード,";
            sql += " a.item_name as 商品名,";
            sql += " a.standard as 規格,";
            sql += " b.stock_count as 在庫";
            //sql += " sum(20 - b.stock_count) as 欠品数";
            sql += " from mst_items as a";
            sql += " left join trn_stocks as b";
            sql += " on a.id = b.mst_item_id";
            sql += " left join mst_places as c";
            sql += " on b.place_id = c.id";
            sql += " where a.mst_dealer_id = " + DealerId;
            sql += " and a.jan_code = '" + txtSearch.Text + "'";
            //sql += " group by c.place_name, a.jan_code, a.item_name, a.standard, b.stock_count";
            sql += " order by c.order_num asc";

            DataTable dt = db.SelectSpl(sql);

            return dt;
        }

        /// <summary>
        /// 補充
        /// </summary>
        private void Rep()
        {
            string sql;
            sql = "";
            sql += " select";
            sql += " c.place_name as 場所名,";
            sql += " c.id as 場所id,";
            sql += " b.mst_item_id as 商品id,";
            sql += " a.jan_code as JANコード,";
            //sql += " a.item_name as 商品名,";
            //sql += " a.standard as 規格,";
            sql += " b.stock_count as 在庫";
            //sql += " sum(20 - b.stock_count) as 欠品数";
            sql += " from mst_items as a";
            sql += " left join trn_stocks as b";
            sql += " on a.id = b.mst_item_id";
            sql += " left join mst_places as c";
            sql += " on b.place_id = c.id";
            sql += " where a.mst_dealer_id = " + DealerId;
            sql += " and a.jan_code = '" + txtSearch.Text + "'";
            //sql += " group by c.place_name, a.jan_code, a.item_name, a.standard, b.stock_count";
            sql += " order by c.order_num asc";

            DataTable dt = db.SelectSpl(sql);
            int count = 0;
            int count2 = 0;

            foreach(DataRow row in dt.Rows)
            {
                int stock = int.Parse(row["在庫"].ToString());
                for (int i = 0; i < 6; i++)
                {
                    if(stock < 20)
                    {
                        sql = "";
                        sql += " update trn_stocks";
                        sql += " set stock_count = stock_count + 1";
                        sql += " where mst_item_id =" + row["商品id"];
                        sql += " and place_id =" + row["場所id"];

                        db.TranSpl(sql);

                        stock += 1;
                        count += 1;
                        count2 += 1;
                    }
                    if(count == 6)
                    {
                        break;
                    }
                }

                if(count2 > 0)
                {
                    MessageBox.Show(row["場所名"].ToString() + count2 + "個補充");
                }

                count2 = 0;

                if(count == 6)
                {
                    break;
                }
            }
        }

        
    }
}
