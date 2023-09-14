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
    public partial class Form48 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in, date, month, year,spo_id, student_id,team_id;

        private void Form48_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from student_has_sport where sport_sport_id='" + spo_id + "' and status='0'";
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());

                for (int j = 0; j < i; j++)
                {
                    student_id = Convert.ToInt32(dt.Rows[j]["student_student_id"].ToString());
                    cmd.CommandText = "select * from student where student_id='" + student_id + "'";
                    da.Fill(dt1);
                    cmd.ExecuteNonQuery();

                    team_id = Convert.ToInt32(dt.Rows[j]["team_team_id"].ToString());
                    cmd.CommandText = "select * from team where team_id='" + team_id + "'";
                    da.Fill(dt2);
                    cmd.ExecuteNonQuery();

                    dataGridView1.Rows.Add(dt1.Rows[0]["student_name"].ToString(), dt1.Rows[0]["age"].ToString(), dt.Rows[j]["position"].ToString(), dt.Rows[j]["since"].ToString(), dt2.Rows[0]["age_catagary"].ToString());
                    dt1.Rows.Clear();
                    dt2.Rows.Clear();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
           "database=ecams_database;" + "password=facebook2018;");
        public Form48(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
        }
    }
}
