using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace TeamProject
{
    public partial class order : Form
    {
        private DBClass dbClass;
        private string SubcategoryId; // 제품번호를 저장할 변수
        private string totalCount; // 재고수량을 저장할 변수
        private string amount; // 금액을 저장할 변수

        public string SelectedOrderId { get; set; }
        public string SelectedInqId { get; set; }

        public order()
        {
            InitializeComponent();
        }

        private void order_Load(object sender, EventArgs e)
        {
            LoadSubcategoryData();
        }

        private void LoadSubcategoryData()
        {
            // 올바른 연결 문자열 형식
            string ConStr = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));";

            using (OracleConnection conn = new OracleConnection(ConStr))
            {
                try
                {
                    dbClass.DS.Clear();
                    dbClass.DA.Fill(dbClass.DS, "Product");
                    dataGridView1.DataSource = dbClass.DS.Tables["Product"].DefaultView;
                }
                catch (DataException DE)
                {
                    MessageBox.Show(DE.Message);
                }
                catch (Exception DE)
                {
                    MessageBox.Show(DE.Message);
                }
            }
        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            string ConStr = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME =xe) ) );";
            OracleConnection conn = new OracleConnection(ConStr);
            conn.Open();
            OracleDataAdapter DBAdapter = new OracleDataAdapter();
            DBAdapter.SelectCommand = new OracleCommand("select * from Product where productName=:productName", conn);
            DBAdapter.SelectCommand.Parameters.Add("productName", OracleDbType.Varchar2, 20);
            DBAdapter.SelectCommand.Parameters["productName"].Value = textBox1.Text.Trim();
            DataSet DS = new DataSet();
            DBAdapter.Fill(DS, "Product");
            DataTable productTable = DS.Tables["Product"];
            dataGridView1.DataSource = productTable;
        }
        private void 업데이트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 전체 DataGridView 데이터를 업데이트합니다.
            LoadSubcategoryData();
        }
        private void 장바구니추가ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedRow = dataGridView1.CurrentRow;
                var productId = Convert.ToString(selectedRow.Cells["ProductIdColumn"].Value); // 'ProductIdColumn'은 해당 열의 이름으로 변경해야 합니다.

                orderAdd orderAddForm = new orderAdd(productId);
                orderAddForm.ShowDialog();
            }
        }
        private void ConfirmOrderMenuItem_Click(object sender, EventArgs e)
        {
            // 주문 확정 관련 코드
        }
    }
}