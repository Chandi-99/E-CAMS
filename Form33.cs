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
    public partial class Form33 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in, date, month, year,soc_id,student_id;

        private void Form33_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from student_has_society where (`society_society_id`= '"+soc_id+"' and `status`= '0')";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count);
                
                DataTable dt1 = new DataTable();

                for(int j = 0; j < i; j++)
                {
                    student_id = Convert.ToInt32(dt.Rows[j]["student_student_id"].ToString());
                    cmd.CommandText="select * from student where student_id='"+ student_id+"'";
                    da.Fill(dt1);
                    cmd.ExecuteNonQuery();

                    dataGridView1.Rows.Add(dt1.Rows[0]["student_name"].ToString(), dt1.Rows[0]["age"].ToString(), dt.Rows[j]["position"].ToString(), dt.Rows[j]["since"].ToString());
                    dt1.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form33(int society_id)
        {
            InitializeComponent();
            soc_id = society_id;
        }
    }
}
