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
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }
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
                MessageBox.Show("Bir hata oluştu!", ex.ToString());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                text_Ad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                text_Soyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                if (dataGridView1.CurrentRow.Cells[3].Value != null && dataGridView1.CurrentRow.Cells[3].Value != DBNull.Value)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
                }
                cmbGender.SelectedItem = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Students SET FirstName = @FirstName, LastName = @LastName , BirthDate = @BirthDate , Gender = @Gender WHERE StudentID = @StudentID", connection);
                cmd.Parameters.AddWithValue("@StudentID", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                cmd.Parameters.AddWithValue("@FirstName", text_Ad.Text);
                cmd.Parameters.AddWithValue("@LastName", text_Soyad.Text);
                cmd.Parameters.AddWithValue("@BirthDate", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    RefreshData();
                    MessageBox.Show("Kullanıcı Başarılı Bir Şekilde Güncellendi");
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Bir Hata Oluştu", ex.ToString());
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
