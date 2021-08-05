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
    public partial class FrmBuy : Form
    {
        public FrmBuy()
        {
            InitializeComponent();
        }

        Class1 db = new Class1();

        int OrderNum;
        int PlaceId;

        ///ボタン表示用変数
        int ItemId1;
        int ItemId2;
        int ItemId3;
        int ItemId4;
        int ItemId5;
        int ItemId6;
        int ItemId7;
        int ItemId8;
        int ItemId9;

        double ItemPrice1;
        double ItemPrice2;
        double ItemPrice3;
        double ItemPrice4;
        double ItemPrice5;
        double ItemPrice6;
        double ItemPrice7;
        double ItemPrice8;
        double ItemPrice9;

        #region イベント

        /// <summary>
        /// ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBuy_Load(object sender, EventArgs e)
        {
            Place(1);
            BtnDisplay();
            ChangeDisplay();
        }

        /// <summary>
        /// 前へボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblInput.Text.Equals("0"))
            {
                if (OrderNum > 1)
                {
                    OrderNum -= 1;
                    Place(OrderNum);
                }
                BtnDisplay();
            }
        }

        /// <summary>
        /// 次へボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lblInput.Text.Equals("0"))
            {
                string sql;
                sql = "";
                sql += " select";
                sql += " max(order_num)";
                sql += " from mst_places";

                DataTable dt = db.SelectSpl(sql);

                int max = int.Parse(dt.Rows[0]["max"].ToString());

                if (OrderNum < max)
                {
                    OrderNum += 1;
                    Place(OrderNum);
                }
                else
                {
                    return;
                }
                BtnDisplay();
            }
        }

        /// <summary>
        /// ボタン1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId1, ItemPrice1);
        }

        /// <summary>
        /// ボタン2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId2, ItemPrice2);
        }

        /// <summary>
        /// ボタン3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId3, ItemPrice3);
        }

        /// <summary>
        /// ボタン4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId4, ItemPrice4);
        }

        /// <summary>
        /// ボタン5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId5, ItemPrice5);
        }

        /// <summary>
        /// ボタン6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId6, ItemPrice6);
        }

        /// <summary>
        /// ボタン7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId7, ItemPrice7);
        }

        /// <summary>
        /// ボタン8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId8, ItemPrice8);
        }

        /// <summary>
        /// ボタン9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            BuyItem(ItemId9, ItemPrice9);
        }

        /// <summary>
        /// 10円ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn10_Click(object sender, EventArgs e)
        {
            int money = int.Parse(lblInput.Text);
            lblInput.Text = (money + 10).ToString();
            BuyChange(true, 10);
        }

        /// <summary>
        /// 50円ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn50_Click(object sender, EventArgs e)
        {
            int money = int.Parse(lblInput.Text);
            lblInput.Text = (money + 50).ToString();
            BuyChange(true, 50);
        }

        /// <summary>
        /// 100円ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn100_Click(object sender, EventArgs e)
        {
            int money = int.Parse(lblInput.Text);
            lblInput.Text = (money + 100).ToString();
            BuyChange(true, 100);
        }

        /// <summary>
        /// 500円ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn500_Click(object sender, EventArgs e)
        {
            int money = int.Parse(lblInput.Text);
            lblInput.Text = (money + 500).ToString();
            BuyChange(true, 500);
        }

        /// <summary>
        /// 1000円ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1000_Click(object sender, EventArgs e)
        {
            int money = int.Parse(lblInput.Text);
            lblInput.Text = (money + 1000).ToString();
            BuyChange(true, 1000);
        }

        /// <summary>
        /// 返却ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnChange_Click(object sender, EventArgs e)
        {
            ReturnChange();
            lblInput.Text = 0.ToString();
        }

        #endregion

        #region メソッド

        /// <summary>
        /// ボタン表示
        /// </summary>
        private void BtnDisplay()
        {
            string sql;
            sql = "";
            sql += " select";
            sql += " a.id, a.item_name, b.price";
            sql += " from mst_items as a";
            sql += " left join mst_prices as b";
            sql += " on a.id = b.mst_item_id";
            sql += " left join trn_stocks as c";
            sql += " on a.id = c.mst_item_id";
            sql += " left join mst_places as d";
            sql += " on c.place_id = d.id";
            sql += " where b.start_date <= current_date";
            sql += " and b.end_date >= current_date";
            sql += " and d.order_num = " + OrderNum;
            sql += " order by a.id asc";

            DataTable dt = db.SelectSpl(sql);

            //ボタン1
            ItemId1 = int.Parse(dt.Rows[0]["id"].ToString());
            ItemPrice1 = double.Parse(dt.Rows[0]["price"].ToString());
            button1.Text = dt.Rows[0]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice1) + "円";

            //ボタン2
            ItemId2 = int.Parse(dt.Rows[1]["id"].ToString());
            ItemPrice2 = double.Parse(dt.Rows[1]["price"].ToString());
            button2.Text = dt.Rows[1]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice2) + "円";

            //ボタン3
            ItemId3 = int.Parse(dt.Rows[2]["id"].ToString());
            ItemPrice3 = double.Parse(dt.Rows[2]["price"].ToString());
            button3.Text = dt.Rows[2]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice3) + "円";

            //ボタン4
            ItemId4 = int.Parse(dt.Rows[3]["id"].ToString());
            ItemPrice4 = double.Parse(dt.Rows[3]["price"].ToString());
            button4.Text = dt.Rows[3]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice4) + "円";

            //ボタン5
            ItemId5 = int.Parse(dt.Rows[4]["id"].ToString());
            ItemPrice5 = double.Parse(dt.Rows[4]["price"].ToString());
            button5.Text = dt.Rows[4]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice5) + "円";

            //ボタン6
            ItemId6 = int.Parse(dt.Rows[5]["id"].ToString());
            ItemPrice6 = double.Parse(dt.Rows[5]["price"].ToString());
            button6.Text = dt.Rows[5]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice6) + "円";

            //ボタン7
            ItemId7 = int.Parse(dt.Rows[6]["id"].ToString());
            ItemPrice7 = double.Parse(dt.Rows[6]["price"].ToString());
            button7.Text = dt.Rows[6]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice7) + "円";

            //ボタン8
            ItemId8 = int.Parse(dt.Rows[7]["id"].ToString());
            ItemPrice8 = double.Parse(dt.Rows[7]["price"].ToString());
            button8.Text = dt.Rows[7]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice8) + "円";

            //ボタン9
            ItemId9 = int.Parse(dt.Rows[8]["id"].ToString());
            ItemPrice9 = double.Parse(dt.Rows[8]["price"].ToString());
            button9.Text = dt.Rows[8]["item_name"].ToString() + Environment.NewLine + Math.Floor(ItemPrice9) + "円";
        }

        /// <summary>
        /// 購入処理
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="price"></param>
        private void BuyItem(int itemId, double price)
        {
            string sql;
            sql = "";
            sql += " select";
            sql += " a.item_name, b.price, c.stock_count";
            sql += " from mst_items as a";
            sql += " left join mst_prices as b";
            sql += " on a.id = b.mst_item_id";
            sql += " left join trn_stocks as c";
            sql += " on a.id = c.mst_item_id";
            sql += " where c.place_id =" + PlaceId;
            sql += " and c.mst_item_id =" + itemId.ToString();

            DataTable dt = db.SelectSpl(sql);

            double purchasePrice = Math.Floor(double.Parse(dt.Rows[0]["price"].ToString()));
            int stockCount = int.Parse(dt.Rows[0]["stock_count"].ToString());
            double inputAmount = double.Parse(lblInput.Text);

            if (inputAmount >= purchasePrice && stockCount >= 1)
            {
                if (CheakChange(purchasePrice) == 0)
                {
                    MessageBox.Show("釣銭切れ");
                    return;
                }
                lblInput.Text = (inputAmount - purchasePrice).ToString();
            }
            else if (inputAmount >= purchasePrice && stockCount <= 0)
            {
                MessageBox.Show("売り切れ");
                return;
            }
            else
            {
                MessageBox.Show("金額不足");
                return;
            }

            sql = "";
            sql += " update trn_stocks";
            sql += " set stock_count=stock_count-1";
            sql += " where place_id =" + PlaceId;
            sql += " and mst_item_id =" + itemId.ToString();

            db.TranSpl(sql);

            sql = "";
            sql += " insert into trn_claims";
            sql += " (place_id, mst_item_id, buy_count, buy_price, buy_date)";
            sql += " values( " + PlaceId + ", " + itemId + ", 1, " + price + ", now())";

            db.TranSpl(sql);

            ChangeDisplay();
        }

        /// <summary>
        /// 釣銭切れ表示
        /// </summary>
        private void ChangeDisplay()
        {
            string sql;
            sql = "";
            sql += " select id, coin_bill_stock_count";
            sql += " from mst_coin";
            sql += " order by id asc";

            DataTable dt = db.SelectSpl(sql);

            int change10 = int.Parse(dt.Rows[0]["coin_bill_stock_count"].ToString());
            int change100 = int.Parse(dt.Rows[2]["coin_bill_stock_count"].ToString());

            //10円釣銭
            if (change10 == 0)
            {
                lblChange10.ForeColor = Color.Red;
            }
            else if (change10 != 0)
            {
                lblChange10.ForeColor = Color.Black;
            }

            //100円釣銭
            if (change100 == 0)
            {
                lblChange100.ForeColor = Color.Red;
            }
            else if (change100 != 0)
            {
                lblChange100.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 硬貨投入、減らす
        /// </summary>
        /// <param name="coinName"></param>
        private void BuyChange(bool increase, int coinName)
        {
            string sql;
            sql = "";
            sql += " update mst_coin";
            if (increase == true)
            {
                sql += " set coin_bill_stock_count = coin_bill_stock_count + 1";
            }
            else if (increase == false)
            {
                sql += " set coin_bill_stock_count = coin_bill_stock_count - 1";
            }
            sql += " where coin_bill_name =" + coinName;

            db.TranSpl(sql);
        }

        /// <summary>
        /// お釣り在庫
        /// </summary>
        /// <param name="coinName"></param>
        /// <returns></returns>
        private int StockChange(int coinName)
        {
            string sql;
            sql = "";
            sql += " select coin_bill_name, coin_bill_stock_count";
            sql += " from mst_coin";
            sql += " where coin_bill_name =" + coinName;

            DataTable dt = db.SelectSpl(sql);

            int coinStock = int.Parse(dt.Rows[0]["coin_bill_stock_count"].ToString());

            return coinStock;
        }

        /// <summary>
        /// お釣り処理
        /// </summary>
        private void ReturnChange()
        {
            int change = int.Parse(lblInput.Text);

            while (change != 0)
            {
                if (change >= 1000 && StockChange(1000) > 0)
                {
                    change -= 1000;
                    BuyChange(false, 1000);
                }
                else if (change >= 500 && StockChange(500) > 0)
                {
                    change -= 500;
                    BuyChange(false, 500);
                }
                else if (change >= 100 && StockChange(100) > 0)
                {
                    change -= 100;
                    BuyChange(false, 100);
                }
                else if (change >= 50 && StockChange(50) > 0)
                {
                    change -= 50;
                    BuyChange(false, 50);
                }
                else
                {
                    change -= 10;
                    BuyChange(false, 10);
                }
            }
            ChangeDisplay();
        }

        /// <summary>
        /// お釣り判定
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <returns></returns>
        private int CheakChange(double itemPrice)
        {
            double change = double.Parse(lblInput.Text) - itemPrice;

            int c10 = StockChange(10);
            int c50 = StockChange(50);
            int c100 = StockChange(100);
            int c500 = StockChange(500);
            int c1000 = StockChange(1000);

            while (change != 0)
            {
                if (change >= 1000 && c1000 > 0)
                {
                    change -= 1000;
                    c1000 -= 1;
                }
                else if (change >= 500 && c500 > 0)
                {
                    change -= 500;
                    c500 -= 1;
                }
                else if (change >= 100 && c100 > 0)
                {
                    change -= 100;
                    c100 -= 1;
                }
                else if (change >= 50 && c50 > 0)
                {
                    change -= 50;
                    c50 -= 1;
                }
                else
                {
                    change -= 10;
                    c10 -= 1;
                }
            }

            if (c10 < 0 || c50 < 0 || c100 < 0 || c500 < 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 場所id取得
        /// </summary>
        /// <param name="pId"></param>
        private void Place(int pId)
        {
            string sql;
            sql = "";
            sql += " select";
            sql += " * from";
            sql += " mst_places";
            sql += " where order_num =" + pId;
            sql += " order by order_num asc";

            DataTable dt = db.SelectSpl(sql);

            PlaceId = int.Parse(dt.Rows[0]["id"].ToString());
            OrderNum = int.Parse(dt.Rows[0]["order_num"].ToString());
            lblPlace.Text = dt.Rows[0]["place_name"].ToString();
        }

        #endregion
    }
}
