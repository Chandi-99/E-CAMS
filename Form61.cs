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
    public partial class Form61 : Form
    {
        
        int i,spo_id,teacher_id,teacher_id1,teacher_id2,teacher_id3,co_id,head_coach_id,head_coach_id1,check_int;
        bool error = false;
        bool bool1=false,bool2=false,bool3=false,bool4=false,bool5=false;
        int since = Convert.ToInt32(DateTime.Now.Year.ToString());

        bool update_1 = false;
        bool update_2 = false;
        bool update_3 = false;

        string[] coach = new string[5];
        string[] agecat = new string[5];
        int[] coach_id = new int[5];
        int[] team_id = new int[5];

        string head, sub,head_coach;

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_sportname.Text == "" || txt_registerednumber.Text == "" || txt_playercount.Text == "" ||
               txt_teacherhead.Text == "" || txt_teachersub.Text == ""  || textBox1.Text == "" || int.TryParse(textBox1.Text,out check_int)
               || int.TryParse(txt_sportname.Text, out check_int)  || !int.TryParse(txt_registerednumber.Text, out check_int) || !int.TryParse(txt_playercount.Text, out check_int) ||
               int.TryParse(txt_teacherhead.Text, out check_int) || int.TryParse(txt_teachersub.Text, out check_int))
                {
                    MessageBox.Show("Some areas not filled or Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                //else if ((comboBox1.Text != null && (comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text || comboBox1.Text == comboBox4.Text || comboBox1.Text == comboBox5.Text)) ||
                //   (comboBox2.Text != null && (comboBox2.Text == comboBox3.Text || comboBox2.Text == comboBox4.Text || comboBox2.Text == comboBox5.Text)) || 
                //   (comboBox3.Text != null && (comboBox3.Text == comboBox4.Text || comboBox3.Text == comboBox5.Text)) ||
                //    (comboBox4.Text != null && (comboBox4.Text == comboBox5.Text)))
                //{
                //    MessageBox.Show("You have entered a same age catagory twice!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                //}

                else
                {
                    //Assign initial values
                    head = txt_teacherhead.Text;
                    sub = txt_teachersub.Text;

                    string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");


                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);


                    if (head != txt_teacherhead.Text)
                    {
                        cmd.CommandText = "select * from teacher where teacher_name='" + txt_teacherhead.Text + "'";
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("There is no teacher registered for that name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            teacher_id2 = Convert.ToInt32(dt.Rows[0]["teacher_id"].ToString());
                        }
                    }


                    if (sub != txt_teachersub.Text)
                    {
                        cmd.CommandText = "select * from teacher where teacher_name='" + txt_teachersub.Text + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("There is no teacher registered for that name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            teacher_id3 = Convert.ToInt32(dt.Rows[0]["teacher_id"].ToString()); 
                        }

                    }


                    if(comboBox1.Visible == true && comboBox1.Text!=null)
                    {

                        if (txt_age1_coach.Text == null)
                        {
                            error = true;
                            MessageBox.Show("There is no coach for one or more teams", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.CommandText = "select * from team where age_catagary='" + comboBox1.SelectedItem + "' and sport_sport_id='"+spo_id+"'";
                            dt.Rows.Clear();
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();

                            i = Convert.ToInt32(dt.Rows.Count);
                            if (i == 1)
                            {
                                error = true;
                                MessageBox.Show("This team is already created", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                            else
                            {
                                cmd.CommandText = "select * from coach where coach_name='" + txt_age1_coach.Text + "' and sport_sport_id='" + spo_id + "' and status='0'";
                                dt.Rows.Clear();
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();

                                i = Convert.ToInt32(dt.Rows.Count);
                                if (i == 0)
                                {
                                    error = true;
                                    MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    bool1 = true;
                                    coach_id[0] = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                                }
                            }
                        }


                    }

                    if (comboBox2.Visible == true && comboBox2.Text != null)
                    {

                        if (txt_age2_coach.Text == null)
                        {
                            error = true;
                            MessageBox.Show("There is no coach for one or more teams", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.CommandText = "select * from team where age_catagary='" + comboBox2.SelectedItem + "' and sport_sport_id='" + spo_id + "'";
                            dt.Rows.Clear();
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();

                            i = Convert.ToInt32(dt.Rows.Count);
                            if (i == 1)
                            {
                                error = true;
                                MessageBox.Show("This team is already created", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                            else
                            {
                                cmd.CommandText = "select * from coach where coach_name='" + txt_age2_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                                dt.Rows.Clear();
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();

                                i = Convert.ToInt32(dt.Rows.Count);
                                if (i == 0)
                                {
                                    error = true;
                                    MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    bool2 = true;
                                    coach_id[1] = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                                }
                            }
                        }
                    }

                    if (comboBox3.Visible == true && comboBox3.Text != null)
                    {

                        if (txt_age3_coach.Text == null)
                        {
                            error = true;
                            MessageBox.Show("There is no coach for one or more teams", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.CommandText = "select * from team where age_catagary='" + comboBox3.SelectedItem + "' and sport_sport_id='" + spo_id + "'";
                            dt.Rows.Clear();
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();

                            i = Convert.ToInt32(dt.Rows.Count);
                            if (i == 1)
                            {
                                error = true;
                                MessageBox.Show("This team is already created", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                            else
                            {
                                cmd.CommandText = "select * from coach where coach_name='" + txt_age3_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                                dt.Rows.Clear();
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();

                                i = Convert.ToInt32(dt.Rows.Count);
                                if (i == 0)
                                {
                                    error = true;
                                    MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    bool3 = true;
                                    coach_id[2] = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                                }
                            }
                        }
                    }

                    if (comboBox4.Visible == true && comboBox4.Text != null)
                    {

                        if (txt_age4_coach.Text == null)
                        {
                            error = true;
                            MessageBox.Show("There is no coach for one or more teams", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.CommandText = "select * from team where age_catagary='" + comboBox4.SelectedItem + "' and sport_sport_id='" + spo_id + "'";
                            dt.Rows.Clear();
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();

                            i = Convert.ToInt32(dt.Rows.Count);
                            if (i == 1)
                            {
                                error = true;
                                MessageBox.Show("This team is already created", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                            else
                            {
                                cmd.CommandText = "select * from coach where coach_name='" + txt_age4_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                                dt.Rows.Clear();
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();

                                i = Convert.ToInt32(dt.Rows.Count);
                                if (i == 0)
                                {
                                    error = true;
                                    MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    bool4 = true;
                                    coach_id[3] = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                                }
                            }
                        }
                    }

                    if (comboBox5.Visible == true && comboBox5.Text != null)
                    {

                        if (txt_age5_coach.Text == null)
                        {
                            error = true;
                            MessageBox.Show("There is no coach for one or more teams", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.CommandText = "select * from team where age_catagary='" + comboBox5.SelectedItem + "' and sport_sport_id='" + spo_id + "'";
                            dt.Rows.Clear();
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();

                            i = Convert.ToInt32(dt.Rows.Count);
                            if (i == 1)
                            {
                                error = true;
                                MessageBox.Show("This team is already created", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                            else
                            {
                                cmd.CommandText = "select * from coach where coach_name='" + txt_age5_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                                dt.Rows.Clear();
                                da.Fill(dt);
                                cmd.ExecuteNonQuery();

                                i = Convert.ToInt32(dt.Rows.Count);
                                if (i == 0)
                                {
                                    error = true;
                                    MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    bool5 = true;
                                    coach_id[4] = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                                }
                            }
                        }
                    }

                    if (comboBox5.Visible==false && coach[4]==txt_age5_coach.Text)
                    {
                        cmd.CommandText = "select * from coach where coach_name='" + txt_age5_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i==1)
                        {
                            cmd.CommandText="update team set coach_coach_id='"+ Convert.ToInt32(dt.Rows[0]["coach_id"].ToString())+"' where team_id='"+team_id[4]+"'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (comboBox4.Visible == false && coach[3] == txt_age4_coach.Text)
                    {
                        cmd.CommandText = "select * from coach where coach_name='" + txt_age4_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            cmd.CommandText = "update team set coach_coach_id='" + Convert.ToInt32(dt.Rows[0]["coach_id"].ToString()) + "' where team_id='" + team_id[3] + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (comboBox3.Visible == false && coach[2] == txt_age3_coach.Text)
                    {
                        cmd.CommandText = "select * from coach where coach_name='" + txt_age3_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            cmd.CommandText = "update team set coach_coach_id='" + Convert.ToInt32(dt.Rows[0]["coach_id"].ToString()) + "' where team_id='" + team_id[2] + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (comboBox2.Visible == false && coach[1] == txt_age2_coach.Text)
                    {
                        cmd.CommandText = "select * from coach where coach_name='" + txt_age2_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            cmd.CommandText = "update team set coach_coach_id='" + Convert.ToInt32(dt.Rows[0]["coach_id"].ToString()) + "' where team_id='" + team_id[1] + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (comboBox1.Visible == false && coach[0] == txt_age1_coach.Text)
                    {
                        cmd.CommandText = "select * from coach where coach_name='" + txt_age1_coach.Text + "' and sport_sport_id='" + spo_id + "'and status='0'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("Invalid coach name", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            cmd.CommandText = "update team set coach_coach_id='" + Convert.ToInt32(dt.Rows[0]["coach_id"].ToString()) + "' where team_id='" + team_id[0] + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (head != txt_teacherhead.Text)
                    {
                        cmd.CommandText = "select * from teacher where teacher_name='" + txt_teacherhead.Text + "'";
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("There is no teacher registered for that name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            update_1 = true;
                            teacher_id2 = Convert.ToInt32(dt.Rows[0]["teacher_id"].ToString());
                        }
                    }


                    if (sub != txt_teachersub.Text)
                    {
                        dt.Rows.Clear();
                        cmd.CommandText = "select * from teacher where teacher_name='" + txt_teachersub.Text + "'";
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("There is no teacher registered for that name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            update_2 = true;
                            teacher_id3 = Convert.ToInt32(dt.Rows[0]["teacher_id"].ToString());
                        }
                    }


                    if (head_coach != textBox1.Text)
                    {
                        dt.Rows.Clear();
                        cmd.CommandText = "select * from coach where coach_name='" + textBox1.Text + "'";
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i == 0)
                        {
                            error = true;
                            MessageBox.Show("There is no coach registered for that name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (i == 1)
                        {
                            update_3 = true;
                            head_coach_id1 = Convert.ToInt32(dt.Rows[0]["coach_id"].ToString());
                        }
                    }

                    if (error != true) 
                    {
                        if (update_1 == true)
                        {
                            cmd.CommandText = "update teacher_has_sport set status='" + "finished" + "' where teacher_teacher_id='" + teacher_id + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "insert into teacher_has_sport(sport_sport_id,teacher_teacher_id,position,status,since) values('" + spo_id + "','" + teacher_id2 + "','" + "head" + "','0','" + since + "')";
                            cmd.ExecuteNonQuery();
                        }

                        if (update_2 == true)
                        {
                            cmd.CommandText = "update teacher_has_sport set status='" + "finished" + "' where teacher_teacher_id='" + teacher_id1 + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "insert into teacher_has_sport (sport_sport_id,teacher_teacher_id,position,status,since) values('" + spo_id + "','" + teacher_id3 + "','" + "sub" + "','0','" + since + "')";
                            cmd.ExecuteNonQuery();
                        }

                        if (update_3==true)
                        {
                            cmd.CommandText = "update coach set status='" + "finished" + "' where coach_id='" + head_coach_id + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "update coach set position='" + "head" + "' where coach_id='" + head_coach_id1 + "'";
                            cmd.ExecuteNonQuery();
                        }
                        

                        if (bool1 == true)
                        {
                            cmd.CommandText = "insert into team (age_catagary,coach_coach_id,sport_sport_id) values('" + comboBox1.SelectedItem + "','" + coach_id[0] + "','" + spo_id + "')";
                            cmd.ExecuteNonQuery();
                        }

                        if (bool2 == true)
                        {
                            cmd.CommandText = "insert into team (age_catagary,coach_coach_id,sport_sport_id) values('" + comboBox2.SelectedItem + "','" + coach_id[1] + "','" + spo_id + "')";
                            cmd.ExecuteNonQuery();
                        }

                        if (bool3 == true)
                        {
                            cmd.CommandText = "insert into team (age_catagary,coach_coach_id,sport_sport_id) values('" + comboBox3.SelectedItem + "','" + coach_id[2] + "','" + spo_id + "')";
                            cmd.ExecuteNonQuery();
                        }

                        if (bool4 == true)
                        {
                            cmd.CommandText = "insert into team (age_catagary,coach_coach_id,sport_sport_id) values('" + comboBox4.SelectedItem + "','" + coach_id[3] + "','" + spo_id + "')";
                            cmd.ExecuteNonQuery();
                        }

                        if (bool5 == true)
                        {
                            cmd.CommandText = "insert into team (age_catagary,coach_coach_id,sport_sport_id) values('" + comboBox5.SelectedItem + "','" + coach_id[4] + "','" + spo_id + "')";
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Information Updated Successfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                    }
                }
            }
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
            if (txt_searchsport.Text == null)
            {
                MessageBox.Show("You haven't entered any sport name or registered number ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from sport where sport_name='" + txt_searchsport.Text + "'or sport_id='" + txt_searchsport.Text + "'";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    label6.Visible = true;
                    label6.Text = "There is no sport registered for searched sport name or ID, Chek the name and try again...";
                }
                else if(i==1)
                {
                    label6.Visible = false;
                    panel1.Visible = true;
                    spo_id = Convert.ToInt32(dt.Rows[0]["sport_id"].ToString());
                    txt_sportname.Text = dt.Rows[0]["sport_name"].ToString();
                    txt_registerednumber.Text= dt.Rows[0]["registered_number"].ToString();
                    dateTimePicker1.Value= Convert.ToDateTime(dt.Rows[0]["d_o_b"].ToString());
                    txt_playercount.Text = dt.Rows[0]["players_count"].ToString();

                    cmd.CommandText = "select * from teacher_has_sport where sport_sport_id='" + spo_id + "' and position='" + "head" + "' and status='0'";
                    dt.Rows.Clear();
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    i = Convert.ToInt32(dt.Rows.Count);
                    if (i == 0)
                    {
                        txt_teacherhead.Text = "";
                    }
                    else if (i == 1)
                    {
                        teacher_id = Convert.ToInt32(dt.Rows[0]["teacher_teacher_id"].ToString());
                        cmd.CommandText = "select * from teacher where teacher_id='" + teacher_id + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        txt_teacherhead.Text = head = dt.Rows[0]["teacher_name"].ToString();

                    }

                    cmd.CommandText = "select * from teacher_has_sport where sport_sport_id='" + spo_id + "' and position='" + "sub" + "'and status='0'";
                    dt.Rows.Clear();
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    i = Convert.ToInt32(dt.Rows.Count);
                    if (i == 0)
                    {
                        txt_teachersub.Text = "";
                    }
                    else if (i == 1)
                    {
                        teacher_id1 = Convert.ToInt32(dt.Rows[0]["teacher_teacher_id"].ToString());
                        cmd.CommandText = "select * from teacher where teacher_id='" + teacher_id1 + "'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        txt_teachersub.Text = sub = dt.Rows[0]["teacher_name"].ToString();

                    }

                    cmd.CommandText = "select * from team where sport_sport_id='"+ spo_id+"'";
                    dt.Rows.Clear();
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    DataTable dt1 = new DataTable();
                    i = Convert.ToInt32(dt.Rows.Count);
                    if (i == 0)
                    {
                        MessageBox.Show("There are no teams to display","Message",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                        comboBox1.Visible = comboBox2.Visible = comboBox3.Visible = comboBox4.Visible = comboBox5.Visible = true;
                    }
                    else if (i == 1)
                    {
                        team_id[0]= Convert.ToInt32(dt.Rows[0]["team_id"].ToString());
                        coach_id[0] = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[0] + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age1_coach.Text =coach[0]= dt1.Rows[0]["coach_name"].ToString();
                        textBox4.Text =agecat[0]= dt.Rows[0]["age_catagary"].ToString();
                         comboBox2.Visible = comboBox3.Visible = comboBox4.Visible = comboBox5.Visible = true;
                    }
                    else if (i == 2)
                    {
                        team_id[0] = Convert.ToInt32(dt.Rows[0]["team_id"].ToString());
                        team_id[1] = Convert.ToInt32(dt.Rows[1]["team_id"].ToString());
                        coach_id[0] = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[0] + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age1_coach.Text = coach[0] = dt1.Rows[0]["coach_name"].ToString();
                        textBox4.Text = agecat[0] = dt.Rows[0]["age_catagary"].ToString();

                        coach_id[1] = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[1] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age2_coach.Text = coach[1] = dt1.Rows[0]["coach_name"].ToString();
                        textBox5.Text = agecat[1] = dt.Rows[1]["age_catagary"].ToString();

                        comboBox3.Visible = comboBox4.Visible = comboBox5.Visible = true;
                    }
                    else if (i==3)
                    {
                        team_id[0] = Convert.ToInt32(dt.Rows[0]["team_id"].ToString());
                        team_id[1] = Convert.ToInt32(dt.Rows[1]["team_id"].ToString());
                        team_id[2] = Convert.ToInt32(dt.Rows[2]["team_id"].ToString());

                        coach_id[0] = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[0] + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age1_coach.Text = coach[0] = dt1.Rows[0]["coach_name"].ToString();
                        textBox4.Text = agecat[0] = dt.Rows[0]["age_catagary"].ToString();

                        coach_id[1] = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[1] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age2_coach.Text = coach[1] = dt1.Rows[0]["coach_name"].ToString();
                        textBox5.Text = agecat[1] = dt.Rows[1]["age_catagary"].ToString();

                        coach_id[2] = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[2] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age3_coach.Text = coach[2] = dt1.Rows[0]["coach_name"].ToString();
                        textBox6.Text = agecat[2] = dt.Rows[2]["age_catagary"].ToString();

                        comboBox4.Visible = comboBox5.Visible = true;
                    }
                    else if (i==4)
                    {
                        team_id[0] = Convert.ToInt32(dt.Rows[0]["team_id"].ToString());
                        team_id[1] = Convert.ToInt32(dt.Rows[1]["team_id"].ToString());
                        team_id[2] = Convert.ToInt32(dt.Rows[2]["team_id"].ToString());
                        team_id[3] = Convert.ToInt32(dt.Rows[3]["team_id"].ToString());

                        coach_id[0] = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[0] + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age1_coach.Text = coach[0] = dt1.Rows[0]["coach_name"].ToString();
                        textBox4.Text = agecat[0] = dt.Rows[0]["age_catagary"].ToString();

                        coach_id[1] = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[1] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age2_coach.Text = coach[1] = dt1.Rows[0]["coach_name"].ToString();
                        textBox5.Text = agecat[1] = dt.Rows[1]["age_catagary"].ToString();

                        coach_id[2] = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[2] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age3_coach.Text = coach[2] = dt1.Rows[0]["coach_name"].ToString();
                        textBox6.Text = agecat[2] = dt.Rows[1]["age_catagary"].ToString();

                        coach_id[3] = Convert.ToInt32(dt.Rows[3]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[3] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age4_coach.Text = coach[3] = dt1.Rows[0]["coach_name"].ToString();
                        textBox7.Text = agecat[3] = dt.Rows[3]["age_catagary"].ToString();

                         comboBox5.Visible = true;
                    }
                    else if (i == 5)
                    {
                        team_id[0] = Convert.ToInt32(dt.Rows[0]["team_id"].ToString());
                        team_id[1] = Convert.ToInt32(dt.Rows[1]["team_id"].ToString());
                        team_id[2] = Convert.ToInt32(dt.Rows[2]["team_id"].ToString());
                        team_id[3] = Convert.ToInt32(dt.Rows[3]["team_id"].ToString());
                        team_id[4] = Convert.ToInt32(dt.Rows[4]["team_id"].ToString());

                        coach_id[0] = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[0] + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age1_coach.Text = coach[0] = dt1.Rows[0]["coach_name"].ToString();
                        textBox4.Text = agecat[0] = dt.Rows[0]["age_catagary"].ToString();

                        coach_id[1] = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[1] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age2_coach.Text = coach[1] = dt1.Rows[0]["coach_name"].ToString();
                        textBox5.Text = agecat[1] = dt.Rows[1]["age_catagary"].ToString();

                        coach_id[2] = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[2] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age3_coach.Text = coach[2] = dt1.Rows[0]["coach_name"].ToString();
                        textBox6.Text = agecat[2] = dt.Rows[1]["age_catagary"].ToString();

                        coach_id[3] = Convert.ToInt32(dt.Rows[3]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[3] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age4_coach.Text = coach[3] = dt1.Rows[0]["coach_name"].ToString();
                        textBox7.Text = agecat[3] = dt.Rows[3]["age_catagary"].ToString();

                        coach_id[4] = Convert.ToInt32(dt.Rows[4]["coach_coach_id"].ToString());
                        cmd.CommandText = "select * from coach where coach_id='" + coach_id[4] + "'";
                        dt1.Rows.Clear();
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_age5_coach.Text = coach[4] = dt1.Rows[0]["coach_name"].ToString();
                        textBox8.Text = agecat[4] = dt.Rows[4]["age_catagary"].ToString();
                    }

                    cmd.CommandText = "select * from coach where sport_sport_id='"+spo_id+"' and position='head' and status='0'";
                    dt.Rows.Clear();
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    i = Convert.ToInt32(dt.Rows.Count);
                    if (i == 1)
                    {
                        textBox1.Text = head_coach=dt.Rows[0]["coach_name"].ToString();
                    }

                }
            }
        }


        public Form61()
        {
            InitializeComponent();
            panel1.Visible = false;
            comboBox1.Visible = comboBox2.Visible = comboBox3.Visible = comboBox4.Visible = comboBox5.Visible = false;
        }
    }
}
