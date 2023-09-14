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
    public partial class Form59 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,j,user_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        public Form59(int id)
        {
            InitializeComponent();
            panel1.Visible = false;
            user_id = id;
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            if (txt_present_password.Text == "")
            {
                MessageBox.Show("You haven't entered the Password","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
            else
            {
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from user where  password='" + txt_present_password.Text + "' and user_id='"+ user_id +"'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                //convert string to integer
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                
                if (i == 0)
                {
                    MessageBox.Show("Password you entered is incorrect!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    txt_present_password.Text = "";
                    j = Convert.ToInt32(dt.Rows[0]["user_id"].ToString());
                    panel1.Visible = true;
                    
                }
            }
               
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_username.Text = txt_new_password.Text = txt_confirm_password.Text = "";
            panel1.Visible = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (panel1.Visible != false)
                {
                    panel1.Visible = false;
                }
                if(txt_username.Text =="" ||  txt_new_password.Text == "" ||  txt_confirm_password.Text == "")
                {
                    MessageBox.Show("Invalid user inputs!","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                }
                else if (txt_new_password.Text.Length < 7)
                {
                    MessageBox.Show("Passwords must be contain atleast 8 characters!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if (txt_new_password.Text != txt_confirm_password.Text)
                {
                    MessageBox.Show("Passwords are not matched!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                { 

                    MySqlCommand cmd1 = connectionstring.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    //sql command
                    cmd1.CommandText =" UPDATE user SET user_name = '"+txt_username.Text+"', password = '"+txt_confirm_password.Text+"' WHERE(user_id = '"+j+"')";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                    da.Fill(dt);
                    cmd1.ExecuteNonQuery();

                    MessageBox.Show("User Name and Password are updated successfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
        }
    }
}
