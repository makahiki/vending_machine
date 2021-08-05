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
    public partial class FrmHojuCheak : Form
    {
        Class1 db = new Class1();

        private string DealerId;

        public FrmHojuCheak(string dealerId)
        {
            InitializeComponent();

            DealerId = dealerId;
        }

        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCheak_Load(object sender, EventArgs e)
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
        /// グリッドダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            // グリッドを基準としたカーソル位置のポイントを取得
            Point p = dataGridView1.PointToClient(Cursor.Position);
            // 取得したポイントからHitTestでセル位置取得
            DataGridView.HitTestInfo ht = dataGridView1.HitTest(p.X, p.Y);

            if (ht.RowIndex >= 0 && ht.ColumnIndex >= 0)
            {
                string janCode = dataGridView1.Rows[ht.RowIndex].Cells[0].Value.ToString();

                FrmZaikoCheak fz = new FrmZaikoCheak(DealerId, janCode);
                fz.ShowDialog();
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
            sql += " a.jan_code as JANコード,";
            sql += " a.item_name as 商品名,";
            sql += " a.standard as 規格,";
            sql += " ceil(sum(ceil(20-stock_count)/6)) as 補充数（箱）";
            sql += " from mst_items as a";
            sql += " left join trn_stocks as b";
            sql += " on a.id = b.mst_item_id";
            sql += " where a.mst_dealer_id =" + DealerId;
            sql += " group by a.jan_code, a.item_name, a.standard";
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
