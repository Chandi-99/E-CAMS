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
    public partial class Form28 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in;
        int student_id,soc_id;
        string position,topboard;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
          "database=ecams_database;" + "password=facebook2018;");

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                label16.Visible = false;
                panel1.Visible = false;
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_search.Text=="" || int.TryParse(txt_search.Text,out check_in))
                {
                    MessageBox.Show("Invalid inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    //initialize sql command variable and datatable variable
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from student where student_name='" + txt_search.Text + "'or full_name='" + txt_search.Text + "'";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    
                    i = Convert.ToInt32(dt.Rows.Count.ToString());

                    if (i == 1)
                    {
                        panel1.Visible = true;
                        txt_studentname.Text = dt.Rows[0]["student_name"].ToString();
                        txt_admission.Text = dt.Rows[0]["student_admission"].ToString();
                        student_id = Convert.ToInt32(dt.Rows[0]["student_id"].ToString());
                        cmd.CommandText = "select * from student_has_society where student_student_id='" + student_id + "'and society_society_id='" + soc_id + "' and status ='0'";
                        DataTable dt1 = new DataTable();
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd);
                        da1.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        txt_position.Text = position= dt1.Rows[0]["position"].ToString();
                        
                        txt_since.Text = dt1.Rows[0]["since"].ToString();

                    }
                    else if (i == 0)
                    {
                        label16.Visible = true;
                        label16.Text = "There is no Student registered for that name!";
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        public Form28(int society_id)
        {
            InitializeComponent();
            panel1.Visible = false;
            label16.Visible = false;
            soc_id = society_id;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (int.TryParse(txt_position.Text, out check_in) || !int.TryParse(txt_since.Text, out check_in) || txt_position.Text=="" || txt_since.Text=="")
                {
                    MessageBox.Show("Invalid inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    if (position!=txt_position.Text)
                    {
                        cmd.CommandText = "update student_has_society set status='finished' where (student_student_id='" + student_id + "' and society_society_id='" + soc_id + "' and position='" + position + "')";
                        cmd.ExecuteNonQuery();

                        if (txt_position.Text == "member" || txt_position.Text == "committee member")
                        {
                            topboard = "false";
                        }
                        else
                        {
                            topboard = "true";
                        }

                        cmd.CommandText = "insert into student_has_society (student_student_id,society_society_id,position,since,top_board,status) values('" + student_id + "','" + soc_id + "','" + txt_position.Text + "','" + txt_since.Text + "','" + topboard + "','0')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student information updated succussfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        cmd.CommandText = "update student_has_society set since='"+txt_since.Text+"' where (student_student_id='" + student_id + "' and society_society_id='" + soc_id + "' and position='" + position + "')";
                        cmd.ExecuteNonQuery();
                    }
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
