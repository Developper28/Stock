using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstApp
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conStr = "server=localhost;user=root;database=data;";
            MySqlConnection con = new MySqlConnection(conStr);
            // Insert Logic
            con.Open();

            var mysqlQuery= "";
           if (ifProductExists(con, textBox1.Text))
            {
                mysqlQuery = @"UPDATE products SET ProductCode='" + textBox1.Text +"',ProductName='"+textBox2.Text+"',ProductStatus='" + comboBox1.Text+ "' WHERE ProductCode='" + textBox1.Text + "'";
            }
           else
            {
                mysqlQuery = @"INSERT INTO products(ProductCode, ProductName, ProductStatus) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "')";
            }

            MySqlCommand cmd = new MySqlCommand(mysqlQuery,con);
            cmd.ExecuteNonQuery();
            con.Close();
            loadData();
        }

        private bool ifProductExists(MySqlConnection con, String productCode)
        {
            //Reading data
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM products WHERE ProductCode='"+productCode+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void loadData ()
        {
            string conStr = "server=localhost;user=root;database=data;";
            MySqlConnection con = new MySqlConnection(conStr);
            //Reading data
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT * FROM products", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["ProductStatus"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string conStr = "server=localhost;user=root;database=data;";
            MySqlConnection con = new MySqlConnection(conStr);
            var mysqlQuery = "";
            if (ifProductExists(con, textBox1.Text))
            {
                con.Open();
                mysqlQuery = @"DELETE FROM Products WHERE ProductCode = '"+textBox1.Text+"'";
                MySqlCommand cmd = new MySqlCommand(mysqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record not exists...");
            }
            loadData();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}
