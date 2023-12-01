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

namespace TeamProject
{
    public partial class order : Form
    {
        private DBClass dbClass;
        private string userId; // userId를 저장할 변수 추가
        private string userAdress; // userAdress를 저장할 변수 추가
        private string userPhone; // userId를 저장할 변수 추가
        private string orderId; // orderId를 저장할 변수 추가
        private string InqId; // InqId를 저장할 변수 추가

        public string SelectedOrderId { get; set; }
        public string SelectedInqId { get; set; }
        public order(string loggedInUserId, string loggedInUserAdress, string loggedInUserPhone)
        {
            InitializeComponent();
            dbClass = new DBClass();

            // 생성자에서 userId, userAdress, userPhone 초기화
            userId = loggedInUserId;
            userAdress = loggedInUserAdress;
            userPhone = loggedInUserPhone;

            // TextBox1 텍스트 업데이트
            UpdateTextBox1();

            LoadDataToGridView(); // 데이터 로드 메서드 호출
        }

        // SetLoggedInUserId 메서드 정의 추가
        public void SetLoggedInUserId(string loggedInUserId, string loggedInUserAdress, string loggedInUserPhone)
        {
            userId = loggedInUserId;
            // TextBox1 텍스트 업데이트
            UpdateTextBox1();
            userAdress = loggedInUserAdress;
            // TextBox2 텍스트 업데이트
            UpdateTextBox2();
            userPhone = loggedInUserPhone;
            // TextBox3 텍스트 업데이트
            UpdateTextBox3();
        }

        private void UpdateTextBox1()
        {
            textBox1.Text = $"{userId}";
        }
        private void UpdateTextBox2()
        {
            textBox2.Text = $"{userAdress}";
        }
        private void UpdateTextBox3()
        {
            textBox3.Text = $"{userPhone}";
        }
        private void LoadDataToGridView()
        {
            try
            {
                // DB 연결
                if (!dbClass.ConnectToDatabase())
                {
                    MessageBox.Show("DB 연결에 실패했습니다.");
                    return;
                }

                // DataGridView2 초기화
                dataGridView2.Columns.Clear();
                dataGridView2.Rows.Clear();

                // SQL 쿼리를 생성하여 데이터 가져오기
                string selectQuery = "SELECT * FROM Inquiries";
                OracleDataAdapter dataAdapter = new OracleDataAdapter(selectQuery, dbClass.DCom.Connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Inquiries");

                // 그리드뷰 데이터 초기화
                dataGridView2.DataSource = dataSet.Tables["Inquiries"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}");
            }
            finally
            {
                // DB 연결 종료
                dbClass.DisconnectFromDatabase();
            }
        }
        public void ResetMypage()
        {
            // userId 초기화
            userId = null;

            // TextBox1 초기화
            UpdateTextBox1();

            // DataGridView 초기화
            dataGridView1.DataSource = null;

            // 주문 횟수 초기화
            Label2.Text = "주문횟수: 0 번";
        }
        private void order_Load(object sender, EventArgs e)
        {
            // DB 연결
            if (dbClass.ConnectToDatabase())
            {
                // Orders 테이블을 DataGridView에 표시
                DisplayOrdersTable();

                // Orders 테이블의 열 헤더 설정
                Orders_header();

                // Orders 테이블의 행 수를 표시
                Orders_counter();

                // Inquiries 테이블을 DataGridView에 표시
                DisplayInquiriesTable();

                // DataGridView2의 CellClick 이벤트 핸들러 등록
                dataGridView2.CellClick += dataGridView2_CellClick;

                // Inquiries 테이블의 열 헤더 설정
                Inquiries_header();

                // Inquiries 테이블의 행 수를 표시
                Inquiries_counter();
            }
            else
            {
                MessageBox.Show("DB 연결에 실패했습니다.");
                this.Close();
            }
        }
    }
}
