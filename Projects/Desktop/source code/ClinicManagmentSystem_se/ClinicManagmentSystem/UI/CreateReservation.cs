using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Configuration;
using System.Linq;


using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClinicManagmentSystem.UI
{
    public partial class CreateReservation : Form
    {
          int  secretary_id;
        public CreateReservation(int id)
        {
            InitializeComponent();
            secretary_id = id;
        }

        SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
        SqlCommand command;
        private void updateList(string query)
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT account_id,account_name,account_type FROM account WHERE account_type = 2 AND(account_name LIKE @query OR account_phone LIKE @query)";
            command.Parameters.AddWithValue("@query", query + "%");
            con.Open();
            listBox1.Items.Clear();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            listBox1.Items.Add(new account(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));



            con.Close();
        }



        

        private void CreateReservation_Load(object sender, EventArgs e)
        {
            updateList("");
            updateSlots();
            dateTimePicker1.MinDate = DateTime.Today;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateList(textBox1.Text);
        }

        //create a function to update the combo box

        private void updateSlots()
        {
            command = con.CreateCommand();
            command.CommandText = "SELECT reservation_visit_slot FROM reservation WHERE reservation_visit_date=@date";
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            con.Open();
            //first let's get our dictionary
            SqlDataReader reader = command.ExecuteReader();
            Dictionary<int, string> slots = Utils.getSlots();

            
            while (reader.Read())
            {
                slots.Remove(reader.GetInt32(0));
            }
            //after this we need to fill the combobox with the rest slots
            comboBox1.Items.Clear();
            foreach (object slot in slots.ToArray())
            comboBox1.Items.Add(slot);
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;

            con.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            updateSlots();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //in order to reserve a slot we have to make sure we choose a patient , a slot
            //iput validation
            //check for patient first
            if (listBox1.SelectedIndex < 0 || listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Please select a patient");
                return;
            }
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please selct a slot");
                return;
            }

            //perform the reservation
            //get patient id and slot chosen
            int patient_id = ((account)listBox1.SelectedItem).getID();
            int slot = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            command = con.CreateCommand();
            command.CommandText = "INSERT INTO reservation(reservation_secretary_id,reservation_patient_id,reservation_visit_date,reservation_visit_slot,reservation_date)VALUES(@secretary_id,@patient_id,@visit_date,@visit_slot,@date)";
            command.Parameters.AddWithValue("@secretary_id", secretary_id);
            command.Parameters.AddWithValue("@patient_id", patient_id);
            command.Parameters.AddWithValue("@visit_date", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@visit_slot", slot);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            con.Open();

            if (command.ExecuteNonQuery() > 0)
            {
                //we suces made the 
                //we suceshuly prmorm the reservatio
                command.CommandText = "SELECT reservation_id FROM reservation WHERE reservation_visit_date=@visit_date AND reservation_visit_slot=@visit_slot";
                int reservation_id = (int)command.ExecuteScalar();
                MessageBox.Show("Reservation was made");
                MessageBox.Show("Reservation ID: " + reservation_id.ToString());

            }
            else { 
            MessageBox.Show("Failed to perform reservation");
              }

            con.Close();
            updateSlots();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
