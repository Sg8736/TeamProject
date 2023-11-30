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
    public partial class map : Form
    {
        public map()
        {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                //엔터를 누르면 버튼을 누른다는 뜻
                button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Locale> locales = KakaoApi.Search(textBox1.Text);
            listBox1.Items.Clear();
            foreach (Locale locale in locales)
            {
                listBox1.Items.Add(locale);
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            Locale ml = listBox1.SelectedItem as Locale;
            object[] pos = new object[] { ml.Lat, ml.Lng };
            HtmlDocument hdoc = webBrowser1.Document;
            hdoc.InvokeScript("setCenter", pos);
        }
    }
}
