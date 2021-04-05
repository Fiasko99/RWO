using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml;

namespace RWO
{
    public partial class AdminForm : Form
    {
        public int countAddedUsers = 0;
        AuthForm Auth;
        public AdminForm(AuthForm auth)
        {
            InitializeComponent();
            Auth = auth;
        }


        private void OpenXml(string fileContent)
        {
            var countUsers = 0;
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(fileContent);
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                countUsers++;
                string surname = string.Empty;
                string email = string.Empty;
                string password = string.Empty;
                string login = string.Empty;
                // обходим все дочерние узлы элемента user
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "Фамилия")
                        surname = childnode.InnerText;
                    if (childnode.Name == "email")
                        email = childnode.InnerText;
                    if (childnode.Name == "Пароль")
                        password = childnode.InnerText;
                    if (childnode.Name == "login")
                        login = childnode.InnerText;
                }
                if (surname != string.Empty && email != string.Empty && password != string.Empty && login != string.Empty)
                {
                    var API = new DBConnection();
                    string answer = API.GetJSON("/admin/validate/import/" + email + "/" + login);
                    if (answer.Contains("unique"))
                    {
                        try
                        {
                            var user = new Admin(surname, email, login, password);
                            string json = JsonSerializer.Serialize(user);
                            countAddedUsers++;
                            API.PostJson("/admin/registr/admins", json);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        if (answer.Contains("email"))
                        {
                            login = "";
                        }
                        if (answer.Contains("login"))
                        {
                            email = "";
                        }

                        var fm = new fmCorrectUserData(surname, email, password, login, this);
                        fm.ShowDialog();
                    }
                }
            }
            MessageBox.Show("Из " + countUsers + " пользователей было добавлено " + countAddedUsers);
            countAddedUsers = 0;
        }

        private void OpenCsv(string fileContent)
        {
            var countUsers = 0;
            fileContent.Replace("\r", "");
            string[] rows = fileContent.Split('\n');
            if (rows.Length > 1)
            {
                string[] header = rows[0].Split('/');
                if (
                        header.Length >= 4 && header[0] == "Фамилия" && header[1] == "email" &&
                        header[2] == "Пароль" && header[3] == "login\r"
                    )
                {
                    for (int i = 1; i < rows.Length; i++)
                    {
                        countUsers++;
                        string[] valuesRow = rows[i].Split('/');
                        if (valuesRow.Length >= 4)
                        {
                            if (valuesRow[0] != "" && valuesRow[1] != "" && valuesRow[2] != "" && valuesRow[3] != "")
                            {
                                DBConnection API = new DBConnection();
                                string email = valuesRow[1];
                                string login = valuesRow[3];
                                string answer = API.GetJSON("/admin/validate/import/" + email + "/" + login);
                                if (answer.Contains("unique"))
                                {
                                    try
                                    {
                                        Admin user = new Admin(valuesRow[0], valuesRow[1], valuesRow[2], valuesRow[3]);
                                        string json = JsonSerializer.Serialize(user);
                                        countAddedUsers++;
                                        API.PostJson("/admin/registr/admins", json);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                                else
                                {
                                    if (answer.Contains("email"))
                                    {
                                        valuesRow[1] = "";
                                    }
                                    if (answer.Contains("login"))
                                    {
                                        valuesRow[3] = "";
                                    }
                                    fmCorrectUserData fm = new fmCorrectUserData(
                                            valuesRow[0], valuesRow[1], valuesRow[2],
                                            valuesRow[3], this
                                        );
                                    fm.ShowDialog();
                                }
                            }
                            else
                            {
                                var fm = new fmCorrectUserData(
                                        valuesRow[0], valuesRow[1], valuesRow[2],
                                        valuesRow[3], this
                                    );
                                fm.ShowDialog();
                            }
                        }
                    }
                    MessageBox.Show("Из " + countUsers + " пользователей было добавлено " + countAddedUsers);
                    countAddedUsers = 0;
                }
                else
                    MessageBox.Show("Файл не соответствует формату");
            }
            else
                MessageBox.Show("Файл не соответствует формату");
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Auth.Show();
        }

        private void ImportAdmin_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Stream file = ofd.OpenFile();
                string fileContent = null;
                var srcEncoding = Encoding.GetEncoding(1251);
                using (StreamReader reader = new StreamReader(file, encoding: srcEncoding))
                {
                    fileContent = reader.ReadToEnd();
                }
                if (fileContent != null && ofd.FileName.EndsWith("csv"))
                {
                    OpenCsv(fileContent);
                }
                else if (fileContent != null && ofd.FileName.EndsWith("xml"))
                {
                    OpenXml(fileContent);
                }
            }
        }
    }

    public class Admin
    {
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string login { get; set; }
        public Admin (
                string surname, string login,
                string password, string email
            )
        {
            this.surname = surname;
            this.login = login;
            this.password = password;
            this.email = email;
        }
    }
}
