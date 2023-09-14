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
    public partial class Form22 : Form
    {
        int i=0, checked_in,soc_id,student_id,teacher_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void Form22_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from student_has_society where society_society_id='" + soc_id + "'and top_board='" + "true" + "'and status='0'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                i = dt.Rows.Count;
                if (i == 0)
                {
                    MessageBox.Show("Top board students are not defined!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                }
                else
                {
                    for(int j = 0; j < i; j++)
                    {
                        student_id = Convert.ToInt32(dt.Rows[j]["student_student_id"].ToString());
                        cmd.CommandText = "select * from student where student_id='" + student_id + "'";
                        da.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        dataGridView1.Rows.Add(dt.Rows[j]["position"].ToString(), dt1.Rows[0]["student_name"].ToString());
                        dt1.Rows.Clear();
                    }
                }

                cmd.CommandText = "select * from society_has_teacher where (`society_society_id`='"+soc_id+"' and `position`='head' and `status`='0')" ;
                dt.Rows.Clear();
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i= Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    MessageBox.Show("Head Teacher in charge is not defined!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else if (i == 1)
                {
                    teacher_id= Convert.ToInt32(dt.Rows[0]["teacher_teacher_id"].ToString());
                    cmd.CommandText = "select * from teacher where teacher_id='" + teacher_id + "'";
                    dt1.Rows.Clear();
                    da.Fill(dt1);
                    cmd.ExecuteNonQuery();
                    txt_teacher1.Text = dt1.Rows[0]["teacher_name"].ToString();
                }


                cmd.CommandText = "select * from society_has_teacher where (`society_society_id`='" + soc_id + "' and `position`='sub' and `status`='0')";
                dt.Rows.Clear();
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    MessageBox.Show("Sub Teachers in charge are not defined!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else if (i == 1)
                {
                    teacher_id = Convert.ToInt32(dt.Rows[0]["teacher_teacher_id"].ToString());
                    cmd.CommandText = "select * from teacher where teacher_id='" + teacher_id + "'";
                    dt1.Rows.Clear();
                    da.Fill(dt1);
                    cmd.ExecuteNonQuery();
                    txt_teacher2.Text = dt1.Rows[0]["teacher_name"].ToString();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        public Form22(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
        }
    }
}
