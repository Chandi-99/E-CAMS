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
    public partial class Form41 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in,spo_id,match_id;
        TimeSpan start_time, end_time;
        string host;
        string fromTimeString_s, fromTimeString_e;

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form41(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
            for(int i = 1; i < 25; i++)
            {
                comboBox9.Items.Add(i);
                comboBox10.Items.Add(i);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_match.Text = txt_opposition.Text = txt_venue.Text =  comboBox4.Text =   comboBox9.Text =
                  comboBox10.Text = "";
            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = false;
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from `match` where `match`='"+txt_match.Text+"'";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                i = Convert.ToInt32(dt.Rows.Count);
                if (i != 0)
                {
                    MessageBox.Show("Match name is already taken","Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                }
                else
                {

                    if (txt_match.Text == "" || txt_opposition.Text == "" || txt_venue.Text == "" || comboBox9.Text == "" || comboBox10.Text == "" ||
                         comboBox4.Text == "" ||  (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
                       || int.TryParse(txt_opposition.Text, out check_in) || int.TryParse(txt_venue.Text, out check_in)  ||
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
                        dt.Rows.Clear();
                        cmd.CommandText = "insert into `match` (opponent,venue,start_time,end_time,result,sport_sport_id,host_by,`match`,type) values('" + txt_opposition.Text + "'," +
                            "'" + txt_venue.Text + "','" + fromTimeString_s + "','" + fromTimeString_e + "','','" + spo_id + "','" + host + "','" + txt_match.Text + "','" + comboBox4.Text + "')";
                        
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "select * from `match`";
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();

                        i = Convert.ToInt32(dt.Rows.Count);
                        match_id = Convert.ToInt32(dt.Rows[i-1]["match_id"].ToString());
                        cmd.CommandText = "insert into `match_date_has_match` (match_match_id,start_date,end_date) values ('" + match_id + "','" + from + "','" + end + "')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Match Created Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

    }
}
