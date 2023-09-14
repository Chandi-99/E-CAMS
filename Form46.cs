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
    public partial class Form46 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in ,spo_id;

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update sport set history='"+ richTextBox1.Text+"' where `sport_id`='"+ spo_id+"'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sport information updated succcessfully","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

            MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
                "database=ecams_database;" + "password=facebook2018;");


            private void Form46_Load(object sender, EventArgs e)
            {
                try
                {
                    if (connectionstring.State != ConnectionState.Open)
                    {
                        connectionstring.Open();
                    }
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from sport where sport_id='" + spo_id + "'";
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    richTextBox1.Text = dt.Rows[0]["history"].ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }

            public Form46(int sport_id)
            {
                InitializeComponent();
                spo_id = sport_id;
            }
        
    }
}

