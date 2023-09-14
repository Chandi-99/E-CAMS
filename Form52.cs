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
using System.Data;

namespace ECAMS
{
    public partial class Form52 : Form
    {
        int tea_id,i,sport_id,society_id;
        int u_id;
        int[] sport= new int[3];
        int[] society = new int[3];
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form52(int teacher_id,int user_id)
        {
            InitializeComponent();
            //hide sub menus in the begining..
            panel3.Visible = false;
            panel4.Visible = false;
            int id = teacher_id;
            tea_id = id;
            u_id = user_id;
            this.WindowState = FormWindowState.Maximized;
        }
        private void hidesubmenu()
        {
            if (panel3.Visible == true)
                panel3.Visible = false;
            if (panel4.Visible == true)
                panel4.Visible = false;
        }
        //sub menu showing method
        private void showsubmenue(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hidesubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }

        }

        private void btn_sports_Click(object sender, EventArgs e)
        {
            showsubmenue(panel3);
        }

        private void btn_socities_Click(object sender, EventArgs e)
        {
            showsubmenue(panel4);
        }

        //active forms closing variable
        Form activeform = null;

        //open new forms in panel8
        private void openchildform(Form childForm)
        {
            if (activeform != null)
            {
                activeform.Close();
            }

            activeform = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            btn_edit_items.Controls.Add(childForm);
            btn_edit_items.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void btn_sport1_Click(object sender, EventArgs e)
        {
            if (sport[0] == 0)
            {
                MessageBox.Show("You are not a teacher in charge of any sport","Message",MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                this.Hide();
                Form19 form19 = new Form19(sport[0],"teacher", tea_id,u_id);
                form19.Show();
            }
            
        }

        private void btn_sport2_Click(object sender, EventArgs e)
        {
            if (sport[1] == 0)
            {
                MessageBox.Show("You don't have a second sport!", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                this.Hide();
                Form19 form19 = new Form19(sport[1], "teacher", tea_id, u_id);
                form19.Show();
            }
           
        }

        private void btn_sport3_Click(object sender, EventArgs e)
        {
            if (sport[2] == 0)
            {
                MessageBox.Show("You don't have a third sport!", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                this.Hide();
                Form19 form19 = new Form19(sport[2], "teacher",tea_id, u_id);
                form19.Show();
            }
        }

        private void btn_society1_Click(object sender, EventArgs e)
        {
            if (society[0] == 0)
            {
                MessageBox.Show("You are not a teacher in charge of any society", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                this.Hide();
                Form18 form18 = new Form18(society[0], "teacher", tea_id, u_id);
                form18.Show();
            }
        }

        private void btn_society2_Click(object sender, EventArgs e)
        {
            if (society[1] == 0)
            {
                MessageBox.Show("You don't have a second society!", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                this.Hide();
                Form18 form18 = new Form18(society[1], "teacher", tea_id, u_id);
                form18.Show();
            }
        }

        private void btn_society3_Click(object sender, EventArgs e)
        {
            if (society[2] == 0)
            {
                MessageBox.Show("You don't have a third society", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                this.Hide();
                Form18 form18 = new Form18(society[2], "teacher", tea_id, u_id);
                form18.Show();
            }
        }

        private void Form52_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Really want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                connectionstring.Close();
                Form1 form1 = new Form1();
                form1.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hidesubmenu();
            openchildform(new Form59(u_id));
        }

        private void Form52_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from teacher_has_sport where teacher_teacher_id='" + tea_id + "' and status='0'";


                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count);

                if (i == 3)
                {
                    for (int j = 0; j < i; j++)
                    {
                        sport[j] = Convert.ToInt32(dt.Rows[j]["sport_sport_id"].ToString());
                    }

                }
                else if (i == 2)
                {
                    sport[0] = Convert.ToInt32(dt.Rows[0]["sport_sport_id"].ToString());
                    sport[1] = Convert.ToInt32(dt.Rows[1]["sport_sport_id"].ToString());
                    sport[2] = 0;
                }
                else if (i == 1)
                {
                    sport[0] = Convert.ToInt32(dt.Rows[0]["sport_sport_id"].ToString());
                    sport[1] = 0;
                    sport[2] = 0;
                }
                else if (i == 0)
                {
                    sport[0] = 0;
                    sport[1] = 0;
                    sport[2] = 0;
                }


                MySqlCommand cmd1 = connectionstring.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from society_has_teacher where teacher_teacher_id='" + tea_id + "' and status='0'";

                DataTable dt1 = new DataTable();
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd1);
                da2.Fill(dt1);
                cmd1.ExecuteNonQuery();

                i = Convert.ToInt32(dt1.Rows.Count);

                if (i == 3)
                {
                    for (int j = 0; j < i; j++)
                    {
                        society[j] = Convert.ToInt32(dt1.Rows[j]["society_society_id"].ToString());
                    }

                }
                else if (i == 2)
                {
                    society[0] = Convert.ToInt32(dt1.Rows[0]["society_society_id"].ToString());
                    society[1] = Convert.ToInt32(dt1.Rows[1]["society_society_id"].ToString());
                    society[2] = 0;
                }
                else if (i == 1)
                {
                    society[0] = Convert.ToInt32(dt1.Rows[0]["society_society_id"].ToString());
                    society[1] = 0;
                    society[2] = 0;
                }
                else if (i == 0)
                {
                    society[0] = 0;
                    society[1] = 0;
                    society[2] = 0;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
           

            
        }
    }
}
