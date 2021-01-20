using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWO
{
    public partial class fmCorrectUserData : Form
    {
        public List<Admin> Users { get; private set; }
        public AdminForm Adminform;
        public fmCorrectUserData(string surname, string email, string password, string login, AdminForm adminForm)
        {
            InitializeComponent();
            SurnameBox.Text = surname;
            EmailBox.Text = email;
            PasswordBox.Text = password;
            LoginBox.Text = login;
            Adminform = adminForm;
        }

        private void textBoxReColor_TextChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void fmCorrectUserData_Paint(object sender, PaintEventArgs e)
        {
            if (EmailBox.Text == "")
            {
                EmailBox.BorderStyle = BorderStyle.None;
                Pen p = new Pen(Color.Red);
                Graphics g = e.Graphics;
                int variance = 3;
                g.DrawRectangle(p, new Rectangle(EmailBox.Location.X - variance, EmailBox.Location.Y - variance, EmailBox.Width + variance, EmailBox.Height + variance));
            }
            else
                EmailBox.BackColor = Color.White;
            if (LoginBox.Text == "")
            {
                LoginBox.BorderStyle = BorderStyle.None;
                Pen p = new Pen(Color.Red);
                Graphics g = e.Graphics;
                int variance = 3;
                g.DrawRectangle(p, new Rectangle(LoginBox.Location.X - variance, LoginBox.Location.Y - variance, LoginBox.Width + variance, LoginBox.Height + variance));
            }
            else
                LoginBox.BackColor = Color.White;
        }

        private void AddAdminBtn_Click(object sender, EventArgs e)
        {
            if (SurnameBox.Text != "" && EmailBox.Text != "" && PasswordBox.Text != "" && LoginBox.Text != "")
            {
                DBConnection API = new DBConnection();
                string answer = API.GetJSON("/admin/validate/import/" + LoginBox.Text + "/" + EmailBox.Text);
                var user = new Admin(
                        SurnameBox.Text, EmailBox.Text,
                        LoginBox.Text, PasswordBox.Text
                    );
                string json = JsonSerializer.Serialize(user);
                if (answer.Contains("unique"))
                {
                    string response = API.PostJson("/admin/registr/admins", json);
                    if (response == "Админ добавлен")
                    {
                        Adminform.countAddedUsers++;
                        Close();
                    }
                    else
                        MessageBox.Show("Ошибка отправки");
                }
                else { 
                    if (answer.Contains("email"))
                    {
                        LoginBox.Text = "";
                        textBoxReColor_TextChanged(LoginBox, new EventArgs());
                    }
                    if (answer.Contains("login"))
                    {
                        EmailBox.Text = "";
                        textBoxReColor_TextChanged(EmailBox, new EventArgs());
                    }
                }
            }
        }
        private void IgnoreBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
