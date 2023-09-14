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
    public partial class Form29 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in,soc_id,old_id;
        int date, month, year, age;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
           "database=ecams_database;" + "password=facebook2018;");
        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Visible = false;
                panel1.Visible = false;
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                label1.Visible = false;
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
                    label1.Visible = true;
                    label1.Text = "There is no Old Boy registered for that name";
                }
                else
                {
                    panel1.Visible = true;
                    txt_oldboy.Text = dt.Rows[0]["old_boy_name"].ToString();
                    old_id = Convert.ToInt32(dt.Rows[0]["old_boy_id"].ToString());
                    dt.Rows.Clear();
                    cmd.CommandText = "select * from society_has_old_boy where old_boy_old_boy_id='"+old_id+"'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    txt_relation.Text = dt.Rows[0]["best_relation"].ToString();
                    txt_year.Text = dt.Rows[0]["best_relation_year"].ToString();
                    txt_contact.Text = dt.Rows[0]["contact"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (txt_oldboy.Text == "" || txt_contact.Text == "" || txt_year.Text == "" || txt_relation.Text == "" || int.TryParse(txt_oldboy.Text, out check_in) ||
               int.TryParse(txt_relation.Text, out check_in) || !int.TryParse(txt_year.Text, out check_in) || !int.TryParse(txt_contact.Text, out check_in))
                {
                    MessageBox.Show("Invalid inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update society_has_old_boy set contact='" + txt_contact.Text+"',best_relation_year='"+ txt_year.Text+"' where (old_boy_old_boy_id='"+ old_id+"' and society_society_id='"+soc_id+"')";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Oldboy Information Updated Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    panel1.Visible = false;
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

       
        public Form29(int society_id)
        {
            InitializeComponent();
            panel1.Visible = false;
            soc_id = society_id;
            label1.Visible = false;
            label16.Visible = false;
        }
    }
}
