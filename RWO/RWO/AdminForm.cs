using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWO
{
    public partial class AdminForm : Form
    {
        DBConnection API = new DBConnection();
        public AdminForm()
        {
            InitializeComponent();
        }

        private void ReadersReport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Stream file = ofd.OpenFile();
                string fileContent = null;
                using (StreamReader reader = new StreamReader(file))
                {
                    fileContent = reader.ReadToEnd();
                }
                string[] RowsFile = null;

                if (fileContent != null)
                {
                    RowsFile = fileContent.Split('\n');
                    for (int i = 1; i < RowsFile.Length; i++)
                    {
                        for (int j = i + 1; j < RowsFile.Length; j++)
                        {
                            string[] ElemRowsFirst = RowsFile[i].Split('/');
                            string[] ElemRowsSecond = RowsFile[j].Split('/');
                            for (int k = 0; k < ElemRowsFirst.Length && k < ElemRowsSecond.Length; k++)
                            {
                                if (ElemRowsFirst[k] != ElemRowsSecond[k])
                                {

                                }
                            }
                        }
                    }
                }
            }

            
        }
    }

    public class WrittersInfo
    {
        public string login { get; set; }
        public string password { get; set; }
        public string surname { get; set; }
        public string email { get; set; }

        public WrittersInfo(
                string login, string password,
                string surname, string email
            )
        {
            this.login = login;
            this.password = password;
            this.surname = surname;
            this.email = email;
        }
    }
}
