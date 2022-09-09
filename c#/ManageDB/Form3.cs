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
using System.Xml.Linq;

namespace ManageDB
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var Resource = ad_resource_textbox.Text;
            var connectionString = "Data Source=mssql.bryan86.hu;Initial Catalog=vm;User ID=demo;Password=Demo1234#";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();


            
            SqlCommand cmd = new SqlCommand("Insert_vm_resources", con);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@resource", Resource);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Resource Group saved successfully ...");
           
            this.Close();

        }
    }
}
