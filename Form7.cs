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
    public partial class Form7 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,stu_id,admission;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        //initialize sql command variable and datatable variable
  

        //initializing form inputs variables
        int check_int, date, month;
        int admission_number, year, age;


        private void btn_search_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if(connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            if (txt_search.Text == null)
            {
                MessageBox.Show("You haven't entered any student name or admission number ","Error",MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from student where student_name='" + txt_search.Text + "'or student_admission='" + txt_search + "'";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    label17.Visible = true;
                    label17.Text = "There is no Student for that name or ID, Chek the name and try again...";
                }
                else
                {
                    label17.Visible = false;
                    panel1.Visible = true;
                    stu_id = Convert.ToInt32(dt.Rows[0]["student_id"].ToString());
                    txt_fullname.Text = dt.Rows[0]["full_name"].ToString();
                    txt_admission.Text = dt.Rows[0]["student_admission"].ToString();
                    admission= Convert.ToInt32(dt.Rows[0]["student_admission"].ToString());
                    txt_homecontact.Text = dt.Rows[0]["home_contact"].ToString();
                    txt_workcontact.Text = dt.Rows[0]["work_contact"].ToString();
                    txt_shortname.Text = dt.Rows[0]["student_name"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["d_o_b"].ToString()); 
                    comboBox4.Text = dt.Rows[0]["relationship"].ToString();
                    txt_guardian.Text = dt.Rows[0]["guardian_name"].ToString();

                    if (dt.Rows[0]["gender"].ToString() == "Male")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (dt.Rows[0]["gender"].ToString() == "Female")
                    {
                        radioButton2.Checked = true;
                    }

                    connectionstring.Close();
                }
            }
        }

        string full_name, short_name, guardian, relation, gender, home_contact, work_contact;
        public Form7()
        {
            InitializeComponent();

            panel1.Visible = false;

        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            if (txt_fullname.Text == "" || txt_shortname.Text == "" || txt_admission.Text == "" || txt_guardian.Text == "" ||
                   comboBox4.Text == "" ||  (txt_homecontact.Text == ""
                   && txt_workcontact.Text == "") || int.TryParse(txt_fullname.Text, out check_int) || int.TryParse(txt_shortname.Text, out check_int)
                   || !int.TryParse(txt_admission.Text, out check_int) || int.TryParse(txt_guardian.Text, out check_int) || int.TryParse(comboBox4.Text, out check_int)
                   || !int.TryParse(txt_homecontact.Text, out check_int) || !int.TryParse(txt_workcontact.Text, out check_int) || (radioButton1.Checked == false && radioButton2.Checked == false))
            {
                MessageBox.Show("Some areas not filled or Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    //Assign initial values
                    full_name = txt_fullname.Text;
                    short_name = txt_shortname.Text;

                    home_contact = txt_homecontact.Text;
                    work_contact = txt_workcontact.Text;
                    relation = comboBox4.SelectedItem.ToString();
                    guardian = txt_guardian.Text;
                    string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    admission_number = Convert.ToInt32(txt_admission.Text);
                    age = DateTime.Today.Year - dateTimePicker1.Value.Year ;

                    //Assign gendervalues
                    if (radioButton1.Checked == true)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }

                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update student set student_name='" + txt_fullname.Text + "',d_o_b='" + theDate + "',gender='" + gender + "',full_name='" + full_name + "',guardian_name='" + guardian + "',relationship='" + relation + "'," +
                    "home_contact='" + home_contact + "',work_contact='" + work_contact + "',student_admission='" + txt_admission.Text + "',age='" + age + "' where student_id = '" + stu_id + "'";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Students Updated Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    connectionstring.Close();

                    panel1.Visible = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
