using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Exmpl1
{
    public partial class Autorization : Form
    {

        DB db = new DB();

        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();

        SqlDataAdapter adapter1 = new SqlDataAdapter();
        SqlDataAdapter adapter2 = new SqlDataAdapter();
        SqlDataAdapter adapter3 = new SqlDataAdapter();

        public Autorization()
        {
            InitializeComponent();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Autorization_Load(object sender, EventArgs e)
        {
            tbx_Password.Enabled = false;
            tbx_Code.Enabled = false;
            btn_Enter.Enabled = false;
            btn_Ok.Enabled = false;
            btn_GetCode.Enabled = false;
            btn_Reset.Enabled = false;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            tbx_Login.Clear();
            tbx_Password.Clear();
            tbx_Password.Enabled = false;
            tbx_Code.Enabled = false;
            btn_Enter.Enabled = false;
            btn_GetCode.Enabled = false;
            btn_Reset.Enabled = false;
        }

        private void tbx_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SqlCommand command1 = new SqlCommand("SELECT login FROM sotrudniki WHERE login = @ul", db.GetConnection());
                command1.Parameters.Add("@ul", SqlDbType.NVarChar).Value = Convert.ToString(tbx_Login.Text);
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                if (table1.Rows.Count > 0)
                {
                    tbx_Password.Enabled = true;
                    tbx_Password.Focus();
                }
                else
                {
                    MessageBox.Show("Введён неверный логин!");
                }
            }
        }

        private void tbx_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SqlCommand command2 = new SqlCommand("SELECT password FROM sotrudniki WHERE password = @up", db.GetConnection());
                command2.Parameters.Add("@up", SqlDbType.NVarChar).Value = Convert.ToString(tbx_Password.Text);
                adapter2.SelectCommand = command2;
                adapter2.Fill(table2);
                if (table2.Rows.Count > 0) 
                {
                    tbx_Code.Enabled = true;
                    btn_GetCode.Enabled = true;
                }
            }
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            SqlCommand command3 = new SqlCommand("SELECT * FROM sotrudniki WHERE login = @ur AND password = @un", db.GetConnection());
            command3.Parameters.Add("@ur", SqlDbType.NVarChar).Value = Convert.ToString(tbx_Login.Text);
            command3.Parameters.Add("@un", SqlDbType.NVarChar).Value = Convert.ToString(tbx_Password.Text);
            adapter3.SelectCommand = command3;
            adapter3.Fill(table3);
            if (table3.Rows.Count > 0)
            {
                if (table3.Rows[0]["specialnost"].ToString() == "администратор")
                {
                    Form_Sotr wAdm = new Form_Sotr();
                    wAdm.Show();
                    this.Hide();
                }
                else if (table3.Rows[0]["specialnost"].ToString() == "отоларинголог")
                {
                    FormSotr1 wOtl = new FormSotr1();
                    wOtl.Show();
                }
            }
        }

        public string text = string.Empty;
        public void random()
        {
            Random random = new Random();
            text = String.Empty;
            string alf = "1234567890abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < 6; ++i)
            {
                text += alf[random.Next(alf.Length)];
            }
            if (MessageBox.Show(text) == DialogResult.OK)
            {
                tbx_Code.Enabled = true;
                btn_Reset.Enabled = true;
                btn_Ok.Enabled = true;
            }
        }

        private void btn_GetCode_Click(object sender, EventArgs e)
        {
            random();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (tbx_Code.Text == text)
            {
                btn_Enter.Enabled = true;
            }
            else
            {
                MessageBox.Show("Введён неверный код. Перезапустите и попробуйте ещё раз.");

            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            tbx_Code.Clear();
            random();
        }
    }
}
