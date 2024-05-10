using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp2vtbn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        VeriTabaniH vh = new VeriTabaniH("kutuphane");
        vtClass1 vt = new vtClass1("kutuphane");
        private void Form1_Load(object sender, EventArgs e)
        {
            vh.Doldur("kıtap",dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vt.kayitEkle("kitap",dataGridView1);
        }
    }
}
