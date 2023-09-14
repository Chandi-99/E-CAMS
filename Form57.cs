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
    public partial class Form57 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_int, experience, age,coach_id,sport_id;
        string position;
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State!=ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_fullname.Text == "" || txt_contact.Text == "" || comboBox4.Text == "" || txt_sport.Text == "" ||
                    int.TryParse(txt_fullname.Text, out check_int) || comboBox1.Text == "" || int.TryParse(comboBox1.Text, out check_int)
                    || !int.TryParse(comboBox4.Text, out check_int) || !int.TryParse(txt_contact.Text, out check_int)
                     || (radioButton1.Checked == false && radioButton2.Checked == false) )
                {
                    MessageBox.Show("Some areas not filled or Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                if (comboBox1.Text != "Head")
                {
                    if (comboBox1.Text != "Sub")
                    {
                        MessageBox.Show("Invalid position entered", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                else
                {
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
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    if (comboBox1.Text == "Head" && position!="Head")
                    {
                        cmd.CommandText = "select * from coach where position='Head' and sport_sport_id='" + sport_id + "' and `status`='0'";
                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 1)
                        {
                            int coach_id = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                            cmd.CommandText = "update coach set status='finished' where coach_id='" + coach_id + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                   
                    cmd.CommandText = "update coach set dob='" + db + "',experience='" + comboBox4.SelectedItem+"',contact='" + txt_contact.Text+"',gender='" + gender+"',age='" + age+"',position='"+ comboBox1.Text+"' where coach_id='" + coach_id + "'";
                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show("Information Updated Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
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
            panel1.Visible = false;
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            if (txt_search.Text == null)
            {
                MessageBox.Show("You haven't entered any Name! ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from coach where coach_name='" + txt_search.Text + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    label1.Visible = true;
                    label1.Text = "There is no Coach for that name, Chek the name and try again...";
                }
                else
                {
                    label1.Visible = false;
                    panel1.Visible = true;
                    sport_id = Convert.ToInt32(dt.Rows[0]["sport_sport_id"].ToString());
                    coach_id = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                    txt_fullname.Text = dt.Rows[0]["coach_name"].ToString();

                    cmd.CommandText = "select * from sport where sport_id='" + sport_id + "'";
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd);
                    da1.Fill(dt1);
                    txt_sport.Text= dt1.Rows[0]["sport_name"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["dob"].ToString());
                    comboBox4.Text = dt.Rows[0]["experience"].ToString();
                    comboBox1.Text = position=dt.Rows[0]["position"].ToString();


                    txt_contact.Text = dt.Rows[0]["contact"].ToString();
                    if (dt.Rows[0]["gender"].ToString() == "Male")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (dt.Rows[0]["gender"].ToString() == "Female")
                    {
                        radioButton2.Checked = true;
                    }

                }
            }
        }


        string gender;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form57()
        {
            InitializeComponent();

            panel1.Visible = false;
            for(int i = 0; i < 40; i++)
            {
                comboBox4.Items.Add(i);
            }
        }
    }
}
