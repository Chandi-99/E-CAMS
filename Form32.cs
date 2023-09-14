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
    public partial class Form32 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in, id, soc_id,part_id;
        String donate, by;

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txt_donate.ReadOnly = true;
        }

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form32(int society_id)
        {
            InitializeComponent();
            panel1.Visible = false;
            label1.Visible = false;
            soc_id = society_id;
            for (int i = 1; i < 25; i++)
            {
                comboBox1.Items.Add(i.ToString());
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
                if (txt_item.Text == "" || txt_value.Text == "" || comboBox1.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false)
                    || int.TryParse(txt_item.Text, out check_in) || !int.TryParse(txt_value.Text, out check_in) || int.TryParse(txt_donate.Text, out check_in) ||
                    !int.TryParse(comboBox1.Text, out check_in))
                {
                    MessageBox.Show("Invalid inputs entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if (txt_item.Text == "" || txt_value.Text == "" || comboBox1.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false)
                   || int.TryParse(txt_item.Text, out check_in) || !int.TryParse(txt_value.Text, out check_in) || int.TryParse(txt_donate.Text, out check_in) ||
                   !int.TryParse(comboBox1.Text, out check_in))
                    {
                        MessageBox.Show("Invalid inputs entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (radioButton1.Checked == true)
                        {
                            donate = "true";
                            by = txt_donate.Text;
                        }
                        else
                        {
                            donate = "false";
                            by = "bought";
                        }
                        MySqlCommand cmd = connectionstring.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update part set quantity='" + comboBox1.SelectedItem + "',donate='" + donate + "',donated_by='" + by + "',value='" + txt_value.Text + "' where part_id='" + part_id + "' ";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Item updated Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from society where society_id='" + soc_id + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                id = Convert.ToInt32(dt.Rows[0]["inventory_inventory_id"].ToString());
                dt.Rows.Clear();

                cmd.CommandText = "select * from part where part_name='" + txt_search.Text + "'and inventory_inventory_id='" + id + "'";
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(dt.Rows.Count);

                if (i == 0)
                {
                    label1.Visible = true;
                    label1.Text = "There is no item for that name in Inventory!";
                }
                else if (i == 1)
                {
                    panel1.Visible = true;
                    part_id = Convert.ToInt32(dt.Rows[0]["part_id"].ToString());
                    txt_item.Text = dt.Rows[0]["part_name"].ToString();
                    comboBox1.SelectedItem = Convert.ToInt32(dt.Rows[0]["quantity"].ToString());
                    if (dt.Rows[0]["donate"].ToString() == "true")
                    {
                        radioButton1.Checked = true;
                        txt_donate.Text = dt.Rows[0]["donated_by"].ToString();
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }

                    txt_value.Text = dt.Rows[0]["value"].ToString();
                    comboBox1.Text = dt.Rows[0]["quantity"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }

            
        }
    }
}
