using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageDB
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
       
                var Location = ad_location_textbox.Text;
                var connectionString = "Data Source=mssql.bryan86.hu;Initial Catalog=vm;User ID=demo;Password=Demo1234#";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand("Insert_vm_locations", con);


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@location", Location);

                cmd.ExecuteNonQuery();

                MessageBox.Show("New location saved successfully ...");

                this.Close();

         
        }
    }
}
