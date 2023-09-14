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
    public partial class Form68 : Form
    {
        int i, event_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        DateTime event_date;

        public Form68()
        {
            InitializeComponent();
        }

        private void Form68_Load(object sender, EventArgs e)
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

                i = Convert.ToInt32(dt.Rows.Count.ToString());

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        event_id = Convert.ToInt32(dt.Rows[j]["event_id"].ToString());
                        cmd.CommandText = "select * from society where society_id='" + Convert.ToInt32(dt.Rows[j]["society_society_id"].ToString()) + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        event_date = Convert.ToDateTime(dt.Rows[j]["event_date"].ToString());
                        dataGridView1.Rows.Add(dt1.Rows[0]["society_name"], dt.Rows[j]["event_name"].ToString(),event_date.Date.ToShortDateString(),dt.Rows[j]["venue"]
                            , dt.Rows[j]["start_time"], dt.Rows[j]["end_time"]);
                        dt1.Rows.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
