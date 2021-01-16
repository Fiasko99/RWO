
namespace RWO
{
    partial class WritterProfile
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
            this.NameWritter = new System.Windows.Forms.Label();
            this.workexp = new System.Windows.Forms.Label();
            this.DateRegistration = new System.Windows.Forms.Label();
            this.CountBooks = new System.Windows.Forms.Label();
            this.GetBooks = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.GetWritterOffers = new System.Windows.Forms.Button();
            this.StayOffer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BackToWrittersListForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameWritter
            // 
            this.NameWritter.AutoSize = true;
            this.NameWritter.Location = new System.Drawing.Point(15, 52);
            this.NameWritter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.NameWritter.Name = "NameWritter";
            this.NameWritter.Size = new System.Drawing.Size(126, 25);
            this.NameWritter.TabIndex = 0;
            this.NameWritter.Text = "Инициалы: ";
            // 
            // workexp
            // 
            this.workexp.AutoSize = true;
            this.workexp.Location = new System.Drawing.Point(15, 77);
            this.workexp.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.workexp.Name = "workexp";
            this.workexp.Size = new System.Drawing.Size(70, 25);
            this.workexp.TabIndex = 1;
            this.workexp.Text = "Стаж:";
            // 
            // DateRegistration
            // 
            this.DateRegistration.AutoSize = true;
            this.DateRegistration.Location = new System.Drawing.Point(15, 102);
            this.DateRegistration.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.DateRegistration.Name = "DateRegistration";
            this.DateRegistration.Size = new System.Drawing.Size(197, 25);
            this.DateRegistration.TabIndex = 2;
            this.DateRegistration.Text = "Зарегистрирован: ";
            // 
            // CountBooks
            // 
            this.CountBooks.AutoSize = true;
            this.CountBooks.Location = new System.Drawing.Point(15, 127);
            this.CountBooks.Name = "CountBooks";
            this.CountBooks.Size = new System.Drawing.Size(153, 25);
            this.CountBooks.TabIndex = 3;
            this.CountBooks.Text = "Написал книг:";
            // 
            // GetBooks
            // 
            this.GetBooks.BackColor = System.Drawing.SystemColors.ControlLight;
            this.GetBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GetBooks.ForeColor = System.Drawing.Color.Black;
            this.GetBooks.Location = new System.Drawing.Point(20, 181);
            this.GetBooks.Name = "GetBooks";
            this.GetBooks.Size = new System.Drawing.Size(211, 35);
            this.GetBooks.TabIndex = 4;
            this.GetBooks.Text = "Посмотреть книги";
            this.GetBooks.UseVisualStyleBackColor = false;
            this.GetBooks.Click += new System.EventHandler(this.GetBooks_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Почта:";
            // 
            // GetWritterOffers
            // 
            this.GetWritterOffers.Location = new System.Drawing.Point(237, 180);
            this.GetWritterOffers.Name = "GetWritterOffers";
            this.GetWritterOffers.Size = new System.Drawing.Size(196, 35);
            this.GetWritterOffers.TabIndex = 6;
            this.GetWritterOffers.Text = "Инвесторы";
            this.GetWritterOffers.UseVisualStyleBackColor = true;
            this.GetWritterOffers.Click += new System.EventHandler(this.GetWritterOffers_Click);
            // 
            // StayOffer
            // 
            this.StayOffer.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.StayOffer.Location = new System.Drawing.Point(439, 180);
            this.StayOffer.Name = "StayOffer";
            this.StayOffer.Size = new System.Drawing.Size(235, 35);
            this.StayOffer.TabIndex = 7;
            this.StayOffer.Text = "Стать инвестором";
            this.StayOffer.UseVisualStyleBackColor = false;
            this.StayOffer.Click += new System.EventHandler(this.StayOffer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 37);
            this.label2.TabIndex = 8;
            this.label2.Text = "Профиль писателя";
            // 
            // BackToWrittersListForm
            // 
            this.BackToWrittersListForm.Location = new System.Drawing.Point(439, 9);
            this.BackToWrittersListForm.Name = "BackToWrittersListForm";
            this.BackToWrittersListForm.Size = new System.Drawing.Size(235, 37);
            this.BackToWrittersListForm.TabIndex = 9;
            this.BackToWrittersListForm.Text = "Вернуться к списку";
            this.BackToWrittersListForm.UseVisualStyleBackColor = true;
            this.BackToWrittersListForm.Click += new System.EventHandler(this.BackToWrittersListForm_Click);
            // 
            // WritterProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 223);
            this.Controls.Add(this.BackToWrittersListForm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StayOffer);
            this.Controls.Add(this.GetWritterOffers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetBooks);
            this.Controls.Add(this.CountBooks);
            this.Controls.Add(this.DateRegistration);
            this.Controls.Add(this.workexp);
            this.Controls.Add(this.NameWritter);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "WritterProfile";
            this.Text = "WritterProfile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameWritter;
        private System.Windows.Forms.Label workexp;
        private System.Windows.Forms.Label DateRegistration;
        private System.Windows.Forms.Label CountBooks;
        private System.Windows.Forms.Button GetBooks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GetWritterOffers;
        private System.Windows.Forms.Button StayOffer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BackToWrittersListForm;
    }
}