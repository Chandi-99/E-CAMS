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
    public partial class Form24 : Form
    {
        int i, check_in,old_id,soc_id;
        //Initializing mysql connection and datatable row counter
        Boolean error = false;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form24(int sport_id)
        {
            InitializeComponent();
            soc_id = sport_id;
        }

        private void btn_add_Click(object sender, EventArgs e)
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
                    MySqlCommand cmd = new MySqlCommand(@"select * from old_boy where old_boy_name='"+ txt_oldboy.Text+"'",connectionstring);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                    i = Convert.ToInt32(dt.Rows.Count);
                    if (i == 0)
                    {
                        dt.Rows.Clear();
                        cmd.CommandText = "insert into old_boy (old_boy_name) values('" + txt_oldboy.Text + "')";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "select * from old_boy ";
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        old_id = dt.Rows.Count;
                        cmd.CommandText = "insert into society_has_old_boy (old_boy_old_boy_id,society_society_id,best_relation,best_relation_year,contact) values('" + Convert.ToInt32(dt.Rows[old_id - 1]["old_boy_id"].ToString()) + "','" + soc_id + "','" + txt_relation.Text + "','" + txt_year.Text + "'" + ",'" + txt_contact.Text + "')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Oldboys Information Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else if(i==1)
                    {
                        DialogResult result = MessageBox.Show("There is someone registered for that name. Do you wish to insert information in to that person?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            
                            old_id = Convert.ToInt32(dt.Rows[0]["old_boy_id"].ToString());
                            cmd.CommandText = "insert into society_has_old_by (old_boy_old_boy_id,society_society_id,best_relation,best_relation_year,contact) values('" + Convert.ToInt32(dt.Rows[old_id - 1]["old_boy_id"].ToString()) + "','" + soc_id + "','" + txt_relation.Text + "','" + txt_year.Text + "'" + ",'" + txt_contact.Text + "')";
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Oldboys Information Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else if (result == DialogResult.No)
                        {
                            MessageBox.Show("Please insert other name to store old boy information","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                        }
                    } 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_oldboy.Text = txt_relation.Text = txt_year.Text = txt_contact.Text = "";
        }

       
    }
}
