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
    public partial class Form49 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in,spo_id,match_id;

        private void Form49_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from `match` where `sport_sport_id`='" + spo_id + "'";
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i!=0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        match_id = Convert.ToInt32(dt.Rows[j]["match_id"].ToString());
                        cmd.CommandText = "select * from `match_date_has_match` where `match_match_id`='" + match_id + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();

                        dataGridView1.Rows.Add(dt.Rows[j]["match_id"].ToString(), dt.Rows[j]["opponent"].ToString(), dt.Rows[j]["host_by"].ToString(), dt.Rows[j]["venue"].ToString(),
                            dt1.Rows[0]["start_date"].ToString(), dt1.Rows[0]["end_date"].ToString(), dt.Rows[j]["start_time"].ToString(), dt.Rows[j]["end_time"].ToString(), dt.Rows[j]["result"].ToString());
                        dt1.Rows.Clear();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
         "database=ecams_database;" + "password=facebook2018;");
        public Form49(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
        }
    }
}
