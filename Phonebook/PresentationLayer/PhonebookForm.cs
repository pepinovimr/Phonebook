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
            OnButtonClick<bool>(logic.AddNewContact(GetContactFromTextBoxes()));
            //TODO: scroll to end of dg so that user can see change
        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            OnButtonClick<bool>(logic.EditContact(GetContactFromTextBoxes()));
        }
        private void deleteContactBtn_Click(object sender, EventArgs e)
        {
            OnButtonClick<bool>(logic.RemoveContact(GetContactFromTextBoxes()));
        }
        private void PhonebookForm_Click(object sender, EventArgs e)
        {
            addTextToTextBoxes();
        }
        private Contact GetContactFromTextBoxes()
        {
            var c = new Contact()
            {
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Gender = genderTextBox.Text,
                Phone = phoneTextBox.Text,
                Email = emailTextBox.Text
            };
            return c;
        }
        private void OnButtonClick<T>(bool methodToCall)
        {
            if (methodToCall)
            {
                contactBindingSource.ResetBindings(false);
                addTextToTextBoxes();
            }
        }

        private void contactDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] contactValues = new string[contactDataGridView.Rows[contactDataGridView.CurrentRow.Index].Cells.Count];
            for (int i = 0; i < contactDataGridView.Rows[contactDataGridView.CurrentRow.Index].Cells.Count; i++)
            {
                contactValues[i] = contactDataGridView.Rows[contactDataGridView.CurrentRow.Index].Cells[i].Value.ToString();
            }

            addTextToTextBoxes(contactValues[0], contactValues[1], contactValues[2], contactValues[3], contactValues[4]);
            logic.fillSelectedContact(GetContactFromTextBoxes());
        }
        private void addTextToTextBoxes(string firstName = "", string lastName = "", string gender = "", string email = "", string phone = "")
        {
            firstNameTextBox.Text = firstName;
            lastNameTextBox.Text = lastName;
            genderTextBox.Text = gender;
            emailTextBox.Text = email;
            phoneTextBox.Text = phone;
        }
    }
}
