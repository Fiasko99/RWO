using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RWO
{
    public partial class WrittersListForm : Form
    {
        public int countbooksvalue;
        public List<Writter> writters;
        public User UserActive;
        string UserInfo = null;
        public WrittersListForm(User NetworkUser)
        {
            InitializeComponent();
            UserActive = NetworkUser;
        }

        private void WrittersListForm_Load(object sender, EventArgs e)
        {
            if (UserActive is UserOffer offer)
            {
                UserInfo = UserActive.id + "/" + offer.role;
            }
            else if (UserActive is UserReader reader)
            {
                UserInfo = UserActive.id + "/" + reader.role;
            }
            else if (UserActive is UserWritter writter)
            {
                UserInfo = UserActive.id + "/" + writter.role;
            }
            DBConnection API = new DBConnection();
            new Thread(() =>
            {
                string writterslist = API.GetJSON("/api/writters/list/" + UserInfo);
                if (writterslist != null && API.ExceptionMessage == null)
                {
                    writters = JsonSerializer.Deserialize<List<Writter>>(writterslist);
                    Invoke(
                        (MethodInvoker)delegate
                        {
                            WrittersOnList.Text = "30";
                            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(WrittersOnList.Text);
                            FillWrittersInListView();
                        }
                    );
                }
                else
                {
                    MessageBox.Show(
                            API.ExceptionMessage,
                            "Ошибка получения книг",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                }
            }).Start();
        }



        public void FillWrittersInListView()
        {
            try
            {
                WrittersListView.Items.Clear();
                for (
                    int i = int.Parse(NumberPage.Text) * 
                    int.Parse(WrittersOnList.Text) - 
                    int.Parse(WrittersOnList.Text);
                    i < countbooksvalue && i < writters.Count;
                    i++
                )
                {
                    string name = writters[i].name + " " + writters[i].surname;
                    string workexp = writters[i].work_expirience != null ? writters[i].work_expirience : "Не указано";
                    string lastbook = writters[i].last_book != null ? writters[i].last_book : "Не найдено";
                    ListViewItem lvi = new ListViewItem(
                            new string[]
                            {
                                name,
                                workexp,
                                lastbook
                            }
                        );
                    lvi.Tag = writters[i];
                    WrittersListView.Items.Add(lvi);
                    TableFormatForListView();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                        "Приложение закрыто до завершения загрузки пользователей",
                        "Ошибка закрытия",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
            }
        }
        public void TableFormatForListView()
        {
            // ContentListView - контейнер ListView
            int count = WrittersListView.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    WrittersListView.Items[i].BackColor = Color.Gray;
                }
                else
                {
                    WrittersListView.Items[i].BackColor = Color.White;
                }
            }
        }

        public void WrittersListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
            e.DrawText();
        }

        public void WrittersListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        public void PreviousPage_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumberPage.Text) - 1 > 0)
            {
                NumberPage.Text = (int.Parse(NumberPage.Text) - 1).ToString();
                countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(WrittersOnList.Text);
                FillWrittersInListView();
            }
        }

        public void NextPage_Click(object sender, EventArgs e)
        {
            NumberPage.Text = (int.Parse(NumberPage.Text) + 1).ToString();
            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(WrittersOnList.Text);
            FillWrittersInListView();
        }

        public void FirstPage_Click(object sender, EventArgs e)
        {
            NumberPage.Text = "1";
            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(WrittersOnList.Text);
            FillWrittersInListView();
        }

        public void LastPage_Click(object sender, EventArgs e)
        {
            int lastpage;
            if (writters.Count % int.Parse(WrittersOnList.Text) == 0)
            {
                lastpage = writters.Count / int.Parse(WrittersOnList.Text);
            }
            else
            {
                lastpage = writters.Count / int.Parse(WrittersOnList.Text) + 1;
            }
            NumberPage.Text = lastpage.ToString();
            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(WrittersOnList.Text);
            FillWrittersInListView();
        }

        private void WrittersOnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(WrittersOnList.Text);
            FillWrittersInListView();
        }

        private void WrittersListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Writter SelectItemTag = (Writter)WrittersListView.SelectedItems[0].Tag;
            new Thread(() => {
                WritterProfile WP = new WritterProfile(SelectItemTag, UserActive, this);
                WP.ShowDialog();
            }).Start();
            Hide();
        }

        private void FormatReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetReport.Enabled = true;
        }

        private void GetReport_Click(object sender, EventArgs e)
        {
            DBConnection API = new DBConnection();
            string answer = API.GetJSON($"/api/report/create/writters/{UserActive.id}/Инвестор");
            string IsJson = answer.Contains('{') && answer.Contains('}') ? answer : "";
            if (IsJson != "")
            {
                List<FullWritterReports> WrittersForReports = JsonSerializer.Deserialize<List<FullWritterReports>>(answer);
                CreateReport(WrittersForReports);
            }
        }

        private void CreateReport(List<FullWritterReports> writtersForReports)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                OverwritePrompt = true,
                Filter = FormatReport.Text,
            };
            if (FormatReport.SelectedIndex == 3 && sfd.ShowDialog() == DialogResult.OK)
            {
                XDocument xdoc = new XDocument();
                var xTree = new XElement("root",
                            new XElement("Отчёт")
                        );
                foreach (FullWritterReports writter in writtersForReports)
                {
                    var writterxml = new XElement("Писатель",
                                new XElement("Наименование", writter.name + " " + writter.surname),
                                new XElement("ДатаРегистрации", writter.createdAt.ToString()),
                                new XElement("email", writter.email),
                                new XElement("login", writter.login),
                                new XElement("Подтверждён", writter.confirm)
                            );
                    xTree.Add(writterxml);
                }
                xdoc.Add(xTree);
                xdoc.Save(sfd.InitialDirectory + sfd.FileName);
            } 
            else
            {

            }
        }
    }
    public class Writter
    {
        public long id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string work_expirience { get; set; }
        public string last_book { get; set; }
        public Writter(
            long id,
            string name, string surname,
            string work_expirience, string last_book
        )
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.work_expirience = work_expirience;
            this.last_book = last_book;
        }
    }

    public class FullWritterReports 
    {
        public long id { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public DateTime createdAt { get; set; }
        public bool confirm { get; set; }

        public FullWritterReports(
            long id, string login, string name, 
            string surname, string email,
            DateTime createdAt, bool confirm
        )
        {
            this.id = id;
            this.login = login;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.createdAt = createdAt;
            this.confirm = confirm;
        }
    }
}
