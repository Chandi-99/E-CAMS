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
    public partial class Form42 : Form
    {
        int i, check_in,spo_id, id;
        string donate,by;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txt_donate.ReadOnly = true;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_item.Text = txt_donate.Text = comboBox1.Text =txt_value.Text= "";
            radioButton1.Checked = radioButton2.Checked = false;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
            {
                    connectionstring.Open();
                }
                if (txt_item.Text == "" ||  txt_value.Text == "" || comboBox1.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false)
                    || int.TryParse(txt_item.Text, out check_in)  || !int.TryParse(txt_value.Text, out check_in) ||
                    !int.TryParse(comboBox1.Text, out check_in))
                {
                    MessageBox.Show("Invalid inputs entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from sport where sport_id='"+ spo_id +"'";

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    id = Convert.ToInt32(dt.Rows[0]["inventory_inventory_id"]);
                    dt.Rows.Clear();

                    cmd.CommandText = "select * from part where inventory_inventory_id='" + id + "' and part_name='"+txt_item.Text+"'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    i = Convert.ToInt32(dt.Rows.Count);
                    if (i == 1)
                    {
                        MessageBox.Show("This Item is already exist!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        connectionstring.Close();
                    }
                    else
                    {
                        if (radioButton1.Checked == true)
                        {
                            donate = "true";
                            by = txt_donate.Text;
                            dt.Rows.Clear();
                            cmd.CommandText = "select * from `old_boy` where `old_boy_name`='" + by + "'";
                            
                            da.Fill(dt);
                            cmd.ExecuteNonQuery();
                            i = Convert.ToInt32(dt.Rows.Count);

                            if (i == 1)
                            {
                                cmd.CommandText = "insert into old_boy_donation(old_boy_old_boy_id,donation) values('" + Convert.ToInt32(dt.Rows[0]["old_boy_id"].ToString()) + "','" + txt_item.Text + "')";
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("This item is recognize as donate from an oldboy!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }

                        }
                        else if(radioButton2.Checked == true)
                        {
                            donate = "false";
                            by = "bought";
                        }

                        dt.Rows.Clear();
                        cmd.CommandText = "insert into part (part_name,quantity,inventory_inventory_id,donate,donated_by,value) values('"+ txt_item.Text+ "','" + comboBox1.SelectedItem+ "','" + id + "','" + donate + "','"+ by+"','"+ txt_value.Text+"')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Item Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Form42(int sport_id)
        {
            InitializeComponent();
            for (int i = 1; i < 25; i++)
            {
                comboBox1.Items.Add(i.ToString());
                spo_id = sport_id;
            }
        }
    }
}
