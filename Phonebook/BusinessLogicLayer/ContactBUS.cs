using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Phonebook.DataAccessLayer;

namespace Phonebook.BusinessLogicLayer
{
    internal class ContactBUS
    {
        public ContactDAO _contactDAO = ContactDAO.Instance;
        private Contact selectedContact;
        public ContactBUS() { }

        public bool AddNewContact(Contact contact)
        {
            if (ValidateContact(contact))
            {
                if (!_contactDAO.ContactsList.Contains(contact))
                {
                    _contactDAO.ContactsList.Add(contact);
                    MessageBox.Show("Kontakt přidán");
                    return true;
                }
                else
                    ErrorWindow("Seznam tento kontakt již obsahuje");
            }
            return false;
        }
        public bool  RemoveContact(Contact contact)
        {

            if (!_contactDAO.ContactsList.Contains(contact))
            {
                ErrorWindow("Nelze odstranit kontakt: Kontakt není v seznamu");
                return false;
            }
            _contactDAO.ContactsList.Remove(contact);
            MessageBox.Show("Kontakt odstraněn");
            return true;
        }
        public void fillSelectedContact(Contact contact)
        {
            selectedContact = contact;
        }
        public bool EditContact(Contact contact)
        {
            if (!ValidateContact(contact))
                return false;
            bool doesListContainContact = 
                _contactDAO.ContactsList.Any(item => item.Email == contact.Email && item.Phone == contact.Phone);
            if (doesListContainContact)
            {
                ErrorWindow("Snažíte se přidat duplicitní Kontakty: Email a Telefonní číslo musí být unikátní");
                return false;
            }

            _contactDAO.ContactsList[_contactDAO.ContactsList.IndexOf(selectedContact)] = contact;
            MessageBox.Show("Kontakt upraven");
            return true;
        }
        public bool ValidateContact(Contact contact)
        {
            ContactValidator validator = new ContactValidator(contact);
            //determine if any of contacts properties values are null/empty
            if (validator.isContactEmpty())
            {
                ErrorWindow("Některé pole nejsou vyplněna!");
                return false;
            }

            //validating email adress
            if (!validator.isEmailValid())
            {
                ErrorWindow("Emailová adresa má špatný formát!!");
                return false;
            }

            //Validate phone number
            if (!validator.isPhoneValid())
            {
                ErrorWindow("Telefonní číslo má špatný formát!");
                return false;
            }
            return true;
        }
        public void ErrorWindow(string errorMsg)
        {
            MessageBox.Show(errorMsg, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
