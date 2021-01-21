
namespace RWO
{
    partial class AuthForm
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
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.PassLabel = new System.Windows.Forms.Label();
            this.LoginBox = new System.Windows.Forms.TextBox();
            this.PassBox = new System.Windows.Forms.TextBox();
            this.RePassBtn = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.BtnToRegistForm = new System.Windows.Forms.Button();
            this.RoleCombo = new System.Windows.Forms.ComboBox();
            this.RoleLabel = new System.Windows.Forms.Label();
            this.RememberLogin = new System.Windows.Forms.CheckBox();
            this.Captcha = new System.Windows.Forms.PictureBox();
            this.CaptchaBox = new System.Windows.Forms.TextBox();
            this.UpdCaptcha = new System.Windows.Forms.Button();
            this.CheckCaptcha = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Captcha)).BeginInit();
            this.SuspendLayout();
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.Font = new System.Drawing.Font("Nirmala UI", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WelcomeLabel.Location = new System.Drawing.Point(253, 9);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(183, 86);
            this.WelcomeLabel.TabIndex = 0;
            this.WelcomeLabel.Text = "RWO";
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Location = new System.Drawing.Point(40, 103);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(64, 30);
            this.LoginLabel.TabIndex = 1;
            this.LoginLabel.Text = "Login";
            // 
            // PassLabel
            // 
            this.PassLabel.AutoSize = true;
            this.PassLabel.Location = new System.Drawing.Point(5, 147);
            this.PassLabel.Name = "PassLabel";
            this.PassLabel.Size = new System.Drawing.Size(99, 30);
            this.PassLabel.TabIndex = 2;
            this.PassLabel.Text = "Password";
            // 
            // LoginBox
            // 
            this.LoginBox.Location = new System.Drawing.Point(110, 100);
            this.LoginBox.Name = "LoginBox";
            this.LoginBox.Size = new System.Drawing.Size(555, 35);
            this.LoginBox.TabIndex = 7;
            // 
            // PassBox
            // 
            this.PassBox.Location = new System.Drawing.Point(110, 144);
            this.PassBox.Name = "PassBox";
            this.PassBox.Size = new System.Drawing.Size(555, 35);
            this.PassBox.TabIndex = 8;
            this.PassBox.UseSystemPasswordChar = true;
            // 
            // RePassBtn
            // 
            this.RePassBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RePassBtn.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RePassBtn.Location = new System.Drawing.Point(513, 229);
            this.RePassBtn.Name = "RePassBtn";
            this.RePassBtn.Size = new System.Drawing.Size(152, 42);
            this.RePassBtn.TabIndex = 14;
            this.RePassBtn.Text = "Forgot password?";
            this.RePassBtn.UseVisualStyleBackColor = true;
            // 
            // LoginBtn
            // 
            this.LoginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginBtn.Location = new System.Drawing.Point(190, 229);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(317, 42);
            this.LoginBtn.TabIndex = 15;
            this.LoginBtn.Text = "Войти";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // BtnToRegistForm
            // 
            this.BtnToRegistForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnToRegistForm.Location = new System.Drawing.Point(10, 229);
            this.BtnToRegistForm.Name = "BtnToRegistForm";
            this.BtnToRegistForm.Size = new System.Drawing.Size(174, 42);
            this.BtnToRegistForm.TabIndex = 16;
            this.BtnToRegistForm.Text = "Registration";
            this.BtnToRegistForm.UseVisualStyleBackColor = true;
            this.BtnToRegistForm.Click += new System.EventHandler(this.BtnToRegistForm_Click);
            // 
            // RoleCombo
            // 
            this.RoleCombo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RoleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RoleCombo.FormattingEnabled = true;
            this.RoleCombo.Items.AddRange(new object[] {
            "Инвестор",
            "Писатель",
            "Читатель"});
            this.RoleCombo.Location = new System.Drawing.Point(110, 186);
            this.RoleCombo.Name = "RoleCombo";
            this.RoleCombo.Size = new System.Drawing.Size(397, 38);
            this.RoleCombo.TabIndex = 17;
            // 
            // RoleLabel
            // 
            this.RoleLabel.AutoSize = true;
            this.RoleLabel.Location = new System.Drawing.Point(51, 189);
            this.RoleLabel.Name = "RoleLabel";
            this.RoleLabel.Size = new System.Drawing.Size(53, 30);
            this.RoleLabel.TabIndex = 18;
            this.RoleLabel.Text = "Role";
            // 
            // RememberLogin
            // 
            this.RememberLogin.AutoSize = true;
            this.RememberLogin.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RememberLogin.Location = new System.Drawing.Point(526, 192);
            this.RememberLogin.Name = "RememberLogin";
            this.RememberLogin.Size = new System.Drawing.Size(132, 29);
            this.RememberLogin.TabIndex = 19;
            this.RememberLogin.Text = "Запомнить";
            this.RememberLogin.UseVisualStyleBackColor = true;
            // 
            // Captcha
            // 
            this.Captcha.Location = new System.Drawing.Point(12, 280);
            this.Captcha.Name = "Captcha";
            this.Captcha.Size = new System.Drawing.Size(100, 50);
            this.Captcha.TabIndex = 20;
            this.Captcha.TabStop = false;
            this.Captcha.Visible = false;
            // 
            // CaptchaBox
            // 
            this.CaptchaBox.Location = new System.Drawing.Point(118, 280);
            this.CaptchaBox.Name = "CaptchaBox";
            this.CaptchaBox.Size = new System.Drawing.Size(540, 35);
            this.CaptchaBox.TabIndex = 21;
            this.CaptchaBox.Visible = false;
            // 
            // UpdCaptcha
            // 
            this.UpdCaptcha.Location = new System.Drawing.Point(118, 321);
            this.UpdCaptcha.Name = "UpdCaptcha";
            this.UpdCaptcha.Size = new System.Drawing.Size(270, 35);
            this.UpdCaptcha.TabIndex = 22;
            this.UpdCaptcha.Text = "Обновить";
            this.UpdCaptcha.UseVisualStyleBackColor = true;
            this.UpdCaptcha.Visible = false;
            this.UpdCaptcha.Click += new System.EventHandler(this.UpdCaptcha_Click);
            // 
            // CheckCaptcha
            // 
            this.CheckCaptcha.Location = new System.Drawing.Point(394, 321);
            this.CheckCaptcha.Name = "CheckCaptcha";
            this.CheckCaptcha.Size = new System.Drawing.Size(264, 35);
            this.CheckCaptcha.TabIndex = 23;
            this.CheckCaptcha.Text = "Проверить";
            this.CheckCaptcha.UseVisualStyleBackColor = true;
            this.CheckCaptcha.Visible = false;
            this.CheckCaptcha.Click += new System.EventHandler(this.CheckCaptcha_Click);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 364);
            this.Controls.Add(this.CheckCaptcha);
            this.Controls.Add(this.UpdCaptcha);
            this.Controls.Add(this.CaptchaBox);
            this.Controls.Add(this.Captcha);
            this.Controls.Add(this.RememberLogin);
            this.Controls.Add(this.RoleLabel);
            this.Controls.Add(this.RoleCombo);
            this.Controls.Add(this.BtnToRegistForm);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.RePassBtn);
            this.Controls.Add(this.PassBox);
            this.Controls.Add(this.LoginBox);
            this.Controls.Add(this.PassLabel);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.WelcomeLabel);
            this.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "AuthForm";
            this.Text = "Auth";
            this.Load += new System.EventHandler(this.AuthForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Captcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Label PassLabel;
        private System.Windows.Forms.TextBox LoginBox;
        private System.Windows.Forms.TextBox PassBox;
        private System.Windows.Forms.Button RePassBtn;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button BtnToRegistForm;
        private System.Windows.Forms.ComboBox RoleCombo;
        private System.Windows.Forms.Label RoleLabel;
        private System.Windows.Forms.CheckBox RememberLogin;
        private System.Windows.Forms.PictureBox Captcha;
        private System.Windows.Forms.TextBox CaptchaBox;
        private System.Windows.Forms.Button UpdCaptcha;
        private System.Windows.Forms.Button CheckCaptcha;
    }
}