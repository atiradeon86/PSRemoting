using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ManageDB
{
   
    public partial class Form2 : Form
    {
         static class Vmdata
    {
        public static string connectionString;
        public static string hostname;
        public static string resource;
        public static string location;
        public static string domain;
        public static string ADdomain;
        public static string Pem;

        public static int hostname_id;
        public static int resource_id;
        public static int location_id;
        public static int domain_id;
        public static int ADdomain_id;
        public static int Pem_id;



            public static string test()
        {
            return "test";
        }
    }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'vmDataSet.GetPEMS' table. You can move, or remove it, as needed.
            this.getPEMSTableAdapter.Fill(this.vmDataSet.GetPEMS);
            // TODO: This line of code loads data into the 'vmDataSet.GetADDomains' table. You can move, or remove it, as needed.
            this.getADDomainsTableAdapter.Fill(this.vmDataSet.GetADDomains);
            // TODO: This line of code loads data into the 'vmDataSet2.GetResources' table. You can move, or remove it, as needed.
            this.getResourcesTableAdapter.Fill(this.vmDataSet2.GetResources);
            // TODO: This line of code loads data into the 'vmDataSet2.GetResources' table. You can move, or remove it, as needed.
            this.getResourcesTableAdapter.Fill(this.vmDataSet2.GetResources);
            // TODO: This line of code loads data into the 'vmDataSet2.GetResources' table. You can move, or remove it, as needed.
            this.getResourcesTableAdapter.Fill(this.vmDataSet2.GetResources);
            // TODO: This line of code loads data into the 'vmDataSet2.GetResources' table. You can move, or remove it, as needed.
            this.getResourcesTableAdapter.Fill(this.vmDataSet2.GetResources);
            // TODO: This line of code loads data into the 'vmDataSet2.vm_resources' table. You can move, or remove it, as needed.
            //this.vm_resourcesTableAdapter.Fill(this.vmDataSet2.vm_resources);
            // TODO: This line of code loads data into the 'vmDataSet2.GetResources' table. You can move, or remove it, as needed.
            this.getResourcesTableAdapter.Fill(this.vmDataSet2.GetResources);
            // TODO: This line of code loads data into the 'vmDataSet2.GetLocations' table. You can move, or remove it, as needed.
            this.getLocationsTableAdapter.Fill(this.vmDataSet2.GetLocations);
            // TODO: This line of code loads data into the 'vmDataSet2.vm_resources' table. You can move, or remove it, as needed.
           // this.vm_resourcesTableAdapter.Fill(this.vmDataSet2.vm_resources);
            // TODO: This line of code loads data into the 'vmDataSet2.vm_resources' table. You can move, or remove it, as needed.
            //this.vm_resourcesTableAdapter.Fill(this.vmDataSet2.vm_resources);
            // TODO: This line of code loads data into the 'vmDataSet.vm_resources' table. You can move, or remove it, as needed.
            //this.vm_resourcesTableAdapter.Fill(this.vmDataSet.vm_resources);
            // TODO: This line of code loads data into the 'vmDataSet.GetResources' table. You can move, or remove it, as needed.
            this.getResourcesTableAdapter.Fill(this.vmDataSet.GetResources);
            // TODO: This line of code loads data into the 'vmDataSet1.vm_locations' table. You can move, or remove it, as needed.

            this.getLocationsTableAdapter.Fill(this.vmDataSet.GetLocations);
            // TODO: This line of code loads data into the 'vmDataSet.GetHostnames' table. You can move, or remove it, as needed.
            this.getHostnamesTableAdapter.Fill(this.vmDataSet.GetHostnames);

            addomain_box.SelectedItem = null;
            addomain_box.SelectedText = "-";

            pem_box.SelectedItem = null;
            pem_box.SelectedText = "-";

            Vmdata.connectionString  = "Data Source=mssql.bryan86.hu;Initial Catalog=vm;User ID=demo;Password=Demo1234#";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_resource_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
        public void tb_hostname_TextChanged(object sender, EventArgs e)
        {
            Vmdata.hostname = tb_hostname.Text;
        }

        public void tb_domain_TextChanged(object sender, EventArgs e)
        {
            Vmdata.domain = tb_domain.Text;
        }
        

        public void addomain_TextChanged(object sender, EventArgs e)
        {
            Vmdata.ADdomain = addomain_box.Text;
        }

        public void pem_box_TextChanged(object sender, EventArgs e)
        {
            Vmdata.Pem = pem_box.Text;
        }


        public void resource_box_TextChanged(object sender, EventArgs e)
        {
            Vmdata.resource = resource_box.Text;
        }

    


        public void button1_Click(object sender, EventArgs e)
        {
          

            Vmdata.hostname = tb_hostname.Text;
            Vmdata.location = location_combo.Text;
            Vmdata.resource = resource_box.Text;
            Vmdata.domain = tb_domain.Text;
            Vmdata.ADdomain = addomain_box.Text;
            Vmdata.Pem = pem_box.Text;


            /*SQL Query*/

            

            SqlConnection con = new SqlConnection(Vmdata.connectionString);
            con.Open();

            /* query location */



            string selectquery = "select id FROM vm_locations WHERE vm_locations.location =  @location";

            string query_resources = "select id FROM vm_resources WHERE resource =  @resources";

            string query_addomain = "select id FROM vm_addomains WHERE addomain =  @addomain";

            string query_pem = "select id FROM vm_pems WHERE pem =  @pem";



            SqlCommand cmd = new SqlCommand(selectquery, con);
         


            cmd.Parameters.Add("@location", SqlDbType.VarChar);
            cmd.Parameters["@location"].Value = Vmdata.location;


            SqlCommand cmd2 = new SqlCommand(query_resources, con);


            cmd2.Parameters.Add("@resources", SqlDbType.VarChar);
            cmd2.Parameters["@resources"].Value = Vmdata.resource;


            SqlCommand cmd3 = new SqlCommand(query_addomain, con);

            cmd3.Parameters.Add("@addomain", SqlDbType.VarChar);
            cmd3.Parameters["@addomain"].Value = Vmdata.ADdomain;

            SqlCommand cmd4 = new SqlCommand(query_pem, con);

            cmd4.Parameters.Add("@pem", SqlDbType.VarChar);
            cmd4.Parameters["@pem"].Value = Vmdata.Pem;

           



            SqlDataReader reader = cmd.ExecuteReader();

     
            while (reader.Read())
            {
                ReadLocation((IDataRecord)reader);
            }

            reader.Close();



            SqlDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                ReadResource((IDataRecord)reader2);
            }

            reader2.Close();

            SqlDataReader reader3 = cmd3.ExecuteReader();

            while (reader3.Read())
            {
                ReadAddomain((IDataRecord)reader3);
            }

            reader3.Close();

            SqlDataReader reader4 = cmd4.ExecuteReader();

            while (reader4.Read())
            {
                ReadPem((IDataRecord)reader4);
            }

            reader4.Close();


            Console.WriteLine(Vmdata.hostname.GetType());
            Console.WriteLine(Vmdata.hostname.Length);

            if ((Vmdata.hostname.Length) > 2) {

                Console.WriteLine("Új hostnév");

                SqlCommand cmd_vm = new SqlCommand("Insert_vm_hostnames", con);

                cmd_vm.CommandType = CommandType.StoredProcedure;
                cmd_vm.Parameters.AddWithValue("@hostname", Vmdata.hostname);

                cmd_vm.ExecuteNonQuery();


                SqlDataAdapter da = new SqlDataAdapter();
               
                SqlConnection conn = new SqlConnection(Vmdata.connectionString);
                conn.Open();
                da.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM vm_hostnames ORDER BY ID DESC");
                da.SelectCommand.Connection = conn;

                Vmdata.hostname_id = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                Console.WriteLine(Vmdata.hostname_id);



            }

            if ((Vmdata.location.Length) > 2)
            {
                Console.WriteLine("Új location");
                
                SqlCommand cmd_vm_loc = new SqlCommand("Insert_vm_locations", con);


                cmd_vm_loc.CommandType = CommandType.StoredProcedure;
                cmd_vm_loc.Parameters.AddWithValue("@location", Vmdata.location);

                cmd_vm_loc.ExecuteNonQuery();


                SqlDataAdapter da = new SqlDataAdapter();
                
                SqlConnection conn = new SqlConnection(Vmdata.connectionString);
                conn.Open();
                da.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM vm_locations ORDER BY ID DESC");
                da.SelectCommand.Connection = conn;

                Vmdata.location_id =  Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                Console.WriteLine(Vmdata.location_id);


            }



            if ((Vmdata.resource.Length) > 2)
            {
                Console.WriteLine("Új resource");
                Console.WriteLine(Vmdata.resource);

                SqlCommand cmd_vm_res = new SqlCommand("[Insert_vm_resources]", con);


                cmd_vm_res.CommandType = CommandType.StoredProcedure;
                cmd_vm_res.Parameters.AddWithValue("@resource", Vmdata.resource);

                cmd_vm_res.ExecuteNonQuery();


                SqlDataAdapter da = new SqlDataAdapter();
            
                SqlConnection conn = new SqlConnection(Vmdata.connectionString);
                conn.Open();
                da.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM vm_resources ORDER BY ID DESC");
                da.SelectCommand.Connection = conn;

                Vmdata.resource_id = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                Console.WriteLine(Vmdata.resource_id);


            }

            if ((Vmdata.domain.Length) > 2)
            {
                Console.WriteLine("Új domain");
                Console.WriteLine(Vmdata.domain);

                SqlCommand cmd_vm_dom = new SqlCommand("[Insert_vm_domains]", con);


                cmd_vm_dom.CommandType = CommandType.StoredProcedure;
                cmd_vm_dom.Parameters.AddWithValue("@domain", Vmdata.domain);

                cmd_vm_dom.ExecuteNonQuery();


                SqlDataAdapter da = new SqlDataAdapter();
              
                SqlConnection conn = new SqlConnection(Vmdata.connectionString);
                conn.Open();
                da.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM vm_domains ORDER BY ID DESC");
                da.SelectCommand.Connection = conn;

                Vmdata.domain_id = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                Console.WriteLine(Vmdata.domain_id);


            }

            if ((Vmdata.ADdomain.Length) > 2)
            {
                Console.WriteLine("Új addomain");
                Console.WriteLine(Vmdata.ADdomain);

                SqlCommand cmd_vm_addomain = new SqlCommand("[Insert_vm_addomains]", con);


                cmd_vm_addomain.CommandType = CommandType.StoredProcedure;
                cmd_vm_addomain.Parameters.AddWithValue("@addomain", Vmdata.ADdomain);

                cmd_vm_addomain.ExecuteNonQuery();


                SqlDataAdapter da = new SqlDataAdapter();
         
                SqlConnection conn = new SqlConnection(Vmdata.connectionString);
                conn.Open();
                da.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM vm_addomains ORDER BY ID DESC");
                da.SelectCommand.Connection = conn;

                Vmdata.ADdomain_id = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                Console.WriteLine(Vmdata.ADdomain_id);


            }

            if ((Vmdata.Pem.Length) > 2)
            {
                Console.WriteLine("Új pem");
                Console.WriteLine(Vmdata.Pem);

                SqlCommand cmd_vm_pem = new SqlCommand("[Insert_vm_pem]", con);


                cmd_vm_pem.CommandType = CommandType.StoredProcedure;
                cmd_vm_pem.Parameters.AddWithValue("@pem", Vmdata.Pem);

                cmd_vm_pem.ExecuteNonQuery();


                SqlDataAdapter da = new SqlDataAdapter();
                
                SqlConnection conn = new SqlConnection(Vmdata.connectionString);
                conn.Open();
                da.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM vm_pems ORDER BY ID DESC");
                da.SelectCommand.Connection = conn;

                Vmdata.Pem_id = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                Console.WriteLine(Vmdata.Pem_id);


            }


            /*Write to SQL based on stored procedure */

            
            SqlCommand cmd_vm_insert= new SqlCommand("Insert_vm_data", con);

            cmd_vm_insert.CommandType = CommandType.StoredProcedure;
            cmd_vm_insert.Parameters.AddWithValue("@hostname_id", Vmdata.hostname_id);
            cmd_vm_insert.Parameters.AddWithValue("@location_id", Vmdata.location_id);
            cmd_vm_insert.Parameters.AddWithValue("@resource_id", Vmdata.resource_id);
            cmd_vm_insert.Parameters.AddWithValue("@domain_id", Vmdata.domain_id);
            cmd_vm_insert.Parameters.AddWithValue("@ad_domain_id", Vmdata.ADdomain_id);
            cmd_vm_insert.Parameters.AddWithValue("@pem_id", Vmdata.Pem_id);

            cmd_vm_insert.ExecuteNonQuery();
            con.Close();
            

            
            MessageBox.Show("Data saved successfully ...");
            Form2 frm2 = new Form2();
            frm2.Hide();
            this.Close();
            this.Controls.Clear();
            InitializeComponent();
            



        }
        public static void ReadLocation(IDataRecord dataRecord)
        {
            Console.WriteLine(dataRecord[0]);
            Vmdata.location_id  =Convert.ToInt32(dataRecord[0]);
            

        }

        public static void ReadResource(IDataRecord dataRecord)
        {
            Console.WriteLine(dataRecord[0]);
            Vmdata.resource_id = Convert.ToInt32(dataRecord[0]);


        }

        public static void ReadAddomain(IDataRecord dataRecord)
        {
            Console.WriteLine(dataRecord[0]);
            Vmdata.ADdomain_id = Convert.ToInt32(dataRecord[0]);


        }

        public static void ReadPem(IDataRecord dataRecord)
        {
            Console.WriteLine(dataRecord[0]);
            Vmdata.Pem_id = Convert.ToInt32(dataRecord[0]);


        }

        private void resource_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
