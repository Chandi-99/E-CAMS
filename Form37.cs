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
    public partial class Form37 : Form
    {
        //Initializing mysql connection and datatable row counter
        int i,spo_id;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
            "database=ecams_database;" + "password=facebook2018;");
        public Form37(int sport_id)
        {
            InitializeComponent();
            spo_id = sport_id;
        }

        private void Form37_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }
                //initialize sql command variable and datatable variable
                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //sql command
                cmd.CommandText = "select * from sport where sport_id='" + spo_id +"'";
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();

                //convert string to integer
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 1)
                {
                    richTextBox1.Text = dt.Rows[0]["history"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
