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
    public partial class Form34 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in,soc_id,event_id;
        DateTime event_date;
        private void Form34_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from event where society_society_id='" + soc_id + "'";
                DataTable dt = new DataTable();
               
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());

                for (int j = 0; j < i; j++)
                {
                    event_date = Convert.ToDateTime(dt.Rows[0]["event_date"].ToString());
                    dataGridView1.Rows.Add(dt.Rows[j]["event_name"].ToString(),event_date.Date.ToShortDateString(), dt.Rows[j]["start_time"].ToString(), dt.Rows[j]["end_time"].ToString(), dt.Rows[j]["venue"].ToString()
                        , dt.Rows[j]["parti_count"].ToString(),dt.Rows[j]["income"].ToString(), dt.Rows[j]["cost"].ToString());
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form34(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
        }
    }
}
