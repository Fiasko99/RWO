﻿
namespace RWO
{
    partial class AdminForm
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
            this.ImportAdmin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ImportAdmin
            // 
            this.ImportAdmin.Location = new System.Drawing.Point(12, 12);
            this.ImportAdmin.Name = "ImportAdmin";
            this.ImportAdmin.Size = new System.Drawing.Size(148, 23);
            this.ImportAdmin.TabIndex = 0;
            this.ImportAdmin.Text = "Импортировать админов";
            this.ImportAdmin.UseVisualStyleBackColor = true;
            this.ImportAdmin.Click += new System.EventHandler(this.ReadersReport_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 51);
            this.Controls.Add(this.ImportAdmin);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ImportAdmin;
    }
}