using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ClinicManagmentSystem.UI;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace ClinicManagmentSystem
{
    public partial class Login : Form
    {
        public Login()
        {
           
           
          //  Thread t = new Thread(new ThreadStart(StartForm));
           // t.Start();
          //  Thread.Sleep(2000);
            InitializeComponent();
          //  t.Abort();
            
           

            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = ClinicManagmentSystem.Properties.Resources.connectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT user_id FROM [user] where user_username=@username AND user_password=@password";
            command.Parameters.AddWithValue("@username", UsernameTextBox.Text);
            command.Parameters.AddWithValue("@password", passwordTextBox.Text);
           // command.Parameters.AddWithValue("@password", Utils.hashPassword(passwordTextBox.Text));
            con.Open();
            var result = command.ExecuteScalar();//this return only one value
            con.Close();
            if(result != null)
            {
                //authenticated
                if(UsernameTextBox.Text == "admin")
                {
                    //open admin panel form
                    Hide();
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.ShowDialog();
                    Show();

                }
                else
                {
                    con.Open();
                    command.CommandText = "SELECT account_id,account_type FROM account WHERE account_user_id=@user_id";
                    command.Parameters.AddWithValue("@user_id", result.ToString());
                    SqlDataReader reader = command.ExecuteReader();
                   
                    if (reader.Read())
                    {
                        int account_id = reader.GetInt32(0);
                        int account_type = reader.GetInt32(1);
                        con.Close();
                        if (account_type == 0)
                        {
                            //open secretary panelit means this is the secretary
                            Hide();
                            SecretaryPanel secretaryPanel = new SecretaryPanel(account_id);
                            secretaryPanel.ShowDialog();
                            Show();

                        }
                        else if(account_type == 1)
                        {
                            //open doctor panel
                            Hide();
                            DoctorPanel doctorPanel = new DoctorPanel(account_id);
                            doctorPanel.ShowDialog();
                            Show();

                        }
                    }
                }
            }
            else
            {
               
                MessageBox.Show("Authentication failed");
                
            }

        }

       // public void StartForm()
       // {
       //    Application.Run(new SplashScreen());
      //  }

        private void Login_Load(object sender, EventArgs e)
        {
          

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
