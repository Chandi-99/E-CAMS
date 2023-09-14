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
    public partial class Form31 : Form
    {
        int i, check_in, date, month, year,soc_id;

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void Form31_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from society where society_id='" + soc_id + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                richTextBox1.Text = dt.Rows[0]["history"].ToString();
                richTextBox2.Text = dt.Rows[0]["vision"].ToString();
                richTextBox3.Text = dt.Rows[0]["mission"].ToString();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox2.Text = richTextBox3.Text = "";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (richTextBox1.Text == "" || richTextBox2.Text == "" || richTextBox3.Text == "")
                {
                    MessageBox.Show("Invalid user inputs!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update society set `history`='" + richTextBox1.Text + "',`vision`='" + richTextBox2.Text + "',`mission`='" + richTextBox3.Text + "' where `society_id`='" + soc_id + "'";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Society information updated successfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
        }


        public Form31(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
        }
    }
}
