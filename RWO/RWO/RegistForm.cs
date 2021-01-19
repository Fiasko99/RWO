using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWO
{
    public partial class RegistForm : Form
    {
        public RegistForm()
        {
            InitializeComponent();
        }

        private void GeneratePass_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            string password = "";
            string capitalLet = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string smallLet = "qwertyuiopasdfghjklzxcvbnm";
            string num = "0123456789";
            string specialSymb = "@!№#%^,.:)(?][";
            password +=
                capitalLet[rnd.Next(capitalLet.Length)] +
                smallLet[rnd.Next(smallLet.Length)] +
                num[rnd.Next(num.Length)] +
                specialSymb[rnd.Next(specialSymb.Length)];
            string allLet = capitalLet + smallLet + num + specialSymb;
            for (int i = 4; i < rnd.Next(6, 20); i++)
            {
                password += allLet[rnd.Next(allLet.Length)];
            }
            var passArray = password.ToCharArray();
            for (int i = passArray.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = passArray[j];
                passArray[j] = passArray[i];
                passArray[i] = temp;
            }
            PassBox.Text = new string(passArray);
        }

        private void BtnToAuthForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RegistrationBtn_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                DBConnection API = new DBConnection();
                ValidateUser UserRegistrationing = new ValidateUser(
                        LoginBox.Text, PassBox.Text, RoleCombo.Text,
                        EmailBox.Text, NameBox.Text, SurnameBox.Text,
                        SecretCode.Text
                    );
                string JsonUserLogining = JsonSerializer.Serialize(UserRegistrationing);
                new Thread(
                        () =>
                            {
                                var res = API.PostJson(
                                        "/api/registration",
                                        JsonUserLogining
                                    );
                                if (res.Contains('{') && res.Contains('}'))
                                    MsgBoxSuccess("" +
                                        "Подтвердите регистрацию на почте в течение пяти часов " +
                                        "(не закрывайте приложение, чтобы оно автоматически открылось при подтверждении)" +
                                        "");
                                else if (res == "Ошибка отправки сообщения на почту")
                                    MsgBoxError(res, "Введите корректную почту");
                                else if (res == "База данных отключена")
                                    MsgBoxError(res, "Сервис недоступен");
                            }
                    ).Start();

            }
        }

        public bool ValidateData()
        {
            if (LoginBox.TextLength < 6)
            {
                MsgBoxWrong(
                    "Wrong input login (len symbol > 5). Please, fix it!\n" +
                    "Неправильно ввод данных (длина логина должна быть больше 5 символов)"
                    );
                return false;
            }
            if (PassBox.TextLength < 6)
            {
                MsgBoxWrong(
                        "Wrong input password (len symbol > 7). Please, fix it!\n" +
                        "Неправильно ввод данных (длина пароля должна быть больше 7 символов)"
                    );
                return false;
            }
            if (
                    !EmailBox.Text.Contains('@') || 
                    !EmailBox.Text.Contains('.') || 
                    EmailBox.TextLength < 5
                )
            {
                MsgBoxWrong(
                        "Please, input email.\n" +
                        "Пожалуйста, введите корректную электронную почту."
                    );
                return false;
            }
            if (NameBox.Text.Length == 0)
            {
                MsgBoxWrong(
                        "Please, input name.\n" +
                        "Пожалуйста, введите имя."
                    );
                return false;
            }
            if (SurnameBox.Text.Length == 0)
            {
                MsgBoxWrong(
                        "Please, input name.\n" +
                        "Пожалуйста, введите фамилию."
                    );
                return false;
            }
            if (SecretCode.Text.Length < 8)
            {
                MsgBoxWrong(
                        "Please, input secret code.\n" +
                        "Пожалуйста, введите секретный ключ (длина > 8)."
                    );
                return false;
            }
            if (RoleCombo.Text.Length == 0)
            {
                MsgBoxWrong(
                        "Please, choose role.\n" +
                        "Пожалуйста, выберите роль."
                    );
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
        private void MsgBoxInfo(string msg)
        {
            MessageBox.Show(
                        msg,
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
        }
        private void MsgBoxSuccess(string msg)
        {
            MessageBox.Show(
                        msg,
                        "Регистрация выполнена",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
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

        private void AddInfo_Click(object sender, EventArgs e)
        {
            // TODO: add info about user 
        }
    }
}
