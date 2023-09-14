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
    public partial class Form51 : Form
    {
        int i, check_in, spo_id, match_id;
        int date_s, month_s, year_s, date_e, month_e, year_e,start_t,end_t;
        string host;
        TimeSpan start_time, end_time;
        string fromTimeString_s, fromTimeString_e;

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form51(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;

            panel1.Visible = false;
            for (int i = 1; i < 25; i++)
            {
                comboBox9.Items.Add(i);
                comboBox10.Items.Add(i);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Visible = false;
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from `match` where `match`='"+txt_search.Text+"' and sport_sport_id='"+spo_id+"'";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count);
                if (i == 0)
                {
                    MessageBox.Show("Invalid match name","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                }
                else if (i == 1)
                {
                    panel1.Visible = true;
                    match_id = Convert.ToInt32(dt.Rows[0]["match_id"].ToString());
                    txt_match.Text = dt.Rows[0]["match"].ToString();
                    txt_opposition.Text = dt.Rows[0]["opponent"].ToString();
                    comboBox4.Text = dt.Rows[0]["type"].ToString();
                    if (dt.Rows[0]["host_by"].ToString() == "us")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (dt.Rows[0]["host_by"].ToString() == "opponent")
                    {
                        radioButton2.Checked = true;
                    }
                    else if (dt.Rows[0]["host_by"].ToString() == "other")
                    {
                        radioButton3.Checked = true;
                    }
                    comboBox9.Text=dt.Rows[0]["start_time"].ToString();
                    comboBox10.Text = dt.Rows[0]["end_time"].ToString();

                    txt_venue.Text = dt.Rows[0]["venue"].ToString();
                    dt.Rows.Clear();
                    cmd.CommandText = "select * from `match_date_has_match` where match_match_id='"+ match_id+"'";
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["start_date"].ToString());
                    dateTimePicker2.Value = Convert.ToDateTime(dt.Rows[0]["end_date"].ToString());

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //try
            //{
                
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                if (txt_match.Text == "" || txt_opposition.Text == "" || txt_venue.Text == ""  || comboBox9.Text == "" || comboBox10.Text == "" ||
                     comboBox4.Text == "" ||  (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
                   || int.TryParse(txt_opposition.Text, out check_in) || int.TryParse(txt_venue.Text, out check_in) ||  
                   int.TryParse(comboBox4.Text, out check_in) 
                   || !int.TryParse(comboBox9.Text, out check_in) || !int.TryParse(comboBox10.Text, out check_in) )
                {
                    MessageBox.Show("Invalid inputs entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    if (radioButton1.Checked == true)
                    {
                        host = "us";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        host = "opponent";
                    }
                    else if (radioButton3.Checked == true)
                    {
                        host = "other";
                    }

                    string from = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    string end = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                    start_time = TimeSpan.FromHours(Convert.ToInt32(comboBox9.Text));
                    end_time = TimeSpan.FromHours(Convert.ToInt32(comboBox10.Text));


                    fromTimeString_s = start_time.ToString("hh':'mm");
                    fromTimeString_e = end_time.ToString("hh':'mm");


                    cmd.CommandText = "update  `match` set `venue`='" + txt_venue.Text + "',`start_time`='" + fromTimeString_s + "',`end_time`='" + fromTimeString_e + "',`result`='" + txt_result.Text+"',`host_by`='" + host+"',`type`='" + comboBox4.Text+"' where (match_id='" + match_id + "')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "update `match_date_has_match` set `start_date`='"+ from+"',`end_date`='"+end +"' where (match_match_id='"+ match_id+"')"; 
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Match information updated Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            //}
        
        }
    }
}
