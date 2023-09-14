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
    public partial class Form25 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,check_in,date,month,year,soc_id;
        int start, end;
        string fromTimeString_s, fromTimeString_e;
        string other_schools;
        TimeSpan start_t, end_t;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form25(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
            for (int i=0;i<24;i++)
            {
                comboBox6.Items.Add(i+1);
                comboBox7.Items.Add(i+1);
            }
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
                cmd.CommandText = "select * from event where event_name='" + txt_event.Text + "'";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                i = Convert.ToInt32(dt.Rows.Count);
                if (i != 0)
                {
                    MessageBox.Show("Event name is already taken", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if (txt_event.Text == "" || txt_venue.Text == "" || txt_purpose.Text == "" || txt_cost.Text == "" ||
                    txt_income.Text == "" ||  comboBox6.Text == "" || comboBox7.Text == "" ||
                    (radioButton1.Checked == false && radioButton2.Checked == false)  || int.TryParse(txt_purpose.Text, out check_in) || int.TryParse(txt_venue.Text, out check_in) ||
                     !int.TryParse(txt_cost.Text, out check_in) || !int.TryParse(txt_income.Text, out check_in) || !int.TryParse(comboBox6.Text, out check_in) || !int.TryParse(comboBox7.Text, out check_in))
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
                        other_schools = "Yes";
                    }
                    else if(radioButton2.Checked == true)
                    {
                        other_schools = "No";
                    }

                    cmd.CommandText = "insert into event (event_name,venue,start_time,end_time,society_society_id,parti_count,income,cost,other_schools,event_date,purpose)" +
                        " values('"+txt_event.Text+ "','"+txt_venue.Text+ "','"+ fromTimeString_s + "','"+ fromTimeString_e + "','"+soc_id+"','"+txt_participant.Text+"','"+txt_income.Text+"','"+txt_cost.Text+ "','"+other_schools+"','"+ event_date+"','"+txt_purpose.Text+"')";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Event created succussfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
