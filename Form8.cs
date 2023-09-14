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
    public partial class Form8 : Form
    {
        //initialize  integer variables
        int check_int, date, month, year, contact, i, age,registere_num,teacher_id,subject_id;
        DateTime dob;

        //initialize  string variables
        string full_name, short_name, registered_number, gender, grade, class_number, qualification1, qualification2,
            qualification3, knowledge1, knowledge2, knowledge3, dateofbirth,k1,k2,k3;

        //initializing database connection
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
          "database=ecams_database;" + "password=facebook2018;");
        public Form8()
        {
            InitializeComponent();
            label23.Visible = false;
            panel1.Visible = false;
        }
    

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_fullname.Text == "" || txt_shortname.Text == "" || txt_registerednumber.Text == "" || txt_contact.Text == "" ||
                   comboBox4.Text == "" || comboBox1.Text == "" || 
                    comboBox6.Text == "" || txt_qualification1.Text == "" || int.TryParse(txt_fullname.Text, out check_int) || int.TryParse(txt_shortname.Text, out check_int)
                   || !int.TryParse(txt_registerednumber.Text, out check_int) || int.TryParse(txt_qualification1.Text, out check_int) || int.TryParse(txt_qualification2.Text, out check_int) ||
                   int.TryParse(txt_qualification3.Text, out check_int) || !int.TryParse(txt_contact.Text, out check_int) || (radioButton1.Checked == false && radioButton2.Checked == false) || int.TryParse(comboBox1.Text, out check_int) 
                   || !int.TryParse(comboBox4.Text, out check_int) || int.TryParse(comboBox6.Text, out check_int)
                   || int.TryParse(comboBox7.Text, out check_int) || int.TryParse(comboBox8.Text, out check_int))
                {
                    MessageBox.Show("Some areas not filled or Invalid Inputs Entered! ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if ((comboBox6.Text != "" && (comboBox6.Text == comboBox7.Text || comboBox6.Text == comboBox8.Text)) || (comboBox7.Text != "" && (comboBox7.Text == comboBox8.Text)))
                {
                        MessageBox.Show("You cannot input same knowledge area twice!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    //Assign initialize variables values
                    full_name = txt_fullname.Text;
                    short_name = txt_shortname.Text;
                    registered_number = txt_registerednumber.Text;
                    grade = comboBox4.Text;
                    class_number = comboBox1.Text;

                    //Assign gender value
                    if (radioButton1.Checked == true)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }

                    //Create teacher class string variable
                    string teacher_class = grade + " - " + class_number;

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
                    cmd.CommandText = "update teacher set gender='"+ gender+"', age= '"+ age + "', dob='" + theDate + "', qualification_1='" + qualification1+"', qualification_2='" + qualification2+"', qualification_3='" +qualification3 +"',knowledge_1='"+ knowledge1+ "',knowledge_2='" + knowledge2 + "',knowledge_3='" + knowledge3 + "',contact='"+ txt_contact.Text+"'" +
                    " where teacher_id='"+ teacher_id +"'";
                    cmd.ExecuteNonQuery();

                    MySqlCommand cmd2 = connectionstring.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd2);
                    DataTable dt = new DataTable();

                    if (comboBox6.Text !=k1  && k1==null)
                    {
                        cmd2.CommandText = "select * from subjects where subject_name='" + comboBox6.Text + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();

                        subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                        cmd2.CommandText = "insert into subjects_has_teacher (teacher_teacher_id,subjects_subject_id) values('" + teacher_id + "','" + subject_id + "')";
                        cmd2.ExecuteNonQuery();
                    }

                    if (comboBox7.Text != k2 && k2==null)
                    {
                        cmd2.CommandText = "select * from subjects where subject_name='" + comboBox7.Text + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();

                        subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                        cmd2.CommandText = "insert into subjects_has_teacher (teacher_teacher_id,subjects_subject_id) values('" + teacher_id + "','" + subject_id + "')";
                        cmd2.ExecuteNonQuery();
                    }

                    if (comboBox8.Text != k3 &&  k3==null)
                    {
                        cmd2.CommandText = "select * from subjects where subject_name='" + comboBox8.Text + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();

                        subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                        cmd2.CommandText = "insert into subjects_has_teacher (teacher_teacher_id,subjects_subject_id) values('" + teacher_id + "','" + subject_id + "')";
                        cmd2.ExecuteNonQuery();
                    }

                    if (comboBox6.Text != k1 && k1 != null)
                    {
                        cmd2.CommandText = "select * from subjects where subject_name='" + k1 + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());

                        cmd2.CommandText = "select * from subjects where subject_name='" + comboBox6.Text + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();

                        subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                        cmd2.CommandText = "update subjects_has_teacher set subjects_subject_id='"+ subject_id+"' where (teacher_teacher_id='"+teacher_id +"'and subjects_subject_id='"+i +"')";
                        cmd2.ExecuteNonQuery();
                    }

                    if (comboBox7.Text != k2 && k2 != null)
                    {
                        cmd2.CommandText = "select * from subjects where subject_name='" + k2 + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());

                        cmd2.CommandText = "select * from subjects where subject_name='" + comboBox7.Text + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();

                        subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                        cmd2.CommandText = "update subjects_has_teacher set subjects_subject_id='" + subject_id + "' where (teacher_teacher_id='" + teacher_id + "'and subjects_subject_id='" + i + "')";
                        cmd2.ExecuteNonQuery();
                    }

                    if (comboBox8.Text != k3 && k3 != null)
                    {
                        cmd2.CommandText = "select * from subjects where subject_name='" + k3 + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());

                        cmd2.CommandText = "select * from subjects where subject_name='" + comboBox8.Text + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd2.ExecuteNonQuery();

                        subject_id = Convert.ToInt32(dt.Rows[0]["subject_id"].ToString());
                        cmd2.CommandText = "update subjects_has_teacher set subjects_subject_id='" + subject_id + "' where (teacher_teacher_id='" + teacher_id + "'and subjects_subject_id='" + i + "')";
                        cmd2.ExecuteNonQuery();
                    }

                    MessageBox.Show("Teacher information updated successfully","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                    panel1.Visible = false;
                }
        }
            //Exception handling
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Visible = false;
                label23.Visible = false;
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_search.Text == null)
                {
                    MessageBox.Show("You haven't entered any Teacher name or registered number ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from teacher where teacher_name='" + txt_search.Text + "'or registered_number='" + txt_search + "'";

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 0)
                    {
                        label23.Visible = true;
                        label23.Text = "There is no teacher for that name or registered number, Chek the name and try again...";
                    }
                    else
                    {
                        teacher_id = Convert.ToInt32(dt.Rows[0]["teacher_id"].ToString());
                        panel1.Visible = true;
                        txt_fullname.Text = dt.Rows[0]["full_name"].ToString();
                        txt_shortname.Text = dt.Rows[0]["teacher_name"].ToString();
                        txt_registerednumber.Text = dt.Rows[0]["registered_number"].ToString();

                        dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["dob"].ToString()); 
                        if (dt.Rows[0]["gender"].ToString() == "Male")
                        {
                            radioButton1.Checked = true;
                        }
                        else if (dt.Rows[0]["gender"].ToString() == "Female")
                        {
                            radioButton2.Checked = true;
                        }

                        comboBox1.SelectedItem = dt.Rows[0]["grade"].ToString();
                        comboBox4.SelectedItem = dt.Rows[0]["class"].ToString();
                        txt_qualification1.Text = dt.Rows[0]["qualification_1"].ToString();
                        txt_qualification2.Text = dt.Rows[0]["qualification_2"].ToString();
                        txt_qualification3.Text = dt.Rows[0]["qualification_3"].ToString();
                        comboBox6.SelectedItem = k1=dt.Rows[0]["knowledge_1"].ToString();
                        comboBox7.SelectedItem = k2=dt.Rows[0]["knowledge_2"].ToString();
                        comboBox8.SelectedItem =k3= dt.Rows[0]["knowledge_3"].ToString();
                        txt_contact.Text = dt.Rows[0]["contact"].ToString();
                        comboBox4.Text = dt.Rows[0]["grade"].ToString();
                        comboBox1.Text = dt.Rows[0]["class"].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
           
        }

       
    }
}
