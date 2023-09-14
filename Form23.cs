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
    public partial class Form23 : Form
    {
        int i, check_in,student_id, soc_id;
        string topboard;
        //Initializing mysql connection and datatable row counter

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        public Form23(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
           
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_studentname.Text = txt_admission.Text = txt_position.Text = txt_since.Text = "";
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                for (int i = 0; i < DataGrideView1.Rows.Count; i++)
                {
                    if (DataGrideView1.Rows[i].Cells[2].Value == "member" || DataGrideView1.Rows[i].Cells[2].Value == "committee member")
                    {
                        topboard = "false";
                    }
                    else
                    {
                        topboard = "true";
                    }
                    MySqlCommand cmd = new MySqlCommand(@"insert into student_has_society (student_student_id,society_society_id,position,since,top_board,status) values('" + DataGrideView1.Rows[i].Cells[4].Value + "','" + soc_id + "','" + DataGrideView1.Rows[i].Cells[2].Value + "'" +
                        ",'" + DataGrideView1.Rows[i].Cells[3].Value + "','"+ topboard+"','0')", connectionstring);

                    cmd.ExecuteNonQuery();
                    
                }
                MessageBox.Show("Students Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                DataGrideView1.Rows.Clear();
                txt_studentname.Text = txt_admission.Text = txt_position.Text = txt_since.Text = "";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
        }

   

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_studentname.Text == "" || txt_admission.Text == "" || txt_position.Text == "" || txt_since.Text == "" || int.TryParse(txt_studentname.Text, out check_in) ||
               int.TryParse(txt_position.Text, out check_in) || !int.TryParse(txt_since.Text, out check_in) || !int.TryParse(txt_admission.Text, out check_in))
                {
                    MessageBox.Show("Invalid inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    //initialize sql command variable and datatable variable
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    //sql command
                    cmd.CommandText = "select * from student where `student_name`='" + txt_studentname.Text + "'and `student_admission`='"+ txt_admission.Text + "'" ;

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                  
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    //convert string to integer
                    i = Convert.ToInt32(dt.Rows.Count.ToString());

                    if (i == 1)
                    {

                        DataGrideView1.Rows.Add(txt_studentname.Text, txt_admission.Text.ToString(), txt_position.Text, txt_since.Text,dt.Rows[0]["student_id"]);
                        connectionstring.Close();
                        dt.Rows.Clear();
                    }
                    else if(i==0)
                    {
                        MessageBox.Show("There is no Student registered for that name and admission number", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

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
