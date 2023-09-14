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
    public partial class Form43 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in,spo_id;
        int date, month, year, age;

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                if (txt_competition.Text == "" || txt_venue.Text == "" || txt_result.Text == ""  || comboBox4.Text == ""
                    || int.TryParse(txt_competition.Text, out check_in) || int.TryParse(txt_result.Text, out check_in) || int.TryParse(txt_venue.Text, out check_in) ||
                    int.TryParse(comboBox4.Text, out check_in))
                {
                    MessageBox.Show("Invalid inputs entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
               
                else
                {
                    string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into `achievement` (competition,type,date,venue, leader,sport_sport_id,society_society_id,result) values" +
                        "('"+txt_competition.Text +"','" + comboBox4.Text+"','" + theDate+"','" +txt_venue.Text +"','" +txt_captain.Text +"','" + spo_id+"','" + 3 +"','" +txt_result.Text +"')";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Achievement Added Successfully!", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_competition.Text = txt_venue.Text = txt_result.Text = txt_captain.Text = comboBox4.Text = "";
        }
        
        public Form43(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
        }
    }
}
