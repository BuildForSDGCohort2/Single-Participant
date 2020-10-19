using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClinicManagmentSystem.UI
{
    public partial class DoctorPanel : Form
    {
        int account_id;
        public DoctorPanel(int id)
        {
            InitializeComponent();
            account_id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            EditProfile editProfile = new EditProfile(account_id);
            editProfile.ShowDialog();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            ViewReservations viewReservations = new ViewReservations(account_id);
            viewReservations.ShowDialog();
            Show();
        }
        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        private void DoctorPanel_Load(object sender, EventArgs e)
        {
            //  Hide();
            //  ViewReservations viewReservations = new ViewReservations(account_id);
            //  viewReservations.ShowDialog();
            //  Show();


            SqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT user_username,account_name,account_dob,account_phone,account_type,acount_notes,account_creation_date FROM [user], account WHERE account_user_id = user_id AND account_id=@account_id";
            command.Parameters.AddWithValue("@account_id", account_id);
            con.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //textBox1.Text = account_id.ToString();
                textBox2.Text = reader.GetValue(0).ToString();
                labelWelcome.Text = reader.GetValue(0).ToString();
                textBox3.Text = reader.GetValue(1).ToString();
                try
                {
                    dateTimePicker1.Value = DateTime.Parse(reader.GetValue(2).ToString());

                }
                catch (Exception)
                {

                }
                textBox4.Text = reader.GetValue(3).ToString();
                if (reader.GetInt32(4) == 0)
                    textBox5.Text = "Secretary";
                else if (reader.GetInt32(4) == 1)
                    textBox5.Text = "Doctor";
                else if (reader.GetInt32(4) == 2)
                    textBox5.Text = "Patient";
                textBox6.Text = reader.GetValue(5).ToString();
                textBox7.Text = reader.GetValue(6).ToString();
            }
            con.Close();





        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
