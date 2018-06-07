using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace WindowsFormsApp1
{   
    public partial class Form1 : Form
    {
        bool toggle = true;
        bool toggle2 = true;
        bool toggle3 = true;
        int id = 0;
        string BaglantiAdresi = "Data Source=localhost; Initial Catalog=yigit_abi; Integrated Security=True; connection timeout=10;";
        SqlConnection baglan = new SqlConnection();

        public Form1()
        {
            InitializeComponent();
            ChangeSize(1280, 800);
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
        }
        public void ChangeSize(int width, int height)
        {
            this.Size = new Size(width, height);
        }            

        private void veriGoster()
        {
            try
            {
                baglan.ConnectionString = BaglantiAdresi;
                baglan.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT *FROM urun_kayit", baglan);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                baglan.Close();                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }           
        }
        private void veriEkle()
        {
            try
            {
                baglan.ConnectionString = BaglantiAdresi;
                baglan.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO urun_kayit(Ad, Soyad, Telefon, Marka, Model, [Seri No], [Ek Aparat], [Teslimat Yeri], Fiyat, Tarih, Açıklama, No) VALUES('"+textBox1.Text.ToString()+"', '"+textBox2.Text.ToString()+"', '"+textBox3.Text.ToString()+"', '"+textBox4.Text.ToString()+"', '"+textBox5.Text.ToString()+"', '"+textBox6.Text.ToString()+"', '"+textBox7.Text.ToString()+"', '"+textBox8.Text.ToString()+"', '"+textBox9.Text.ToString()+"', '"+textBox10.Text.ToString()+"', '"+richTextBox1.Text.ToString()+"', '"+textBox12.Text.ToString()+"')", baglan);
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglan.Close();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox11.Clear();
                richTextBox1.Clear();
                textBox12.Focus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }            
        }
        private void veriSil()
        {
            try
            {
                if (MessageBox.Show("Secilen Kayıt Silinecek Onaylıyorum..", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM urun_kayit WHERE No=@numara", baglan);
                    komut.Parameters.AddWithValue("@numara", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglan.Close();
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }            
        }
        private void veriGuncelle()
        {
            try
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("UPDATE urun_kayit SET No='" + textBox12.Text.ToString() + "', Ad='" + textBox1.Text.ToString() + "', Soyad='" + textBox2.Text.ToString() + "', Telefon='" + textBox3.Text.ToString() + "', Marka='" + textBox4.Text.ToString() + "', Model='" + textBox5.Text.ToString() + "', [Seri No]='" + textBox6.Text.ToString() + "', [Ek Aparat]='" + textBox7.Text.ToString() + "', [Teslimat Yeri]='" + textBox8.Text.ToString() + "', Fiyat='" + textBox9.Text.ToString() + "', Tarih='" + textBox10.Text.ToString() + "', Açıklama='" + richTextBox1.Text.ToString() + "'  WHERE No=@numara", baglan);
                komut.Parameters.AddWithValue("@numara", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglan.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);                
            }
            
        }        
        private void veriAra(int x)
        {
            try
            {
                baglan.Open();
                if (x == 1)
                {
                    SqlCommand komut = new SqlCommand("SELECT * FROM urun_kayit WHERE Ad=@ad", baglan);
                    komut.Parameters.AddWithValue("@ad", textBox13.Text.ToString());
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                if (x == 2)
                {
                    SqlCommand komut = new SqlCommand("SELECT * FROM urun_kayit WHERE No=@no", baglan);
                    komut.Parameters.AddWithValue("@no", textBox14.Text);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                if (x == 3)
                {
                    SqlCommand komut = new SqlCommand("SELECT * FROM urun_kayit WHERE Telefon=@telefon", baglan);
                    komut.Parameters.AddWithValue("@telefon", textBox15.Text);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                baglan.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }                        
        }      
        private void veriTarih()
        {
            try
            {
                baglan.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM urun_kayit WHERE Tarih BETWEEN '"+dateTimePicker2.Value.ToString("MM/dd/yyyy")+"' AND '"+dateTimePicker3.Value.ToString("MM/dd/yyyy")+"'", baglan);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "urun_kayit");
                dataGridView1.DataSource = ds.Tables["urun_kayit"];
                baglan.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);                
            }
        }
        private void onlyNumber_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime myDateTime = dateTimePicker1.Value;
            string sqlFormattedDate = myDateTime.ToString("yyyy.MM.dd");
            string textFormated = myDateTime.ToString("dd.MM.yyyy");
            textBox10.Text = sqlFormattedDate;
            textBox11.Text = textFormated;
        }                                              
        private void listeleToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            veriGoster();           
        }
        private void yeniKayıtToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (toggle)
            {
                panel1.Hide();
                toggle = false;
            }
            else
            {
                panel1.Show();
                toggle = true;
            }            
        }
        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox12.Text != "")
            {
                //id kontrolü yap
                //if (dataGridView1.Rows.GetLastRow[)
                //{

                //}
                veriEkle();
                listeleToolStripMenuItem_Click(sender, e);
            }
            
        }
        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            veriGuncelle();            
            veriGoster();
        }
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            veriSil();
            listeleToolStripMenuItem_Click(sender, e);
        }
        private void içeriğiTemizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            richTextBox1.Clear();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1_ValueChanged(sender, e);
            textBox9.Text = "0";
            textBox12.Focus();
        }        
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {            
            decimal toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value);
            }
            label13.Text = toplam.ToString();
        }
        private void işlemlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toggle2)
            {
                panel2.Hide();
                toggle2 = false;
            }
            else
            {
                panel2.Show();
                toggle2 = true;
            }
        }       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox12.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString().Trim();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim();
            textBox8.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString().Trim();
            textBox9.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString().Trim();
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1_ValueChanged(sender, e);
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString().Trim();
        }     
        private void noToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            panel3.Hide();
            panel4.Show();
            panel5.Hide();            
            panel6.Hide();
        }
        private void adToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            panel3.Show();
            panel4.Hide();
            panel5.Hide();   
            panel6.Hide();

        }
        private void telefonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            panel3.Hide();
            panel4.Hide();
            panel5.Show();
            panel6.Hide();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            veriAra(1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            veriAra(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            veriAra(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            veriTarih();
        }

        private void tariharaligiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Show();
        }
    }
    
}
