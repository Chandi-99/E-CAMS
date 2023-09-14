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
    public partial class Form35 : Form
    {
        int i, soc_id,ach_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form35(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
        }

        private void Form35_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from achievement where society_society_id='" + soc_id + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());

                for (int j = 0; j < i; j++)
                { 
                    dataGridView1.Rows.Add(dt.Rows[j]["competition"].ToString(), dt.Rows[j]["type"].ToString(),
                    dt.Rows[j]["date"].ToString(), dt.Rows[j]["venue"].ToString(), dt.Rows[j]["result"].ToString(),dt.Rows[j]["leader"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
