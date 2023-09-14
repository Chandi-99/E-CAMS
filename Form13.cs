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
    public partial class Form13 : Form
    {
        int soc_id, i;
        string subject, message,society;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
           "database=ecams_database;" + "password=facebook2018;");
        private void Form13_Load(object sender, EventArgs e)
        {
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            MySqlCommand cmd = connectionstring.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from society where society_id='"+soc_id+"'";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            society = dt.Rows[0]["society_name"].ToString();
            dt.Rows.Clear();
            cmd.CommandText = "select * from announcement where society_society_id='" + soc_id + "'and `read`='" + "read" + "'";
            
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

                    dataGridView1.Rows.Add("Principal",society,subject,message);
                }
            }
        }

        public Form13(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
        }
    }
}
