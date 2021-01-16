
namespace RWO
{
    partial class Content
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnToProfileForm = new System.Windows.Forms.Button();
            this.ExitProfile = new System.Windows.Forms.Button();
            this.ContentListView = new System.Windows.Forms.ListView();
            this.Images = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameBook = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Genre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CountBooks = new System.Windows.Forms.ComboBox();
            this.BtnPageOne = new System.Windows.Forms.Button();
            this.BtnPageLast = new System.Windows.Forms.Button();
            this.PreviousPage = new System.Windows.Forms.Button();
            this.NextPage = new System.Windows.Forms.Button();
            this.NumberPage = new System.Windows.Forms.Label();
            this.FilterListView = new System.Windows.Forms.ComboBox();
            this.CaptionCountBookLabel = new System.Windows.Forms.Label();
            this.WrittersList = new System.Windows.Forms.Button();
            this.OnLoadBook = new System.Windows.Forms.Button();
            this.ShowReadBook = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnToProfileForm
            // 
            this.BtnToProfileForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnToProfileForm.Location = new System.Drawing.Point(713, 13);
            this.BtnToProfileForm.Name = "BtnToProfileForm";
            this.BtnToProfileForm.Size = new System.Drawing.Size(75, 23);
            this.BtnToProfileForm.TabIndex = 0;
            this.BtnToProfileForm.Text = "Профиль";
            this.BtnToProfileForm.UseVisualStyleBackColor = true;
            // 
            // ExitProfile
            // 
            this.ExitProfile.Location = new System.Drawing.Point(13, 13);
            this.ExitProfile.Name = "ExitProfile";
            this.ExitProfile.Size = new System.Drawing.Size(112, 23);
            this.ExitProfile.TabIndex = 1;
            this.ExitProfile.Text = "Выйти из профиля";
            this.ExitProfile.UseVisualStyleBackColor = true;
            this.ExitProfile.Click += new System.EventHandler(this.ExitProfile_Click);
            // 
            // ContentListView
            // 
            this.ContentListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Images,
            this.NameBook,
            this.Author,
            this.Genre});
            this.ContentListView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContentListView.FullRowSelect = true;
            this.ContentListView.GridLines = true;
            this.ContentListView.HideSelection = false;
            this.ContentListView.Location = new System.Drawing.Point(11, 79);
            this.ContentListView.Name = "ContentListView";
            this.ContentListView.OwnerDraw = true;
            this.ContentListView.Size = new System.Drawing.Size(779, 382);
            this.ContentListView.TabIndex = 2;
            this.ContentListView.UseCompatibleStateImageBehavior = false;
            this.ContentListView.View = System.Windows.Forms.View.Details;
            this.ContentListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ContentListView_DrawColumnHeader);
            this.ContentListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.ContentListView_DrawItem);
            // 
            // Images
            // 
            this.Images.Text = "Обложка";
            this.Images.Width = 76;
            // 
            // NameBook
            // 
            this.NameBook.Text = "Название книги";
            this.NameBook.Width = 203;
            // 
            // Author
            // 
            this.Author.Text = "Автор";
            this.Author.Width = 209;
            // 
            // Genre
            // 
            this.Genre.Text = "Жанры";
            this.Genre.Width = 284;
            // 
            // CountBooks
            // 
            this.CountBooks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CountBooks.FormattingEnabled = true;
            this.CountBooks.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "50",
            "100"});
            this.CountBooks.Location = new System.Drawing.Point(156, 42);
            this.CountBooks.Name = "CountBooks";
            this.CountBooks.Size = new System.Drawing.Size(50, 21);
            this.CountBooks.TabIndex = 3;
            this.CountBooks.SelectedIndexChanged += new System.EventHandler(this.CountBooks_SelectedIndexChanged);
            // 
            // BtnPageOne
            // 
            this.BtnPageOne.Location = new System.Drawing.Point(212, 13);
            this.BtnPageOne.Name = "BtnPageOne";
            this.BtnPageOne.Size = new System.Drawing.Size(118, 23);
            this.BtnPageOne.TabIndex = 4;
            this.BtnPageOne.Text = "На первую старицу";
            this.BtnPageOne.UseVisualStyleBackColor = true;
            this.BtnPageOne.Click += new System.EventHandler(this.BtnPageOne_Click);
            // 
            // BtnPageLast
            // 
            this.BtnPageLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPageLast.Location = new System.Drawing.Point(475, 13);
            this.BtnPageLast.Name = "BtnPageLast";
            this.BtnPageLast.Size = new System.Drawing.Size(151, 23);
            this.BtnPageLast.TabIndex = 5;
            this.BtnPageLast.Text = "На последнюю страницу";
            this.BtnPageLast.UseVisualStyleBackColor = true;
            this.BtnPageLast.Click += new System.EventHandler(this.BtnPageLast_Click);
            // 
            // PreviousPage
            // 
            this.PreviousPage.Location = new System.Drawing.Point(131, 13);
            this.PreviousPage.Name = "PreviousPage";
            this.PreviousPage.Size = new System.Drawing.Size(75, 23);
            this.PreviousPage.TabIndex = 6;
            this.PreviousPage.Text = "<<";
            this.PreviousPage.UseVisualStyleBackColor = true;
            this.PreviousPage.Click += new System.EventHandler(this.PreviousPage_Click);
            // 
            // NextPage
            // 
            this.NextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NextPage.Location = new System.Drawing.Point(632, 13);
            this.NextPage.Name = "NextPage";
            this.NextPage.Size = new System.Drawing.Size(75, 23);
            this.NextPage.TabIndex = 7;
            this.NextPage.Text = ">>";
            this.NextPage.UseVisualStyleBackColor = true;
            this.NextPage.Click += new System.EventHandler(this.NextPage_Click);
            // 
            // NumberPage
            // 
            this.NumberPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NumberPage.AutoSize = true;
            this.NumberPage.Location = new System.Drawing.Point(393, 18);
            this.NumberPage.Name = "NumberPage";
            this.NumberPage.Size = new System.Drawing.Size(13, 13);
            this.NumberPage.TabIndex = 8;
            this.NumberPage.Text = "1";
            // 
            // FilterListView
            // 
            this.FilterListView.FormattingEnabled = true;
            this.FilterListView.Location = new System.Drawing.Point(475, 42);
            this.FilterListView.Name = "FilterListView";
            this.FilterListView.Size = new System.Drawing.Size(315, 21);
            this.FilterListView.TabIndex = 9;
            this.FilterListView.Text = "Фильтр";
            // 
            // CaptionCountBookLabel
            // 
            this.CaptionCountBookLabel.AutoSize = true;
            this.CaptionCountBookLabel.Location = new System.Drawing.Point(12, 45);
            this.CaptionCountBookLabel.Name = "CaptionCountBookLabel";
            this.CaptionCountBookLabel.Size = new System.Drawing.Size(138, 13);
            this.CaptionCountBookLabel.TabIndex = 10;
            this.CaptionCountBookLabel.Text = "Выберите кол-во записей";
            // 
            // WrittersList
            // 
            this.WrittersList.Location = new System.Drawing.Point(212, 40);
            this.WrittersList.Name = "WrittersList";
            this.WrittersList.Size = new System.Drawing.Size(257, 23);
            this.WrittersList.TabIndex = 11;
            this.WrittersList.Text = "Посмотреть список писателей";
            this.WrittersList.UseVisualStyleBackColor = true;
            this.WrittersList.Visible = false;
            this.WrittersList.Click += new System.EventHandler(this.WrittersList_Click);
            // 
            // OnLoadBook
            // 
            this.OnLoadBook.Location = new System.Drawing.Point(212, 40);
            this.OnLoadBook.Name = "OnLoadBook";
            this.OnLoadBook.Size = new System.Drawing.Size(257, 23);
            this.OnLoadBook.TabIndex = 12;
            this.OnLoadBook.Text = "Загрузить произведение";
            this.OnLoadBook.UseVisualStyleBackColor = true;
            this.OnLoadBook.Visible = false;
            this.OnLoadBook.Click += new System.EventHandler(this.OnLoadBook_Click);
            // 
            // ShowReadBook
            // 
            this.ShowReadBook.Location = new System.Drawing.Point(212, 40);
            this.ShowReadBook.Name = "ShowReadBook";
            this.ShowReadBook.Size = new System.Drawing.Size(257, 23);
            this.ShowReadBook.TabIndex = 13;
            this.ShowReadBook.Text = "Показать прочитанное";
            this.ShowReadBook.UseVisualStyleBackColor = true;
            this.ShowReadBook.Visible = false;
            this.ShowReadBook.Click += new System.EventHandler(this.ShowReadBook_Click);
            // 
            // Content
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 473);
            this.Controls.Add(this.ShowReadBook);
            this.Controls.Add(this.OnLoadBook);
            this.Controls.Add(this.WrittersList);
            this.Controls.Add(this.CaptionCountBookLabel);
            this.Controls.Add(this.FilterListView);
            this.Controls.Add(this.NumberPage);
            this.Controls.Add(this.NextPage);
            this.Controls.Add(this.PreviousPage);
            this.Controls.Add(this.BtnPageLast);
            this.Controls.Add(this.BtnPageOne);
            this.Controls.Add(this.CountBooks);
            this.Controls.Add(this.ContentListView);
            this.Controls.Add(this.ExitProfile);
            this.Controls.Add(this.BtnToProfileForm);
            this.Name = "Content";
            this.Text = "Content";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Content_FormClosing);
            this.Load += new System.EventHandler(this.Content_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnToProfileForm;
        private System.Windows.Forms.Button ExitProfile;
        private System.Windows.Forms.ListView ContentListView;
        private System.Windows.Forms.ColumnHeader Images;
        private System.Windows.Forms.ColumnHeader NameBook;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.ColumnHeader Genre;
        private System.Windows.Forms.ComboBox CountBooks;
        private System.Windows.Forms.Button BtnPageOne;
        private System.Windows.Forms.Button BtnPageLast;
        private System.Windows.Forms.Button PreviousPage;
        private System.Windows.Forms.Button NextPage;
        private System.Windows.Forms.Label NumberPage;
        private System.Windows.Forms.ComboBox FilterListView;
        private System.Windows.Forms.Label CaptionCountBookLabel;
        private System.Windows.Forms.Button WrittersList;
        private System.Windows.Forms.Button OnLoadBook;
        private System.Windows.Forms.Button ShowReadBook;
    }
}