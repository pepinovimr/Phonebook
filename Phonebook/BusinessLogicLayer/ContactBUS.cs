using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Phonebook.DataAccessLayer;

namespace Phonebook.BusinessLogicLayer
{
    internal class ContactBUS
    {
        ContactDAO _contactDAO = ContactDAO.Instance;
        private Contact selectedContact;
        public ContactBUS(){}

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
        }
        public bool ValidateContact(Contact contact)
        {
            //determine if all of contacts properties are null/empty
            bool isValid = contact.GetType().GetProperties()
            .Where(pi => pi.PropertyType == typeof(string))
            .Select(pi => (string)pi.GetValue(contact))
            .All(value => string.IsNullOrEmpty(value));

            //validating email adress
            if (!new EmailAddressAttribute().IsValid(contact.Email))
                return false;



            return isValid;
        }
    }
}
