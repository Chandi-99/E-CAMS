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
    public partial class Form18 : Form
    {
        String ut,subject,message;
        int teacher_id,i;
        int soc_id,ann_id;
        int id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
      
        public Form18(int society_id,String user_type,int tea_id,int user_id)
        {
            InitializeComponent();
            //hide sub menus in the begining..
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            id = user_id;
            ut = user_type;
            teacher_id = tea_id;
            this.WindowState = FormWindowState.Maximized;
            soc_id = society_id;

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
            panel.Controls.Add(childForm);
            panel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void btn_history_Click(object sender, EventArgs e)
        {
            openchildform(new Form21(soc_id));
        }


        private void btn_topboard_Click(object sender, EventArgs e)
        {
            openchildform(new Form22(soc_id));
        }

        private void btn_add_member_Click(object sender, EventArgs e)
        {
            openchildform(new Form23(soc_id));
        }

        private void btn_add_event_Click(object sender, EventArgs e)
        {
            openchildform(new Form25(soc_id));
        }

        private void btn_add_oldboy_Click(object sender, EventArgs e)
        {
            openchildform(new Form24(soc_id));
        }

        private void btn_add_victory_Click(object sender, EventArgs e)
        {
            openchildform(new Form27(soc_id));
        }

        private void btn_add_item_Click(object sender, EventArgs e)
        {
            openchildform(new Form26(soc_id));
        }

        private void btn_edit_member_Click(object sender, EventArgs e)
        {
            openchildform(new Form28(soc_id));
        }

        private void btn_edit_oldboy_Click(object sender, EventArgs e)
        {
            openchildform(new Form29(soc_id));
        }

        private void btn_edit_event_Click(object sender, EventArgs e)
        {
            openchildform(new Form30(soc_id));
        }

        private void btn_edit_information_Click(object sender, EventArgs e)
        {
            openchildform(new Form31(soc_id));
        }

        private void btn_edit_item_Click(object sender, EventArgs e)
        {
            openchildform(new Form32(soc_id));
        }

        private void btn_edit_user_Click(object sender, EventArgs e)
        {
            if (ut.ToString() == "teacher")
            {
                MessageBox.Show("You can not edit society username or password!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                openchildform(new Form59(id));
            }
        }

        private void btn_view_member_Click(object sender, EventArgs e)
        {
            openchildform(new Form33(soc_id));
        }

        private void btn_view_events_Click(object sender, EventArgs e)
        {
            openchildform(new Form34(soc_id));
        }

        private void btn_view_victories_Click(object sender, EventArgs e)
        {
            openchildform(new Form35(soc_id));
        }

        private void view_inventory_Click(object sender, EventArgs e)
        {
            openchildform(new Form36(soc_id, "society"));
        }

        private void Form18_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openchildform(new Form20(soc_id));
        }

        private void btn_message_Click(object sender, EventArgs e)
        {
            openchildform(new Form13(soc_id));
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

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form52 form52 = new Form52(teacher_id,id);
            form52.Show();
        }

        private void Form18_Load(object sender, EventArgs e)
        {

            if (ut != "teacher")
            {
                btn_home.Visible = false;
            }
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            MySqlCommand cmd = connectionstring.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM `announcement` where `society_society_id`= '"+soc_id+"' and `read`= 'unread'" ;
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
                for (int j=0;j<i;j++)
                {
                    ann_id = Convert.ToInt32(dt.Rows[j]["announcement_id"].ToString());
                    message = dt.Rows[j]["message"].ToString();
                    subject= dt.Rows[j]["subject"].ToString();
                    MessageBox.Show("Subject :"+subject+"\n Message :"+message,"Message from Principal",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                    cmd.CommandText = "update announcement set `read`='read' where `announcement_id`='" + ann_id + "'";
                    cmd.ExecuteNonQuery();
                }
            }
            dt.Rows.Clear();
            cmd.CommandText = "select * from society where society_id='" + soc_id + "'";
            da.Fill(dt);
            cmd.ExecuteNonQuery();
            label1.Text = dt.Rows[0]["society_name"].ToString();
        }
    }
}
