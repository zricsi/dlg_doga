using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dlg_doga
{
    public partial class Ujvizsgazok : Form
    {
        public string ConnectionString { get; set; }    
        public int sorsz { get; set; } 
        public bool vane { get; set; }
        public Ujvizsgazok(string ConnectionString,int sorsz,bool vane)
        {
            this.ConnectionString = ConnectionString;
            this.sorsz = sorsz;
            this.vane = vane;
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmMain frmMain = new FrmMain();
            frmMain.ShowDialog();
        }

        private void Ujvizsgazok_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var r = new SqlCommand(
                    "SELECT MAX(sorsz)+1 " +
                    "FROM jelentkezesek ;", connection).ExecuteReader();
                while (r.Read())
                {
                    textBox1.Text = $"{r[0]}";
                }
            }
             
            textBox1.Enabled = false;
            if (vane == true)
            {
                btn_add.Enabled = false;
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var r = new SqlCommand(
                        "SELECT sorsz, nev, mobil, szulev " +
                        "FROM jelentkezesek " +
                        $"WHERE sorsz = {sorsz} ;", connection).ExecuteReader();
                    while (r.Read())
                    {
                        textBox1.Text = $"{r[0]}";
                        textBox2.Text = $"{r[1]}";
                        textBox3.Text = $"{r[2]}";
                        textBox4.Text = $"{r[3]}";
                    }
                }
            }
            else
            {
                btn_edit.Enabled = false;
                btn_torles.Enabled = false;
            } 
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var r = new SqlCommand(
                    "INSERT INTO jelentkezesek (sorsz, nev, mobil, szulev) " +
                    $"VALUES ('{textBox1.Text}' , '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}') ;",connection).ExecuteNonQuery();
            }
            MessageBox.Show("Sikeresen hozzáadva");
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var r = new SqlCommand(
                    "UPDATE jelentkezesek " +
                    $"SET nev = '{textBox2.Text}', mobil = '{textBox3.Text}', szulev = '{textBox4.Text}' " + 
                    $"WHERE sorsz = '{textBox1.Text}' ;",connection).ExecuteNonQuery();
            }
            MessageBox.Show("Sikeres módosítás");
        }

        private void btn_torles_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var r = new SqlCommand(
                    "DELETE FROM jelentkezesek " +
                    $"WHERE sorsz = '{textBox1.Text}' ;",connection).ExecuteNonQuery();

            }
            MessageBox.Show("Törölve");
        }
    }
}
