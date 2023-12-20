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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO Table_Students (FirstName,LastName,BirthDate,Gender) VALUES (@FirstName,@LastName,@BirthDate,@Gender)", connection);
            sqlCommand.Parameters.AddWithValue("@FirstName", text_Ad.Text);
            sqlCommand.Parameters.AddWithValue("@LastName", text_Soyad.Text);
            sqlCommand.Parameters.AddWithValue("@BirthDate", dateTimePicker1.Value);
            sqlCommand.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
            
            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                RefreshData();
                MessageBox.Show("Öğrenci ekleme işlemi başarılı.");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Bir hata oluştu!",ex.ToString());
            }
        }
    }
}
