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
    public partial class Form53 : Form
    {
        String user_type = "principal";
        int id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
      
        public Form53(int user_id)
        {
            InitializeComponent();
            //hide sub menus in the begining..
            panel3.Visible = false;
            panel4.Visible = false;
            this.WindowState = FormWindowState.Maximized;
            id = user_id;
  
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

        private void btn_sports_Click(object sender, EventArgs e)
        {
            showsubmenue(panel3);
        }

        private void btn_sport_search_Click(object sender, EventArgs e)
        {
            openchildform(new Form54());
        }

        private void btn_sport_matches_Click(object sender, EventArgs e)
        {
            openchildform(new Form10());
        }

        private void btn_sport_player_Click(object sender, EventArgs e)
        {
            openchildform(new Form62());
        }

        private void btn_sport_announcement_Click(object sender, EventArgs e)
        {
            openchildform(new Form63());
        }

        private void btn_socities_Click(object sender, EventArgs e)
        {
            showsubmenue(panel4);
        }

        private void btn_society_search_Click(object sender, EventArgs e)
        {
            openchildform(new Form55());
        }

        private void btn_society_events_Click(object sender, EventArgs e)
        {
            openchildform(new Form68());
        }

        private void btn_society_member_Click(object sender, EventArgs e)
        {
            openchildform(new Form67());
        }

        private void btn_society_announcement_Click(object sender, EventArgs e)
        {
            openchildform(new Form69());
        }

        private void Form53_FormClosing(object sender, FormClosingEventArgs e)
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
            openchildform(new Form58());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hidesubmenu();
            openchildform(new Form59(id));
        }
    }
}
