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
    public partial class Form66 : Form
    {
        int i, check_in;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_message.Text == "" || txt_subject.Text == "" || comboBox1.Text == "" || int.TryParse(txt_subject.Text, out check_in) || int.TryParse(comboBox1.Text, out check_in))
                {
                    MessageBox.Show("Invalid user inputs!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select sport_name from sport where sport_name='" + comboBox1.Text + "' ";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (comboBox1.Text != "All")
                    {
                        if (i == 0)
                        {
                            MessageBox.Show("Invalid Sport Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        cmd.CommandText = "insert into announcement (subject,reciever,message) values ('"+ txt_subject.Text+ "','" + txt_message.Text + "','" + comboBox1.Text + "')";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_message.Text = txt_subject.Text = comboBox1.Text = "";
        }

        private void Form66_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select sport_name from sport ";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());
                comboBox1.Items.Add("All");
                for (int j = 0; j < i; j++)
                {
                    comboBox1.Items.Add(dt.Rows[j]["sport_name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
           

        public Form66()
        {
            InitializeComponent();
        }
    }
}
