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
    public partial class Form19 : Form
    {
        String ut;
        int id;
        string unread = "unread";
        int i, teacher_id;
        int spo_id,ann_id;
        string message, subject;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form19(int sport_id,String user_type,int tea_id,int user_id)
        {
            InitializeComponent();
            //hide sub menus in the begining..
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            id = user_id;
            ut=user_type;
            teacher_id = tea_id;
            spo_id = sport_id;
            this.WindowState = FormWindowState.Maximized;
        }

        
        //sub menu hiding method
        private void hidesubmenu()
        {
            if (panel3.Visible == true)
                panel3.Visible = false;
            if (panel4.Visible == true)
                panel4.Visible = false;
            if (panel5.Visible == true)
                panel5.Visible = false;
            if (panel6.Visible == true)
                panel6.Visible = false;
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

        private void btn_info_Click(object sender, EventArgs e)
        {
            showsubmenue(panel3);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            showsubmenue(panel4);
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            showsubmenue(panel5);
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            showsubmenue(panel6);
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

        private void btn_history_Click(object sender, EventArgs e)
        {
            openchildform(new Form37(spo_id));
        }

        private void btn_captain_Click(object sender, EventArgs e)
        {
            openchildform(new Form38(spo_id));
        }


        private void btn_add_player_Click(object sender, EventArgs e)
        {
            openchildform(new Form39(spo_id));
        }

        private void btn_add_match_Click(object sender, EventArgs e)
        {
            openchildform(new Form41(spo_id));
        }

        private void btn_add_oldboy_Click(object sender, EventArgs e)
        {
            openchildform(new Form40(spo_id));
        }

        private void btn_add_victory_Click(object sender, EventArgs e)
        {
            openchildform(new Form43(spo_id));
        }

        private void btn_add_item_Click(object sender, EventArgs e)
        {
            openchildform(new Form42(spo_id));
        }

        private void btn_edit_player_Click(object sender, EventArgs e)
        {
            openchildform(new Form44(spo_id));
        }

        private void btn_edit_oldboy_Click(object sender, EventArgs e)
        {
            openchildform(new Form45(spo_id));
        }

        private void btn_edit_match_Click(object sender, EventArgs e)
        {
            openchildform(new Form51(spo_id));
        }

        private void btn_edit_information_Click(object sender, EventArgs e)
        {
            openchildform(new Form46(spo_id));
        }

        private void btn_edit_item_Click(object sender, EventArgs e)
        {
            openchildform(new Form47(spo_id));
        }

        private void btn_edit_user_Click(object sender, EventArgs e)
        {
            if (ut.ToString()=="teacher")
            {
                MessageBox.Show("You can not edit society username or password!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                openchildform(new Form59(id));
            }

        }
        private void btn_view_players_Click(object sender, EventArgs e)
        {
            openchildform(new Form48(spo_id));
        }

        private void btn_view_events_Click(object sender, EventArgs e)
        {
            openchildform(new Form49(spo_id));
        }

        private void btn_view_victories_Click(object sender, EventArgs e)
        {
            openchildform(new Form50(spo_id));
        }

        private void view_inventory_Click(object sender, EventArgs e)
        {
            openchildform(new Form36(spo_id,"sport"));
        }

        private void Form19_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
           DialogResult result=MessageBox.Show("Do You Really want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
            openchildform(new Form70(spo_id));
        }

        private void btn_message_Click(object sender, EventArgs e)
        {
            openchildform(new Form14(spo_id));
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form52 form52 = new Form52(teacher_id,id);
            form52.Show();
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            if (ut != "teacher")
            {
                btn_home.Visible = false;
            }
            if (connectionstring.State!=ConnectionState.Open)
            {
                connectionstring.Open();
            }
            MySqlCommand cmd = connectionstring.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM `announcement` where `sport_sport_id`= '"+spo_id+"' and `read`= 'unread'";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.ExecuteNonQuery();

            i = Convert.ToInt32(dt.Rows.Count);
            if (i == 0)
            {
                //do nothing
            }
            else
            {
                for (int j = 0; j < i; j++)
                {
                    ann_id = Convert.ToInt32(dt.Rows[j]["announcement_id"].ToString());
                    message = dt.Rows[j]["message"].ToString();
                    subject = dt.Rows[j]["subject"].ToString();
                    MessageBox.Show("Subject :" + subject + "\n Message :" + message, "Message from Principal", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    cmd.CommandText = "update announcement set `read`='read' where `announcement_id`='" + ann_id + "'";
                    cmd.ExecuteNonQuery();
                }
            }

            dt.Rows.Clear();
            cmd.CommandText = "select * from sport where sport_id='" + spo_id + "'";
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            label1.Text = dt.Rows[0]["sport_name"].ToString();
        }
    }
}
