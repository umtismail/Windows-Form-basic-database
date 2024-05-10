using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp2vtbn
{
    class vtClass1
    {
        public OleDbConnection baglanti;
        public string vt, vtYolu,tabloAdi;
        public ArrayList tablolar = new ArrayList();
        private OleDbCommand kmt = new OleDbCommand();

        public vtClass1(string vt)
        {
            this.vt = vt;
            try
            {
                this.vtYolu="Provider=Microsoft.ACE.OLEDB.12.0; Data Source ="+Application.StartupPath + "\\"+this.vt;
                this.baglanti = new OleDbConnection(vtYolu);
                this.vtBaglan();
                DataTable dt1 = baglanti.GetSchema("Tables");
                for(int i =0; i<dt1.Rows.Count;i++)//tablo adları arraylist e ekleniyor
                {
                    if (dt1.Rows[i]["TABLE_TYPE"].ToString()=="TABLE")
                    {
                        tablolar.Add(dt1.Rows[i]["TABLE_NAME"]);
                    }
                }
                this.vtBaglantiKes();
            }
            catch(Exception h)
            {
                MessageBox.Show(h.Message);
            }
        }
        public void vtBaglan()
        {
            baglanti.Open();
        }
        public bool kayitSil(string tabloAdi,string alanAdi, object deger)//kayıt silme metodu
        {
            this.baglanti.Open();
            kmt.Connection = baglanti;
            kmt.CommandText = "";
            if(deger is int)
            {
                kmt.CommandText = "delete from" + tabloAdi + "where" + alanAdi + "=" + (int)deger;
            }
            else
            {
                kmt.CommandText = "delete from" + tabloAdi + "where" + alanAdi + "=" + (string)deger;
            }
            if(kmt.ExecuteNonQuery()==1)
            {
                this.baglanti.Close();
                return false;
            }
            else
            {
                this.baglanti.Close();
                return true;
            }

        }
        public bool kayitGuncelle(string tabloadi, string alanAdi, object deger, params object[] liste)
        {
            /// parametre listeli sorgu oluşturuyor
            string ekleSorgu = "update " + tabloAdi + " set ";
            ArrayList KolonAdlariListGelen;
            this.baglanti.Open();
            // adan adları alınıyor
            OleDbDataAdapter da = new OleDbDataAdapter("select * from" + tabloAdi, this.baglanti);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            KolonAdlariListGelen = new ArrayList();
            // alan adlarını array liste ekleme işlemi bitti
            for (int i = 0; i < KolonAdlariListGelen.Count; i++)
            {
                if (i != KolonAdlariListGelen.Count - 2)
                {
                    if (liste[i] is string)
                    {
                        if (liste[i] == null)
                            liste[i] = "";
                        kmt.Parameters.Add("@" + KolonAdlariListGelen[i].ToString() + "", liste[i].ToString());
                        ekleSorgu = ekleSorgu + KolonAdlariListGelen[i].ToString() + "=" + "@" + KolonAdlariListGelen[i] + ",";
                    }
                    else if (liste[i] is int)
                    {
                        if (liste[i] == null)
                            liste[i] = "";
                        kmt.Parameters.Add("@" + KolonAdlariListGelen[i].ToString() + "", liste[i].ToString());
                        ekleSorgu = ekleSorgu + KolonAdlariListGelen[i].ToString() + "=" + "@" + KolonAdlariListGelen[i] + ",";
                    }
                    else
                    {
                        if (liste[i] is string)
                        {
                            if (liste[i] == null)
                                liste[i] = "";
                            kmt.Parameters.Add("@" + KolonAdlariListGelen[i].ToString() + "", liste[i].ToString());
                            ekleSorgu = ekleSorgu + KolonAdlariListGelen[i].ToString() + "=" + "@" + KolonAdlariListGelen[i];
                        }
                        else if (liste[i] is int)
                        {
                            if (liste[i] == null)
                                liste[i] = "";
                            kmt.Parameters.Add("@" + KolonAdlariListGelen[i].ToString() + "", liste[i].ToString());
                            ekleSorgu = ekleSorgu + KolonAdlariListGelen[i].ToString() + "=" + "@" + KolonAdlariListGelen[i];
                        }
                    }
                }
            }
                ekleSorgu = ekleSorgu + "where" + alanAdi + "=" + deger;
                // sorgu oluşturuldu
                this.kmt.Connection = this.baglanti;
                this.kmt.CommandText = ekleSorgu;
                //MessageBox.Show(ekleSorgu);
                this.kmt.ExecuteNonQuery();
                this.baglanti.Close();
                return true;
            
        }
        public void vtBaglantiKes()
        {
            baglanti.Close();
        }
        public DataTable veriAl(string sorgu)
        {
            DataTable dt = new DataTable();
            if (sorgu != "")
            {
                this.baglanti.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(sorgu, this.baglanti);
                this.baglanti.Close();
                da.Fill(dt);
                return dt;
            }
            else
            {
                return null;
            }
        }
        public bool kayitEkle(string tabloAdi,params object[] liste)
        {
            // parametre listeli sorgu oluşturuyor
            string ekleSorgu = "insert into " + tabloAdi + " (";
            ArrayList KolonAdlariList;
            this.baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from" + tabloAdi, this.baglanti);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            KolonAdlariList = new ArrayList();
            for(int i=1;i<dt2.Columns.Count;i++)
                {
                    DataColumn dr = dt2.Columns[i];
                    KolonAdlariList.Add(dr.ToString());
                }
            for(int k=0;k<KolonAdlariList.Count;k++)
                {
                    if(k!=KolonAdlariList.Count-1)
                    {
                        ekleSorgu = ekleSorgu + " " + KolonAdlariList[k] + ",";
                    }
                    else
                    {
                        ekleSorgu = ekleSorgu + " " + KolonAdlariList[k];
                    }
                }
            ekleSorgu = ekleSorgu + ") values (";
            for (int i = 0; i < KolonAdlariList.Count; i++)
            {
                if (i != KolonAdlariList.Count - 1)
                {
                    if (liste[i] is string)
                    {
                        kmt.Parameters.Add("@" + KolonAdlariList[i].ToString() + "", liste[i].ToString());
                        ekleSorgu = ekleSorgu + "@" + KolonAdlariList[i] + ",";
                    }
                    else if (liste[i] is int)
                    {
                        kmt.Parameters.Add("@" + KolonAdlariList[i].ToString() + "", liste[i].ToString());
                        ekleSorgu = ekleSorgu + "@" + KolonAdlariList[i] + ",";
                    }
                    else
                    {
                        if (liste[i] is string)
                        {
                            kmt.Parameters.Add("@" + KolonAdlariList[i].ToString() + "", liste[i]);
                            ekleSorgu = ekleSorgu + "@" + KolonAdlariList[i];
                        }
                        else if (liste[i] is int)
                        {
                            kmt.Parameters.Add("@" + KolonAdlariList[i].ToString() + "", liste[i]);
                            ekleSorgu = ekleSorgu + "@" + KolonAdlariList[i];
                        }
                    }
                }
            }
                    ekleSorgu = ekleSorgu + ")";
                    // sorgu oluşturuldu
                    this.kmt.Connection = this.baglanti;
                    this.kmt.CommandText = ekleSorgu;
                    //MessageBox.Show(ekleSorgu);
                    this.kmt.ExecuteNonQuery();
                    this.baglanti.Close();
                    return true;
                }

        internal void kayitEkle()
        {
            throw new NotImplementedException();
        }
    }
                 
        }
