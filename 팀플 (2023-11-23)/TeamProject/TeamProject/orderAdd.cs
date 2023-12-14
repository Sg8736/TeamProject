using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject
{
    private string productId;
    public partial class orderAdd : Form
    {
        public orderAdd(string productId)
        {
            InitializeComponent();
            this.productId = productId;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("해당 제품에 대한 총 금액은: " + 계산된금액 + "원 입니다.");
            this.Close();
        }
    }
}
