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

        public void AddNewContact(Contact contact)
        {
            try
            {
                _contactDAO.ContactsList.Add(contact);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveContact(Contact contact)
        {
            if (!_contactDAO.ContactsList.Contains(contact))
                return false;
            try
            {
                _contactDAO.ContactsList.Remove(contact);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void fillSelectedContact(Contact contact)
        {
            selectedContact = contact;
        }
        public void EditContact(Contact contact)
        {
            if (!ValidateContact(contact))
                return;
            bool doesListContainContact = 
                _contactDAO.ContactsList.Any(item => item.Email == contact.Email || item.Phone == contact.Phone);
            if (doesListContainContact)
            {
                ErrorWindow("Trying to add duplicate items: Email and Phone number mus be unique!");
                return;
            }
                
            if (_contactDAO.ContactsList.Count == 0 || selectedContact == null)
                _contactDAO.ContactsList.Add(contact);
            else
                _contactDAO.ContactsList[_contactDAO.ContactsList.IndexOf(selectedContact)] = contact;

        }
        public bool ValidateContact(Contact contact)
        {
            //determine if any of contacts properties values are null/empty
            var properties = contact.GetType().GetProperties();
            foreach(var property in properties)
            {
                if(string.IsNullOrEmpty((string)property.GetValue(contact)))
                {
                    ErrorWindow("Not all properties are filled!");
                    return false;
                }
            }
            //validating email adress
            if (!new EmailAddressAttribute().IsValid(contact.Email))
            {
                ErrorWindow("Email address has wrong format!");
                return false;
            }
            //Validate phone number
            if (!Regex.IsMatch(contact.Phone, "^?([+][0-9]{3})?([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$"))
            {
                ErrorWindow("Phone number has wrong format!");
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
