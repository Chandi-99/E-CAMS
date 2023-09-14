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
    public partial class Form44 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in,student_id,team_id,spo_id;
        int date, month, year, age;
        string position,team;
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
                if (txt_studentname.Text == "" || txt_admission.Text == "" || txt_position.Text == "" || txt_team.Text == "" || txt_year.Text == "" || int.TryParse(txt_studentname.Text, out check_in) ||
               int.TryParse(txt_position.Text, out check_in) || !int.TryParse(txt_year.Text, out check_in) || !int.TryParse(txt_admission.Text, out check_in))
                {
                    MessageBox.Show("Invalid inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd1 = connectionstring.CreateCommand();
                    cmd1.CommandType = CommandType.Text;

                    cmd1.CommandText = "select * from team where age_catagary='" + txt_team.Text + "' and sport_sport_id='"+spo_id+"'";
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    cmd1.ExecuteNonQuery();

                    i = Convert.ToInt32(dt1.Rows.Count);

                    if (i == 1)
                    {
                        team_id = Convert.ToInt32(dt1.Rows.Count);
                        if (txt_position.Text == position && txt_team.Text == team)
                        {
                            cmd1.CommandText = "update student_has_sport set end='"+textBox1.Text +"',status='finished' where student_student_id='"+ student_id +"' and sport_sport_id='"+ spo_id+"' and position='"+position+"'";
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Member Information Updated Successfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                        }
                        
                        else if (txt_position.Text == position && txt_team.Text != team)
                        {
                            cmd1.CommandText = "update student_has_sport set end='" + textBox1.Text + "',status='finished' where student_student_id='" + student_id + "' and sport_sport_id='" + spo_id + "'";
                            cmd1.ExecuteNonQuery();
                            cmd1.CommandText = "insert into student_has_sport(student_student_id,sport_sport_id,position,since,team_team_id,end,status) values('"+ student_id+"','"+ spo_id+"','"+ txt_position.Text+"','"+ txt_year.Text+"','"+team_id+"','"+ textBox1.Text+"','0')";
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Member Information Updated Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        
                        }

                    }
                    else if (i==0)
                    {
                        MessageBox.Show("Invalid Age Catagary!","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
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
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }

            MySqlCommand cmd1 = connectionstring.CreateCommand();
            cmd1.CommandType = CommandType.Text;

            cmd1.CommandText = "select * from student where student_name='" + txt_search.Text + "'or full_name='" + txt_search.Text + "'";
            DataTable dt1 = new DataTable();
            MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
            da1.Fill(dt1);
            cmd1.ExecuteNonQuery();

            i = Convert.ToInt32(dt1.Rows.Count.ToString());
            if (i == 0)
            {
                label16.Text = "There is no student for that name";
            }
            else
            {
                panel1.Visible = true;

                i = Convert.ToInt32(dt1.Rows.Count.ToString());

                if (i == 1)
                {
                    student_id = Convert.ToInt32(dt1.Rows[0]["student_id"].ToString());
                    txt_studentname.Text = dt1.Rows[0]["student_name"].ToString();
                    txt_admission.Text = dt1.Rows[0]["student_admission"].ToString();
                    dt1.Rows.Clear();
                    cmd1.CommandText = "select * from student_has_sport where student_student_id='" + student_id + "' and sport_sport_id='"+ spo_id +"' and status='0'";
                    da1.Fill(dt1);
                    cmd1.ExecuteNonQuery();
                    txt_position.Text = position=dt1.Rows[0]["position"].ToString();
                    txt_year.Text = dt1.Rows[0]["since"].ToString();
                    team_id = Convert.ToInt32(dt1.Rows[0]["team_team_id"].ToString());
                    dt1.Rows.Clear();
                    cmd1.CommandText = "select * from team where team_id='" + team_id + "'";
                    da1.Fill(dt1);
                    cmd1.ExecuteNonQuery();
                    txt_team.Text = team= dt1.Rows[0]["age_catagary"].ToString();
                }
                else if (i == 0)
                {
                    MessageBox.Show("There is no Student registered for that name and admission number", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        public Form44(int sport_id)
        {
            InitializeComponent();
            panel1.Visible = false;
            spo_id = sport_id;
        }
    }
}
