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
    public partial class Form21 : Form
    {
        int i, checked_in,soc_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void Form21_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                //initialize sql command variable and datatable variable
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //sql command
                cmd.CommandText = "select * from society where society_id='"+ soc_id+"'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                //convert string to integer
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 1)
                {
                    richTextBox1.Text = dt.Rows[0]["history"].ToString();
                    richTextBox2.Text = dt.Rows[0]["vision"].ToString();
                    richTextBox3.Text = dt.Rows[0]["mission"].ToString();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            
        }

        public Form21(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
        }
    }
}
