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
    public partial class Form36 : Form
    {
        int i, id, part_id, inventory_id;
        String ut;
        MySqlConnection connectionstring = new MySqlConnection("server = localhost;" + "user id = root;" +
           "database=ecams_database;" + "password=facebook2018;");
        public Form36(int society_id,String user_type)
        {
            InitializeComponent();
            id=society_id;
            ut = user_type.ToString();
        }

        private void Form36_Load(object sender, EventArgs e)
        {
            try
            {
                if (connectionstring.State != ConnectionState.Open)
                {
                    connectionstring.Open();
                }

                MySqlCommand cmd = connectionstring.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (ut == "society")
                {
                    cmd.CommandText = "select * from society where society_id='" + id + "'";
                }
                else if (ut == "sport")
                {
                    cmd.CommandText = "select * from sport where sport_id='" + id + "'";
                }

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                
                inventory_id = Convert.ToInt32(dt.Rows[0]["inventory_inventory_id"].ToString());
                dt.Rows.Clear();

                cmd.CommandText = "select * from part where inventory_inventory_id='"+ inventory_id+"' ";
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd);
                da1.Fill(dt);
                cmd.ExecuteNonQuery();

                i = Convert.ToInt32(dt.Rows.Count.ToString());

                for (int j = 0; j < i; j++)
                {
                    dataGridView1.Rows.Add(dt.Rows[j]["part_id"].ToString(), dt.Rows[j]["part_name"].ToString(), dt.Rows[j]["quantity"].ToString(),
                        dt.Rows[j]["donate"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
