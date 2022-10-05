using Phonebook.BusinessLogicLayer;
using Phonebook.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phonebook.PresentationLayer
{
    public partial class PhonebookForm : Form
    {
        ContactBUS logic = new ContactBUS();
        public PhonebookForm()
        {
            InitializeComponent();
            contactBindingSource.DataSource = logic._contactDAO.ContactsList;
        }

        private void addContactBtn_Click(object sender, EventArgs e)
        {
            var c = new Contact()
            { FirstName = "Já", LastName = "On", Gender = "muž", Phone = "725836436", Email = "pepa@seznam.cz" };
            logic.EditContact(c);
            
            //TODO: add binding to list, so that when you change list, datagrid changes too

            //AddToDatagrid(c);
        }
        private void AddToDatagrid(Contact contact)
        {
            contactBindingSource.AddNew();
            contactBindingSource.Add(contact);
        }
    }
}
