using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace ECAMS
{
    public partial class Form64 : Form
    {
        int i, event_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void Form64_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from event ";

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                i = Convert.ToInt32(dt.Rows.Count.ToString());

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        event_id = Convert.ToInt32(dt.Rows[j]["event_id"].ToString());
                        cmd.CommandText = "select * from society where society_id='" + Convert.ToInt32(dt.Rows[j]["society_society_id"].ToString()) + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "select * from event_date where event_event_id='" + event_id + "'";
                        da.Fill(dt2);
                        cmd.ExecuteNonQuery();

                        dataGridView1.Rows.Add(dt1.Rows[0]["society_name"], dt.Rows[j]["venue"], dt2.Rows[j]["start_date"], dt2.Rows[j]["end_date"]
                            , dt.Rows[j]["start_time"], dt.Rows[j]["end_time"]);
                        dt1.Rows.Clear();
                        dt2.Rows.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }


        public Form64()
        {
            InitializeComponent();
        }

    }
    
}
