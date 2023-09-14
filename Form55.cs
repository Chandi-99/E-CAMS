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
    public partial class Form55 : Form
    {
        int i, check_in,teacher_id,soc_id,student_id;


        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                label16.Visible = false;
                panel1.Visible = false;
                dataGridView1.Rows.Clear();
                richTextBox2.Text = "";
                richTextBox3.Text = "";
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_search.Text == "" || int.TryParse(txt_search.Text, out check_in))
                {
                    MessageBox.Show("Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    panel1.Visible = false;
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from society where society_name='" + txt_search.Text + "'";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    i =dt.Rows.Count;
                    if (i == 0)
                    {
                        label16.Visible = true;
                        label16.Text = "Invalid Society Name";
                    }
                    else if (i == 1)
                    {
                        panel1.Visible = true;
                        richTextBox2.Text = dt.Rows[0]["vision"].ToString();
                        richTextBox3.Text = dt.Rows[0]["mission"].ToString();
                        soc_id = Convert.ToInt32(dt.Rows[0]["society_id"].ToString());
                        dt.Rows.Clear();

                        cmd.CommandText = "select * from student_has_society where society_society_id='" + soc_id + "'and top_board='" + "true" + "'and status='0'";
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        DataTable dt1 = new DataTable();
                        i = Convert.ToInt32(dt.Rows.Count.ToString());
                        if (i == 0)
                        {
                            MessageBox.Show("Top board students are not defined!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else
                        {
                            for (int j = 0; j < i; j++)
                            {
                                student_id = Convert.ToInt32(dt.Rows[j]["student_student_id"].ToString());
                                cmd.CommandText = "select * from student where student_id='" + student_id + "'";
                                da.Fill(dt1);
                                cmd.ExecuteNonQuery();
                                dataGridView1.Rows.Add(dt.Rows[j]["position"].ToString(), dt1.Rows[0]["student_name"].ToString());
                                dt1.Rows.Clear();
                            }
                        }

                        cmd.CommandText = "select * from society_has_teacher where society_society_id='" + soc_id + "' and position='" + "head" + "' and status='0'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count.ToString());
                        
                        if (i == 0)
                        {
                            MessageBox.Show("Head Teacher in charge is not defined!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else if (i == 1)
                        {
                            dt1.Rows.Clear();
                            teacher_id = Convert.ToInt32(dt.Rows[0]["teacher_teacher_id"].ToString());
                            cmd.CommandText = "select * from teacher where teacher_id='" + teacher_id + "'";
                            da.Fill(dt1);
                            cmd.ExecuteNonQuery();
                            textBox1.Text = dt1.Rows[0]["teacher_name"].ToString();
                        }

                        cmd.CommandText = "select * from society_has_teacher where society_society_id='" + soc_id + "' and position='" + "sub" + "'and status='0'";
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
                            textBox2.Text = dt1.Rows[0]["teacher_name"].ToString();
                        }
                    }
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Form55()
        {
            InitializeComponent();
            panel1.Visible = false;
        }
    }
}
