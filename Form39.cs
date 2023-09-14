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
    public partial class Form39 : Form
    {
        int i, check_in,spo_id,k,student_id,team_id;

        private void Form39_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from team where `sport_sport_id`='" + spo_id + "' ";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count);

                for (int j=0;j<i;j++)
                {
                    comboBox1.Items.Add(dt.Rows[j]["age_catagary"].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
        }

        //Initializing mysql connection and datatable row counter

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_studentname.Text = txt_admission.Text = txt_position.Text = txt_year.Text=comboBox1.Text = "";
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_studentname.Text == "" || txt_admission.Text == "" || txt_position.Text == "" || comboBox1.Text == "" || txt_year.Text == "" || int.TryParse(txt_studentname.Text, out check_in) ||
               int.TryParse(txt_position.Text, out check_in) || !int.TryParse(txt_year.Text, out check_in) || int.TryParse(comboBox1.Text, out check_in) || !int.TryParse(txt_admission.Text, out check_in))
                {
                    MessageBox.Show("Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from student where student_name='" + txt_studentname.Text +  "' and student_admission='"+txt_admission.Text+"'";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    //convert string to integer
                    i = Convert.ToInt32(dt.Rows.Count.ToString());

                    if (i == 1)
                    {
                        DataTable dt1 = new DataTable();
                        cmd.CommandText = "select * from team where age_catagary='"+ comboBox1.SelectedItem+"' and sport_sport_id='"+spo_id+"'";
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd);
                        da1.Fill(dt1);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt1.Rows.Count.ToString());
                        if (i == 1)
                        {
                            student_id = Convert.ToInt32(dt.Rows[0]["student_id"].ToString());
                            team_id = Convert.ToInt32(dt1.Rows[0]["team_id"].ToString());

                            
                            if (txt_position.Text.ToLower() == "captain")
                            {
                                dt.Rows.Clear();
                                cmd.CommandText = "select * from `student_has_sport` where sport_sport_id='" + spo_id + "' and team_team_id='" + team_id + "'and status='0'";
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();
                                k = Convert.ToInt32(dt.Rows.Count);
                                if (k == 0)
                                {
                                    cmd.CommandText = "insert into `student_has_sport`(student_student_id,sport_sport_id,position,since,team_team_id,status) values('" + student_id + "','" + spo_id + "','" + txt_position.Text + "','" + txt_year.Text + "','" + team_id + "','0')";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "update team set captain='"+txt_studentname.Text+"' where team_id='"+team_id+"'";
                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Students Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                                else if (k==1)
                                {
                                    cmd.CommandText = "update `student_has_sport` set status='finished', end='"+DateTime.Today.Year+"' where student_student_id='"+ Convert.ToInt32(dt.Rows[0]["student_student_id"].ToString())+"' and sport_sport_id='"+ spo_id+"' and team_team_id='"+team_id+"'";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "insert into `student_has_sport`(student_student_id,sport_sport_id,position,since,team_team_id,status) values('" + student_id + "','" + spo_id + "','" + txt_position.Text + "','" + txt_year.Text + "','" + team_id + "','0')";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "update team set captain='" + txt_studentname.Text + "' where team_id='" + team_id + "'";
                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Students Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                            }

                           else  if (txt_position.Text.ToLower() == "vice captain")
                            {
                                dt.Rows.Clear();
                                cmd.CommandText = "select * from `student_has_sport` where sport_sport_id='" + spo_id + "' and team_team_id='" + team_id + "'and status='0'";
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();
                                k = Convert.ToInt32(dt.Rows.Count);
                                if (k == 0)
                                {
                                    cmd.CommandText = "insert into `student_has_sport`(student_student_id,sport_sport_id,position,since,team_team_id,status) values('" + student_id + "','" + spo_id + "','" + txt_position.Text + "','" + txt_year.Text + "','" + team_id + "','0')";

                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "update team set vice_captain='" + txt_studentname.Text + "' where team_id='" + team_id + "'";
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Students Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                                else if (k == 1)
                                {
                                    cmd.CommandText = "update `student_has_sport` set status='finished', end='" + DateTime.Today.Year + "' where student_student_id='" + Convert.ToInt32(dt.Rows[0]["student_student_id"].ToString()) + "' and sport_sport_id='" + spo_id + "' and team_team_id='" + team_id + "'";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "insert into `student_has_sport`(student_student_id,sport_sport_id,position,since,team_team_id,status) values('" + student_id + "','" + spo_id + "','" + txt_position.Text + "','" + txt_year.Text + "','" + team_id + "','0')";
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "update team set vice_captain='" + txt_studentname.Text + "' where team_id='" + team_id + "'";
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Students Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                cmd.CommandText = "insert into `student_has_sport`(student_student_id,sport_sport_id,position,since,team_team_id,status) values('" + student_id + "','" + spo_id + "','" + txt_position.Text + "','" + txt_year.Text + "','" + team_id + "','0')";

                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Students Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            MessageBox.Show("There is no team registered in that age catagory!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                       
                    }
                    else if(i==0)
                    {
                        MessageBox.Show("There is no Student registered for that name and admission number", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Form39(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;

        }
    }
}
