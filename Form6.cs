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
    public partial class Form6 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i;
        int inven_id, user_id,spo_id;
        int teacher_id1, teacher_id2;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        //initializing form inputs string and integer variables
        int check_int, date, month, registered_number, year, player_count,age;


        int since = Convert.ToInt32(DateTime.Now.Year.ToString());
      

        private void btn_reset_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
            txt_sportname.Text = txt_registerednumber.Text = txt_playercount.Text  =
               txt_teacherhead.Text = txt_teachersub.Text  = txt_password.Text = txt_confirm_password.Text = txt_username.Text = "";
        }

        string sport_name, headteacher_name, subteacher_name;
        public Form6()
        {
            InitializeComponent();
            //Initialize combo box items 
            
        }
        
        //add society button click event
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                if (txt_sportname.Text == null ||  txt_registerednumber.Text == null || txt_playercount.Text == null || txt_password.Text == null || txt_confirm_password.Text == null || txt_username.Text == null || txt_teacherhead.Text == null
                    || int.TryParse(txt_sportname.Text, out check_int)  || !int.TryParse(txt_registerednumber.Text, out check_int) || !int.TryParse(txt_playercount.Text, out check_int) ||
                    int.TryParse(txt_teacherhead.Text, out check_int) || int.TryParse(txt_teachersub.Text, out check_int))
                {
                    MessageBox.Show("Some areas not filled or Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }    
                    else if (txt_password.Text.Length < 7)
                    {
                        MessageBox.Show("Passwords must be contain atleast 8 characters!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else if (txt_password.Text != txt_confirm_password.Text)
                    {
                        MessageBox.Show("Passwords are not matched!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {

                        MySqlCommand cmd7 = connectionstring.CreateCommand();
                        cmd7.CommandType = CommandType.Text;
                        //sql command
                        cmd7.CommandText = "select * from teacher where teacher_name='" + txt_teacherhead.Text + "' or teacher_name='" + txt_teachersub.Text + "'";
                        DataTable dt3 = new DataTable();
                        MySqlDataAdapter da3 = new MySqlDataAdapter(cmd7);
                        da3.Fill(dt3);
                        cmd7.ExecuteNonQuery();
                        i = Convert.ToInt32(dt3.Rows.Count);

                        if (txt_teachersub.Text!=null && i != 2)
                        {
                            
                            MessageBox.Show("One of the teachers is not included in the system!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else if (txt_teachersub.Text == null && i != 1)
                        {
                            MessageBox.Show("Head teacher is not registered in the system!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                           
                             if (txt_teachersub != null)
                            {
                                teacher_id1 = Convert.ToInt32(dt3.Rows[0]["teacher_id"].ToString());
                                teacher_id2 = Convert.ToInt32(dt3.Rows[1]["teacher_id"].ToString());
                               
                            }
                            else
                            {
                                teacher_id1 = Convert.ToInt32(dt3.Rows[0]["teacher_id"].ToString());
                               
                            }

                            //Assign initial values
                            sport_name = txt_sportname.Text;
                            headteacher_name = txt_teacherhead.Text;
                            subteacher_name = txt_teachersub.Text;

                            player_count = Convert.ToInt32(txt_playercount.Text);
                            registered_number = int.Parse(txt_registerednumber.Text);
                            string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                            //initialize sql command variable and datatable variable
                            MySqlCommand cmd = connectionstring.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            //sql command
                            cmd.CommandText = "select * from sport where registered_number='" + txt_registerednumber.Text + "'";
                            DataTable dt = new DataTable();
                            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();

                            //convert string to integer
                            i = Convert.ToInt32(dt.Rows.Count.ToString());

                            if (i == 0)
                            {
                                MySqlCommand cmd4 = new MySqlCommand(@"insert into user (user_name,password,user_type) values('" + txt_username.Text + "','" + txt_confirm_password.Text + "','" + "sport" + "')", connectionstring);
                                cmd4.ExecuteNonQuery();

                                MySqlCommand cmd2 = connectionstring.CreateCommand();
                                cmd2.CommandType = CommandType.Text;
                                cmd2.CommandText = ("select * from user");
                                DataTable dt1 = new DataTable();
                                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd2);
                                da1.Fill(dt1);
                                cmd2.ExecuteNonQuery();

                                int j = dt1.Rows.Count;
                                user_id = Convert.ToInt32(dt1.Rows[j - 1]["user_id"].ToString());

                                MySqlCommand cmd6 = new MySqlCommand(@"insert into inventory (inventory_name) values('" + txt_sportname.Text + "')", connectionstring);
                                cmd6.ExecuteNonQuery();

                                MySqlCommand cmd5 = connectionstring.CreateCommand();
                                cmd5.CommandType = CommandType.Text;
                                cmd5.CommandText = ("select * from inventory");
                                DataTable dt2 = new DataTable();
                                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd5);

                                da2.Fill(dt2);
                                cmd5.ExecuteNonQuery();

                                j = dt2.Rows.Count;
                                
                                inven_id = Convert.ToInt32(dt2.Rows[j - 1]["inventory_id"].ToString());

                                MySqlCommand cmd3 = new MySqlCommand(@"insert into sport (sport_name,d_o_b,registered_number,history,players_count,inventory_inventory_id,user_user_id) values('" + txt_sportname.Text + "','" + theDate + "','" + txt_registerednumber.Text + "','', '" + txt_playercount.Text + "', '" + inven_id + "', '" + user_id + "')", connectionstring);
                                cmd3.ExecuteNonQuery();

                                dt2.Rows.Clear();
                                cmd5.CommandText = "select * from sport";
                                da2.Fill(dt2);
                                cmd5.ExecuteNonQuery();
                                i = dt2.Rows.Count;
                                spo_id = Convert.ToInt32(dt2.Rows[i-1]["sport_id"].ToString());

                                if (teacher_id2 != 0)
                                {
                                    cmd5.CommandText = "insert into teacher_has_sport (teacher_teacher_id,sport_sport_id,position,since,status) values('"+teacher_id1+"','"+spo_id+"','Head','"+ since+ "','0')";
                                    cmd5.ExecuteNonQuery();

                                    cmd5.CommandText = "insert into teacher_has_sport (teacher_teacher_id,sport_sport_id,position,since,status) values('" + teacher_id2 + "','" + spo_id + "','Sub','" + since + "','0')";
                                    cmd5.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd5.CommandText = "insert into teacher_has_sport (teacher_teacher_id,sport_sport_id,position,since,status) values('" + teacher_id1 + "','" + spo_id + "','Head','" + since + "','0')";
                                    cmd5.ExecuteNonQuery();
                                }

                                MessageBox.Show("Sport Created Successfully", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                connectionstring.Close();
                            }
                            else
                            {
                                MessageBox.Show("There is a sport registered for the same registered Number!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }

                        }
                    }
                
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
        }
    }
}
