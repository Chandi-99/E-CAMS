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
    public partial class Form17 : Form
    {
        int start, end, date, month, year;
        //Initializing mysql connection and datatable row counter
        int i, checked_in;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form17()
        {
            InitializeComponent();
            label2.Visible = false;
            textBox3.Text = DateTime.Today.Day.ToString();
            textBox4.Text = DateTime.Today.Month.ToString();
            textBox5.Text = DateTime.Today.Year.ToString();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_student.Text = "";
            txt_society_sport.Text = "";
        }

        private void btn_payment_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_student.Text == "" || txt_society_sport.Text == "" || txt_purpose.Text == "" || txt_amount.Text == "" || int.TryParse(txt_student.Text, out checked_in) ||
             int.TryParse(txt_society_sport.Text, out checked_in) || !int.TryParse(txt_amount.Text, out checked_in) || int.TryParse(txt_purpose.Text, out checked_in))
                {
                    MessageBox.Show("Invalid inputs entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    //initialize sql command variable and datatable variable
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    //sql command
                    cmd.CommandText = "select * from student where student_name='" + txt_student.Text + "'or full_name='" + txt_student.Text + "'";
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
                        cmd.CommandText = "select * from sport where sport_name='" + txt_society_sport.Text + "'";

                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count.ToString());
                        if (i == 0)
                        {
                            MessageBox.Show("Invalid Society or Sport Name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.CommandText = "select * from society where society_name='" + txt_society_sport.Text + "'";

                            da.Fill(dt);
                            cmd.ExecuteNonQuery();

                            i = Convert.ToInt32(dt.Rows.Count.ToString());
                            if (i == 0)
                            {
                                MessageBox.Show("Invalid Society or Sport Name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Payment Success!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            
    }

}

