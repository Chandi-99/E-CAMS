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
    public partial class Form38 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,spo_id,t1,t2,t3,t4,t5;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form38(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
        }

        private void Form38_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from teacher_has_sport where sport_sport_id='" + spo_id + "'and position ='" + "head" + "' and status='0'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count);
                if (i == 1)
                {
                    t1 = Convert.ToInt32(dt.Rows[0]["teacher_teacher_id"].ToString());
                    dt.Rows.Clear();
                    cmd.CommandText = "select * from teacher where teacher_id='" + t1 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_teacher1.Text = dt.Rows[0]["teacher_name"].ToString();
                    dt.Rows.Clear();
                }

                cmd.CommandText = "select * from teacher_has_sport where sport_sport_id='" + spo_id + "'and position ='" + "sub" + "' and status='0'";
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count);
                if (i == 1)
                {
                    t1 = Convert.ToInt32(dt.Rows[0]["teacher_teacher_id"].ToString());
                    dt.Rows.Clear();
                    cmd.CommandText = "select * from teacher where teacher_id='" + t1 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_teacher2.Text = dt.Rows[0]["teacher_name"].ToString();
                }

                dt.Rows.Clear();
                cmd.CommandText = "select * from coach where sport_sport_id='" + spo_id + "'and position ='" + "head" + "'and status='0'";
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count);
                if (i == 1)
                {
                    textBox3.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();
                }


                cmd.CommandText = "select * from team where sport_sport_id='" + spo_id + "'";
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(dt.Rows.Count);

                if (i == 1)
                {
                    label36.Text = label3.Text=dt.Rows[0]["age_catagary"].ToString();
                    t1 = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                    txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                    txt_team1_cap.Text = dt.Rows[0]["vice_captain"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t1 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team1_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();
                }
                else if (i == 2)
                {
                    label36.Text = label3.Text=dt.Rows[0]["age_catagary"].ToString();
                    label34.Text = label4.Text = dt.Rows[1]["age_catagary"].ToString();
                    t1 = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                    t2 = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                    txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                    txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();
                    txt_team2_cap.Text = dt.Rows[1]["captain"].ToString();
                    txt_team2_vice.Text = dt.Rows[1]["vice_captain"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t1 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team1_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t2 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team2_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                }
                else if (i == 3)
                {
                    label36.Text = label3.Text = dt.Rows[0]["age_catagary"].ToString();
                    label34.Text = label4.Text = dt.Rows[1]["age_catagary"].ToString();
                    label32.Text = label5.Text = dt.Rows[1]["age_catagary"].ToString();
                    t1 = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                    t2 = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                    t3 = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                    txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                    txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();
                    txt_team2_cap.Text = dt.Rows[1]["captain"].ToString();
                    txt_team2_vice.Text = dt.Rows[1]["vice_captain"].ToString();
                    txt_team3_cap.Text = dt.Rows[2]["captain"].ToString();
                    txt_team3_vice.Text = dt.Rows[2]["vice_captain"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t1 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team1_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t2 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team2_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t3 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team3_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();
                }
                else if (i == 4)
                {
                    label36.Text = label3.Text = dt.Rows[0]["age_catagary"].ToString();
                    label34.Text = label4.Text = dt.Rows[1]["age_catagary"].ToString();
                    label32.Text = label5.Text = dt.Rows[1]["age_catagary"].ToString();
                    label30.Text = label6.Text = dt.Rows[1]["age_catagary"].ToString();
                    t1 = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                    t2 = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                    t3 = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                    t4 = Convert.ToInt32(dt.Rows[3]["coach_coach_id"].ToString());
                    txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                    txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();
                    txt_team2_cap.Text = dt.Rows[1]["captain"].ToString();
                    txt_team2_vice.Text = dt.Rows[1]["vice_captain"].ToString();
                    txt_team3_cap.Text = dt.Rows[2]["captain"].ToString();
                    txt_team3_vice.Text = dt.Rows[2]["vice_captain"].ToString();
                    txt_team4_cap.Text = dt.Rows[3]["captain"].ToString();
                    txt_team4_vice.Text = dt.Rows[3]["vice_captain"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t1 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team1_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t2 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team2_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t3 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team3_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t4 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team4_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();
                }
                else if (i == 5)
                {
                    label36.Text = label3.Text = dt.Rows[0]["age_catagary"].ToString();
                    label34.Text = label4.Text = dt.Rows[1]["age_catagary"].ToString();
                    label32.Text = label5.Text = dt.Rows[1]["age_catagary"].ToString();
                    label30.Text = label6.Text = dt.Rows[1]["age_catagary"].ToString();
                    label28.Text = label7.Text = dt.Rows[1]["age_catagary"].ToString();
                    t1 = Convert.ToInt32(dt.Rows[0]["coach_coach_id"].ToString());
                    t2 = Convert.ToInt32(dt.Rows[1]["coach_coach_id"].ToString());
                    t3 = Convert.ToInt32(dt.Rows[2]["coach_coach_id"].ToString());
                    t4 = Convert.ToInt32(dt.Rows[3]["coach_coach_id"].ToString());
                    t5 = Convert.ToInt32(dt.Rows[4]["coach_coach_id"].ToString());
                    txt_team1_cap.Text = dt.Rows[0]["captain"].ToString();
                    txt_team1_vice.Text = dt.Rows[0]["vice_captain"].ToString();
                    txt_team2_cap.Text = dt.Rows[1]["captain"].ToString();
                    txt_team2_vice.Text = dt.Rows[1]["vice_captain"].ToString();
                    txt_team3_cap.Text = dt.Rows[2]["captain"].ToString();
                    txt_team3_vice.Text = dt.Rows[2]["vice_captain"].ToString();
                    txt_team4_cap.Text = dt.Rows[3]["captain"].ToString();
                    txt_team4_vice.Text = dt.Rows[3]["vice_captain"].ToString();
                    txt_team5_cap.Text = dt.Rows[4]["captain"].ToString();
                    txt_team5_vice.Text = dt.Rows[4]["vice_captain"].ToString();
                    dt.Rows.Clear();


                    cmd.CommandText = "select * from coach where coach_id='" + t1 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team1_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t2 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team2_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t3 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team3_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t4 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team4_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from coach where coach_id='" + t5 + "'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_team5_coach.Text = dt.Rows[0]["coach_name"].ToString();
                    dt.Rows.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }

}
    }
}
