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

   
    public partial class Content : Form
    {
        private readonly AuthForm auth;
        public int countbooksvalue;
        public List<Book> books;
        User NetworkUser;
        public Content(AuthForm Auth, User user)
        {
            InitializeComponent();
            auth = Auth;
            NetworkUser = user;
        }

        private void Content_Load(object sender, EventArgs e)
        {
            DBConnection API = new DBConnection();
            string result = API.GetJSON("/api/books");
            if (result != null && API.ExceptionMessage == null)
            {
                books = JsonSerializer.Deserialize<List<Book>>(result);
                var ImgList = new ImageList
                {
                    ImageSize = new Size(32, 42)
                };
                
                ContentListView.SmallImageList = ImgList;
                // default countbooks value
                CountBooks.Text = "10";
                countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(CountBooks.Text);
                CountBooksStep();
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
            if (NetworkUser is UserOffer)
            {
                WrittersList.Visible = true;
            } 
            else if (NetworkUser is UserReader)
            {
                ShowReadBook.Visible = true;
            }
            else if (NetworkUser is UserWritter)
            {
                OnLoadBook.Visible = true;
            }
        }
        public void CountBooksStep()
        {
            ContentListView.Items.Clear();
            for (
                    int i = int.Parse(NumberPage.Text) * int.Parse(CountBooks.Text) - int.Parse(CountBooks.Text);
                    i < countbooksvalue && i < books.Count;
                    i++
                )
            {
                ListViewItem lv = new ListViewItem(
                    new string[] {
                        "",
                        books[i].name_composition +
                        " (" + books[i].age_limit_value + ")",
                        books[i].writter_name,
                        books[i].genre
                    });
                lv.Tag = books[i];
                lv.ImageIndex = 0;

                ContentListView.Items.Add(
                        lv
                    );
            }
            TableFormatForListView();
        }
        public void TableFormatForListView()
        {
            // ContentListView - контейнер ListView
            int count = ContentListView.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0 )
                {
                    ContentListView.Items[i].BackColor = Color.Gray;
                }
                else
                {
                    ContentListView.Items[i].BackColor = Color.White;
                }
            }
        }

        private void Content_FormClosing(object sender, FormClosingEventArgs e)
        {
            auth.Show();
        }

        private void ExitProfile_Click(object sender, EventArgs e)
        {
            auth.Show();
            auth.ResetForm();
            Close();
        }

        private void ContentListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
            e.DrawText();
        }

        private void ContentListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void CountBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(CountBooks.Text);
            CountBooksStep();
        }

        private void NextPage_Click(object sender, EventArgs e)
        {
            NumberPage.Text = (int.Parse(NumberPage.Text) + 1).ToString();
            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(CountBooks.Text);
            CountBooksStep();
        }

        private void PreviousPage_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumberPage.Text) - 1 > 0)
            {
                NumberPage.Text = (int.Parse(NumberPage.Text) - 1).ToString();
                countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(CountBooks.Text);
                CountBooksStep();
            }
        }

        private void BtnPageOne_Click(object sender, EventArgs e)
        {
            NumberPage.Text = "1";
            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(CountBooks.Text);
            CountBooksStep();
        }

        private void BtnPageLast_Click(object sender, EventArgs e)
        {
            int lastpage;
            if(books.Count % int.Parse(CountBooks.Text) == 0)
            {
                lastpage = books.Count / int.Parse(CountBooks.Text);
            } 
            else
            {
                lastpage = books.Count / int.Parse(CountBooks.Text) + 1;
            }
            NumberPage.Text = lastpage.ToString();
            countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(CountBooks.Text);
            CountBooksStep();
        }

        private void WrittersList_Click(object sender, EventArgs e)
        {
            WrittersListForm WritersListF = new WrittersListForm(NetworkUser);
            WritersListF.Show();
        }

        private void OnLoadBook_Click(object sender, EventArgs e)
        {
            // TODO: Load Book
        }

        private void ShowReadBook_Click(object sender, EventArgs e)
        {
            // TODO: Read book show
        }
    }
    public class Book
    {
        public long id { get; set; }
        public string name_composition { get; set; }
        public string genre { get; set; }
        public string writter_name { get; set; }
        public string age_limit_value { get; set; }

        public Book(
                long id, 
                string name_composition, 
                string genre,
                string writter_name,
                string age_limit_value
            )
        {
            this.id = id;
            this.name_composition = name_composition;
            this.genre = genre;
            this.writter_name = writter_name;
            this.age_limit_value = age_limit_value;
        }
    }
}
