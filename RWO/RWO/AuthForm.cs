using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace RWO
{
    public partial class AuthForm : Form
    {
        RegistForm RegForm;
        HttpListener listener = new HttpListener();
        Task listenposttask;
        List<DateTime> unSuccessLogins = new List<DateTime>();
        string text;
        int incorrectCapcha = 0;
        int capchaUpdates = 0;
        public AuthForm()
        {
            InitializeComponent();
            Captcha.Image = CreateImage(Captcha.Width, Captcha.Height);
        }

        private Image CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(10);
            int Ypos = rnd.Next(10);

            //Добавим различные цвета ддя текста
            Brush[] colors =
            {
                Brushes.Black,
                Brushes.DarkRed,
                Brushes.DarkBlue,
                Brushes.DarkGreen,
                Brushes.DarkOrange
            };

            //Добавим различные цвета линий
            Pen[] colorpens = {
                Pens.White,
                Pens.Tomato,
                Pens.Sienna,
                Pens.Pink,
                Pens.AliceBlue};

            Color[] BgColor =
            {
                Color.Aqua,
                Color.Aquamarine,
                Color.Azure,
                Color.Blue,
                Color.Bisque,
                Color.BlueViolet,
                Color.LightBlue,
                Color.LightGreen,
                Color.LightPink,
                Color.LightGray,
                Color.LightSalmon,
                Color.LightSeaGreen,
                Color.LightSkyBlue,
                Color.LightSlateGray,
                Color.LightYellow
            };

            //Делаем случайный стиль текста
            FontStyle[] fontstyle =
            {
                FontStyle.Bold,
                FontStyle.Italic,
                FontStyle.Regular,
                FontStyle.Strikeout,
                FontStyle.Underline
            };

            //Добавим различные углы поворота текста
            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((Image)result);

            //Пусть фон картинки будет серым
            g.Clear(BgColor[rnd.Next(BgColor.Length)]);

            //Делаем случайный угол поворота текста
            g.RotateTransform(rnd.Next(rotate.Length));

            //Генерируем текст
            text = "";
            text += rnd.Next(9);
            string ALF = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            for (int i = 0; i < rnd.Next(5, 10); ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
            new Font("Arial", 14, fontstyle[rnd.Next(fontstyle.Length)]),
            colors[rnd.Next(colors.Length)],
            new PointF(Xpos, Ypos));

            //Добавим немного помех
            //Линии из углов
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new Point(0, rnd.Next(Height - 1)),
            new Point(Width - 1, rnd.Next(Height - 1)));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new Point(0, rnd.Next(Height - 1)),
            new Point(Width - 1, rnd.Next(Height - 1)));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new Point(0, rnd.Next(Height - 1)),
            new Point(Width - 1, rnd.Next(Height - 1)));

            //Белый шум
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }

        private void MakeCapcha()
        {
            if (LoginBox.Text != "" && PassBox.Text != "" && RoleCombo.SelectedIndex != -1)
            {
                LoginBox.Enabled = false;
                PassBox.Enabled = false;
                RoleCombo.Enabled = false;
                Captcha.Visible = true;
                CaptchaBox.Visible = true;
                UpdCaptcha.Visible = true;
                CheckCaptcha.Visible = true;
                LoginBtn.Enabled = false;
                Captcha.Image = CreateImage(Captcha.Width, Captcha.Height);
            }
            else
                MessageBox.Show("Имя пользователя или пароль не могут быть пустыми",
                        "Проверьте заполнение полей", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (ValidateData() && incorrectCapcha < 7 && capchaUpdates < 15)
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
                            unSuccessLogins.Add(DateTime.Now);
                            int count = 0;
                            foreach (var dt in unSuccessLogins)
                            {
                                if ((DateTime.Now - dt).TotalHours < 1)
                                    count++;
                            }
                            if (count >= 5)
                                Invoke(
                                        (MethodInvoker)delegate
                                        {
                                            MakeCapcha();
                                        }
                                    );
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
                    AdminForm AP = new AdminForm(this);
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

        private void AuthForm_Load(object sender, EventArgs e)
        {
            LoginBox.Text = "admin";
            PassBox.Text = "admin";
        }

        private void UpdCaptcha_Click(object sender, EventArgs e)
        {
            if (capchaUpdates >= 15)
            {
                var API = new DBConnection();
                string answer = API.GetJSON("/api/incorrect/captcha/" + LoginBox.Text + "/" + RoleCombo.Text);
                if (answer.Length > 0)
                {
                    MsgBoxError("На Ваш email отправлено письмо для смены пароля", "Учётная запись заблокирована");
                }
                Close();
            }
            else
            {
                Captcha.Image = CreateImage(Captcha.Width, Captcha.Height);
                capchaUpdates++;
            }
        }

        private void CheckCaptcha_Click(object sender, EventArgs e)
        {
            if (CaptchaBox.Text == text)
            {
                LoginBox.Enabled = true;
                PassBox.Enabled = true;
                RoleCombo.Enabled = true;
                Captcha.Visible = false;
                CaptchaBox.Visible = false;
                UpdCaptcha.Visible = false;
                LoginBtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Капча введена неверно", "Капча введена неверно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                incorrectCapcha++;
                if (incorrectCapcha >= 15)
                {
                    DBConnection API = new DBConnection();
                    API.GetJSON("/api/incorrect/captcha/" + LoginBox.Text + "/" + RoleCombo.Text);
                    MessageBox.Show("На Ваш email отправлено письмо для смены пароля", "Учётная запись заблокирована", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                }
            }
        }
    }
}
