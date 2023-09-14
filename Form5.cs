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
    public partial class Form5 : Form
    {
        //initialize  integer variables
        int check_int, date, month, year, contact, i, age,user_id,subject_id;

        //initialize  string variables
        string full_name, short_name, registered_number, gender, grade, class_number, qualification1, qualification2,
            qualification3, knowledge1, knowledge2, knowledge3, dateofbirth;

        //initializing database connection
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
          "database=ecams_database;" + "password=facebook2018;");

        //reset button click event
        private void button8_Click(object sender, EventArgs e)
        {
            txt_fullname.Text = txt_registerednumber.Text = txt_contact.Text = txt_shortname.Text = comboBox1.Text = 
                comboBox4.Text =  comboBox6.Text = comboBox7.Text = comboBox8.Text = txt_password.Text = txt_confirm_password.Text = txt_username.Text = "";
        }

        //initialize teacher form
        public Form5()
        {
            InitializeComponent();
        }

        //Add button event
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_fullname.Text == "" || txt_shortname.Text == "" || txt_registerednumber.Text == "" || txt_contact.Text == "" || txt_password.Text == "" || txt_confirm_password.Text == "" || txt_username.Text == "" ||
                   comboBox4.Text == "" || comboBox1.Text == "" || 
                    comboBox6.Text == "" || txt_qualification1.Text == "" || int.TryParse(txt_fullname.Text, out check_int) || int.TryParse(txt_shortname.Text, out check_int)
                   || !int.TryParse(txt_registerednumber.Text, out check_int) || int.TryParse(txt_qualification1.Text, out check_int) || int.TryParse(txt_qualification2.Text, out check_int) ||
                   int.TryParse(txt_qualification3.Text, out check_int) || !int.TryParse(txt_contact.Text, out check_int) || (radioButton1.Checked == false && radioButton2.Checked == false) || int.TryParse(comboBox1.Text, out check_int) 
                   ||  !int.TryParse(comboBox4.Text, out check_int) ||  int.TryParse(comboBox6.Text, out check_int)
                   || int.TryParse(comboBox7.Text, out check_int) || int.TryParse(comboBox8.Text, out check_int))
                {
                    MessageBox.Show("Some areas not filled or Invalid Inputs Entered! ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
              
                else if ((comboBox6.Text!= "" && (comboBox6.Text == comboBox7.Text || comboBox6.Text == comboBox8.Text)) || (comboBox7.Text!="" &&(comboBox7.Text == comboBox8.Text)))
                {

                        MessageBox.Show("You cannot input same knowledge area twice!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else 
                {
                    MySqlCommand cmd1 = connectionstring.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select * from user where user_name='" +
                    txt_username.Text + "'";

                    cmd1.ExecuteNonQuery();

                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dt1);

                    i = Convert.ToInt32(dt1.Rows.Count);
                    if (i == 1)
                    {
                        MessageBox.Show("User Name is Already Taken!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
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
                        //Assign initialize variables values
                        full_name = txt_fullname.Text;
                        short_name = txt_shortname.Text;
                        registered_number = txt_registerednumber.Text;
                        grade = comboBox4.Text;
                        class_number = comboBox1.Text;
                        contact = Convert.ToInt32(txt_contact.Text);

                        //Assign gender value
                        if (radioButton1.Checked == true)
                        {
                            gender = "Male";
                        }
                        else
                        {
                            gender = "Female";
                        }


                        //Asiign qualifications string values
                        qualification1 = txt_qualification1.Text;
                        qualification2 = txt_qualification2.Text;
                        qualification3 = txt_qualification3.Text;

                        //Assign knowledge string values
                        knowledge1 = comboBox6.Text;
                        knowledge2 = comboBox7.Text;
                        knowledge3 = comboBox8.Text;

                        string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                        age = DateTime.Today.Year - dateTimePicker1.Value.Year;

                        //initialize sql command variable and datatable variable
                        MySqlCommand cmd = connectionstring.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        DataTable dt = new DataTable();

                        //sql command
                        cmd.CommandText = "select * from teacher where registered_number='" + txt_registerednumber.Text + "'";

                        //initialize DataAdapter
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        //convert string to integer
                        i = Convert.ToInt32(dt.Rows.Count);
                        dt.Rows.Clear();
                        if (i == 0)
                        {
                            cmd.CommandText = "insert into user(user_name, password, user_type) values('"+ txt_username.Text+"','"+ txt_password.Text+"','"+ "teacher"+"')";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "select * from user";
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();
                            i = Convert.ToInt32(dt.Rows.Count);
                            user_id = Convert.ToInt32(dt.Rows[i-1]["user_id"].ToString());
                            MySqlCommand cmd2 = new MySqlCommand(@"insert into teacher (teacher_name,class,gender,full_name,registered_number,dob,age,qualification_1,qualification_2,qualification_3,knowledge_1,knowledge_2,knowledge_3,contact,grade,user_user_id)
                            values('"+ txt_username.Text+ "','" + comboBox1.Text+"','" +gender +"','" + full_name+"','" + registered_number+"','" +theDate +"','" + age+"','" + txt_qualification1.Text+"','" + txt_qualification2.Text + "','" + txt_qualification3.Text + "','" + comboBox6.SelectedItem+"','" +comboBox7.SelectedItem +"','" + comboBox8.SelectedItem+"','" +txt_contact.Text +"','" +comboBox4.Text +"','"+user_id+"')", connectionstring);
                            cmd2.ExecuteNonQuery();
                            dt.Rows.Clear();
                            cmd.CommandText = "select * from `teacher`";
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();
                            i = Convert.ToInt32(dt.Rows.Count);
                            int teacher_id = Convert.ToInt32(dt.Rows[i-1]["teacher_id"].ToString());
                            
                            if (comboBox6.Text != null)
                            {
                                dt.Rows.Clear();
                                cmd.CommandText = "select * from subjects where subject_name='" + comboBox6.Text + "'";
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();

                                subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                                cmd.CommandText = "insert into subjects_has_teacher (teacher_teacher_id,subjects_subject_id) values('" + teacher_id + "','" + subject_id+"')";
                                cmd.ExecuteNonQuery();
                            }

                            if (comboBox7.Text != null)
                            {
                                dt.Rows.Clear();
                                cmd.CommandText = "select * from subjects where subject_name='" + comboBox7.Text + "'";
                                
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();

                                subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                                cmd.CommandText = "insert into subjects_has_teacher (teacher_teacher_id,subjects_subject_id) values('" + teacher_id + "','" + subject_id + "')";
                                cmd.ExecuteNonQuery();
                            }

                            if (comboBox8.Text != null)
                            {
                                dt.Rows.Clear();
                                cmd.CommandText = "select * from subjects where subject_name='" + comboBox8.Text + "'";
                                
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();

                                subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                                cmd.CommandText = "insert into subjects_has_teacher (teacher_teacher_id,subjects_subject_id) values('" + teacher_id + "','" + subject_id + "')";
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Teacher added Successfully","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("There is someone registered for the same Registered Number!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            connectionstring.Close();
                        }
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
