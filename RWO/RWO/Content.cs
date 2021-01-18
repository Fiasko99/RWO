using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace RWO
{
    public partial class Content : Form
    {
        private readonly AuthForm auth;
        public int countbooksvalue;
        public List<Book> books;
        public List<Book> AllBooks;
        User NetworkUser;
        public List<Book> FilterWritters = new List<Book>();
        public List<Book> FilterGenres = new List<Book>();
        public List<Book> FilterLanguages = new List<Book>();
        public List<Book> FilterAgeLimits = new List<Book>();
        public string UserInfo = null;
        public Content(AuthForm Auth, User user)
        {
            InitializeComponent();
            auth = Auth;
            NetworkUser = user;
        }

        public void Content_Load(object sender, EventArgs e)
        {
            
            if (NetworkUser is UserOffer offer)
            {
                WrittersList.Visible = true;
                UserInfo = NetworkUser.id + "/" + offer.role;
            }
            else if (NetworkUser is UserReader reader)
            {
                ShowReadBook.Visible = true;
                UserInfo = NetworkUser.id + "/" + reader.role;
            }
            else if (NetworkUser is UserWritter writter)
            {
                OnLoadBook.Visible = true;
                UserInfo = NetworkUser.id + "/" + writter.role;
            }
            DBConnection API = new DBConnection();
            string JsonBook = API.GetJSON("/api/books/" + UserInfo);
            if (JsonBook != null && API.ExceptionMessage == null)
            {
                AllBooks = JsonSerializer.Deserialize<List<Book>>(JsonBook);
                books = new List<Book>(AllBooks);
                var ImgList = new ImageList
                {
                    ImageSize = new Size(32, 42)
                };
                
                ContentListView.SmallImageList = ImgList;
                // default countbooks value
                CountBooks.Text = "10";
                countbooksvalue = int.Parse(NumberPage.Text) * int.Parse(CountBooks.Text);
                CountBooksStep();
                FillFilters();
            } 
            else
            {
                MessageBox.Show(
                        API.ExceptionMessage != null ? API.ExceptionMessage : JsonBook,
                        "Ошибка получения книг",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
            }
            ToolStripMenuItem DownloadItem = new ToolStripMenuItem("Скачать");
            ToolStripMenuItem DeleteItem = new ToolStripMenuItem("Удалить из списка");
            // добавляем элементы в меню
            SelectBookMenu.Items.AddRange(new[] { DownloadItem, DeleteItem });
            // ассоциируем контекстное меню с текстовым полем
            ContentListView.ContextMenuStrip = SelectBookMenu;
            // устанавливаем обработчики событий для меню
            DownloadItem.Click += DownLoadItem_Click;
            DeleteItem.Click += DeleteItem_Click;
        }

        public void DeleteItem_Click(object sender, EventArgs e)
        {
            var SelectItems = ContentListView.SelectedItems;
            foreach (ListViewItem SelectItem in SelectItems)
            {
                int index = books.FindIndex(book => book == (Book)SelectItem.Tag);
                if (index != -1)
                {
                    books.RemoveAt(index);
                    AllBooks.RemoveAt(index);
                }
            }
            CountBooksStep();
            FillFilters();
        } 

        private void DownLoadItem_Click(object sender, EventArgs e)
        {
            var SelectItems = ContentListView.SelectedItems;
            DBConnection API = new DBConnection();
            string FolderPath = null;
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK)
                {
                    FolderPath = fbd.SelectedPath;
                }
            }
            string path = null;
            foreach (ListViewItem SelectItem in SelectItems)
            {
                Book book = books.Find(item => item == (Book)SelectItem.Tag);
                if (book != null)
                {
                    string textbook = API.GetJSON("/api/text/book/" + book.id + "/" + UserInfo);
                    book.text_composition = textbook.Remove(0, 2);
                    if (FolderPath != null)
                    {
                        path = FolderPath + "/Книга " + book.name_composition + ".txt";
                        File.WriteAllText(path,
                                "Название произведения: " + book.name_composition + "\r\n\n" +
                                "Возрастное ограничение: " + book.age_limit_value + "\r\n\n" +
                                "Автор произведения: " + book.writter_name + "\r\n\n" +
                                book.text_composition
                            );
                        // TODO: save on pdf & doxx format
                    }
                }
            }
            if (path != null)
            {
                MessageBox.Show(
                        "Сохранено",
                        "Успешное сохранение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
            } 
            else
            {
                MessageBox.Show(
                        "Вы не выбрали путь",
                        "Не сохранено",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
            }

        }

        public void FillFilters()
        {
            SelectValueForFilters(ComboWritters, "Все писатели");
            SelectValueForFilters(ComboGenres, "Все жанры");
            SelectValueForFilters(ComboLang, "Все языки");
            SelectValueForFilters(ComboAgeLimits, "Все возраста");

            for (int i = 0; i < books.Count; i++)
            {
                if (ComboWritters.FindString(books[i].writter_name) == -1)
                {
                    ComboWritters.Items.Add(books[i].writter_name);
                }
                if (ComboGenres.FindString(books[i].genre) == -1)
                {
                    ComboGenres.Items.Add(books[i].genre);
                }
                if (ComboLang.FindString(books[i].language) == -1)
                {
                    ComboLang.Items.Add(books[i].language);
                }
                if (ComboAgeLimits.FindString(books[i].age_limit_value) == -1)
                {
                    ComboAgeLimits.Items.Add(books[i].age_limit_value);
                }
            }
        }

        private void SelectValueForFilters(ComboBox combobox, string defaultstr )
        {
            if (combobox.Text.Contains("Все"))
            {
                combobox.Items.Clear();
            } 
            else if (combobox.SelectedIndex != -1)
            {
                string tmp = combobox.Text;
                combobox.Items.Clear();
                combobox.Items.Add(tmp);
                combobox.Text = tmp;
            }
            if (combobox.FindString(defaultstr) == -1)
            {
                combobox.Items.Add(defaultstr);
                if (combobox.SelectedIndex == -1)
                {
                    combobox.Text = defaultstr;
                }
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
                        books[i].genre,
                        books[i].language
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

        private void FilterProcess()
        {
            books.Clear();
            ResetListFilters();
            FilterGenres = FilterOfComboName(ComboGenres, "genre");
            FilterWritters = FilterOfComboName(ComboWritters, "writter");
            FilterAgeLimits = FilterOfComboName(ComboAgeLimits, "ageLimit");
            FilterLanguages = FilterOfComboName(ComboLang, "language");

            books = new List<Book>(FilterGenres);
            books = new List<Book>(books.Intersect(FilterWritters));
            books = new List<Book>(books.Intersect(FilterAgeLimits));
            books = new List<Book>(books.Intersect(FilterLanguages));
            CountBooksStep();
            FillFilters();
        }

        private void ResetListFilters()
        {
            FilterGenres.Clear();
            FilterWritters.Clear();
            FilterAgeLimits.Clear();
            FilterLanguages.Clear();
        }

        private List<Book> FilterOfComboName(ComboBox comboBox, string typeFilter)
        {
            if (comboBox.Text.Contains("Все"))
            {
                return new List<Book>(AllBooks);
            }
            else 
            {
                List<Book> FilterList = new List<Book>();
                foreach (Book book in AllBooks)
                {
                    if (comboBox.Text == book.genre && typeFilter == "genre")
                    {
                        FilterList.Add(book);
                    }
                    else if (comboBox.Text == book.age_limit_value && typeFilter == "ageLimit")
                    {
                        FilterList.Add(book);
                    }
                    else if (comboBox.Text == book.writter_name && typeFilter == "writter")
                    {
                        FilterList.Add(book);
                    }
                    else if (comboBox.Text == book.language && typeFilter == "language")
                    {
                        FilterList.Add(book);
                    }
                }
                return FilterList;
            }
        }

        private void ComboGenres_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterProcess();
        }
        private void ComboWritters_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterProcess();
        }
        private void ComboAgeLimits_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterProcess();
        }
        private void ComboLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterProcess();
        }
    }
    public class Book
    {
        public long id { get; set; }
        public string name_composition { get; set; }
        public string genre { get; set; }
        public string writter_name { get; set; }
        public string age_limit_value { get; set; }
        public string language { get; set; }
        public string text_composition { get; set; }

        public Book(
                long id, 
                string name_composition, 
                string genre,
                string writter_name,
                string age_limit_value,
                string language
            )
        {
            this.id = id;
            this.name_composition = name_composition;
            this.genre = genre;
            this.writter_name = writter_name;
            this.age_limit_value = age_limit_value;
            this.language = language;
        }
    }
}
