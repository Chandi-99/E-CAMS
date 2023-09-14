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
    public partial class Form12 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,teacher_id,sport_id,society_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        String society_name, sport_name;
        public Form12()
        {
            InitializeComponent();
            panel1.Visible = false;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                label8.Visible = false;
                panel1.Visible = false;
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_teachername.Text == null)
                {
                    MessageBox.Show("You haven't entered any teacher name or registered number ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from teacher where teacher_name='" + txt_teachername.Text + "'or registered_number='" + txt_teachername.Text + "'or full_name='" + txt_teachername.Text + "'";
                    cmd.ExecuteNonQuery();

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    i = Convert.ToInt32(dt.Rows.Count.ToString());
                    if (i == 0)
                    {
                        label8.Visible = true;
                        label8.Text = "There is no Teacher for searched  name or Number, Chek the name and number and try again...";
                        connectionstring.Close();
                    }
                    else
                    {
                        label8.Visible = false;
                        txt_teachername.Text = "";
                        panel1.Visible = true;

                        txt_shortname.Text = dt.Rows[0]["teacher_name"].ToString();
                        txt_registerednumber.Text = dt.Rows[0]["registered_number"].ToString();

                        teacher_id = Convert.ToInt32(dt.Rows[0]["teacher_id"].ToString());

                        cmd.CommandText = "select * from society_has_teacher where teacher_teacher_id='" + teacher_id + "' and status='0'";
                        DataTable dt1 = new DataTable();
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd);
                        da1.Fill(dt1);
                        cmd.ExecuteNonQuery();
                        DataTable dt3 = new DataTable();
                        MySqlDataAdapter da3 = new MySqlDataAdapter(cmd);

                        i = dt1.Rows.Count;
                        for (int j = 0; j < i; j++)
                        {
                            society_id = Convert.ToInt32(dt1.Rows[j]["society_society_id"].ToString());
                            cmd.CommandText = "select * from society where society_id='" + society_id + "'";
                            da3.Fill(dt3);
                            cmd.ExecuteNonQuery();
                            society_name = dt3.Rows[0]["society_name"].ToString();
                            dataGridView1.Rows.Add(dt1.Rows[j]["since"], dt1.Rows[j]["position"], society_name);
                            dt3.Rows.Clear();
                        }

                        cmd.CommandText = "select * from teacher_has_sport where teacher_teacher_id='" + teacher_id + "'and status='0'";
                        DataTable dt2 = new DataTable();
                        MySqlDataAdapter da2 = new MySqlDataAdapter(cmd);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
