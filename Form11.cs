using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace ECAMS
{
    public partial class Form11 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,student_id,society_id,sport_id;
        String society_name, sport_name;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form11()
        {
            InitializeComponent();
            txt_admission.Visible = txt_shortname.Visible = label2.Visible = label3.Visible = label4.Visible = label5.Visible = dataGridView1.Visible =
                dataGridView2.Visible = label6.Visible = label7.Visible = label8.Visible = false;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                txt_admission.Text = txt_shortname.Text = "";
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                panel1.Visible = false;
                label8.Visible = false;
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_studentname.Text == null)
                {
                    MessageBox.Show("You haven't entered any student name or admission number ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from student where student_name='" + txt_studentname.Text + "'or student_admission='" + txt_studentname.Text + "'or full_name='" + txt_studentname.Text + "'";
                    cmd.ExecuteNonQuery();

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 0)
                    {
                        label8.Visible = true;
                        label8.Text = "There is no Student for searched student name or admission number!";
                        connectionstring.Close();
                    }
                    else
                    {
                        label8.Visible = false;
                        txt_studentname.Text = "";
                        panel1.Visible = true;
                        txt_admission.Visible = txt_shortname.Visible = label2.Visible = label3.Visible = label4.Visible = label5.Visible = dataGridView1.Visible =
                        dataGridView2.Visible = label6.Visible = label7.Visible = label8.Visible = true ;
                        txt_shortname.Text = dt.Rows[0]["student_name"].ToString();
                        txt_admission.Text= dt.Rows[0]["student_admission"].ToString();

                        student_id = Convert.ToInt32(dt.Rows[0]["student_id"].ToString());

                        cmd.CommandText = "select * from student_has_society where student_student_id='"+ student_id+"'";
                        DataTable dt1 = new DataTable();
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd);
                        da1.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        DataTable dt3 = new DataTable();
                        MySqlDataAdapter da3 = new MySqlDataAdapter(cmd);
                      
                        i = dt1.Rows.Count;
                        for(int j = 0; j < i; j++)
                        {
                            society_id = Convert.ToInt32(dt1.Rows[j]["society_society_id"].ToString());
                            cmd.CommandText = "select * from society where society_id='"+ society_id+"'";
                            da3.Fill(dt3);
                            cmd.ExecuteNonQuery();
                            society_name = dt3.Rows[0]["society_name"].ToString();
                            dataGridView1.Rows.Add(dt1.Rows[j]["since"], dt1.Rows[j]["position"], society_name);
                            dt3.Rows.Clear();
                        }

                        cmd.CommandText = "select * from student_has_sport where student_student_id='" + student_id + "'";
                        DataTable dt2 = new DataTable();
                        MySqlDataAdapter da2= new MySqlDataAdapter(cmd);
                        da2.Fill(dt2);
                        cmd.ExecuteNonQuery();

                        DataTable dt4 = new DataTable();
                        MySqlDataAdapter da4 = new MySqlDataAdapter(cmd);
                        i = dt2.Rows.Count;
                        for (int j = 0; j < i; j++)
                        {
                            sport_id = Convert.ToInt32(dt2.Rows[j]["sport_sport_id"].ToString());
                            cmd.CommandText = "select * from sport where sport_id='" + sport_id + "'";
                            da4.Fill(dt4);
                            cmd.ExecuteNonQuery();
                            sport_name = dt4.Rows[0]["sport_name"].ToString();
                            dataGridView2.Rows.Add(dt2.Rows[j]["since"], dt2.Rows[j]["position"], sport_name);
                            dt4.Rows.Clear();
                        }

                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
