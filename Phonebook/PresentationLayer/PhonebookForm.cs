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
            logic.AddNewContact(GetContactFromTextBoxs());
            contactBindingSource.ResetBindings(false);
        }
        private Contact GetContactFromTextBoxs()
        {
            var c = new Contact()
            { FirstName = firstNameTextBox.Text, LastName = lastNameTextBox.Text, Gender = genderTextBox.Text, Phone = phoneTextBox.Text, Email = emailTextBox.Text };
            return c;
        }
    }
}
