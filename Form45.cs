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
    public partial class Form45 : Form
    {
        int i, check_in,spo_id,old_id;
        //Initializing mysql connection and datatable row counter

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        public Form45(int sport_id)
        {
            InitializeComponent();
            panel1.Visible = false;
            spo_id = sport_id;
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from old_boy where old_boy_name='" + txt_search.Text + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                //convert string to integer
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    label16.Text = "There is no Old Boy registered for that name";
                }
                else if(i==1)
                {
                    panel1.Visible = true;
                    old_id = Convert.ToInt32(dt.Rows[0]["old_boy_id"].ToString());
                    txt_oldboy.Text = dt.Rows[0]["old_boy_name"].ToString();
                    dt.Rows.Clear();
                    cmd.CommandText = "select * from old_boy_has_sport";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_relation.Text = dt.Rows[0]["best_relation"].ToString();
                    txt_year.Text = dt.Rows[0]["best_relation_year"].ToString();
                    txt_contact.Text = dt.Rows[0]["contact"].ToString();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
            
        }
      

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_oldboy.Text == "" || txt_contact.Text == "" || txt_year.Text == "" || txt_relation.Text == "" || int.TryParse(txt_oldboy.Text, out check_in) ||
               int.TryParse(txt_relation.Text, out check_in) || !int.TryParse(txt_year.Text, out check_in) || !int.TryParse(txt_contact.Text, out check_in))
                {
                    MessageBox.Show("Invalid inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "update old_boy_has_sport set  best_relation_year='"+txt_year.Text+"',contact='"+txt_contact.Text+"' where old_boy_old_boy_id='"+ old_id+"'";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Information updated succussfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                    panel1.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
        }

    }
}
