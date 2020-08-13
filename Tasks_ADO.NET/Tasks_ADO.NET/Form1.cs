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
using System.Diagnostics.Contracts;
using System.Configuration;

namespace Tasks_ADO.NET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            stat.Items.Add("To Do");
            stat.Items.Add("In Progress");
            stat.Items.Add("Done");

            // To view all database rows when application is started
            RefreshData();
            
        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // method to refresh the gridview and gets called whenever necessary
        private void RefreshData()
        {
            BindingSource bi = new BindingSource();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            try
            {

                //SqlCommand cmd = new SqlCommand($"Insert Into tasks (Name,Status,Date) Values({task.Text},{stat.GetItemText(this.stat.SelectedItem)},{dateTimePicker1.Value}) ", con);
                SqlCommand cmd = new SqlCommand("Select * from tasksV2", con);
                con.Open();
                SqlCommand check_sql = new SqlCommand("Select Count(Id) from tasksV2", con);
                int rows_inDB = (int)check_sql.ExecuteScalar();

                if (rows_inDB > 0)
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    bi.DataSource = rdr;
                    dataGridView1.DataSource = bi;
                    dataGridView1.Refresh();
                }
            }
            catch { Console.WriteLine("EXCEPTION"); }
            finally { con.Close(); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand($"Insert Into tasksV2(Name, Status, Date) Values('{ task.Text }','{ stat.GetItemText(this.stat.SelectedItem)}','{ dateTimePicker1.Value}')", con);
                con.Open();
                int rows_inserted = cmd.ExecuteNonQuery();
                Console.WriteLine(rows_inserted);
                RefreshData();
            }
        }

        private void stat_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand($"Delete from tasksV2 where Id={dataGridView1.SelectedCells[0].Value}", con);
                con.Open();
                cmd.ExecuteNonQuery();
                RefreshData();
            }
        }
    }
}
