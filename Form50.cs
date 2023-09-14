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
    public partial class Form50 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i, check_in, date, month, year,spo_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
       "database=ecams_database;" + "password=facebook2018;");
        private void Form50_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from achievement where sport_sport_id='" + spo_id + "'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());

                for (int j = 0; j < i; j++)
                {
                    dataGridView1.Rows.Add(dt.Rows[j]["competition"].ToString(), dt.Rows[j]["type"].ToString(),
                    dt.Rows[j]["date"].ToString(), dt.Rows[j]["venue"].ToString(), dt.Rows[j]["result"].ToString(), dt.Rows[j]["leader"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        public Form50(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
        }
    }
}
