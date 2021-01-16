using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace RWO
{
    public partial class AuthForm : Form
    {
        RegistForm RegForm;
        HttpListener listener = new HttpListener();
        Task listenposttask;
        public AuthForm()
        {
            InitializeComponent();
        }

        private void BtnToRegistForm_Click(object sender, EventArgs e)
        {
            listenposttask = new Task(ListenPort);
            listenposttask.Start();
            Hide();
            RegForm = new RegistForm();
            RegForm.ShowDialog();
            Show();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DBConnection API = new DBConnection();
                ValidateUser UserLogining = new ValidateUser(
                        LoginBox.Text, PassBox.Text, RoleCombo.Text
                    );
                string JsonUserLogining = JsonSerializer.Serialize(UserLogining);
                new Thread(
                        () =>
                        {
                            var res = API.PostUserJson(
                                    "/api/login",
                                    JsonUserLogining,
                                    UserLogining.Role
                                );
                            CheckResult(res, API);
                        }
                    ).Start();
            }
        }
        public void ListenPort()
        {
            // установка адресов прослушки
            listener.Prefixes.Add("http://localhost:9876/sharpstart/");
            listener.Start();
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                Invoke(new Action(() =>
                {
                    Text = "Учётная запись подтверждена";
                    TopMost = true;
                    TopMost = false;
                    if (RegForm != null)
                        RegForm.Close();
                }));
                break;
            }
            listener.Prefixes.Clear();
            listener.Stop();
        }

        public void ResetForm()
        {
            LoginBox.Text = "";
            PassBox.Text = "";
            RoleCombo.SelectedIndex = 0;
        }

        private void CheckResult(User res, DBConnection API)
        {
            if (res != null && API.ExceptionMessage == null)
            {
                StepAuth(res);
            } 
            else if (API.ExceptionMessage != null)
            {
                MsgBoxError(API.ExceptionMessage, "Внутренняя ошибка");
            }
            else
            {
                MsgBoxError(
                    "Проверьте правильность введённых данных",
                    "Пользователь не найден"
                   );
            }
        }

        private void StepAuth(User res)
        {
            Invoke(
                (MethodInvoker)delegate
                {
                    Hide();
                    Content ContentForm = new Content(this, res);
                    ContentForm.Show();
                }
            );
        }

        private bool ValidateData()
        {
            if (LoginBox.TextLength < 6)
            {
                MsgBoxWrong("Wrong input login (len symbol > 5). Please, fix it!\nНеправильно ввод данных (длина логина должна быть больше 5 символов)");
                return false;
            }
            if (PassBox.TextLength < 8)
            {
                MsgBoxWrong("Wrong input password (len symbol > 7). Please, fix it!\nНеправильно ввод данных (длина пароля должна быть больше 7 символов)");
                return false;
            }
            if (RoleCombo.Text.Length == 0)
            {
                MsgBoxWrong("Please, choose role.\nПожалуйста, выберите роль.");
                return false;
            }
            return true;
        }

        private void MsgBoxWrong(string msg)
        {
            MessageBox.Show(
                        msg,
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
        }

        private void MsgBoxError(string msg, string caption)
        {
            MessageBox.Show(
                        msg,
                        caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
        }

        private void LoginBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PassBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AuthForm_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {
            LoginBox.Text = "Ivanov";
            PassBox.Text = "12345678";
            RoleCombo.Text = "Инвестор";
        }
    }
}
