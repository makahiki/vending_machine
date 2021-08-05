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
    public partial class FrmZaikoCheak : Form
    {
        Class1 db = new Class1();

        private int DealerId;
        private string JanCode;

        public FrmZaikoCheak(string dealerId, string janCode)
        {
            InitializeComponent();

            DealerId = int.Parse(dealerId);
            JanCode = janCode;

            if(JanCode != string.Empty)
            {
                txtSearch.Visible = false;
            }
        }

        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmZaikoCheak_Load(object sender, EventArgs e)
        {
            DataTable dt = GetData();

            dataGridView1.DataSource = dt;
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

                GetSearch(dt);
            }
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
            sql += " c.place_name as 場所,";
            sql += " a.jan_code as JANコード,";
            sql += " a.item_name as 商品名,";
            sql += " a.standard as 規格,";
            sql += " b.stock_count as 在庫数,";
            sql += " sum(20 - b.stock_count) as 欠品数";
            sql += " from mst_items as a";
            sql += " left join trn_stocks as b";
            sql += " on a.id = b.mst_item_id";
            sql += " left join mst_places as c";
            sql += " on b.place_id = c.id";
            sql += " where a.mst_dealer_id =" + DealerId;
            if(JanCode != string.Empty)
            {
                sql += " and a.jan_code = '" + JanCode + "'";
            }
            sql += " group by c.place_name, a.jan_code, a.item_name, a.standard, b.stock_count";
            sql += " order by a.jan_code";

            DataTable dt = db.SelectSpl(sql);

            return dt;
        }

        /// <summary>
        /// 検索結果取得
        /// </summary>
        /// <param name="dt"></param>
        private void GetSearch(DataTable dt)
        {
            var searchWord = txtSearch.Text;

            var result = dt.AsEnumerable()
                            .Where(x => x["JANコード"].ToString().Contains(searchWord)
                            || x["商品名"].ToString().Contains(searchWord)
                            || x["規格"].ToString().Contains(searchWord))
                            .CopyToDataTable();

            dataGridView1.DataSource = result;
        }
    }
}
