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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var Pem = ad_pem_textbox.Text;
            var connectionString = "Data Source=mssql.bryan86.hu;Initial Catalog=vm;User ID=demo;Password=Demo1234#";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();



            SqlCommand cmd = new SqlCommand("Insert_vm_pem", con);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pem", Pem);

            cmd.ExecuteNonQuery();

            MessageBox.Show("New PEM saved successfully ...");

            this.Close();
        }
    }
}
