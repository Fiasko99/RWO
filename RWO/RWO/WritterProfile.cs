using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWO
{
    public partial class WritterProfile : Form
    {
        Writter writter;
        User activeuser;
        WrittersListForm writtersform;
        List<Book> books;
        List<UserOffer> offers;
        public WritterProfile(Writter SelectWritter, User ActiveUser, WrittersListForm WrittersForm)
        {
            InitializeComponent();
            DBConnection API = new DBConnection();
            writtersform = WrittersForm;
            writter = SelectWritter;
            activeuser = ActiveUser;
            new Task(() =>
            {
                //string BooksJsonStr = API.GetJSON("/api/writter/books/" + writter.id);
                //books = JsonSerializer.Deserialize<List<Book>>(BooksJsonStr);
            }).Start();
            new Task(() =>
            {
                //string OffersJsonStr = API.GetJSON("/api/writter/offers/" + writter.id);
                //offers = JsonSerializer.Deserialize<List<UserOffer>>(OffersJsonStr);
            }).Start();
        }

        private void GetBooks_Click(object sender, EventArgs e)
        {

        }

        private void GetWritterOffers_Click(object sender, EventArgs e)
        {

        }

        private void StayOffer_Click(object sender, EventArgs e)
        {

        }

        private void BackToWrittersListForm_Click(object sender, EventArgs e)
        {
            Invoke(
                (MethodInvoker)delegate
                {
                    writtersform.Show();
                }      
            );
            Close();
        }
    }
}
