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
    public partial class Form16 : Form
    {
        int start, end, date, month, year;
        //Initializing mysql connection and datatable row counter
        int i, checked_in;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form16()
        {
            InitializeComponent();
            panel1.Visible = false;
        }

        private void btn_check_Click(object sender, EventArgs e)
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
                cmd.CommandText = "select * from student where student_name='" + txt_studentname.Text + "'or full_name='" + txt_studentname.Text + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                //convert string to integer
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    MessageBox.Show("Invalid Student Name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    cmd.CommandText = "select * from sport where sport_name='" + txt_sportname.Text + "'";

                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 0)
                    {
                        MessageBox.Show("Invalid Sport Name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("");
                        panel1.Visible = true;
                        txt_student.Text = txt_studentname.Text;
                        txt_sport.Text = txt_sportname.Text;
                        textBox3.Text = DateTime.Today.Day.ToString();
                        textBox4.Text = DateTime.Today.Month.ToString();
                        textBox5.Text = DateTime.Today.Year.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }

        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            txt_studentname.Text = "";
            txt_sportname.Text = "";
        }

        private void btn_payment_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Payment Success!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

    }
}
