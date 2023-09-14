using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace ECAMS
{
    public partial class Form70 : Form
    {
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        int i, spo_id;
        int old_boy_id;
        public Form70(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
        }

        private void Form70_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from old_boy_has_sport where sport_sport_id='" + spo_id + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                i = dt.Rows.Count;
                for (int j = 0; j < i; j++)
                {
                    old_boy_id = Convert.ToInt32(dt.Rows[j]["old_boy_old_boy_id"].ToString());
                    cmd.CommandText = "select * from old_boy where old_boy_id='" + old_boy_id + "'";
                    da.Fill(dt1);
                    cmd.ExecuteNonQuery();

                    dataGridView1.Rows.Add(dt1.Rows[0]["old_boy_name"].ToString(), dt.Rows[0]["best_relation"].ToString(), dt.Rows[0]["best_relation_year"].ToString(), dt.Rows[0]["contact"].ToString());
                    dt1.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
