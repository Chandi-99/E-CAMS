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
    public partial class Form56 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,check_int,experience,age;
        string gender;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form56()
        {
            InitializeComponent();
            for(int i = 0; i < 50; i++)
            {
                comboBox4.Items.Add(i);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_fullname.Text = txt_contact.Text = comboBox4.Text = txt_sport.Text= "";
            comboBox1.Text = "";
            radioButton1.Checked = radioButton2.Checked = false;
            dateTimePicker1.Value = DateTime.Today.Date;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State!=ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_fullname.Text == "" || txt_contact.Text == "" || comboBox4.Text == "" || txt_sport.Text == "" ||comboBox1.Text=="" ||
                    int.TryParse(txt_fullname.Text, out check_int) || int.TryParse(comboBox1.Text, out check_int)
                    || !int.TryParse(comboBox4.Text, out check_int) || !int.TryParse(txt_contact.Text, out check_int)
                     || (radioButton1.Checked == false && radioButton2.Checked == false) )
                {
                    MessageBox.Show("Some areas not filled or Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd2 = connectionstring.CreateCommand();
                    cmd2.CommandType = CommandType.Text;

                    //sql command
                    cmd2.CommandText = ("select * from coach where coach_name= '" + txt_fullname.Text + "'");
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd2);
                    da1.Fill(dt1);
                    cmd2.ExecuteNonQuery();
                    i = Convert.ToInt32(dt1.Rows.Count.ToString());

                    if (i == 1)
                    {
                        MessageBox.Show("There is someone already registered for the same name.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (comboBox1.Text != "Head")
                        {
                            if (comboBox1.Text != "Sub")
                            {
                                MessageBox.Show("Invalid position entered", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                            else
                            {
                                experience = int.Parse(comboBox4.Text);

                                string db = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                                age = DateTime.Today.Year - dateTimePicker1.Value.Year;


                                //Assign gendervalues
                                if (radioButton1.Checked == true)
                                {
                                    gender = "Male";
                                }
                                else if (radioButton2.Checked == true)
                                {
                                    gender = "Female";
                                }
                                MySqlCommand cmd1 = connectionstring.CreateCommand();
                                cmd1.CommandType = CommandType.Text;

                                //sql command
                                cmd1.CommandText = ("select * from sport where sport_name= '" + txt_sport.Text + "' or registered_number='" + txt_sport.Text + "'");
                                DataTable dt = new DataTable();
                                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                                da.Fill(dt);
                                cmd1.ExecuteNonQuery();
                                i = Convert.ToInt32(dt.Rows.Count);
                                if (i == 0)
                                {
                                    MessageBox.Show("Invalid Sport Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }
                                else if (i == 1)
                                {
                                    int sport_id = Convert.ToInt32(dt.Rows[0]["sport_id"].ToString());
                                    MySqlCommand cmd = connectionstring.CreateCommand();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = (@"insert into coach (coach_name,sport,dob,experience,contact,gender,age,sport_sport_id,position,status) 
                                        values('" + txt_fullname.Text + "','" + txt_sport.Text + "','" + db + "','" + experience + "','" + txt_contact.Text + "','" + gender + "','" + age + "','" + sport_id + "','" + comboBox1.Text + "','0')");
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Information Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            experience = int.Parse(comboBox4.Text);

                            string db = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                            age = DateTime.Today.Year - dateTimePicker1.Value.Year;


                            //Assign gendervalues
                            if (radioButton1.Checked == true)
                            {
                                gender = "Male";
                            }
                            else if (radioButton2.Checked == true)
                            {
                                gender = "Female";
                            }
                            MySqlCommand cmd1 = connectionstring.CreateCommand();
                            cmd1.CommandType = CommandType.Text;

                            //sql command
                            cmd1.CommandText = ("select * from sport where sport_name= '" + txt_sport.Text + "' or registered_number='" + txt_sport.Text + "'");
                            DataTable dt = new DataTable();
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                            da.Fill(dt);
                            cmd1.ExecuteNonQuery();
                            i = Convert.ToInt32(dt.Rows.Count);
                            if (i == 0)
                            {
                                MessageBox.Show("Invalid Sport Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                            else if (i == 1)
                            {
                                int sport_id = Convert.ToInt32(dt.Rows[0]["sport_id"].ToString());
                                if (comboBox1.Text == "Head")
                                {
                                    cmd1.CommandText = "select * from coach where position='Head' and sport_sport_id='" + sport_id + "'and `status`='0'";
                                    dt.Rows.Clear();
                                    da.Fill(dt);
                                    cmd1.ExecuteNonQuery();

                                    i = Convert.ToInt32(dt.Rows.Count);

                                    if (i == 1)
                                    {
                                        int coach_id = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                                        MessageBox.Show(i.ToString());
                                        cmd1.CommandText = "update coach set `status`='finished' where coach_id='" + coach_id + "'";
                                        cmd1.ExecuteNonQuery();
                                    }
                                }

                                MySqlCommand cmd = connectionstring.CreateCommand();
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = (@"insert into coach (coach_name,sport,dob,experience,contact,gender,age,sport_sport_id,position,status) 
                                        values('" + txt_fullname.Text + "','" + txt_sport.Text + "','" + db + "','" + experience + "','" + txt_contact.Text + "','" + gender + "','" + age + "','" + sport_id + "','" + comboBox1.Text + "','0')");
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Information Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }


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
