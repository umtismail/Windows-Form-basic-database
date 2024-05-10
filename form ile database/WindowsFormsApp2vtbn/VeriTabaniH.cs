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
using System.Collections;

namespace WindowsFormsApp2vtbn
{
    class VeriTabaniH
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        DataGridView zaz;
        string Vt;
        int Calisma=0;
        public VeriTabaniH(string Vt)
        {
            try
            {
                this.Vt = Vt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Doldur(string Tablo_Adi,DataGridView h)
        {
            try
            {
                Calisma++;
                //Verilen İsimdeki Veritab
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=" + Vt + ".accdb");
                da = new OleDbDataAdapter("Select *from " + Tablo_Adi, con); //Üst taraftaki tabloda seçilen veritabanında bulunan Tablolardan İstenileni Gösterir
                ds = new DataSet();//DataGridWiew'e Atamak İçin Doldurma Tanımlanır
                con.Open();//Veritabanı Açılır
                da.Fill(ds, Tablo_Adi);//Bağlantı Kurulan Tablonun İçine Seçilen Tablomuz Doldurularak Sıkıştırılır
                h.DataSource = ds.Tables[Tablo_Adi];//Seçilen Tablomuzdaki Verileri DataTable Türünde Geri Döndürürüz
                zaz = h;
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
                h.DataSource = ds.Tables[Tablo_Adi];
            }
            finally
            {
                con.Close();//Bağlantımız Kapatılır
            }
        }

        public void Ekle(string Hangi_Tablo,string Hangi_Bolumler,string Hangi_Veriler)
        {
            try
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=" + Vt + ".accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into " + Hangi_Tablo + "(" + Hangi_Bolumler + ") values (" + Hangi_Veriler + ")";
                cmd.ExecuteNonQuery();
                con.Close();
                if (Calisma < 1)
                {
                    Doldur(Hangi_Tablo, zaz);
                }
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }

        public void Sil(string Hangi_Tablo,string Hangi_Ozelligi,string Ne_Olan)
        {
            try
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=" + Vt + ".accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from " + Hangi_Tablo + " where " + Hangi_Ozelligi + "=" + Ne_Olan + "";
                cmd.ExecuteNonQuery();
                con.Close();
                if (Calisma < 1)
                {
                    Doldur(Hangi_Tablo, zaz);
                }
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }

        public void Guncelle(string Hangi_Tablo,string Hangi_Ozellig,string Ne_Yap,string Hangi_Ozelligi,string Ne_Olanı)
        {
            try
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=" + Vt + ".accdb");
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update " + Hangi_Tablo + " set " + Hangi_Ozellig + " = '" + Ne_Yap + "' where " + Hangi_Ozelligi + " = " + Ne_Olanı;
                cmd.ExecuteNonQuery();
                con.Close();
                if (Calisma < 1)
                {
                    Doldur(Hangi_Tablo, zaz);
                }
            }
            catch (Exception g)
            {
                MessageBox.Show(g.Message);
            }
        }

        public string Secili(DataGridView a,string veri_Turu)
        {
            string Sonuc = "DoğruİşlemGirin";
            try
            {
                if (veri_Turu == "İd")
                {
                    int g = a.SelectedCells[0].RowIndex;
                    Sonuc = a.Rows[g].Cells[0].Value.ToString();
                }
                else if (veri_Turu == "SütunAdı")
                {
                    Sonuc = a.Columns[a.CurrentCell.ColumnIndex].HeaderText.ToString();
                }
                return Sonuc;
            }
            catch (Exception)
            {
                return Sonuc;
            }
        }

        public string Baglanti(Hashtable a,ComboBox g)
        {
            string Sonuc="";
            foreach (DictionaryEntry l in a)
            {
                if (g.SelectedItem == l.Key)
                {
                    Sonuc = Convert.ToString(l.Value);
                }
            }
            return Sonuc;
        }

        public void Yapimci()
        {
            MessageBox.Show("Bu Sınıf Hüseyin Topkaya Tarafından Yapılmıştır");
        }

    }
}
