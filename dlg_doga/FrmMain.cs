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
    public partial class FrmMain : Form
    {
        public string ConnectionString { set; get; }
        public FrmMain()
        {
            ConnectionString =
           @"Server = (localdb)\MSSQLLocalDB;" +
           "Database = csharp";
            InitializeComponent();
        }

        

        private void FrmMain_Load(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var r = new SqlCommand(
                    "SELECT jelentkezesek.sorsz, jelentkezesek.nev, nyelvek.nyelv, vizsgak.szint " +
                    "FROM jelentkezesek, nyelvek, vizsgak " +
                    "WHERE jelentkezesek.vizsga = vizsgak.sorsz AND vizsgak.nyelvid = nyelvek.id ;", connection).ExecuteReader();
                while (r.Read())
                {
                    dgv.Rows.Add(r[0], r[1], r[2], r[3]);
                }
            }
        }
        private void btn_vizsga_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kereso krs = new Kereso(ConnectionString);
            krs.ShowDialog();
        }

        private void btn_ujvizsgazo_Click(object sender, EventArgs e)
        {
            bool vane = false;
            this.Hide();
            Ujvizsgazok ujvzsgzk = new Ujvizsgazok(ConnectionString,int.Parse(dgv.CurrentRow.Cells[0].Value.ToString()),vane);
            ujvzsgzk.ShowDialog();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool vane = true;
            this.Hide();
            Ujvizsgazok ujvzsgzk = new Ujvizsgazok(ConnectionString, int.Parse(dgv.CurrentRow.Cells[0].Value.ToString()),vane);
            ujvzsgzk.ShowDialog();
        }
    }
}
