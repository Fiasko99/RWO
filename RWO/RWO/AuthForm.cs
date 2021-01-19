using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Linq;

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
            DBConnection API = new DBConnection();
            if (ValidateData())
            {
                ValidateUser UserLogining = new ValidateUser(
                        LoginBox.Text, PassBox.Text, RoleCombo.Text
                    );
                string JsonUserLogining = JsonSerializer.Serialize(UserLogining);
                new Thread(
                        () =>
                        {
                            string res = API.PostJson(
                                    "/api/login",
                                    JsonUserLogining
                                );
                            if (res == "Почта не подтверждена")
                            {
                                MsgBoxError(
                                        "Подтвердите почту",
                                        res
                                    );
                            } 
                            else if (res == "Доступ запрещён")
                            {
                                MsgBoxError(
                                        res,
                                        "Профиль заблокирован"
                                    );
                            } 
                            else if (API.ExceptionMessage == null && res.Contains('{') && res.Contains('}'))
                            {
                                CheckResult(res, UserLogining.Role, API);
                            } 
                            else
                            {
                                MsgBoxError(res, "Неизвестная ошибка");
                            }
                        }
                    ).Start();
            } 
            else if (LoginBox.TextLength > 0 && PassBox.TextLength > 0)
            {
                ValidateUser UserLogining = new ValidateUser(
                        LoginBox.Text, PassBox.Text, RoleCombo.Text
                    );
                string JsonUserLogining = JsonSerializer.Serialize(UserLogining);
                string IsAdmin = API.PostJson("/api/admin/" + LoginBox.Text, JsonUserLogining);
                if (IsAdmin == "true")
                {
                    AdminForm AP = new AdminForm();
                    AP.Show();
                    Hide();
                }

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

        private void CheckResult(string res, string role, DBConnection API)
        {
            User user = null;
            if (role == "Инвестор")
            {
                user = JsonSerializer.Deserialize<UserOffer>(res);
            } 
            else if (role == "Читатель") {
                user = JsonSerializer.Deserialize<UserReader>(res);
            }
            else if (role == "Писатель")
            {
                user = JsonSerializer.Deserialize<UserWritter>(res);
            }
            if (user != null)
            {
                StepAuth(user);
            }
        }

        private void StepAuth(User res)
        {
            Invoke(
                (MethodInvoker)delegate
                {
                    Content ContentForm = new Content(this, res);
                    ContentForm.Show();
                    Hide();
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
            if (PassBox.TextLength < 6)
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
            LoginBox.Text = "admin";
            PassBox.Text = "admin";
        }
    }
}
