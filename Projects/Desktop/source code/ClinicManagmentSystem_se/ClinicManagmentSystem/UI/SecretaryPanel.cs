using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace ClinicManagmentSystem.UI
{
    public partial class SecretaryPanel : Form
    {
        int account_id;
         

        public SecretaryPanel(int id)
        {
            InitializeComponent();
            account_id = id;
        }

        public SecretaryPanel()
        {
            
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
            PatientProfile patientProfile = new PatientProfile();
            patientProfile.ShowDialog();
            Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            CreateReservation createReservation = new CreateReservation(account_id);
            createReservation.ShowDialog();
            Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            ViewReservations viewReservations = new ViewReservations(account_id);
            viewReservations.ShowDialog();
            Show();
        }

        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        private void SecretaryPanel_Load(object sender, EventArgs e)

        {




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

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog OD = new OpenFileDialog();
        //    OD.FileName = "";
        //    OD.Filter = "Supported Images|*.jpg;*.jpeg;*.png";
        //    if (OD.ShowDialog() == DialogResult.OK)
        //    {
        //        pictureBox1.Load(OD.FileName);

        //        saveImageInDatabase();
        //    }
        //}

        //private void saveImageInDatabase()
        //{
        //    con.Open();
        //    SqlCommand command = con.CreateCommand();

        //    var image = new ImageConverter().ConvertTo(pictureBox1.Image, typeof(Byte[]));
        //    command.Parameters.AddWithValue("@image", image);

        //    command.CommandText = "INSERT INTO imageTable (image) VALUES(@image)";

        //    if (command.ExecuteNonQuery() > 0)
        //    {
        //        MessageBox.Show("Profle Image update is successful");

               

        //    }
        //    else
        //    {
        //        MessageBox.Show("Profle Image update not successful");

        //    }

        //    con.Close();
        //}

        //private void SetProfile()
        //{
           

        //    SqlCommand command = con.CreateCommand();

        //    con.Open();
            
        //    command.CommandText = "SELECT * FROM imageTable WHERE id = 0";
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        MemoryStream stream = new MemoryStream(reader.GetSqlBytes(1).Buffer);
        //        pictureBox1.Image = Image.FromStream(stream);

        //    }
        //    con.Close();
           
        //}

        private void button6_Click(object sender, EventArgs e)
        {
           

        }
    }
}
