using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 自販機
{
    public partial class FrmRecord : Form
    {
        Class1 db = new Class1();

        private int DealerId;

        public FrmRecord(string dealerId)
        {
            InitializeComponent();

            DealerId = int.Parse(dealerId);
        }

        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmRecord_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "";
            sql += " select";
            sql += " d.buy_date,";
            sql += " c.place_name,";
            sql += " a.jan_code,";
            sql += " a.item_name,";
            sql += " a.standard,";
            sql += " d.buy_price";
            sql += " from mst_items as a";
            sql += " left join trn_stocks as b";
            sql += " on a.id = b.mst_item_id";
            sql += " left join mst_places as c";
            sql += " on b.place_id = c.id";
            sql += " left join trn_claims as d";
            sql += " on b.mst_item_id = d.mst_item_id";
            sql += " where a.mst_dealer_id = " + DealerId;
            sql += " and d.buy_date is not null";
            sql += " order by d.buy_date asc";

            DataTable dt = db.SelectSpl(sql);

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
        /// csv出力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCsv_Click(object sender, EventArgs e)
        {
            DataTable dt;
            dt = (DataTable)this.dataGridView1.DataSource;
            Csv(dt);
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
                string search = txtSearch.Text;
                string startTime = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                string endTime = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                int count = 0;

                string[] split = {};
                string rep = search.Replace(" ", "　");

                if (rep != "")
                {
                    split = rep.Split();
                }

                string sql;
                sql = "";
                sql += " select";
                sql += " buy_date,";
                sql += " place_name,";
                sql += " jan_code,";
                sql += " item_name,";
                sql += " standard,";
                sql += " buy_price";
                sql += " from mst_items as a";
                sql += " left join trn_stocks as c";
                sql += " on a.id = c.mst_item_id";
                sql += " left join mst_places as d";
                sql += " on c.place_id = d.id";
                sql += " left join mst_dealers as e";
                sql += " on a.mst_dealer_id = e.id";
                sql += " left join mst_users as f";
                sql += " on a.mst_dealer_id = f.id";
                sql += " left join trn_claims as g";
                sql += " on c.mst_item_id  = g.id";
                sql += " where a.mst_dealer_id =" + DealerId;

                foreach (string value in split)
                {
                    if (count == 0)
                    {
                        sql += " and";
                        sql += " (a.jan_code Like '%" + value + "%'";
                        sql += " or";
                        sql += " a.item_name Like '%" + value + "%'";
                        sql += " or";
                        sql += " a.standard Like '%" + value + "%')";
                        count = count + 1;
                    }
                    else
                    {
                        sql += " and";
                        sql += " (a.jan_code Like '%" + value +"%'";
                        sql += " or";
                        sql += " a.item_name Like '%" + value + "%'";
                        sql += " or";
                        sql += " a.standard Like '%" + value + "%')";
                    }  
                }
                sql += " and";
                sql += " buy_date BETWEEN '" + startTime + " 00:00:00' AND '" + endTime + " 23:59:59' ";
                sql += " order by d.order_num asc";

                DataTable dt = db.SelectSpl(sql);

                dataGridView1.DataSource = dt;
            }
        }

        /// <summary>
        /// csv出力
        /// </summary>
        /// <param name="dt"></param>
        private void Csv(DataTable dt)
        {
            using(SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = "outputc#.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.GetEncoding("shift_jis")))
                    {
                        int rowCount = dt.Rows.Count - 1;
                        int colCount = dt.Columns.Count;
                        //見出し
                        var strList = new List<string>();
                        for(int i = 0; i < colCount; i++)
                        {
                            strList.Add(dt.Columns[i].Caption);
                        }

                        string[] strary = strList.ToArray();

                        string strCsvData = string.Join(",", strary);

                        sw.WriteLine(strCsvData);

                        //行
                        for (int i = 0; i < rowCount; i++)
                        {
                            var strList2 = new List<string>();
                            //列
                            for (int j = 0; j < colCount; j++)
                            {
                                strList2.Add(dt.Rows[i][j].ToString());
                            }

                            string[] strArray2 = strList2.ToArray();

                            string strCsvData2 = string.Join(",", strArray2);

                            sw.WriteLine(strCsvData2);
                        }
                    }
                }
            }
        }

        private DataTable LinqSearch()
        {
            string sql;
            sql = "";
            sql += " select";
            sql += " d.buy_date,";
            sql += " c.place_name,";
            sql += " a.jan_code,";
            sql += " a.item_name,";
            sql += " a.standard,";
            sql += " d.buy_price";
            sql += " from mst_items as a";
            sql += " left join trn_stocks as b";
            sql += " on a.id = b.mst_item_id";
            sql += " left join mst_places as c";
            sql += " on b.place_id = c.id";
            sql += " left join trn_claims as d";
            sql += " on b.mst_item_id = d.mst_item_id";
            sql += " where a.mst_dealer_id = " + DealerId;
            sql += " and d.buy_date is not null";
            sql += " order by d.buy_date asc";

            DataTable dt = db.SelectSpl(sql);

            //dataGridView1.DataSource = dt;

            return dt;
        }

        private void Linq(DataTable dt)
        {
            var itemName = textBox1.Text;
            var itemName2 = textBox2.Text;
            string startTime = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            string endTime = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            int count = 0;

            string[] split = { };
            string rep = itemName.Replace(" ", "　");

            if (rep != "")
            {
                split = rep.Split();
            }

            var result = dt.AsEnumerable()
                           .Select(x => new { name = x["item_name"] }).ToArray();

            //var result2 = dt.AsEnumerable()
            //                .Where(x => x.Field<string>("item_name") == itemName)
            //                .CopyToDataTable();

            //var result3 = dt.AsEnumerable()
            //                .Where(x => x["item_name"].ToString().Contains(itemName))
            //                .CopyToDataTable();

            //var result3 = dt.AsEnumerable()
            //                .Where(x => x["item_name"].ToString().Contains(itemName)
            //                || x["place_name"].ToString().Contains(itemName)
            //                || x["jan_code"].ToString().Contains(itemName))
            //                .CopyToDataTable();

            //IEnumerable<DataTable> work = dt;

            //foreach (var key in split)
            //{
            //    var result3 = dt.AsEnumerable()
            //                .Where(x => x["item_name"].ToString().Contains(key))
            //                //.Where(x => x["place_name"].ToString().Contains(itemName2))
            //                .CopyToDataTable();
            //}

            var result4 = dt.AsEnumerable().ToList<DataRow>();

            var result5 = result4.OrderBy(x => x["place_name"])
                                 .CopyToDataTable();

            List<string> _list = new List<string>(dt.AsEnumerable()
                                                    .Select(x => x.Field<string>("item_name")));

            //var result3 = dt.AsEnumerable()
            //                .Where(x => x["item_name"].ToString().Contains(itemName))
            //                .Where(x => x["place_name"].ToString().Contains(itemName2))
            //                .CopyToDataTable();



            //var result3 = dt.AsEnumerable()
            //                .OrderByDescending(x => x["jan_code"])
            //                .CopyToDataTable();


            //DataTable dt2 = result3;

           dataGridView1.DataSource = result5;//dt2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = LinqSearch();

            Linq(dt);
        }
    }
}
