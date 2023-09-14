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
    public partial class Form54 : Form
    {
        int i,check_in;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form54()
        {
            InitializeComponent();
            panel1.Visible = false;

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                label16.Visible = false;
                panel1.Visible = false;
                txt_teacher1.Text = txt_teacher2.Text =  txt_team1_cap.Text = txt_team1_coach.Text = txt_team1_vice.Text =
                txt_team2_coach.Text = txt_team2_vice.Text = txt_team2_cap.Text = txt_team3_coach.Text = txt_team3_vice.Text = txt_team3_cap.Text =
                txt_team4_coach.Text = txt_team4_vice.Text = txt_team4_cap.Text = txt_team5_coach.Text = txt_team5_vice.Text = txt_team5_cap.Text = textBox3.Text = "";
                if (panel1.Visible != false)
                {
                    panel1.Visible = false;
                }
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_search.Text == "" || int.TryParse(txt_search.Text, out check_in))
                {
                    MessageBox.Show("Invalid Inputs Entered!", "Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                }
                else
                {

                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from sport where sport_name='" +
                    txt_search.Text  + "'";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    i = Convert.ToInt32(dt.Rows.Count.ToString());

                    if (i == 0)
                    {
                        label16.Visible = true;
                        label16.Text = "Invalid Sport Name !";
                        connectionstring.Close();
                    }
                    else
                    {
                        panel1.Visible = true;
                       
                        int sport_id = Convert.ToInt32(dt.Rows[0]["sport_id"].ToString());
                        MySqlCommand cmd1 = connectionstring.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "select * from teacher_has_sport where sport_sport_id='" +sport_id + "' and position='"+"head"+ "'and status='0'";
                        DataTable dt1 = new DataTable();
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                        da1.Fill(dt1);
                        cmd1.ExecuteNonQuery();
                        

                        i = Convert.ToInt32(dt1.Rows.Count);
                        if (i == 1)
                        {

                            int te_id = Convert.ToInt32(dt1.Rows[0]["teacher_teacher_id"].ToString());
                            MySqlCommand cmd4 = connectionstring.CreateCommand();
                            cmd4.CommandType = CommandType.Text;
                            cmd4.CommandText = "select * from teacher where teacher_id='" +
                            te_id + "'";
                            
                            DataTable dt5 = new DataTable();
                            MySqlDataAdapter da5 = new MySqlDataAdapter(cmd4);
                            da5.Fill(dt5);
                            cmd4.ExecuteNonQuery();
                           
                            txt_teacher1.Text = dt5.Rows[0]["teacher_name"].ToString();
                            
                        }

                       
                        MySqlCommand cmd2 = connectionstring.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "select * from teacher_has_sport where sport_sport_id='" +sport_id + "' and position='" + "sub" + "'and status='0'";
                        DataTable dt3 = new DataTable();
                        MySqlDataAdapter da3 = new MySqlDataAdapter(cmd2);
                        da3.Fill(dt3);
                        cmd2.ExecuteNonQuery();

                        i = Convert.ToInt32(dt3.Rows.Count.ToString());

                        if(i==1)
                        {
                            int te1_id = Convert.ToInt32(dt3.Rows[0]["teacher_teacher_id"].ToString());
                            dt3.Rows.Clear();
                            MySqlCommand cmd3 = connectionstring.CreateCommand();
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "select * from teacher where teacher_id='" + te1_id + "'";

                            DataTable dt4 = new DataTable();
                            MySqlDataAdapter da4 = new MySqlDataAdapter(cmd3);
                            da4.Fill(dt3);
                            cmd3.ExecuteNonQuery();

                            txt_teacher2.Text = dt3.Rows[0]["teacher_name"].ToString();
                        }
                        
                        cmd.CommandText = "select * from coach where sport_sport_id='" +sport_id + "' and position='"+"head"+ "'and status='0'";
                        dt.Rows.Clear();
                        MySqlDataAdapter da6 = new MySqlDataAdapter(cmd);
                        da6.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i!=0)
                        {
                            textBox3.Text = dt.Rows[0]["coach_name"].ToString();
                        }

                        dt.Rows.Clear();
                        cmd.CommandText = "select * from team where sport_sport_id='" + sport_id + "'";
                        MySqlDataAdapter da7 = new MySqlDataAdapter(cmd);
                        da7.Fill(dt);
                        cmd.ExecuteNonQuery();
                        int team_count = Convert.ToInt32(dt.Rows.Count.ToString());
                        if (team_count == 1)
                        {
                            label36.Text = label3.Text=dt.Rows[0]["age_catagary"].ToString();
                            int coach_id = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da8 = new MySqlDataAdapter(cmd);
                            da8.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team1_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                            txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();
                        }

                        if (team_count == 2)
                        {
                            label36.Text = label3.Text = dt.Rows[0]["age_catagary"].ToString();
                            label34.Text = label4.Text = dt.Rows[1]["age_catagary"].ToString();
                            int coach_id = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da8 = new MySqlDataAdapter(cmd);
                            da8.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team1_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da9 = new MySqlDataAdapter(cmd);
                            da9.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team2_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                            txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();

                            txt_team2_cap.Text = dt.Rows[1]["captain"].ToString();
                            txt_team2_vice.Text = dt.Rows[1]["vice_captain"].ToString();
                        }
                        if (team_count == 3)
                        {
                            label36.Text = label3.Text = dt.Rows[0]["age_catagary"].ToString();
                            label34.Text = label4.Text = dt.Rows[1]["age_catagary"].ToString();
                            label32.Text = label26.Text = dt.Rows[1]["age_catagary"].ToString();

                            int coach_id = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da8 = new MySqlDataAdapter(cmd);
                            da8.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team1_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da9 = new MySqlDataAdapter(cmd);
                            da9.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team2_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da10 = new MySqlDataAdapter(cmd);
                            da10.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team3_coach.Text = dt1.Rows[0]["coach_name"].ToString();
  
                            txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                            txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();

                            txt_team2_cap.Text = dt.Rows[1]["captain"].ToString();
                            txt_team2_vice.Text = dt.Rows[1]["vice_captain"].ToString();

                            txt_team3_cap.Text = dt.Rows[2]["captain"].ToString();
                            txt_team3_vice.Text = dt.Rows[2]["vice_captain"].ToString();

                            connectionstring.Close();

                        }
                        else if(team_count==4)
                        {
                            label36.Text = label3.Text = dt.Rows[0]["age_catagary"].ToString();
                            label34.Text = label4.Text = dt.Rows[1]["age_catagary"].ToString();
                            label32.Text = label26.Text = dt.Rows[1]["age_catagary"].ToString();
                            label30.Text = label14.Text = dt.Rows[1]["age_catagary"].ToString();

                            int coach_id = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da8 = new MySqlDataAdapter(cmd);
                            da8.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team1_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da9 = new MySqlDataAdapter(cmd);
                            da9.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team2_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da10 = new MySqlDataAdapter(cmd);
                            da10.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team3_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[3]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da11 = new MySqlDataAdapter(cmd);
                            da11.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team4_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                            txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();

                            txt_team2_cap.Text = dt.Rows[1]["captain"].ToString();
                            txt_team2_vice.Text = dt.Rows[1]["vice_captain"].ToString();

                            txt_team3_cap.Text = dt.Rows[2]["captain"].ToString();
                            txt_team3_vice.Text = dt.Rows[2]["vice_captain"].ToString();

                            txt_team4_cap.Text = dt.Rows[3]["captain"].ToString();
                            txt_team4_vice.Text = dt.Rows[3]["vice_captain"].ToString();

                            connectionstring.Close();
                        }
                        else if (team_count == 5)
                        {
                            label36.Text = label3.Text = dt.Rows[0]["age_catagary"].ToString();
                            label34.Text = label4.Text = dt.Rows[1]["age_catagary"].ToString();
                            label32.Text = label26.Text = dt.Rows[1]["age_catagary"].ToString();
                            label30.Text = label14.Text = dt.Rows[1]["age_catagary"].ToString();
                            label28.Text = label7.Text = dt.Rows[1]["age_catagary"].ToString();
                            int coach_id = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da8 = new MySqlDataAdapter(cmd);
                            da8.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team1_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da9 = new MySqlDataAdapter(cmd);
                            da9.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team2_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da10 = new MySqlDataAdapter(cmd);
                            da10.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team3_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[3]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da11 = new MySqlDataAdapter(cmd);
                            da11.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team4_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            coach_id = Convert.ToInt32(dt.Rows[4]["coach_coach_id"].ToString());
                            dt1.Rows.Clear();
                            cmd.CommandText = "select * from coach where coach_id='" + coach_id + "'";
                            MySqlDataAdapter da12 = new MySqlDataAdapter(cmd);
                            da12.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            txt_team5_coach.Text = dt1.Rows[0]["coach_name"].ToString();

                            txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                            txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();

                            txt_team2_cap.Text = dt.Rows[1]["captain"].ToString();
                            txt_team2_vice.Text = dt.Rows[1]["vice_captain"].ToString();

                            txt_team3_cap.Text = dt.Rows[2]["captain"].ToString();
                            txt_team3_vice.Text = dt.Rows[2]["vice_captain"].ToString();

                            txt_team4_cap.Text = dt.Rows[3]["captain"].ToString();
                            txt_team4_vice.Text = dt.Rows[3]["vice_captain"].ToString();

                            txt_team5_cap.Text = dt.Rows[3]["captain"].ToString();
                            txt_team5_vice.Text = dt.Rows[3]["vice_captain"].ToString();

                            connectionstring.Close();
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
