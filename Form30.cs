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
    public partial class Form30 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,check_in,date,month,year,soc_id,id,start,end;
        string other_schools;
        TimeSpan start_t, end_t;
        string fromTimeString_s, fromTimeString_e;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form30(int society_id)
        {
            InitializeComponent();
            panel1.Visible = false;
            soc_id = society_id;

            for (int i = 0; i < 24; i++)
            {
                comboBox6.Items.Add(i + 1);
                comboBox7.Items.Add(i + 1);
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
                cmd.CommandText = "select * from event where event_name='" + txt_search.Text + "'and society_society_id='" + soc_id + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    label1.Text = "There is no event created for that name";
                }
                else
                {
                    panel1.Visible = true;
                    id = Convert.ToInt32(dt.Rows[0]["event_id"].ToString());
                    txt_event.Text = dt.Rows[0]["event_name"].ToString();
                    txt_purpose.Text = dt.Rows[0]["purpose"].ToString();
                    txt_venue.Text = dt.Rows[0]["venue"].ToString();

                    if (dt.Rows[0]["other_schools"].ToString() == "true")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (dt.Rows[0]["other_schools"].ToString() == "false")
                    {
                        radioButton2.Checked = true;
                    }
                    txt_partipant.Text = dt.Rows[0]["parti_count"].ToString();
                    txt_income.Text = dt.Rows[0]["income"].ToString();
                    txt_cost.Text = dt.Rows[0]["cost"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["event_date"].ToString());

                    comboBox6.Text = dt.Rows[0]["start_time"].ToString();
                    comboBox7.Text = dt.Rows[0]["end_time"].ToString();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
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

                if (txt_event.Text == "" || txt_venue.Text == "" || txt_purpose.Text == "" || txt_cost.Text == "" ||
                    txt_income.Text == "" || comboBox6.Text == "" || comboBox7.Text == "" ||
                    (radioButton1.Checked == false && radioButton2.Checked == false) || int.TryParse(txt_purpose.Text, out check_in) || int.TryParse(txt_venue.Text, out check_in) ||
                     !int.TryParse(txt_cost.Text, out check_in) || !int.TryParse(txt_income.Text, out check_in))
                {
                    MessageBox.Show("Invalid inputs entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
               
                else
                {
                    string event_date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                    start = Convert.ToInt32(comboBox6.Text.ToString());
                    end = Convert.ToInt32(comboBox7.Text.ToString());

                    start_t = TimeSpan.FromHours(start);
                    end_t = TimeSpan.FromHours(end);

                    fromTimeString_s = start_t.ToString("hh':'mm");
                    fromTimeString_e = end_t.ToString("hh':'mm");

                    if (radioButton1.Checked == true)
                    {
                        other_schools = "true";
                    }
                    else if (radioButton2.Checked == false)
                    {
                        other_schools = "false";
                    }
                   
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update event set venue='"+txt_venue.Text +"',event_date='" +event_date +"',start_time='"+ fromTimeString_s + "',end_time='" + fromTimeString_e + "',other_schools='" +other_schools +"',income='" + txt_income.Text+"',cost='" + txt_cost.Text+"',parti_count='" +txt_partipant.Text +"' where (event_id='"+ id+"')";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Information Updated Successfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                    panel1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
