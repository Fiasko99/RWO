
namespace RWO
{
    partial class WrittersListForm
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
            this.WrittersListView = new System.Windows.Forms.ListView();
            this.NameWritter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WorkExp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastBook = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PreviousPage = new System.Windows.Forms.Button();
            this.NextPage = new System.Windows.Forms.Button();
            this.NumberPage = new System.Windows.Forms.Label();
            this.SortWritters = new System.Windows.Forms.ComboBox();
            this.FirstPage = new System.Windows.Forms.Button();
            this.LastPage = new System.Windows.Forms.Button();
            this.WrittersOnList = new System.Windows.Forms.ComboBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // WrittersListView
            // 
            this.WrittersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameWritter,
            this.WorkExp,
            this.LastBook});
            this.WrittersListView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WrittersListView.FullRowSelect = true;
            this.WrittersListView.HideSelection = false;
            this.WrittersListView.Location = new System.Drawing.Point(13, 46);
            this.WrittersListView.MultiSelect = false;
            this.WrittersListView.Name = "WrittersListView";
            this.WrittersListView.OwnerDraw = true;
            this.WrittersListView.Size = new System.Drawing.Size(775, 392);
            this.WrittersListView.TabIndex = 0;
            this.WrittersListView.UseCompatibleStateImageBehavior = false;
            this.WrittersListView.View = System.Windows.Forms.View.Details;
            this.WrittersListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.WrittersListView_DrawColumnHeader);
            this.WrittersListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.WrittersListView_DrawItem);
            this.WrittersListView.DoubleClick += new System.EventHandler(this.WrittersListView_DoubleClick);
            this.WrittersListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WrittersListView_MouseDoubleClick);
            // 
            // NameWritter
            // 
            this.NameWritter.Text = "Имя писателя";
            this.NameWritter.Width = 155;
            // 
            // WorkExp
            // 
            this.WorkExp.Text = "Стаж";
            this.WorkExp.Width = 72;
            // 
            // LastBook
            // 
            this.LastBook.Text = "Последняя написанная книга";
            this.LastBook.Width = 535;
            // 
            // PreviousPage
            // 
            this.PreviousPage.Location = new System.Drawing.Point(12, 12);
            this.PreviousPage.Name = "PreviousPage";
            this.PreviousPage.Size = new System.Drawing.Size(36, 23);
            this.PreviousPage.TabIndex = 1;
            this.PreviousPage.Text = "<<";
            this.PreviousPage.UseVisualStyleBackColor = true;
            this.PreviousPage.Click += new System.EventHandler(this.PreviousPage_Click);
            // 
            // NextPage
            // 
            this.NextPage.Location = new System.Drawing.Point(73, 12);
            this.NextPage.Name = "NextPage";
            this.NextPage.Size = new System.Drawing.Size(29, 23);
            this.NextPage.TabIndex = 2;
            this.NextPage.Text = ">>";
            this.NextPage.UseVisualStyleBackColor = true;
            this.NextPage.Click += new System.EventHandler(this.NextPage_Click);
            // 
            // NumberPage
            // 
            this.NumberPage.AutoSize = true;
            this.NumberPage.Location = new System.Drawing.Point(54, 17);
            this.NumberPage.Name = "NumberPage";
            this.NumberPage.Size = new System.Drawing.Size(13, 13);
            this.NumberPage.TabIndex = 3;
            this.NumberPage.Text = "1";
            this.NumberPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SortWritters
            // 
            this.SortWritters.FormattingEnabled = true;
            this.SortWritters.Items.AddRange(new object[] {
            "По имени",
            "По имени книги",
            "По стажу"});
            this.SortWritters.Location = new System.Drawing.Point(559, 14);
            this.SortWritters.Name = "SortWritters";
            this.SortWritters.Size = new System.Drawing.Size(108, 21);
            this.SortWritters.TabIndex = 4;
            this.SortWritters.Text = "Сортировка";
            // 
            // FirstPage
            // 
            this.FirstPage.Location = new System.Drawing.Point(108, 12);
            this.FirstPage.Name = "FirstPage";
            this.FirstPage.Size = new System.Drawing.Size(111, 23);
            this.FirstPage.TabIndex = 5;
            this.FirstPage.Text = "Первая страница";
            this.FirstPage.UseVisualStyleBackColor = true;
            this.FirstPage.Click += new System.EventHandler(this.FirstPage_Click);
            // 
            // LastPage
            // 
            this.LastPage.Location = new System.Drawing.Point(225, 12);
            this.LastPage.Name = "LastPage";
            this.LastPage.Size = new System.Drawing.Size(134, 23);
            this.LastPage.TabIndex = 6;
            this.LastPage.Text = "Последняя страница";
            this.LastPage.UseVisualStyleBackColor = true;
            this.LastPage.Click += new System.EventHandler(this.LastPage_Click);
            // 
            // WrittersOnList
            // 
            this.WrittersOnList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WrittersOnList.FormattingEnabled = true;
            this.WrittersOnList.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "50",
            "100"});
            this.WrittersOnList.Location = new System.Drawing.Point(498, 14);
            this.WrittersOnList.Name = "WrittersOnList";
            this.WrittersOnList.Size = new System.Drawing.Size(55, 21);
            this.WrittersOnList.TabIndex = 7;
            this.WrittersOnList.SelectedIndexChanged += new System.EventHandler(this.WrittersOnList_SelectedIndexChanged);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(365, 17);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(127, 13);
            this.InfoLabel.TabIndex = 8;
            this.InfoLabel.Text = "Писателей на странице";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "По имени",
            "По имени книги",
            "По стажу"});
            this.comboBox1.Location = new System.Drawing.Point(673, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(115, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Text = "Сортировка";
            // 
            // WrittersListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.WrittersOnList);
            this.Controls.Add(this.LastPage);
            this.Controls.Add(this.FirstPage);
            this.Controls.Add(this.SortWritters);
            this.Controls.Add(this.NumberPage);
            this.Controls.Add(this.NextPage);
            this.Controls.Add(this.PreviousPage);
            this.Controls.Add(this.WrittersListView);
            this.Name = "WrittersListForm";
            this.Text = "WrittersListForm";
            this.Load += new System.EventHandler(this.WrittersListForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView WrittersListView;
        private System.Windows.Forms.ColumnHeader NameWritter;
        private System.Windows.Forms.ColumnHeader WorkExp;
        private System.Windows.Forms.ColumnHeader LastBook;
        private System.Windows.Forms.Button PreviousPage;
        private System.Windows.Forms.Button NextPage;
        private System.Windows.Forms.Label NumberPage;
        private System.Windows.Forms.ComboBox SortWritters;
        private System.Windows.Forms.Button FirstPage;
        private System.Windows.Forms.Button LastPage;
        private System.Windows.Forms.ComboBox WrittersOnList;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}