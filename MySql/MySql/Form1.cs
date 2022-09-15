using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MySql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string _server =txtServer.Text;
            string _database = txtDatabase.Text;
            string _user = txtUser.Text;
            string _password = txtPassword.Text;

            string MySQLConnect = $"SERVER={_server}; DATABASE={_database}; UID={_user}; PWD={_password}";

            if(string.IsNullOrEmpty(txtServer.Text)==true|| string.IsNullOrEmpty(txtDatabase.Text) == true || string.IsNullOrEmpty(txtUser.Text) == true || string.IsNullOrEmpty(txtPassword.Text) == true )
            {
                MessageBox.Show("Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                using(var connect=new MySqlConnection(MySQLConnect))
                {
                    using (var cmd = new MySqlCommand("SELECT product FROM sales order by product ASC LIMIT 6", connect))
                    {
                        try
                        {
                            cmd.Connection.Open();
                            MySqlDataReader mdr = cmd.ExecuteReader();
                            lstProduct.Items.Add("Company Products");
                            lstProduct.Items.Add("----------------");
                            while (mdr.Read())
                            {
                                lstProduct.Items.Add(mdr[ "product"].ToString());
                            }
                            labelConnectionStatus.Text = "Connected";

                        }
                        catch (Exception error)
                        {

                            MessageBox.Show("Connection Failured:\n" + error.ToString(), "Error:" + error.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                }

            }

        }


    }
}
