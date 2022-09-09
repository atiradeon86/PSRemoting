using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace ManageDB
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vmDataSet.GetAllData' table. You can move, or remove it, as needed.
            this.getAllDataTableAdapter.Fill(this.vmDataSet.GetAllData);

        }

        static class Data
        {
           
            public static int data_selected;



            public static string test()
            {
                return "test";
            }
        }
        public void getAllDataDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //var  data = getAllDataDataGridView[e.ColumnIndex, e.RowIndex].Value;
            var data = getAllDataDataGridView.CurrentRow.Cells[0].Value;
            Data.data_selected = Convert.ToInt32(data);
           
          
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            

            var createForm = new Form2();
      
            createForm.Show();
        }

        public void button5_Click(object sender, EventArgs e)
        {
            this.getAllDataTableAdapter.Fill(this.vmDataSet.GetAllData);


        }


     

        public void button6_Click(object sender, EventArgs e)
        {
            
            var connectionString = "Data Source=mssql.bryan86.hu;Initial Catalog=vm;User ID=demo;Password=Demo1234#";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

           
            string query =  "DELETE FROM vm_data WHERE id = @id";
            SqlCommand cmd = new SqlCommand("DeleteRow", con);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Data.data_selected);

            cmd.ExecuteNonQuery();
            this.getAllDataTableAdapter.Fill(this.vmDataSet.GetAllData);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            var createForm = new Form3();

            createForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var createForm = new Form4();

            createForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var createForm = new Form5();

            createForm.Show();
        }
    }
}
