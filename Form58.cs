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
    public partial class Form58 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, day, month, year, check_int, experience, age;

        string gender;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form58()
        {
            InitializeComponent();
            for(int i = 1980; i < DateTime.Today.Year; i++)
            {
                comboBox4.Items.Add(i);
            }

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_name.Text = txt_contact.Text = comboBox4.Text = txt_position.Text = txt_password.Text = txt_confirm_password.Text = txt_username.Text="";
            dateTimePicker1.Value.Equals(null);  
            radioButton1.Checked = radioButton2.Checked =false;
            dateTimePicker1.Value = DateTime.Today.Date;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                //Errors Analysing 
                if (txt_name.Text == "" || txt_contact.Text == "" || comboBox4.Text == "" || txt_position.Text == "" || txt_password.Text == "" || txt_confirm_password.Text == "" || txt_username.Text == "" ||
                    int.TryParse(txt_name.Text, out check_int)
                    || !int.TryParse(comboBox4.Text, out check_int) || !int.TryParse(txt_contact.Text, out check_int)
                     || (radioButton1.Checked == false && radioButton2.Checked == false) || dateTimePicker1.Value.ToString() == "")
                {
                    MessageBox.Show("Some areas not filled or Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if (txt_password.Text.Length < 7)
                {
                    MessageBox.Show("Passwords must be contain atleast 8 characters!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if (txt_password.Text != txt_confirm_password.Text)
                {
                    MessageBox.Show("Passwords are not matched!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd3 = connectionstring.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "select * from staff where staff_member_name='" + txt_name.Text + "'";
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd3);
                    da1.Fill(dt1);
                    cmd3.ExecuteNonQuery();
                    i = Convert.ToInt32(dt1.Rows.Count);

                    if (i == 1)
                    {
                        MessageBox.Show("There is another one registered for that name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                    else if(i==0)
                    {
                        experience = int.Parse(comboBox4.Text);
                        string db = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                        age = DateTime.Today.Year - dateTimePicker1.Value.Year;

                        //Assign gendervalues
                        if (radioButton1.Checked == true)
                        {
                            gender = "Male";
                        }
                        else
                        {
                            gender = "Female";
                        }

                        MySqlCommand cmd1 = connectionstring.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = (@"insert into user (user_name,password,user_type) 
                    values('" + txt_username.Text + "','" + txt_password.Text + "','" + "staff" + "')");
                        cmd1.ExecuteNonQuery();

                        MySqlCommand cmd2 = connectionstring.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = ("select * from user");
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd2);
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();

                        int j = dt.Rows.Count;
                        i = Convert.ToInt32(dt.Rows[j - 1]["user_id"]);
                        //initialize sql command variable and datatable variable
                        MySqlCommand cmd = connectionstring.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        //sql command
                        cmd.CommandText = (@"insert into staff (staff_member_name,position,d_o_b,working_since,contact,gender,age,user_user_id) 
                    values('" + txt_name.Text + "','" + txt_position.Text + "','" + db + "','" + comboBox4.Text + "','" + txt_contact.Text + "','" + gender + "','" + age + "','" + i + "')");
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Information Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    }
                }

            }
            //Exception handling
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
