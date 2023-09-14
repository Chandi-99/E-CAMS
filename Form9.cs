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
    public partial class Form9 : Form
    {

        //Initializing mysql connection and datatable row counter
        int i,society_id,teacher_id,teacher_id1,teacher_id2,teacher_id3;
        string sub, head;
        Boolean error = false;
        Boolean update_1 = false;
        Boolean update_2 = false;
        int since = Convert.ToInt32(DateTime.Now.Year.ToString());
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        //initializing form inputs string and integer variables
        int check_int, date, month, registered_number, year, member_count, age;

        private void btn_search_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            if (txt_searchsociety.Text == null)
            {
                MessageBox.Show("You haven't entered any society name or registered number ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from society where society_name='" + txt_searchsociety.Text + "'";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    label9.Visible = true;
                    label9.Text = "There is no Society registered for for searched society name or ID, Chek the name and try again...";
                }
                else
                {
                    label9.Visible = false;
                    panel1.Visible = true;
                    society_id = Convert.ToInt32(dt.Rows[0]["society_id"].ToString());
                    txt_societyname.Text = dt.Rows[0]["society_name"].ToString();
                    txt_registerednumber.Text= dt.Rows[0]["registered_number"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["d_o_b"].ToString());
                    txt_membercount.Text = dt.Rows[0]["member_count"].ToString();

                    cmd.CommandText = "select * from society_has_teacher where society_society_id='"+ society_id+"' and position='head' and status='0'";
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
                        teacher_id=Convert.ToInt32(dt.Rows[0]["teacher_teacher_id"].ToString());
                        dt.Rows.Clear();
                        cmd.CommandText = "select * from teacher where teacher_id='" + teacher_id + "'";
                        
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        txt_teacherhead.Text = head=dt.Rows[0]["teacher_name"].ToString();

                    }

                    dt.Rows.Clear();
                    cmd.CommandText = "select * from society_has_teacher where society_society_id='" + society_id + "' and position='sub' and status='0'";
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
                        dt.Rows.Clear();
                        cmd.CommandText = "select * from teacher where teacher_id='" + teacher_id1 + "'";
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        txt_teachersub.Text =sub= dt.Rows[0]["teacher_name"].ToString();

                    }
                }
            }
        }

        public Form9()
        {
            InitializeComponent();

          panel1.Visible=false;
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_societyname.Text = txt_registerednumber.Text = txt_membercount.Text =
               txt_teacherhead.Text = txt_teachersub.Text  = "";
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_societyname.Text == " " || txt_registerednumber.Text == " " || txt_membercount.Text == " " ||
               txt_teacherhead.Text == " " || txt_teachersub.Text == " " 
               || int.TryParse(txt_societyname.Text, out check_int) || !int.TryParse(txt_registerednumber.Text, out check_int) || !int.TryParse(txt_membercount.Text, out check_int) ||
               int.TryParse(txt_teacherhead.Text, out check_int) || int.TryParse(txt_teachersub.Text, out check_int) )
             
                {
                    MessageBox.Show("Some areas not filled or Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {

                    member_count = Convert.ToInt32(txt_membercount.Text);
                    registered_number = int.Parse(txt_registerednumber.Text);

                    string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    age = DateTime.Today.Year - dateTimePicker1.Value.Year;

                    //initialize sql command variable and datatable variable
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    if (head != txt_teacherhead.Text)
                    {
                        cmd.CommandText = "select * from teacher where teacher_name='"+ txt_teacherhead.Text+"'";
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
                    if (error != true)
                    {
                       
                        cmd.CommandText = "update society set member_count='" + member_count + "',d_o_b='" + theDate + "' where society_id='" + society_id + "'";
                        cmd.ExecuteNonQuery();

                        if (update_1 == true)
                        {
                            cmd.CommandText = "update society_has_teacher set status='" + "finished" + "' where teacher_teacher_id='" + teacher_id + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "insert into society_has_teacher(society_society_id,teacher_teacher_id,position,since,status) values('" + society_id + "','" + teacher_id2 + "','" + "head" + "','" + since + "','0')";
                            cmd.ExecuteNonQuery();
                        }

                        if (update_2 == true)
                        {

                            cmd.CommandText = "update society_has_teacher set status='" + "finished" + "' where teacher_teacher_id='" + teacher_id1 + "'";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "insert into society_has_teacher(society_society_id,teacher_teacher_id,position,since,status) values('" + society_id + "','" + teacher_id3 + "','" + "sub" + "','" + since + "','0')";
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Information Updated Successfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                        panel1.Visible = false;
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
