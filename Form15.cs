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
    public partial class Form15 : Form
    {
        int start,end,day,month,year,resource_id;
        TimeSpan start_t, end_t;
        int i, checked_in;
        String r_date;
        DateTime date;
        string fromTimeString_s;
        string fromTimeString_e;
        string resource_name;
        string db;

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
          "database=ecams_database;" + "password=facebook2018;");
        private void button3_Click(object sender, EventArgs e)
        {
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            if ( textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || int.TryParse(textBox8.Text, out checked_in)
                || int.TryParse(textBox9.Text, out checked_in) || !int.TryParse(textBox10.Text, out checked_in))
            {
                MessageBox.Show("Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into reservation (date, start_time,end_time,resource_resource_id,reserved_by,purpose,count ) values('"+ db+ "','" + fromTimeString_s + "','" + fromTimeString_e + "','" + resource_id+"','"+textBox8.Text+"','"+ textBox9.Text+"','"+ textBox10.Text+"')";
                cmd.ExecuteNonQuery();

                MessageBox.Show("Resource reserved succussfully!","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                panel1.Visible = false;
            }
        }

        public Form15()
        {
            InitializeComponent();
            if (connectionstring.State != ConnectionState.Open)
            {
                connectionstring.Open();
            }
            panel1.Visible = false;
            for (int i=0;i<24;i++)
            {
                comboBox6.Items.Add(i+1);
                comboBox7.Items.Add(i + 1);
            }
            MySqlCommand cmd = connectionstring.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from resource";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteNonQuery();

            i = Convert.ToInt32(dt.Rows.Count);
            for(int j = 0; j < i; j++)
            {
                comboBox1.Items.Add(dt.Rows[j]["resource_name"].ToString()) ;
            }

        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            try
            {
                start = Convert.ToInt32(comboBox6.Text.ToString());
                end = Convert.ToInt32(comboBox7.Text.ToString());

                start_t = TimeSpan.FromHours(start);
                end_t = TimeSpan.FromHours(end);

                fromTimeString_s = start_t.ToString("hh':'mm");
                fromTimeString_e = end_t.ToString("hh':'mm");

                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                if (  comboBox6.Text == "" || comboBox7.Text == "" || comboBox1.Text==""|| dateTimePicker1.Value.ToString()=="")
                {
                    MessageBox.Show("Invalid Inputs Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                 else if (int.TryParse(comboBox1.Text, out checked_in) 
                   || !int.TryParse(comboBox6.Text, out checked_in) || !int.TryParse(comboBox7.Text, out checked_in))
                {
                    MessageBox.Show("Invalid input Entered!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if (start > 12 || start < 0 || end > 12 || end < 0)
                {
                    MessageBox.Show("Invalid Time ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    db = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    MySqlCommand cmd = connectionstring.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from resource where resource_name='" + comboBox1.Text + "'";
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();

                    i = Convert.ToInt32(dt.Rows.Count.ToString());

                    if (i == 0)
                    {
                        MessageBox.Show("There is no Resource registered for that name!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else if (i == 1)
                    {
                        resource_id = Convert.ToInt32(dt.Rows[0]["resource_id"].ToString());
                        resource_name = dt.Rows[0]["resource_name"].ToString();
                        i = Convert.ToInt32(dt.Rows[0]["resource_id"].ToString());
                        dt.Rows.Clear();
                        
                        cmd.CommandText = "select * from reservation where resource_resource_id='"+ i+"' and date='"+ db+"'";
                        dt.Rows.Clear();
                        da.Fill(dt);
                        cmd.ExecuteNonQuery();
                        i = Convert.ToInt32(dt.Rows.Count);
                        if (i!=0)
                        {
                             MessageBox.Show("Sorry! There is another reservation in the same day", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }

                        else
                        {
                            panel1.Visible = true;
                            textBox2.Text = resource_name;
                            dateTimePicker2.Value = Convert.ToDateTime(db.ToString());
                            textBox6.Text = fromTimeString_s.ToString();
                            textBox7.Text = fromTimeString_e.ToString();

                            dateTimePicker2.MinDate = dateTimePicker1.Value;
                            dateTimePicker2.MaxDate = dateTimePicker1.Value;
                        }
                    }
                }
        }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
}
           
    }
}
