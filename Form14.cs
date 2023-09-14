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
    public partial class Form14 : Form
    {
        int spo_id, i;
        string subject, message, sport;

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
           "database=ecams_database;" + "password=facebook2018;");
        private void Form14_Load(object sender, EventArgs e)
        {
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            MySqlCommand cmd = connectionstring.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from sport where sport_id='" + spo_id + "'";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            sport = dt.Rows[0]["sport_name"].ToString();
            dt.Rows.Clear();
            cmd.CommandText = "select * from announcement where sport_sport_id='" + spo_id + "'and `read`='" + "read" + "'";

            da.Fill(dt);
            cmd.ExecuteNonQuery();

            i = Convert.ToInt32(dt.Rows.Count);
            if (i == 0)
            {
                //do nothing
            }
            else
            {
                for (int j = 0; j < i; j++)
                {
                    message = dt.Rows[j]["message"].ToString();
                    subject = dt.Rows[j]["subject"].ToString();

                    dataGridView1.Rows.Add("Principal", sport, subject, message);
                }
            }
        }

        public Form14(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
        }

    }
}
