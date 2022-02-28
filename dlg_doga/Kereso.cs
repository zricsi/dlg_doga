using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dlg_doga
{
    public partial class Kereso : Form
    {
        public string ConnectionString { get; set; }
        public Kereso(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            InitializeComponent();
        }

        private void Kereso_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var r = new SqlCommand(
                    "SELECT nyelv " +
                    "FROM nyelvek ;", connection).ExecuteReader();
                while (r.Read())
                {
                    cbm.Items.Add(r[0]);
                }
            } 
        }
        private void cbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var r = new SqlCommand(
                "SELECT vizsgak.idopont, nyelvek.nyelv, vizsgak.szint " +
                "FROM vizsgak, nyelvek " +
                $"WHERE vizsgak.nyelvid = nyelvek.id AND nyelvek.nyelv LIKE '{cbm.SelectedItem}%' ;", connection).ExecuteReader();
                while (r.Read())
                {
                    dgv.Rows.Add(r[0], r[1], r[2]);
                }
            } 
        }
    }
}
