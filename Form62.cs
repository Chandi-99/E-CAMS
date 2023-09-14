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
    public partial class Form62 : Form
    {
        int i, check_in,student_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Visible = false;
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
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from sport where sport_name='" + txt_search.Text + "'";

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    DataTable dt1 = new DataTable();

                    i = Convert.ToInt32(dt.Rows.Count.ToString());

                    if (i == 0)
                    {
                        label16.Text = "Invalid Sport Name !";
                        connectionstring.Close();
                        
                    }
                    else
                    {
                        i = Convert.ToInt32(dt.Rows[0]["sport_id"].ToString());
                        cmd.CommandText = "select * from student_has_sport where sport_sport_id='"+i+"' and status='0'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        i= Convert.ToInt32(dt.Rows.Count.ToString());

                        for(int j = 0; j < i; j++)
                        {
                            student_id = Convert.ToInt32(dt.Rows[i]["student_student_id"].ToString());
                            cmd.CommandText = "select student_name from student where student_id='"+student_id+"'";
                            da.Fill(dt1);
                            cmd.ExecuteNonQuery();
                           dataGridView1.Rows.Add(dt1.Rows[0]["student_name"].ToString(), dt.Rows[0]["team"].ToString(), dt.Rows[i]["position"].ToString(), dt.Rows[i]["since"].ToString());
                            dt1.Rows.Clear();
                        }
                        dataGridView1.Visible = true;
                        connectionstring.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        public Form62()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
                
        }

    }
}
