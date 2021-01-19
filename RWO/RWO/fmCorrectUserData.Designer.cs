
namespace RWO
{
    partial class fmCorrectUserData
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
            this.SurnameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LoginBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.IgnoreBtn = new System.Windows.Forms.Button();
            this.AddAdminBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SurnameBox
            // 
            this.SurnameBox.Location = new System.Drawing.Point(68, 7);
            this.SurnameBox.Margin = new System.Windows.Forms.Padding(2);
            this.SurnameBox.Name = "SurnameBox";
            this.SurnameBox.Size = new System.Drawing.Size(249, 20);
            this.SurnameBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Email";
            // 
            // EmailBox
            // 
            this.EmailBox.Location = new System.Drawing.Point(68, 32);
            this.EmailBox.Margin = new System.Windows.Forms.Padding(2);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(249, 20);
            this.EmailBox.TabIndex = 2;
            this.EmailBox.TextChanged += new System.EventHandler(this.textBoxReColor_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Логин";
            // 
            // LoginBox
            // 
            this.LoginBox.Location = new System.Drawing.Point(71, 84);
            this.LoginBox.Margin = new System.Windows.Forms.Padding(2);
            this.LoginBox.Name = "LoginBox";
            this.LoginBox.Size = new System.Drawing.Size(249, 20);
            this.LoginBox.TabIndex = 4;
            this.LoginBox.TextChanged += new System.EventHandler(this.textBoxReColor_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Пароль";
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(71, 60);
            this.PasswordBox.Margin = new System.Windows.Forms.Padding(2);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(249, 20);
            this.PasswordBox.TabIndex = 6;
            // 
            // IgnoreBtn
            // 
            this.IgnoreBtn.Location = new System.Drawing.Point(11, 112);
            this.IgnoreBtn.Margin = new System.Windows.Forms.Padding(2);
            this.IgnoreBtn.Name = "IgnoreBtn";
            this.IgnoreBtn.Size = new System.Drawing.Size(153, 45);
            this.IgnoreBtn.TabIndex = 8;
            this.IgnoreBtn.Text = "Не добавлять";
            this.IgnoreBtn.UseVisualStyleBackColor = true;
            this.IgnoreBtn.Click += new System.EventHandler(this.IgnoreBtn_Click);
            // 
            // AddAdminBtn
            // 
            this.AddAdminBtn.Location = new System.Drawing.Point(168, 112);
            this.AddAdminBtn.Margin = new System.Windows.Forms.Padding(2);
            this.AddAdminBtn.Name = "AddAdminBtn";
            this.AddAdminBtn.Size = new System.Drawing.Size(149, 45);
            this.AddAdminBtn.TabIndex = 9;
            this.AddAdminBtn.Text = "Сохранить";
            this.AddAdminBtn.UseVisualStyleBackColor = true;
            this.AddAdminBtn.Click += new System.EventHandler(this.AddAdminBtn_Click);
            // 
            // fmCorrectUserData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 163);
            this.Controls.Add(this.AddAdminBtn);
            this.Controls.Add(this.IgnoreBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LoginBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SurnameBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fmCorrectUserData";
            this.Text = "Исправление данных пользователя";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.fmCorrectUserData_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SurnameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LoginBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Button IgnoreBtn;
        private System.Windows.Forms.Button AddAdminBtn;
    }
}