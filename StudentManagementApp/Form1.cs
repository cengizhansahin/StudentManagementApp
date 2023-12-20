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

namespace StudentManagementApp
{
    public partial class Form1 : Form
    {
        public static string connectionString = "Server=DESKTOP-S5SNUUO;Database=DB_STUDENT;Trusted_Connection=True;";
        SqlConnection connection = new SqlConnection(connectionString);
        public Form1()
        {
            InitializeComponent();
            RefreshData();
        }
        public void RefreshData()
        {
            string query = "SELECT * FROM Table_Students";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
